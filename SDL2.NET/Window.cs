using SDL2.Bindings;
using SDL2.NET.Exceptions;
using SDL2.NET.Input;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static SDL2.Bindings.SDL;

namespace SDL2.NET;

/// <summary>
/// Represents an SDL Window object
/// </summary>
public class Window : IDisposable
{
    internal static readonly ConcurrentDictionary<IntPtr, WeakReference<Window>> _handleDict = new(2, 2);
    internal static readonly ConcurrentDictionary<uint, WeakReference<Window>> _idDict = new(2, 2);
    
    /// <summary>
    /// The internal handle that points to the <see cref="Window"/> object in SDL
    /// </summary>
    /// <remarks>Unless you know exactly what you're doing, and are trying to work directly with SDL, ignore this field. This field is NOT set to <see cref="IntPtr.Zero"/> (null) upon disposal</remarks>
    protected internal readonly IntPtr _handle = IntPtr.Zero;

    /// <summary>
    /// Creates a Window and a Renderer together
    /// </summary>
    /// <param name="title">The title of the <see cref="Window"/></param>
    /// <param name="width">The width of the <see cref="Window"/></param>
    /// <param name="height">The height of the <see cref="Window"/></param>
    /// <param name="configuration">The configuration parameters of the <see cref="Window"/></param>
    /// <param name="centerPointX">The center point along the X axis of the <see cref="Window"/></param>
    /// <param name="centerPointY">The center point along the Y axis of the <see cref="Window"/></param>
    /// <param name="rendererIndex">The renderer index of the renderer</param>
    /// <param name="rendererFlags">The RendererFlags of the renderer</param>
    /// <returns></returns>
    public static WindowRenderer CreateWindowAndRenderer(string title, int width, int height, int rendererIndex = -1, RendererFlags rendererFlags = RendererFlags.Accelerated | RendererFlags.PresentVSync, WindowConfig? configuration = null, int? centerPointX = null, int? centerPointY = null)
    {
        var win = new Window(title, width, height, configuration, centerPointX, centerPointY);
        return new WindowRenderer(win, rendererIndex);
    }

    /// <summary>
    /// Attempts to find a <see cref="Window"/>, looking by its numerical <paramref name="id"/>
    /// </summary>
    /// <param name="id">The <see cref="WindowId"/> of the <see cref="Window"/> in question</param>
    /// <param name="window">The <see cref="Window"/> in question, if it was found</param>
    /// <returns>Whether the <see cref="Window"/> was found or not</returns>
    public static bool TryGetWindow(uint id, [NotNullWhen(true)] out Window? window)
    {
        if (_idDict.TryGetValue(id, out var r) && r.TryGetTarget(out window))
            return true;
        window = null;
        return false;
    }

    /// <summary>
    /// Instantiates a new <see cref="Window"/>
    /// </summary>
    /// <param name="title">The title of the <see cref="Window"/></param>
    /// <param name="width">The width of the <see cref="Window"/></param>
    /// <param name="height">The height of the <see cref="Window"/></param>
    /// <param name="configuration">The configuration parameters of the <see cref="Window"/></param>
    /// <param name="centerPointX">The center point along the X axis of the <see cref="Window"/></param>
    /// <param name="centerPointY">The center point along the Y axis of the <see cref="Window"/></param>
    /// <exception cref="SDLWindowCreationException"></exception>
    public Window(string title, int width, int height, WindowConfig? configuration, int? centerPointX = null, int? centerPointY = null)
    {
        _handle = SDL_CreateWindow(
            title, 
            centerPointX ?? SDL_WINDOWPOS_CENTERED,
            centerPointY ?? SDL_WINDOWPOS_CENTERED,
            width,
            height,
            (configuration ?? WindowConfig.Default).GenerateFlags()
        );
        if (_handle == IntPtr.Zero)
            throw new SDLWindowCreationException(SDL_GetError());

        Surface = new Surface(SDL_GetWindowSurface(_handle));

        hitTestDelegate = htcallback;
        IsHitTestSupported = SDL_SetWindowHitTest(_handle, hitTestDelegate, IntPtr.Zero) == 0;

        // local function
        SDL_HitTestResult htcallback(IntPtr win, IntPtr area, IntPtr data)
            => hitTestCallback is null ? SDL_HitTestResult.SDL_HITTEST_NORMAL : (SDL_HitTestResult)hitTestCallback(this, Marshal.PtrToStructure<SDL_Point>(area), hitTestCallbackData);

        UpdateCenterPoint();

        var r = new WeakReference<Window>(this);
        _handleDict[_handle] = r;
        _idDict[WindowId = SDL_GetWindowID(_handle)] = r;
    }

    #region Events

    #region DragAndDropEvents

    /// <summary>
    /// Represents a change in the Window's Drop status, it could mean a drop operation just began or just ended.
    /// </summary>
    /// <param name="sender">The <see cref="Window"/> sender</param>
    /// <param name="timestamp">The amount of time that has passed since SDL initialized</param>
    public delegate void DropStatusEvent(Window sender, TimeSpan timestamp);

    /// <summary>
    /// A new set of drops is beginning
    /// </summary>
    public event DropStatusEvent? DropBegan;

    /// <summary>
    /// The set of drops is now complete
    /// </summary>
    public event DropStatusEvent? DropCompleted;

    internal void TriggerDropBegan(SDL_DropEvent e)
    {
        DropBegan?.Invoke(this, TimeSpan.FromMilliseconds(e.timestamp));
    }

    internal void TriggerDropCompleted(SDL_DropEvent e)
    {
        DropCompleted?.Invoke(this, TimeSpan.FromMilliseconds(e.timestamp));
    }

    /// <summary>
    /// Represents the dropping of an object into the window
    /// </summary>
    /// <param name="sender">The <see cref="Window"/> sender</param>
    /// <param name="timestamp">The amount of time that has passed since SDL initialized</param>
    /// <param name="data">The data of the drop event. Either a filename, or plaintext</param>
    public delegate void DropEvent(Window sender, TimeSpan timestamp, string data);

    /// <summary>
    /// Fired when a file is dropped into the app
    /// </summary>
    public event DropEvent? FileDropped;

