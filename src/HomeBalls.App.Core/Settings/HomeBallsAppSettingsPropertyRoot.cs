namespace CEo.Pokemon.HomeBalls.App.Settings;

public abstract class HomeBallsAppSettingsPropertyRoot :
    HomeBallsAppSettingsProperty
{
    IReadOnlyCollection<IHomeBallsAppSettingsProperty>? _subproperties;

    protected HomeBallsAppSettingsPropertyRoot(
        String propertyName,
        String identifier,
        ILocalStorageService localStorage,
        IJSRuntime jsRuntime,
        IEventRaiser eventRaiser,
        ILogger? logger = default) :
        base(propertyName, identifier, localStorage, jsRuntime, eventRaiser, logger) { }

    protected internal IReadOnlyCollection<IHomeBallsAppSettingsProperty> Subproperties =>
        _subproperties ??= CreateSubpropertyCollection();

    protected internal virtual IHomeBallsAppSettingsCollectionProperty<TValue> CreateCollectionProperty<TValue>(
        IHomeBallsObservableCollection<TValue> collection,
        String propertyName) =>
        new HomeBallsAppSettingsCollectionProperty<TValue>(
            collection,
            propertyName, CreateSubpropertyIdentifier(propertyName),
            LocalStorage, JSRuntime, EventRaiser, Logger);

    protected internal abstract IReadOnlyCollection<IHomeBallsAppSettingsProperty> CreateSubpropertyCollection();

    protected internal virtual String CreateSubpropertyIdentifier(String name) =>
        String.Join('.', new[] { Identifier, name });

    protected internal virtual IHomeBallsAppSettingsValueProperty<TValue> CreateValueProperty<TValue>(
        TValue defaultValue,
        String propertyName) =>
        new HomeBallsAppSettingsValueProperty<TValue>(
            new MutableNotifyingProperty<TValue>(defaultValue, propertyName, EventRaiser, Logger),
            propertyName, CreateSubpropertyIdentifier(propertyName),
            LocalStorage, JSRuntime, EventRaiser, Logger);

    protected internal override Task EnsureLoadedCoreAsync(CancellationToken cancellationToken = default) =>
        Task.WhenAll(Subproperties.Select(property => property.EnsureLoadedAsync(cancellationToken).AsTask()));
}
