namespace CEo.Pokemon.HomeBalls;

public abstract record TimedActionEventArgs : RecordEventArgs
{
    protected TimedActionEventArgs(
        DateTime? startTime,
        String? propertyName)
    {
        StartTime = startTime ?? DateTime.Now;
        PropertyName = propertyName;
    }

    public virtual DateTime StartTime { get; init; }

    public virtual String? PropertyName { get; init; }
}


public record TimedActionStartingEventArgs : TimedActionEventArgs
{
    public TimedActionStartingEventArgs(
        DateTime? startTime = default(DateTime?),
        String? propertyName = default(String)) :
        base(startTime, propertyName) { }
}

public record TimedActionEndedEventArgs : TimedActionEventArgs
{
    public TimedActionEndedEventArgs(
        DateTime startTime,
        DateTime? endTime = default(DateTime?),
        String? propertyName = default(String)) :
        this(startTime, endTime ?? DateTime.Now, propertyName) { }

    public TimedActionEndedEventArgs(
        DateTime startTime,
        TimeSpan elapsedTime,
        String? propertyName = default(String)) :
        this(startTime, startTime + elapsedTime, elapsedTime, propertyName) { }

    private TimedActionEndedEventArgs(
        DateTime startTime,
        DateTime endTime,
        String? propertyName = default(String)) :
        this(startTime, endTime, endTime - startTime, propertyName) { }

    protected TimedActionEndedEventArgs(
        DateTime startTime,
        DateTime endTime,
        TimeSpan elapsedTime,
        String? propertyName) :
        base(startTime, propertyName) =>
        (EndTime, ElapsedTime) = (endTime, elapsedTime);

    public virtual DateTime EndTime { get; init; }

    public virtual TimeSpan ElapsedTime { get; init; }
}