namespace CEo.Pokemon.HomeBalls.App.Settings;

public interface IHomeBallsAppSettingsValueProperty<TValue> :
    IHomeBallsAppSettingsProperty,
    IMutableNotifyingProperty<TValue>,
    IAsyncLoadable<IHomeBallsAppSettingsValueProperty<TValue>> { }

public class HomeBallsAppSettingsValueProperty<TValue> :
    HomeBallsAppSettingsPropertyEndpoint,
    IHomeBallsAppSettingsValueProperty<TValue>,
    IAsyncLoadable<HomeBallsAppSettingsValueProperty<TValue>>
{
    public HomeBallsAppSettingsValueProperty(
        IMutableNotifyingProperty<TValue> property,
        String propertyName,
        String identifier,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILogger? logger = default) :
        base(propertyName, identifier, localStorage, jsRuntime, eventRaiser, logger)
    {
        IsValueString = typeof(TValue).IsAssignableTo(typeof(String));
        Property = property;
        ValueChanged += OnValueChanged;
    }

    public TValue Value
    {
        get => Property.Value;
        set => Property.Value = value;
    }

    protected internal Boolean IsValueString { get; }

    protected internal IMutableNotifyingProperty<TValue> Property { get; }

    TValue INotifyingProperty<TValue>.Value => Value;

    TValue IProperty<TValue>.Value => Value;

    public event EventHandler<PropertyChangedEventArgs<TValue>>? ValueChanged
    {
        add => Property.ValueChanged += value;
        remove => Property.ValueChanged -= value;
    }

    new public virtual async ValueTask<HomeBallsAppSettingsValueProperty<TValue>> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        await base.EnsureLoadedAsync(cancellationToken);
        return this;
    }

    protected internal override async Task LoadAsync(CancellationToken cancellationToken = default)
    {
        var value = IsValueString ?
            (TValue)(Object)(await LocalStorage.GetItemAsStringAsync(Identifier, cancellationToken)) :
            await LocalStorage.GetItemAsync<TValue>(Identifier, cancellationToken);

        Property.Value = value;
    }

    protected internal override Task SaveAsync(CancellationToken cancellationToken = default) => (IsValueString ?
        LocalStorage.SetItemAsStringAsync(Identifier, (String)(Object)Property.Value!, cancellationToken) :
        LocalStorage.SetItemAsync(Identifier, Property.Value, cancellationToken))
        .AsTask();

    protected internal virtual async void OnValueChanged(
        Object? sender,
        PropertyChangedEventArgs<TValue> value)
    {
        if (!IsLoading) await SaveAsync();
    }

    async ValueTask<IHomeBallsAppSettingsValueProperty<TValue>> IAsyncLoadable<IHomeBallsAppSettingsValueProperty<TValue>>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);
}
