namespace CEo.Pokemon.HomeBalls;

public interface IEventRaiser
{
    IEventRaiser RaisedBy(Object sender);

    TEventArgs Raise<TEventArgs>(
        EventHandler<TEventArgs>? handler,
        TEventArgs eventArgs)
        where TEventArgs : notnull;

    NotifyCollectionChangedEventArgs Raise(
        NotifyCollectionChangedEventHandler? handler,
        NotifyCollectionChangedEventArgs eventArgs);

    HomeBallsPropertyChangedEventArgs Raise(
        EventHandler<HomeBallsPropertyChangedEventArgs>? handler,
        Object? oldValue,
        Object? newValue,
        [CallerMemberName] String? propertyName = default,
        IEqualityComparer? comparer = default);

    TimedActionStartingEventArgs Raise(
        EventHandler<TimedActionStartingEventArgs>? handler);

    TimedActionEndedEventArgs Raise(
        EventHandler<TimedActionEndedEventArgs>? handler,
        DateTime startTime);
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

    public virtual HomeBallsPropertyChangedEventArgs Raise(
        EventHandler<HomeBallsPropertyChangedEventArgs>? handler,
        Object? oldValue,
        Object? newValue,
        [CallerMemberName] String? propertyName = default,
        IEqualityComparer? comparer = default)
    {
        var args = new HomeBallsPropertyChangedEventArgs(oldValue, newValue, propertyName);
        if (comparer != default && comparer.Equals(oldValue, newValue)) return args;
        if (oldValue?.Equals(newValue) ?? newValue?.Equals(default) ?? true) return args;

        return Raise(handler, args);
    }

    public virtual TimedActionStartingEventArgs Raise(
        EventHandler<TimedActionStartingEventArgs>? handler) =>
        Raise(handler, new TimedActionStartingEventArgs(DateTime.Now));

    public virtual TimedActionEndedEventArgs Raise(
        EventHandler<TimedActionEndedEventArgs>? handler,
        DateTime startTime) =>
        Raise(handler, new TimedActionEndedEventArgs(startTime, DateTime.Now));

    public virtual EventRaiser RaisedBy(Object sender)
    {
        Sender = sender;
        return this;
    }

    IEventRaiser IEventRaiser.RaisedBy(Object sender) => RaisedBy(sender);
}