    /// <summary>
    /// Fired when plaintext was dropped into the app
    /// </summary>
    public event DropEvent? TextDropped;

    internal void TriggerDropFile(SDL_DropEvent e)
    {
        FileDropped?.Invoke(this, TimeSpan.FromMilliseconds(e.timestamp), Marshal.PtrToStringAuto(e.file)!);
    }

    internal void TriggerDropText(SDL_DropEvent e)
    {
        TextDropped?.Invoke(this, TimeSpan.FromMilliseconds(e.timestamp), Marshal.PtrToStringAuto(e.file)!);
    }

    #endregion

    #region MouseEvents

    /// <summary>
    /// Represents an event fired when the mouse wheel moves within the <see cref="Window"/>
    /// </summary>
    /// <param name="sender">The <see cref="Window"/> sender</param>
    /// <param name="timestamp">The amount of time that has passed since SDL initialized</param>
    /// <param name="mouseId">The mouse's id, or <see cref="Mouse.TouchMouseId"/></param>
    /// <param name="verticalScroll">The vertical scroll by the wheel. </param>
    /// <param name="horizontalScroll">The horizontal scroll by the wheel. Positive values means the wheel was scrolled to the right, while negative values means the wheel was scrolled to the left.</param>
    /// <remarks>Unlike in native SDL, whether the mouse wheel is inverted or not IS abstracted away. This means that, in the event that the scroll wheel is inverted, the values will be inverted to compensate.</remarks>
    public delegate void MouseWheelEvent(Window sender, TimeSpan timestamp, uint mouseId, float verticalScroll, float horizontalScroll);

    /// <summary>
    /// Fired when the Mouse Wheel is scrolled inside this <see cref="Window"/>
    /// </summary>
    public event MouseWheelEvent? MouseWheelScrolled;

    internal void TriggerEvent(SDL_MouseWheelEvent e)
    {
        if (MouseWheelScrolled is not null)
            MouseWheelScrolled(this, TimeSpan.FromMilliseconds(e.timestamp), e.which, e.direction == 1 ? e.preciseY * -1 : e.preciseY, e.direction == 1 ? e.preciseX * -1 : e.preciseX);
    }

    /// <summary>
    /// Represents an event fired when the mouse moves within the Window
    /// </summary>
    /// <param name="sender">The <see cref="Window"/> sender</param>
    /// <param name="timestamp">The amount of time that has passed since SDL initialized</param>
    /// <param name="delta">The difference between the mouse's last recorded position and the new one</param>
    /// <param name="newPosition">The mouse's actual position within this <see cref="Window"/></param>
    /// <param name="mouseId">The mouse's id, or <see cref="Mouse.TouchMouseId"/></param>
    /// <param name="pressed">The mouse button that was pressed at the time of the mouse being moved</param>
    public delegate void MouseMovedEvent(Window sender, TimeSpan timestamp, Point delta, Point newPosition, uint mouseId, MouseButton pressed);

    /// <summary>
    /// Fired when the mouse is moved inside this <see cref="Window"/>
    /// </summary>
    public event MouseMovedEvent? MouseMoved;

    internal void TriggerEvent(SDL_MouseMotionEvent e)
    {
        if (MouseMoved is not null)
            MouseMoved(this, TimeSpan.FromMilliseconds(e.timestamp), new(e.xrel, e.yrel), new(e.x, e.y), e.which, Mouse.CheckButton(e.state));
    }

    /// <summary>
    /// Represents an event fired when a mouse's button is interacted with within the Window
    /// </summary>
    /// <param name="sender">The <see cref="Window"/> sender</param>
    /// <param name="timestamp">The amount of time that has passed since SDL initialized</param>
    /// <param name="mouseId">The mouse's id, or <see cref="Mouse.TouchMouseId"/></param>
    /// <param name="state">The mouse's button state</param>
    /// <param name="clicks">The amount of clicks the user did on this mouse. 1 for a single click, 2 for a double click</param>
    public delegate void MouseButtonEvent(Window sender, TimeSpan timestamp, uint mouseId, int clicks, MouseButton state);

    /// <summary>
    /// Fired when a mouse button is released inside this <see cref="Window"/>
    /// </summary>
    public event MouseButtonEvent? MouseButtonReleased;

    /// <summary>
    /// Fired when a mouse button is pressed inside this <see cref="Window"/>
    /// </summary>
    public event MouseButtonEvent? MouseButtonPressed;

    internal void TriggerEventMBUP(SDL_MouseButtonEvent e)
    {
        if (MouseButtonReleased is not null)
            MouseButtonReleased(this, TimeSpan.FromMilliseconds(e.timestamp), e.which, e.clicks, Mouse.MapButton(e.button));
    }

    internal void TriggerEventMBDOWN(SDL_MouseButtonEvent e)
    {
        if (MouseButtonPressed is not null)
            MouseButtonPressed(this, TimeSpan.FromMilliseconds(e.timestamp), e.which, e.clicks, Mouse.MapButton(e.button));
    }

    #endregion

    #region TextEvents

    /// <summary>
    /// Represents a text input event
    /// </summary>
    /// <param name="sender">The Window that fired this event</param>
    /// <param name="timestamp">The time elapsed between SDL's initialization and this event firing</param>
    /// <param name="text">The text inputed</param>
    /// <remarks>
    /// In a typical GUI application, the OS will be responsible for telling you the candidate text via <see cref="TextEditing"/>, you can choose how (and where) to show it in your UI. Let's say with an input method I typed "abc" and got unicode character "X", the <see cref="Window"/> will first fire three <see cref="TextEditing"/> events with 'a', 'ab' and 'abc', then finally receive SDL_TEXTINPUT event with unicode character 'X'. During this text compositing process, user can press any arbitrary keys such as Function, backspace, both the <see cref="Window"/> and OS input method will receive it and decide whether to deal with these keys or not. For instance when user press backspace, most input methods will delete the last candidate character typed and <see cref="Window"/> will receive a new <see cref="TextEditing"/> event (let's say user typed a, b, backspace, c, then the application will receive 4 events containing 'a', 'ab', 'a', 'ac' each)
    /// </remarks>
    public delegate void TextInputEvent(Window sender, TimeSpan timestamp, ReadOnlySpan<char> text);

