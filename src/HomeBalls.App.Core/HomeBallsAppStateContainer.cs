namespace CEo.Pokemon.HomeBalls.App;

public interface IHomeBallsAppStateContainer
{
    ICollection<Type> RootComponentsRendered {get; }

    IMutableNotifyingProperty<String?> LoadingMessage { get; }

    IMutableNotifyingProperty<UInt16?> HoveredBallId { get; }
}

public class HomeBallsAppStateContainer : IHomeBallsAppStateContainer
{
    public HomeBallsAppStateContainer(
        ILogger? logger = default)
    {
        EventRaiser = new EventRaiser().RaisedBy(this);
        Logger = logger;

        LoadingMessage = new MutableNotifyingProperty<String?>(default, nameof(LoadingMessage), EventRaiser, Logger);
        HoveredBallId = new MutableNotifyingProperty<UInt16?>(default, nameof(HoveredBallId), EventRaiser, Logger);
        RootComponentsRendered = new HashSet<Type> { };
    }

    public ICollection<Type> RootComponentsRendered { get; }

    public IMutableNotifyingProperty<String?> LoadingMessage { get; }

    public IMutableNotifyingProperty<UInt16?> HoveredBallId { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }
}