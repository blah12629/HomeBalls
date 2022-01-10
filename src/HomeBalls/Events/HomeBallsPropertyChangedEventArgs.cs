namespace CEo.Pokemon.HomeBalls;

public record HomeBallsPropertyChangedEventArgs : RecordEventArgs
{
    public HomeBallsPropertyChangedEventArgs(
        Object? newValue,
        [CallerMemberName] String? propertyName = default) :
        this(default, newValue, propertyName) { }

    public HomeBallsPropertyChangedEventArgs(
        Object? oldValue,
        Object? newValue,
        [CallerMemberName] String? propertyName = default)
    {
        (OldValue, NewValue) = (oldValue, newValue);
        PropertyName = propertyName ?? throw new ArgumentException();
    }

    public Object? OldValue { get; }

    public Object? NewValue { get; }

    public String PropertyName { get; }
}