    /// <summary>
    /// Represents a text input event
    /// </summary>
    /// <remarks>
    /// In a typical GUI application, the OS will be responsible for telling you the candidate text via <see cref="TextEditing"/>, you can choose how (and where) to show it in your UI. Let's say with an input method I typed "abc" and got unicode character "X", the <see cref="Window"/> will first fire three <see cref="TextEditing"/> events with 'a', 'ab' and 'abc', then finally receive SDL_TEXTINPUT event with unicode character 'X'. During this text compositing process, user can press any arbitrary keys such as Function, backspace, both the <see cref="Window"/> and OS input method will receive it and decide whether to deal with these keys or not. For instance when user press backspace, most input methods will delete the last candidate character typed and <see cref="Window"/> will receive a new <see cref="TextEditing"/> event (let's say user typed a, b, backspace, c, then the application will receive 4 events containing 'a', 'ab', 'a', 'ac' each)
    /// </remarks>
    public event TextInputEvent? TextInput;

    internal unsafe void TriggerEvent(SDL_TextInputEvent e)
    {
        if (TextEditing != null)
        {
            int len = 0;
            {
                Span<char> buff = new(e.text, 32);
                while (buff[len] != 0)
                    len++;
            }

            var text = new ReadOnlySpan<char>(e.text, len);
            TextEditing(this, TimeSpan.FromMilliseconds(e.timestamp), text, 0);
        }
    }

    /// <summary>
    /// Represents a text edit event
    /// </summary>
    /// <param name="sender">The Window that fired this event</param>
    /// <param name="timestamp">The time elapsed between SDL's initialization and this event firing</param>
    /// <param name="text">The text inputed</param>
    /// <param name="start">The location to begin editing from</param>
    /// <remarks>
    /// In a typical GUI application, the OS will be responsible for telling you the candidate text via <see cref="TextEditing"/>, you can choose how (and where) to show it in your UI. Let's say with an input method I typed "abc" and got unicode character "X", the <see cref="Window"/> will first fire three <see cref="TextEditing"/> events with 'a', 'ab' and 'abc', then finally receive SDL_TEXTINPUT event with unicode character 'X'. During this text compositing process, user can press any arbitrary keys such as Function, backspace, both the <see cref="Window"/> and OS input method will receive it and decide whether to deal with these keys or not. For instance when user press backspace, most input methods will delete the last candidate character typed and <see cref="Window"/> will receive a new <see cref="TextEditing"/> event (let's say user typed a, b, backspace, c, then the application will receive 4 events containing 'a', 'ab', 'a', 'ac' each)
    /// </remarks>
    public delegate void TextEditEvent(Window sender, TimeSpan timestamp, ReadOnlySpan<char> text, int start);

    /// <summary>
    /// Represents a text edit event
    /// </summary>
    /// <remarks>
    /// In a typical GUI application, the OS will be responsible for telling you the candidate text via <see cref="TextEditing"/>, you can choose how (and where) to show it in your UI. Let's say with an input method I typed "abc" and got unicode character "X", the <see cref="Window"/> will first fire three <see cref="TextEditing"/> events with 'a', 'ab' and 'abc', then finally receive SDL_TEXTINPUT event with unicode character 'X'. During this text compositing process, user can press any arbitrary keys such as Function, backspace, both the <see cref="Window"/> and OS input method will receive it and decide whether to deal with these keys or not. For instance when user press backspace, most input methods will delete the last candidate character typed and <see cref="Window"/> will receive a new <see cref="TextEditing"/> event (let's say user typed a, b, backspace, c, then the application will receive 4 events containing 'a', 'ab', 'a', 'ac' each)
    /// </remarks>
    public event TextEditEvent? TextEditing;

    internal unsafe void TriggerEvent(SDL_TextEditingEvent e)
    {
        if (TextEditing != null)
        {
            var text = new ReadOnlySpan<char>(e.text, e.length);
            TextEditing(this, TimeSpan.FromMilliseconds(e.timestamp), text, e.start);
        }
    }

    #endregion

    #region KeyboardEvents

    /// <summary>
    /// Represents an event of a key in the keyboard, when a key is pressed
    /// </summary>
    /// <param name="scancode">The scancode of the pressed key</param>
    /// <param name="key">The character represented by the key map</param>
    /// <param name="isPressed">Whether the key is pressed at the time of the event firing or not</param>
    /// <param name="modifiers">The active keyboard modifiers modifying the key pressed</param>
    /// <param name="repeat">Whether the key is a repeat press (Most usually a held key) or not</param>
    /// <param name="sender">The <see cref="Window"/> that fired this event</param>
    /// <param name="timestamp">The amount of time that has passed since SDL's initialization and this event firing</param>
    /// <param name="unicode">The unicode value of the character pressed</param>
    public delegate void KeyEvent(Window sender, TimeSpan timestamp, Scancode scancode, Keycode key, KeyModifier modifiers, bool isPressed, bool repeat, uint unicode);

    /// <summary>
    /// Represents an event of a key in the keyboard, when a key is released
    /// </summary>
    public event KeyEvent? KeyReleased;

    /// <summary>
    /// Represents an event of a key in the keyboard, when a key is pressed
    /// </summary>
    public event KeyEvent? KeyPressed;

    internal void TriggerEventKDown(SDL_KeyboardEvent e)
    {
        var k = e.keysym;
        KeyPressed?.Invoke(this, TimeSpan.FromMilliseconds(e.timestamp), (Scancode)k.scancode, (Keycode)k.sym, (KeyModifier)k.mod, e.state == 1, e.repeat == 1, k.unicode);
    }

    internal void TriggerEventKUp(SDL_KeyboardEvent e)
    {
        var k = e.keysym;
        KeyReleased?.Invoke(this, TimeSpan.FromMilliseconds(e.timestamp), (Scancode)k.scancode, (Keycode)k.sym, (KeyModifier)k.mod, e.state == 1, e.repeat == 1, k.unicode);
    }

    #endregion

    #region WindowEvents

