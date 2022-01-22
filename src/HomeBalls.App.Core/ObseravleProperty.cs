namespace CEo.Pokemon.HomeBalls.App.Core;

public interface IObservableProperty :
    IIdentifiable
{
    event EventHandler<HomeBallsPropertyChangedEventArgs>? PropertyChanged;
}

public interface IObservableProperty<T> :
    IObservableProperty
{
    T Value { get; set; }
}

public class ObservableProperty<T> : IObservableProperty<T>
{
    T _value;

    public ObservableProperty(
        T defaultValue,
        String propertyName,
        IEventRaiser eventRaiser,
        ILogger? logger = default) :
        this(defaultValue, propertyName, propertyName.ToCamelCase(), eventRaiser, logger) { }

    public ObservableProperty(
        T defaultValue,
        String propertyName,
        String identifier,
        IEventRaiser eventRaiser,
        ILogger? logger = default)
    {
        (_value, PropertyName, Identifier) = (defaultValue, propertyName, identifier);
        (EventRaiser, Logger) = (eventRaiser, logger);
    }

    public T Value
    {
        get => _value;
        set => EventRaiser.SetField(ref _value, value, PropertyChanged, PropertyName);
    }

    public String Identifier { get; }

    protected internal String PropertyName { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    public event EventHandler<HomeBallsPropertyChangedEventArgs>? PropertyChanged;
}