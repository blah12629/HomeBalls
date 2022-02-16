namespace CEo.Pokemon.HomeBalls.App.Categories;

public interface IHomeBallsAppSettingsProperty
{
    Task SaveAsync(CancellationToken cancellationToken = default);
}

public interface IHomeBallsAppSettingsValueProperty<T> :
    IHomeBallsAppSettingsProperty,
    IMutableNotifyingProperty<T>,
    IAsyncLoadable<IHomeBallsAppSettingsValueProperty<T>> { }

public class HomeBallsAppSettingsValueProperty<T> :
    MutableNotifyingProperty<T>,
    IHomeBallsAppSettingsValueProperty<T>,
    IAsyncLoadable<HomeBallsAppSettingsValueProperty<T>>
{
    public HomeBallsAppSettingsValueProperty(
        T defaultValue,
        String propertyName,
        ILocalStorageService localStorage,
        IEventRaiser eventRaiser,
        ILogger? logger = default,
        IEqualityComparer<T>? comparer = default) :
        base(defaultValue, propertyName, eventRaiser, logger, comparer) =>
        LocalStorage = localStorage;

    public HomeBallsAppSettingsValueProperty(
        T defaultValue,
        String propertyName,
        String identifier,
        ILocalStorageService localStorage,
        IEventRaiser eventRaiser,
        ILogger? logger = default,
        IEqualityComparer<T>? comparer = default) :
        base(defaultValue, propertyName, identifier, eventRaiser, logger, comparer) =>
        LocalStorage = localStorage;

    protected internal virtual ILocalStorageService LocalStorage { get; }

    public virtual async ValueTask<HomeBallsAppSettingsValueProperty<T>> EnsureLoadedAsync(
        CancellationToken cancellationToken = default)
    {
        if (!await LocalStorage.ContainKeyAsync(Identifier, cancellationToken))
            await SaveAsync(cancellationToken);

        ValueSilent = await LocalStorage.GetItemAsync<T>(Identifier, cancellationToken);
        return this;
    }

    protected override void OnValueChanged(
        Object? sender,
        PropertyChangedEventArgs<T> e)
    {
        base.OnValueChanged(sender, e);
        SaveAsync().Start();
    }

    public virtual Task SaveAsync(CancellationToken cancellationToken = default)
    {
        var valueTask = typeof(T) == typeof(String) ?
            LocalStorage.SetItemAsync<String>(Identifier, (String)(Object)ValueSilent!) :
            LocalStorage.SetItemAsync(Identifier, ValueSilent);

        return valueTask.AsTask();
    }

    async ValueTask<IHomeBallsAppSettingsValueProperty<T>> IAsyncLoadable<IHomeBallsAppSettingsValueProperty<T>>
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);

    async ValueTask IAsyncLoadable
        .EnsureLoadedAsync(CancellationToken cancellationToken) =>
        await EnsureLoadedAsync(cancellationToken);
}