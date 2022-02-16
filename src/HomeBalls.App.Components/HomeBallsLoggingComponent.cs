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

    new public virtual Int32 Id
    {
        get => base.Id;
        protected internal set
        {
            base.Id = value;
            EventId = new(base.Id);
        }
    }

    public EventId EventId { get; private set; }
}