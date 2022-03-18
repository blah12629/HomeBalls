namespace CEo.Pokemon.HomeBalls.App.Components;

public abstract class HomeBallsLoggingComponent :
    HomeBallsBaseComponent
{
    ILoggerFactory? _loggerFactory;
    ILogger? _logger;

    [Inject]
    protected internal ILoggerFactory LoggerFactory
    {
        get => _loggerFactory ?? throw new NullReferenceException();
        init => _loggerFactory = value;
    }

    protected internal ILogger Logger => _logger ??=
        LoggerFactory.CreateLogger(GetType());

    public EventId EventId { get; private set; }

    protected internal virtual void LogInformation(EventId eventId, Exception? exception, String? message, params Object?[] args) => Logger.LogInformation(eventId, exception, message, args);

    protected internal virtual void LogInformation(EventId eventId, String? message, params Object?[] args) => Logger.LogInformation(eventId, message, args);

    protected internal virtual void LogInformation(Exception? exception, String? message, params Object?[] args) => LogInformation(EventId, exception, message, args);

    protected internal virtual void LogInformation(String? message, params Object?[] args) => LogInformation(EventId, message, args);
}