    /// <summary>
    /// Represents a <see cref="Window"/> event
    /// </summary>
    /// <param name="sender">The <see cref="Window"/> that fired the event</param>
    /// <param name="timestamp">TimeSpan.FromMilliseconds(e.timestamp)</param>
    public delegate void WindowEvent(Window sender, TimeSpan timestamp);

    /// <summary>
    /// Represents a <see cref="Window"/> event where the <see cref="Window"/>'s size changed
    /// </summary>
    /// <param name="sender">The <see cref="Window"/> that fired the event</param>
    /// <param name="timestamp">TimeSpan.FromMilliseconds(e.timestamp)</param>
    /// <param name="newsize">The new size of the <see cref="Window"/></param>
    public delegate void WindowSizeChangedEvent(Window sender, TimeSpan timestamp, Size newsize);

    /// <summary>
    /// Represents a <see cref="Window"/> event where the <see cref="Window"/> 's position changed
    /// </summary>
    /// <param name="sender">The Window that fired the event</param>
    /// <param name="timestamp">TimeSpan.FromMilliseconds(e.timestamp)</param>
    /// <param name="newLocation">The <see cref="Window"/>'s new location</param>
    public delegate void WindowMovedEvent(Window sender, TimeSpan timestamp, Point newLocation);

    public event WindowEvent? Shown;
    public event WindowEvent? Hidden;
    public event WindowEvent? Exposed;
    public event WindowMovedEvent? Moved;
    public event WindowSizeChangedEvent? Resized;
    public event WindowSizeChangedEvent? SizeChanged;
    public event WindowEvent? Minimized;
    public event WindowEvent? Maximized;
    public event WindowEvent? Restored;
    public event WindowEvent? Entered;
    public event WindowEvent? Left;
    public event WindowEvent? FocusGained;
    public event WindowEvent? FocusLost;
    public event WindowEvent? Closed;
    public event WindowEvent? FocusTaken;
    public event WindowEvent? HitTest;
    public event WindowEvent? IccprofChanged;
    public event WindowEvent? DisplayChanged;

    internal void TriggerEvent(SDL_WindowEvent e)
    {
        switch (e.windowEvent)
        {
            case SDL_WindowEventID.SDL_WINDOWEVENT_SHOWN:
                Shown?.Invoke(this, time());
                return;
            
            case SDL_WindowEventID.SDL_WINDOWEVENT_HIDDEN:
                Hidden?.Invoke(this, time());
                return;
            
            case SDL_WindowEventID.SDL_WINDOWEVENT_EXPOSED:
                Exposed?.Invoke(this, time());
                return;
            
            case SDL_WindowEventID.SDL_WINDOWEVENT_MOVED:
                Moved?.Invoke(this, time(), new Point(e.data1, e.data2));
                return;
            
            case SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED:
                Resized?.Invoke(this, time(), new Size(e.data1, e.data2));
                return;
            
            case SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED:
                SizeChanged?.Invoke(this, time(), new Size(e.data1, e.data2));
                UpdateCenterPoint();
                return;
            
            case SDL_WindowEventID.SDL_WINDOWEVENT_MINIMIZED:
                Minimized?.Invoke(this, time());
                return;
            
            case SDL_WindowEventID.SDL_WINDOWEVENT_MAXIMIZED:
                Maximized?.Invoke(this, time());
                return;
            
            case SDL_WindowEventID.SDL_WINDOWEVENT_RESTORED:
                Restored?.Invoke(this, time());
                return;
            
            case SDL_WindowEventID.SDL_WINDOWEVENT_ENTER:
                Entered?.Invoke(this, time());
                return;
            
            case SDL_WindowEventID.SDL_WINDOWEVENT_LEAVE:
                Left?.Invoke(this, time());
                return;
            
            case SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_GAINED:
                FocusGained?.Invoke(this, time());
                return;
            
            case SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_LOST:
                FocusLost?.Invoke(this, time());
                return;
            
            case SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE:
                Closed?.Invoke(this, time());
                return;
            
            case SDL_WindowEventID.SDL_WINDOWEVENT_TAKE_FOCUS:
                FocusTaken?.Invoke(this, time());
                return;
            
            case SDL_WindowEventID.SDL_WINDOWEVENT_HIT_TEST:
                HitTest?.Invoke(this, time());
                return;
            
            case SDL_WindowEventID.SDL_WINDOWEVENT_ICCPROF_CHANGED:
                IccprofChanged?.Invoke(this, time());
                return;
            
            case SDL_WindowEventID.SDL_WINDOWEVENT_DISPLAY_CHANGED:
                DisplayChanged?.Invoke(this, time());
                return;
        }

        TimeSpan time() => TimeSpan.FromMilliseconds(e.timestamp);
    }

    #endregion

    #endregion

    /// <summary>
    /// Represents the center <see cref="Point"/> of the <see cref="Window"/>
    /// </summary>
    public Point CenterPoint { get; private set; }

    private void UpdateCenterPoint()
    {
        var size = Size;
        CenterPoint = new(size.Width / 2, size.Height / 2);
    }

    /// <summary>
    /// The Window Id
    /// </summary>
    /// <remarks>Cached in .NET</remarks>
    public uint WindowId { get; }

