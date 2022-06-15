namespace SDL2.NET;

/// <summary>
/// Represents the methods that are used to instance and pre-configure an <see cref="SDLApplication{TApp}"/>
/// </summary>
public static class SDLAppBuilder
{
    internal static object? AppInstance;

    /// <summary>
    /// Creates the Instance of <see cref="SDLApplication{TApp}"/> with the desired type of app
    /// </summary>
    /// <remarks>Remember that this class can only be instanced once. If your type doesn't have a default constructor, see <see cref="CreateInstance{TApp}(Func{TApp})"/></remarks>
    /// <returns>The new instance created</returns>
    public static TApp CreateInstance<TApp>() where TApp : SDLApplication<TApp>, new()
    {
        return new TApp(); // It seems dirty, I don't particularly like it, but the constructor itself sets the AppInstance
    }

    /// <summary>
    /// Creates the Instance of <see cref="SDLApplication{TApp}"/> with the desired type of app and a custom constructor method
    /// </summary>
    /// <param name="constructor">The method to call to instance the app</param>
    /// <remarks>Remember that this class can only be instanced once.</remarks>
    /// <returns>The new instance created</returns>
    public static TApp CreateInstance<TApp>(Func<TApp> constructor) where TApp : SDLApplication<TApp>
    {
        ArgumentNullException.ThrowIfNull(constructor);
        return constructor() ?? throw new ArgumentException("The provided constructor delegate returned null", nameof(constructor));
    }

    /// <summary>
    /// Creates the Instance of <see cref="SDLApplication{TApp}"/> as a <see cref="SDLDefaultApp"/> that has no added functionality
    /// </summary>
    /// <remarks>Remember that this class can only be instanced once.</remarks>
    public static SDLDefaultApp CreateDefaultInstance()
        => CreateInstance<SDLDefaultApp>();
}
