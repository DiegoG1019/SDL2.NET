    private ISDLLogger Logger = DefaultSDLLogger.Default;
    public SDLApplication SetLogger(ISDLLogger logger)
    {
        ThrowIfDisposed();
        Logger = logger;
        return this;
    }