    /// <summary>
    /// Gets or Sets the <see cref="SDL"/> <see cref="Window"/> <see cref="Title"/>. get: <see cref="SDL_GetWindowTitle" href="https://wiki.libsdl.org/SDL_GetWindowTitle"/>; set: <see cref="SDL_SetWindowTitle" href="https://wiki.libsdl.org/SDL_SetWindowTitle"/>
    /// </summary>
    public string Title
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetWindowTitle(_handle);
        }
        set
        {
            ThrowIfDisposed();
            SDL_SetWindowTitle(_handle, value);
        }
    }

    /// <summary>
    /// Gets or Sets the <see cref="Window"/>'s <see cref="Opacity"/>. get: <see cref="SDL_GetWindowOpacity" href="https://wiki.libsdl.org/SDL_GetWindowOpacity"/>; set: <see cref="SDL_SetWindowOpacity" href="https://wiki.libsdl.org/SDL_SetWindowOpacity"/>
    /// </summary>
    /// <remarks>
    /// If transparency isn't supported on this platform, <see cref="Opacity"/> will be reported as 1.0f without error. The value will be clamped internally between 0.0f (transparent) and 1.0f (opaque).
    /// </remarks>
    /// <exception cref="SDLWindowException"/>
    /// <exception cref="PlatformNotSupportedException"/>
    public double Opacity
    {
        get
        {
            ThrowIfDisposed();
            SDLWindowException.ThrowIfLessThan(SDL_GetWindowOpacity(_handle, out var opacity), 0);
            return opacity;
        }
        set
        {
            ThrowIfDisposed();
            var error = SDL_SetWindowOpacity(_handle, (float)value);
            if (error is -1)
                throw new PlatformNotSupportedException(SDL_GetError());
            SDLWindowException.ThrowIfLessThan(error, 0);
        }
    }

    /// <summary>
    /// Gets or Sets the <see cref="Window"/>'s <see cref="Opacity"/>. Calculated with Math-Magic
    /// </summary>
    /// <remarks>
    /// If transparency isn't supported on this platform, <see cref="Opacity"/> will be reported as 255 without error. The value will be clamped internally between 0 (transparent) and 255 (opaque).
    /// </remarks>
    /// <exception cref="SDLWindowException"/>
    /// <exception cref="PlatformNotSupportedException"/>
    public byte OpacityByte
    {
        get => (byte)(255 * Opacity);
        set => Opacity = value / 255;
    }

    /// <summary>
    /// Gets the <see cref="Window"/>'s Brightness. get: <see cref="SDL_GetWindowBrightness" href="https://wiki.libsdl.org/SDL_GetWindowBrightness"/>
    /// </summary>
    /// <remarks>
    /// Despite the name, this method retrieves the brightness of the entire <see cref="Display"/>, not an individual <see cref="Window"/>. A <see cref="Window"/> is considered to be owned by the <see cref="Display"/> that contains the <see cref="Window"/>'s center pixel. (The index of this display can be retrieved with <see cref="DisplayIndex"/>.)
    /// </remarks>
    /// <returns></returns>
    public float GetBrightness
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetWindowBrightness(_handle);
        }
    }

    /// <summary>
    /// Sets <see cref="this"/> <see cref="Window"/> as another <see cref="Window"/>'s modal. <see cref="SDL_SetWindowModalFor" href="https://wiki.libsdl.org/SDL_SetWindowModalFor"/>
    /// </summary>
    /// <param name="parent">The parent <see cref="Window"/> to set this window as a modal for</param>
    public void SetAsModalFor(Window parent)
    {
        ThrowIfDisposed();
        if (ReferenceEquals(this, parent))
            throw new InvalidOperationException("Cannot set a Window as a modal of itself");
        SDLWindowException.ThrowIfLessThan(SDL_SetWindowModalFor(_handle, parent._handle), 0);
    }

    /// <summary>
    /// Sets <paramref name="modal"/> as <see cref="this"/> <see cref="Window"/>'s modal.
    /// </summary>
    /// <param name="parent">The parent <see cref="Window"/> to set this <see cref="Window"/> as a modal for</param>
    public void SetAsModal(Window modal)
    {
        ThrowIfDisposed();
        modal.SetAsModalFor(this);
    }

    /// <summary>
    /// Raise a <see cref="Window"/> above other <see cref="Window"/>s and set the input focus. <see cref="SDL_RaiseWindow" href="https://wiki.libsdl.org/SDL_RaiseWindow"/>
    /// </summary>
    public void Raise()
    {
        ThrowIfDisposed();
        SDL_RaiseWindow(_handle);
    }

    /// <summary>
    /// Shows the <see cref="Window"/>. <see cref="SDL_ShowWindow" href="https://wiki.libsdl.org/SDL_ShowWindow"/>
    /// </summary>
    public void Show()
    {
        ThrowIfDisposed();
        SDL_ShowWindow(_handle);
    }

    /// <summary>
    /// Hides the <see cref="Window"/>. <see cref="SDL_HideWindow" href="https://wiki.libsdl.org/SDL_HideWindow"/>
    /// </summary>
    public void Hide()
    {
        ThrowIfDisposed();
        SDL_HideWindow(_handle);
    }

    /// <summary>
    /// Maximize the <see cref="Window"/>. <see cref="SDL_MaximizeWindow" href="https://wiki.libsdl.org/SDL_MaximizeWindow"/>
    /// </summary>
    public void Maximize()
    {
        ThrowIfDisposed();
        SDL_MaximizeWindow(_handle);
    }

    /// <summary>
    /// Minimize the <see cref="Window"/>. <see cref="SDL_MinimizeWindow" href="https://wiki.libsdl.org/SDL_MinimizeWindow"/>
    /// </summary>
    public void Minimize()
    {
        ThrowIfDisposed();
        SDL_MinimizeWindow(_handle);
    }

    /// <summary>
    /// Sets this Window as the OS's input focus. <see cref="SDL_SetWindowInputFocus" href="https://wiki.libsdl.org/SDL_SetWindowInputFocus"/>
    /// </summary>
    /// <remarks>
    /// You almost certainly want <see cref="Raise"/> instead of this method. Use this with caution, as you might give focus to a window that is completely obscured by other windows.
    /// </remarks>
    public void SetInputFocus()
    {
        ThrowIfDisposed();
        SDLWindowException.ThrowIfLessThan(SDL_SetWindowInputFocus(_handle), 0);
    }

    /// <summary>
    /// Gets this Window's display index. <see cref="SDL_GetWindowDisplayIndex" href="https://wiki.libsdl.org/SDL_GetWindowDisplayIndex"/>
    /// </summary>
    /// <remarks>
    /// The index of the display containing the center of the window on success
    /// </remarks>
    public int DisplayIndex
    {
        get
        {
            ThrowIfDisposed();
            var r = SDL_GetWindowDisplayIndex(_handle);
            SDLWindowException.ThrowIfLessThan(r, 0);
            return r;
        }
    }

    /// <summary>
    /// Gets or Sets the Display mode of this window. Getter never returns null. get: <see cref="SDL_GetWindowDisplayMode" href="https://wiki.libsdl.org/SDL_GetWindowDisplayMode"/>; set: <see cref="SDL_SetWindowDisplayMode" href="https://wiki.libsdl.org/SDL_SetWindowDisplayMode"/>
    /// </summary>
    /// <remarks>
    /// This only affects the display mode used when the window is fullscreen. To change the window size when the window is not fullscreen, use <see cref="Size" />.
    /// </remarks>
    public SDL_DisplayMode? DisplayMode
    {
        [return: NotNull]
        get
        {
            ThrowIfDisposed();
            SDLWindowException.ThrowIfLessThan(SDL_GetWindowDisplayMode(_handle, out var mode), 0);
            return mode;
        }
        set
        {
            ThrowIfDisposed();
            if (value is SDL_DisplayMode mode)
            {
                SDLWindowException.ThrowIfLessThan(SDL_SetWindowDisplayMode(_handle, ref mode), 0);
                return;
            }
            SDLWindowException.ThrowIfLessThan(SDL_SetWindowDisplayMode(_handle, IntPtr.Zero), 0);
        }
    }

    private FullscreenMode _fs;

    /// <summary>
    /// Gets or sets this Window as fullscreen with the passed flags. get: Stored in memory on the C# side, may not be representative of SDL's actual state; set: <see cref="SDL_SetWindowFullscreen" href="https://wiki.libsdl.org/SDL_SetWindowFullscreen"/>
    /// </summary>
    public FullscreenMode FullscreenMode
    {
        get => _fs;
        set
        {
            ThrowIfDisposed();
            SDLWindowException.ThrowIfLessThan(SDL_SetWindowFullscreen(_handle, (uint)(_fs = value)), 0);
        }
    }

    /// <summary>
    /// Sets this window's gamma ramp. <see cref="SDL_SetWindowGammaRamp" href="https://wiki.libsdl.org/SDL_SetWindowGammaRamp"/>
    /// </summary>
    /// <param name="red">The gamma translation table for the red channel. Should contain 256 16-bit quantities</param>
    /// <param name="green">The gamma translation table for the green channel. Should contain 256 16-bit quantities</param>
    /// <param name="blue">The gamma translation table for the blue channel. Should contain 256 16-bit quantities</param>
    /// <remarks>
    /// Set the gamma translation table for the red, green, and blue channels of the video hardware. Each table is an array of 256 16-bit quantities, representing a mapping between the input and output for that channel. The input is the index into the array, and the output is the 16-bit gamma value at that index, scaled to the output color precision.
    /// </remarks>
    public void SetGammaRamp(ushort[] red, ushort[] green, ushort[] blue)
    {
        ThrowIfDisposed();
        SDLWindowException.ThrowIfLessThan(SDL_SetWindowGammaRamp(_handle, red, green, blue), 0);
    }

    /// <summary>
    /// Gets or sets whether this window is grabbed. get: <see cref="SDL_GetWindowGrab" href="https://wiki.libsdl.org/SDL_GetWindowGrab"/>; set: <see cref="SDL_SetWindowGrab" href="https://wiki.libsdl.org/SDL_SetWindowGrab"/>
    /// </summary>
    /// <remarks>
    /// When input is grabbed, the mouse is confined to the window. This function will also grab the keyboard if SDL_HINT_GRAB_KEYBOARD is set. To grab the keyboard without also grabbing the mouse, use SDL_SetWindowKeyboardGrab().
    /// </remarks>
    public bool Grab
    {
        get => SDL_GetWindowGrab(_handle) is SDL_bool.SDL_TRUE;
        set => SDL_SetWindowGrab(_handle, value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
    }

    /// <summary>
    /// Gets or sets whether this window is grabbing the keyboard. get: <see cref="SDL_GetWindowKeyboardGrab" href="https://wiki.libsdl.org/SDL_GetWindowKeyboardGrab"/>; set: <see cref="SDL_SetWindowKeyboardGrab" href="https://wiki.libsdl.org/SDL_SetWindowKeyboardGrab"/>
    /// </summary>
    /// <remarks>
    /// Keyboard grab enables capture of system keyboard shortcuts like Alt+Tab or the Meta/Super key. Note that not all system keyboard shortcuts can be captured by applications (one example is Ctrl+Alt+Del on Windows).
    /// </remarks>
    public bool KeyboardGrab
    {
        get => SDL_GetWindowKeyboardGrab(_handle) is SDL_bool.SDL_TRUE;
        set => SDL_SetWindowKeyboardGrab(_handle, value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
    }

    /// <summary>
    /// Gets or sets whether this <see cref="Window"/> is grabbing the mouse. get: <see cref="SDL_GetWindowMouseGrab" href="https://wiki.libsdl.org/SDL_GetWindowMouseGrab"/>; set: <see cref="SDL_SetWindowMouseGrab" href="https://wiki.libsdl.org/SDL_SetWindowMouseGrab"/>
    /// </summary>
    /// <remarks>
    /// Mouse grab confines the mouse cursor to the window.
    /// </remarks>
    public bool MouseGrab
    {
        get => SDL_GetWindowMouseGrab(_handle) is SDL_bool.SDL_TRUE;
        set => SDL_SetWindowMouseGrab(_handle, value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
    }

    /// <summary>
    /// Sets this <see cref="Window"/>'s icon. <see cref="SDL_SetWindowIcon" href="https://wiki.libsdl.org/SDL_SetWindowIcon"/>
    /// </summary>
    /// <param name="icon"></param>
    public void SetIcon(Surface icon)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(icon); // Rather than throwing if null in a property with a nullable type; is it possible to remove a Window's Icon by setting it to IntPtr.Zero?
        SDL_SetWindowIcon(_handle, icon._handle);
    }

    /// <summary>
    /// Gets or sets this <see cref="Window"/>'s maximum size. get: <see cref="SDL_GetWindowMaximumSize" href="https://wiki.libsdl.org/SDL_GetWindowMaximumSize"/>; set: <see cref="SDL_SetWindowMaximumSize" href="https://wiki.libsdl.org/SDL_SetWindowMaximumSize"/>
    /// </summary>
    public Size MaximumSize
    {
        get
        {
            ThrowIfDisposed();
            SDL_GetWindowMaximumSize(_handle, out var w, out var h);
            return new(w, h);
        }
        set
        {
            ThrowIfDisposed();
            SDL_SetWindowMaximumSize(_handle, value.Width, value.Height);
        }
    }

    /// <summary>
    /// Gets or sets this <see cref="Window"/>'s minimum size. get: <see cref="SDL_GetWindowMinimumSize" href="https://wiki.libsdl.org/SDL_GetWindowMinimumSize"/>; set: <see cref="SDL_SetWindowMinimumSize" href="https://wiki.libsdl.org/SDL_SetWindowMinimumSize"/>
    /// </summary>
    public Size MinimumSize
    {
        get
        {
            ThrowIfDisposed();
            SDL_GetWindowMinimumSize(_handle, out var w, out var h);
            return new(w, h);
        }
        set
        {
            ThrowIfDisposed();
            SDL_SetWindowMinimumSize(_handle, value.Width, value.Height);
        }
    }

    /// <summary>
    /// Gets or sets this <see cref="Window"/>'s actual size. get: <see cref="SDL_GetWindowSize" href="https://wiki.libsdl.org/SDL_GetWindowSize"/>; set: <see cref="SDL_SetWindowMinimumSize" href="https://wiki.libsdl.org/SDL_SetWindowMinimumSize"/>
    /// </summary>
    public Size Size
    {
        get
        {
            ThrowIfDisposed();
            SDL_GetWindowSize(_handle, out var w, out var h);
            return new(w, h);
        }
        set
        {
            ThrowIfDisposed();
            SDL_SetWindowSize(_handle, value.Width, value.Height);
        }
    }

    /// <summary>
    /// Gets or sets this <see cref="Window"/>'s position in the screen. get: <see cref="SDL_GetWindowPosition" href="https://wiki.libsdl.org/SDL_GetWindowPosition"/> set: <see cref="SDL_SetWindowPosition" href="https://wiki.libsdl.org/SDL_SetWindowPosition"/>
    /// </summary>
    /// <remarks>
    /// The <see cref="Window"/>'s coordinate origin is the upper left of the <see cref="Display"/>.
    /// </remarks>
    public Point Position
    {
        get
        {
            ThrowIfDisposed();
            SDL_GetWindowPosition(_handle, out var x, out var y);
            return new(x, y);
        }
        set
        {
            ThrowIfDisposed();
            SDL_SetWindowPosition(_handle, value.X, value.Y);
        }
    }

    /// <summary>
    /// Gets the <see cref="Window"/>'s border sizes. <see cref="SDL_GetWindowBordersSize" href="https://wiki.libsdl.org/SDL_GetWindowBordersSize"/>
    /// </summary>
    /// <remarks>
    /// This function may fail on systems where the window has not yet been decorated by the display server (for example, immediately after instantiating a new <see cref="Window"/>). It is recommended that you wait at least until the window has been presented and composited, so that the window system has a chance to decorate the window and provide the border dimensions to SDL.
    /// </remarks>
    /// <param name="top">The size of the top border</param>
    /// <param name="left">The size of the left border</param>
    /// <param name="bottom">The size of the bottom border</param>
    /// <param name="right">The size of the right border</param>
    public void GetBorderSize(out int top, out int left, out int bottom, out int right)
    {
        ThrowIfDisposed();
        SDLWindowException.ThrowIfLessThan(SDL_GetWindowBordersSize(_handle, out top, out left, out bottom, out right), 0);
    }

    /// <summary>
    /// Configures several settings for this <see cref="Window"/>. Calls several <see cref="SDL"/> functions at once.
    /// </summary>
    /// <param name="hasBorder">Whether the window is borderless or not. <see cref="SDL_SetWindowBordered" href="https://wiki.libsdl.org/SDL_SetWindowBordered"/></param>
    /// <param name="alwaysOnTop">Whether the window should remain always on top. <see cref="SDL_SetWindowAlwaysOnTop" href="https://wiki.libsdl.org/SDL_SetWindowAlwaysOnTop"/></param>
    /// <param name="isResizable">Whether the window is resizable. <see cref="SDL_SetWindowResizable" href="https://wiki.libsdl.org/SDL_SetWindowResizable"/></param>
    public void Configure(bool hasBorder, bool alwaysOnTop, bool isResizable)
    {
        ThrowIfDisposed();
        SDL_SetWindowBordered(_handle, hasBorder ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
        SDL_SetWindowAlwaysOnTop(_handle, alwaysOnTop ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
        SDL_SetWindowResizable(_handle, isResizable ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
    }

    /// <summary>
    /// Updates the <see cref="Window"/>'s surface by copying the <see cref="Window"/> surface to the screen. <see cref="SDL_UpdateWindowSurface" href="https://wiki.libsdl.org/SDL_UpdateWindowSurface"/>
    /// </summary>
    /// <remarks>
    /// This is the method you use to reflect any changes to the surface on the screen.
    /// </remarks>
    public void UpdateSurface()
    {
        ThrowIfDisposed();
        SDLWindowException.ThrowIfLessThan(SDL_UpdateWindowSurface(_handle), 0);
    }

    /// <summary>
    /// The <see cref="Surface"/> of this <see cref="Window"/>
    /// </summary>
    public Surface Surface { get; }

    /// <summary>
    /// Copy areas of the window surface to the screen. <see cref="SDL_UpdateWindowSurfaceRects" href="https://wiki.libsdl.org/SDL_UpdateWindowSurfaceRects"/>
    /// </summary>
    /// <param name="rectangles">The areas of the window's surface to copy to the screen</param>
    /// <param name="numrect">The amount of rectangles to update, or null to use <paramref name="rectangles"/>'s length</param>
    /// <remarks>
    /// This is the function you use to reflect changes to portions of the surface on the screen.
    /// </remarks>
    public void UpdateSurfaceRects(Span<Rectangle> rectangles, int? numrect)
    {
        ThrowIfDisposed();
        Span<SDL_Rect> rects = stackalloc SDL_Rect[numrect ?? rectangles.Length];
        for (int i = 0; i < rects.Length; i++)
            rectangles[i].ToSDL(ref rects[i]);
        SDLWindowException.ThrowIfLessThan(SDL_UpdateWindowSurfaceRects(_handle, rects, rects.Length), 0);
    }

    private UserData? hitTestCallbackData;
    private HitTestCallback? hitTestCallback;
    private readonly SDL_HitTest hitTestDelegate;
    /// <summary>
    /// Provide a callback that decides if a window region has special properties. <see cref="SDL_SetWindowHitTest" href="https://wiki.libsdl.org/SDL_SetWindowHitTest"/>
    /// </summary>
    /// <param name="callback">The callback to assign to this <see cref="Window"/></param>
    /// <param name="userData">User data to be held by this window, and passed to the callback when called. This has no effect on the SDL side of things (and is in fact stored in the .NET heap for the purposes of this library) and is intended to provide special, and even identification data for a given <see cref="Window"/></param>
    /// <remarks>
    /// Your callback may fire at any time, and its firing does not indicate any specific behavior (for example, on Windows, this certainly might fire when the OS is deciding whether to drag your window, but it fires for lots of other reasons, too, some unrelated to anything you probably care about and when the mouse isn't actually at the location it is testing). Since this can fire at any time, you should try to keep your callback efficient, devoid of allocations, etc.
    /// </remarks>
    /// <exception cref="PlatformNotSupportedException">Thrown if the current platform does not support assigning a HitTestCallback to a <see cref="Window"/>. See <see cref="IsHitTestSupported"/></exception>
    public void SetHitTestCallback(HitTestCallback? callback, UserData? userData)
    {
        ThrowIfDisposed();
        if (!IsHitTestSupported)
            throw new PlatformNotSupportedException($"Assigning a HitTestCallback to a Window is not supported on this platform.");
        hitTestCallback = callback;
        hitTestCallbackData = userData;
    }

    /// <summary>
    /// true if setting a HitTestCallback is supported in this platform. false otherwise.
    /// </summary>
    /// <remarks>
    /// This is tested inside .NET as SDL does not provide a way to test this. However, for the purposes of this library, a <see cref="Window"/>'s constructor will always assign a callback to an <see cref="SDL"/>'s window, and manage .NET callbacks from that. If that assignment fails, it'll be assumed to be unsupported.
    /// </remarks>
    public bool IsHitTestSupported { get; }

    /// <summary>
    /// Request a <see cref="Window"/> to demand attention from the user. <see cref="SDL_FlashWindow" href="https://wiki.libsdl.org/SDL_FlashWindow"/>
    /// </summary>
    /// <param name="operation">The operation to request for the <see cref="Window"/></param>
    public void Flash(SDL_FlashOperation operation)
    {
        ThrowIfDisposed();
        SDLWindowException.ThrowIfLessThan(SDL_FlashWindow(_handle, operation), 0);
    }

    /// <summary>
    /// A barrier inside the window, defining an area the grabbed mouse is bound in. This does NOT grab the mouse, and only works when the window has mouse focus.
    /// </summary>
    public Rectangle? MouseRectangle
    {
        get
        {
            ThrowIfDisposed();
            var ptr = SDL_GetWindowMouseRect(_handle);
            return ptr == IntPtr.Zero ? null : Marshal.PtrToStructure<SDL_Rect>(ptr);
        }
        set
        {
            ThrowIfDisposed();
            if (value is null)
            {
                SDLWindowException.ThrowIfLessThan(SDL_SetWindowMouseRect(_handle, IntPtr.Zero), 0);
                return;
            }

            SDL_Rect r = default;
            ((Rectangle)value).ToSDL(ref r);
            SDLWindowException.ThrowIfLessThan(SDL_SetWindowMouseRect(_handle, ref r), 0);
        }
    }

    /// <summary>
    /// Gets System specific window information
    /// </summary>
    public SDL_SysWMinfo SystemInfo
    {
        get
        {
            SDL_SysWMinfo info = default;
            SDL_VERSION(out info.version);
            SDL_GetWindowWMInfo(_handle, ref info);
            return info;
        }
    }

    /// <summary>
    /// Get the <see cref="Window"/> that currently has an input grab enabled. <see cref="SDL_GetGrabbedWindow" href="https://wiki.libsdl.org/SDL_GetGrabbedWindow"/>
    /// </summary>
    /// <returns>Returns the <see cref="Window"/> if input is grabbed or null otherwise.</returns>
    /// <exception cref="SDLWindowException"></exception>
    public static Window? GetGrabbedWindow()
    {
        var ptr = SDL_GetGrabbedWindow();
        return ptr == IntPtr.Zero
            ? null
            : _handleDict.TryGetValue(ptr, out var wr)
            ? wr.TryGetTarget(out var window) ?
            window
            : throw new SDLWindowException("This window object has already been garbage collected and disposed")
            : throw new SDLWindowException("Could not match the returned pointer to a window object. Did you instantiate this Window directly through SDL?");
    }

    internal static Window? GetGrabbedWindow(Window prospective, IntPtr ptr) 
        => prospective._handle == ptr ? prospective : GetGrabbedWindow();

    /// <summary>
    /// Represents a method set as a callback for a <see cref="Window"/>'s hit test
    /// </summary>
    /// <param name="window">The <see cref="Window"/> object this callback refers to</param>
    /// <param name="area">The area the <see cref="Window"/> was hit</param>
    /// <param name="data">User defined data held by the <see cref="Window"/></param>
    /// <returns>The appropriate user selected HitTestResult</returns>
    /// <remarks>See <see cref="SetHitTestCallback(HitTestCallback?, UserData?)"/></remarks>
    public delegate HitTestResult HitTestCallback(Window window, Point area, UserData? data);

    #region IDisposable

    private bool disposedValue;

    /// <summary>
    /// Disposes the object, freeing unmanaged SDL resources
    /// </summary>
    /// <remarks>Try not to call this directly</remarks>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            SDL_DestroyWindow(_handle);
            _handleDict.TryRemove(_handle, out _);
            _idDict.TryRemove(WindowId, out _);
            disposedValue = true;
        }
    }

    /// <summary>
    /// Finalizes the object, in case it wasn't disposed
    /// </summary>
    ~Window()
    {
        Dispose(disposing: false);
    }

    /// <summary>
    /// Disposes the object, freeing unmanaged SDL resources
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private void ThrowIfDisposed()
    {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(Window));
    }

    #endregion
}
