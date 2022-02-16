namespace CEo.Pokemon.HomeBalls;

public interface IEventRaiser
{
    PropertyChangedEventArgs<T> SetField<T>(
        ref T field,
        T value,
        EventHandler<PropertyChangedEventArgs<T>>? eventHandler,
        [CallerMemberName] String? propertyName = default,
        IEqualityComparer<T>? comparer = default);

    IEventRaiser RaisedBy(Object sender);

    TEventArgs Raise<TEventArgs>(
        EventHandler<TEventArgs>? handler,
        TEventArgs eventArgs)
        where TEventArgs : notnull;

    NotifyCollectionChangedEventArgs Raise(
        NotifyCollectionChangedEventHandler? handler,
        NotifyCollectionChangedEventArgs eventArgs);

    PropertyChangedEventArgs<T> Raise<T>(
        EventHandler<PropertyChangedEventArgs<T>>? handler,
        T oldValue,
        T newValue,
        [CallerMemberName] String? propertyName = default,
        IEqualityComparer<T>? comparer = default);

    TimedActionStartingEventArgs Raise(
        EventHandler<TimedActionStartingEventArgs>? handler,
        String? propertyName = default);

    TimedActionEndedEventArgs Raise(
        EventHandler<TimedActionEndedEventArgs>? handler,
        DateTime startTime,
        String? propertyName = default);
}

public class EventRaiser : IEventRaiser
{
    public EventRaiser(
        ILogger? logger = default)
    {
        Logger = logger;
    }

    protected internal ILogger? Logger { get; }

    protected internal Object? Sender { get; set; }

    public virtual TEventArgs Raise<TEventArgs>(
        EventHandler<TEventArgs>? handler,
        TEventArgs eventArgs)
        where TEventArgs : notnull
    {
        handler?.Invoke(Sender, eventArgs);
        return eventArgs;
    }

    public virtual NotifyCollectionChangedEventArgs Raise(
        NotifyCollectionChangedEventHandler? handler,
        NotifyCollectionChangedEventArgs eventArgs)
    {
        handler?.Invoke(Sender, eventArgs);
        return eventArgs;
    }

    public virtual PropertyChangedEventArgs<T> Raise<T>(
        EventHandler<PropertyChangedEventArgs<T>>? handler,
        T oldValue,
        T newValue,
        [CallerMemberName] String? propertyName = default,
        IEqualityComparer<T>? comparer = default)
    {
        var args = new PropertyChangedEventArgs<T>(oldValue, newValue, propertyName);
        if (comparer != default && comparer.Equals(oldValue, newValue)) return args;
        if (oldValue?.Equals(newValue) ?? newValue?.Equals(default) ?? true) return args;

        return Raise(handler, args);
    }

    public virtual TimedActionStartingEventArgs Raise(
        EventHandler<TimedActionStartingEventArgs>? handler,
        String? propertyName = default) =>
        Raise(handler, new TimedActionStartingEventArgs(DateTime.Now, propertyName));

    public virtual TimedActionEndedEventArgs Raise(
        EventHandler<TimedActionEndedEventArgs>? handler,
        DateTime startTime,
        String? propertyName = default) =>
        Raise(handler, new TimedActionEndedEventArgs(startTime, DateTime.Now, propertyName));

    public virtual EventRaiser RaisedBy(Object sender)
    {
        Sender = sender;
        return this;
    }

    public virtual PropertyChangedEventArgs<T> SetField<T>(
        ref T field,
        T value,
        EventHandler<PropertyChangedEventArgs<T>>? eventHandler,
        [CallerMemberName] String? propertyName = default,
        IEqualityComparer<T>? comparer = default)
    {
        var (oldValue, newValue) = (field, value);
        field = newValue;
        return Raise(eventHandler, oldValue, newValue, propertyName, comparer);
    }

    IEventRaiser IEventRaiser.RaisedBy(Object sender) => RaisedBy(sender);
}