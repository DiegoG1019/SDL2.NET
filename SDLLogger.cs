using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET;
public class DefaultSDLLogger : ISDLLogger
{
    private static LogLevel _defaultll = LogLevel.Information;
    private static ISDLLogger? _instance;
    public static void SetDefaultLevel(LogLevel level)
    {
        _instance = null;
        _defaultll = level;
    }

    public static ISDLLogger Default => _instance ??= new DefaultSDLLogger(_defaultll);

    private readonly bool _isWarn;
    private readonly bool _isInfo;
    private readonly bool _isDebug;
    private readonly bool _isVerb;

    public LogLevel LogLevel { get; }

    public DefaultSDLLogger(LogLevel logLevel)
    {
        LogLevel = logLevel;
        _isWarn = logLevel >= LogLevel.Warning;
        _isInfo = logLevel >= LogLevel.Information;
        _isDebug = logLevel >= LogLevel.Debug;
        _isVerb = logLevel >= LogLevel.Verbose;
    }

    private static void _log(LogLevel level, ISDLLogContext context, string template, object[] data)
        => Console.WriteLine($"[{level}]({DateTime.Now:g}) {context.FormatContext()}: {string.Format(template, data)}");

    public void Log(LogLevel level, ISDLLogContext context, string template, object[] data)
    {
        if (LogLevel >= level)
            _log(level, context, template, data);
    }

    public void Fatal(ISDLLogContext context, string template, object[] data)
    {
        Log(LogLevel.Fatal, context, template, data);
    }

    public void Warning(ISDLLogContext context, string template, object[] data)
    {
        if (_isWarn)
            Log(LogLevel.Warning, context, template, data);
    }

    public void Information(ISDLLogContext context, string template, object[] data)
    {
        if (_isInfo)
            Log(LogLevel.Information, context, template, data);
    }

    public void Debug(ISDLLogContext context, string template, object[] data)
    {
        if (_isDebug)
            Log(LogLevel.Debug, context, template, data);
    }

    public void Verbose(ISDLLogContext context, string template, object[] data)
    {
        if (_isVerb)
            Log(LogLevel.Verbose, context, template, data);
    }
}

public interface ISDLLogger
{
    public void Log(LogLevel level, ISDLLogContext context, string template, object[] data);
    public void Fatal(ISDLLogContext context, string template, object[] data);        
    public void Warning(ISDLLogContext context, string template, object[] data);        
    public void Information(ISDLLogContext context, string template, object[] data);        
    public void Debug(ISDLLogContext context, string template, object[] data);        
    public void Verbose(ISDLLogContext context, string template, object[] data);        
}

public interface ISDLLogContext
{
    public string FormatContext();
}

public enum LogLevel
{
    Fatal,
    Warning,
    Information,
    Debug,
    Verbose
}