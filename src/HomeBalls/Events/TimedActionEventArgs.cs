namespace CEo.Pokemon.HomeBalls;

public abstract record TimedActionEventArgs : RecordEventArgs
{
    protected TimedActionEventArgs(
        DateTime? startTime = default(DateTime?)) =>
        StartTime = startTime ?? DateTime.Now;

    public virtual DateTime StartTime { get; }
}


public record TimedActionStartingEventArgs : TimedActionEventArgs
{
    public TimedActionStartingEventArgs(
        DateTime? startTime = default(DateTime?)) :
        base(startTime) { }
}

public record TimedActionEndedEventArgs : TimedActionEventArgs
{
    public TimedActionEndedEventArgs(
        DateTime startTime,
        DateTime? endTime = default(DateTime?)) :
        this(startTime, endTime ?? DateTime.Now) { }

    public TimedActionEndedEventArgs(
        DateTime startTime,
        TimeSpan elapsedTime) :
        this(startTime, startTime + elapsedTime, elapsedTime) { }

    private TimedActionEndedEventArgs(
        DateTime startTime,
        DateTime endTime) :
        this(startTime, endTime, endTime - startTime) { }

    protected TimedActionEndedEventArgs(
        DateTime startTime,
        DateTime endTime,
        TimeSpan elapsedTime) :
        base(startTime) =>
        (EndTime, ElapsedTime) = (endTime, elapsedTime);

    public virtual DateTime EndTime { get; }

    public virtual TimeSpan ElapsedTime { get; }
}