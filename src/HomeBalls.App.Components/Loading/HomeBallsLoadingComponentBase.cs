namespace CEo.Pokemon.HomeBalls.App.Components.Loading;

public abstract class HomeBallsLoadingComponentBase :
    HomeBallsLoggingComponent
{
    IHomeBallsAppStateContainer? _state;

    [Inject]
    protected internal IHomeBallsAppStateContainer State
    {
        get => _state ?? throw new NullReferenceException();
        init => _state = value;
    }

    protected internal virtual String? Message =>
        State.LoadingMessage.Value;

    protected internal virtual Boolean HasMessage =>
        !String.IsNullOrWhiteSpace(State.LoadingMessage.Value);

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        State.LoadingMessage.ValueChanged += OnMessageChanged;
    }

    protected internal virtual void OnMessageChanged(
        Object? sender,
        ValueChangedEventArgs<String?> e) { }
}