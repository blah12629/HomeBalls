namespace CEo.Pokemon.HomeBalls.App.Core;

public interface IObservableProperty<T>
{
    T Value { get; set; }

    event EventHandler<HomeBallsPropertyChangedEventArgs>? PropertyChanged;
}

public class ObservableProperty<T> : IObservableProperty<T>
{
    T _value;

    public ObservableProperty(
        T defaultValue,
        IEventRaiser eventRaiser,
        ILogger? logger) :
        this(defaultValue, default, eventRaiser, logger) { }

    public ObservableProperty(
        T defaultValue,
        String? propertyName,
        IEventRaiser eventRaiser,
        ILogger? logger)
    {
        (_value, PropertyName) = (defaultValue, propertyName);
        (EventRaiser, Logger) = (eventRaiser, logger);
    }

    public T Value
    {
        get => _value;
        set => EventRaiser.SetField(ref _value, value, PropertyChanged, PropertyName);
    }

    protected internal String? PropertyName { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    public event EventHandler<HomeBallsPropertyChangedEventArgs>? PropertyChanged;
}