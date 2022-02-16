namespace CEo.Pokemon.HomeBalls;

public record ValueChangedEventArgs :
    RecordEventArgs
{
    public ValueChangedEventArgs(Object? newValue) : this(default, newValue) { }

    public ValueChangedEventArgs(Object? oldValue, Object? newValue) =>
        (OldValue, NewValue) = (oldValue, newValue);

    public Object? OldValue { get; init; }

    public Object? NewValue { get; init; }
}

public record ValueChangedEventArgs<T> :
    ValueChangedEventArgs
{
    public ValueChangedEventArgs(T newValue) : base(newValue) { }

    public ValueChangedEventArgs(T oldValue, T newValue) : base(oldValue, newValue) { }

    new public T OldValue { get => (T)base.OldValue!; init => base.OldValue = value; }

    new public T NewValue { get => (T)base.NewValue!; init => base.NewValue = value; }
}

public record PropertyChangedEventArgs<T> :
    ValueChangedEventArgs<T>
{
    public PropertyChangedEventArgs(
        T newValue,
        [CallerMemberName] String? propertyName = default) :
        base(newValue) =>
        PropertyName = propertyName;

    public PropertyChangedEventArgs(
        T oldValue,
        T newValue,
        [CallerMemberName] String? propertyName = default) :
        base(oldValue, newValue) =>
        PropertyName = propertyName;

    public String? PropertyName { get; init; }
}