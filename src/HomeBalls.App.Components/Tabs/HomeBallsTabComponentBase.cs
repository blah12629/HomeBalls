namespace CEo.Pokemon.HomeBalls.App.Components.Tabs;

public abstract class HomeBallsTabComponentBase :
    HomeBallsLoggingComponent
{
    IHomeBallsAppTabList? _tabs;

    [Inject]
    protected internal IHomeBallsAppTabList Tabs
    {
        get => _tabs ?? throw new NullReferenceException();
        init => _tabs = value;
    }
}