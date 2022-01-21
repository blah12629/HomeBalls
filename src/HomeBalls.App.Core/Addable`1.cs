namespace CEo.Pokemon.HomeBalls.App.Core;

public interface IAddable<in T>
{
    UInt32 Count { get; }

    IAddable<T> Add(T item);
}

public class Addable<T> : IAddable<T>
{
    public Addable(
        Action<T> addAction,
        ILogger? logger = default)
    {
        AddAction = addAction;
        Logger = logger;
    }

    public UInt32 Count { get; protected internal set; }

    protected internal Action<T> AddAction { get; }

    protected internal ILogger? Logger { get; }

    public Addable<T> Add(T item)
    {
        AddCore(item);
        Count += 1;
        return this;
    }

    protected internal virtual Addable<T> AddCore(T item)
    {
        AddAction.Invoke(item);
        return this;
    }

    IAddable<T> IAddable<T>.Add(T item) => Add(item);
}
