namespace CEo.Pokemon.HomeBalls;

public interface IProperty
{
    String PropertyName { get; }
}

public interface IProperty<out T> : IProperty, IIdentifiable
{
    T Value { get; }
}

public interface INotifyingProperty<T> : IProperty<T>
{
    new T Value { get; }

    event EventHandler<PropertyChangedEventArgs<T>>? ValueChanged;
}

public interface IMutableNotifyingProperty<T> : INotifyingProperty<T>
{
    new T Value { get; set; }
}

public class MutableNotifyingProperty<T> :
    IMutableNotifyingProperty<T>
{
    T _value;

    public MutableNotifyingProperty(
        T defaultValue,
        String propertyName,
        IEventRaiser eventRaiser,
        ILogger? logger = default,
        IEqualityComparer<T>? comparer = default) :
        this(defaultValue, propertyName, propertyName, eventRaiser, logger, comparer) { }

    public MutableNotifyingProperty(
        T defaultValue,
        String propertyName,
        String identifier,
        IEventRaiser eventRaiser,
        ILogger? logger = default,
        IEqualityComparer<T>? comparer = default)
    {
        (_value, PropertyName, Identifier) = (defaultValue, propertyName, identifier);
        (EventRaiser, Logger, Comparer) = (eventRaiser, logger, comparer);
        ValueChanged += OnValueChanged;
    }

    public virtual T Value
    {
        get => _value;
        set => EventRaiser.SetField(ref _value, value, ValueChanged, PropertyName, Comparer);
    }

    protected internal virtual T ValueSilent { get => _value; set => _value = value; }

    public String PropertyName { get; }

    protected internal String Identifier { get; }

    protected internal IEqualityComparer<T>? Comparer { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    String IIdentifiable.Identifier => Identifier;

    public event EventHandler<PropertyChangedEventArgs<T>>? ValueChanged;

    protected internal virtual void OnValueChanged(
        Object? sender,
        PropertyChangedEventArgs<T> e) { }
}