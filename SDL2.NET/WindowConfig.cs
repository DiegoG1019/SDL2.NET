//namespace SDL2.NET;

///// <summary>
///// Provides a means to easily configure a Window.
///// </summary>
///// <remarks>
///// Create a <see cref="WindowConfig"/> instance of this class as a local variable, and use the methods provided by it
///// </remarks>
//public sealed class WindowConfig
//{
//    public FullscreenMode FullscreenType { get; private set; }
//    public BackendType BackendType { get; private set; }
//    public WindowFlags ShownOrHidden { get; private set; }
//    public bool IsBorderless { get; private set; }
//    public bool IsResizable { get; private set; }
//    public WindowFlags MaximizedOrMinimized { get; private set; }
//    public bool HasMouseGrabbed { get; private set; }
//    public bool HasInputFocus { get; private set; }
//    public bool HasMouseFocus { get; private set; }
//    public bool IsForeign { get; private set; }
//    public bool AllowHighDpi { get; private set; }
//    public bool HasMouseCaptured { get; private set; }
//    public bool AlwaysOnTop { get; private set; }
//    public bool SkipTaskbar { get; private set; }
//    public bool Utility { get; private set; }
//    public bool Tooltip { get; private set; }
//    public bool PopupMenu { get; private set; }
//    public bool Keyboardgrabbed { get; private set; }
//    public bool InputGrabbed { get; private set; }

//    /// <summary>
//    /// The Backend to use for this Window
//    /// </summary>
//    /// <param name="backend"></param>
//    /// <returns></returns>
//    public WindowConfig Backend(BackendType backend)
//    {
//        BackendType = backend;
//        return this;
//    }

//    /// <summary>
//    /// The fullscreen mode of the window
//    /// </summary>
//    /// <param name="fs"></param>
//    /// <returns></returns>
//    public WindowConfig Fullscreen(FullscreenMode fs)
//    {
//        FullscreenType = fs;
//        return this;
//    }

//    /// <summary>
//    /// Whether or not the window is borderless
//    /// </summary>
//    /// <param name="isBorderless"></param>
//    /// <returns></returns>
//    public WindowConfig Borderless(bool isBorderless)
//    {
//        IsBorderless = isBorderless;
//        return this;
//    }
//}
//#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member