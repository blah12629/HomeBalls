namespace CEo.Pokemon.HomeBalls.Collections;

public class HomeBallsObservableSet<T> :
    HomeBallsObservableList<T>
{
    public HomeBallsObservableSet(
        IEqualityComparer<T>? comparer = default,
        ILogger? logger = default) :
        this((items, index, item) => { }, new(), comparer, logger) { }

    public HomeBallsObservableSet(
        Action<IList<T>, Int32, T> addExistingAction,
        IEqualityComparer<T>? comparer = default,
        ILogger? logger = default) :
        this(addExistingAction, new(), comparer, logger) { }

    public HomeBallsObservableSet(
        List<T> items,
        IEqualityComparer<T>? comparer = default,
        ILogger? logger = default) :
        this((items, index, item) => { }, items, comparer, logger) { }

    public HomeBallsObservableSet(
        Action<IList<T>, Int32, T> addExistingAction,
        List<T> items,
        IEqualityComparer<T>? comparer = default,
        ILogger? logger = default) :
        base(items, comparer, logger) =>
        AddExistingAction = addExistingAction;

    protected internal Action<IList<T>, Int32, T> AddExistingAction { get; }

    public override HomeBallsObservableSet<T> Add(T item)
    {
        var index = IndexOf(item);
        if (index == -1) base.Add(item);
        else AddExisting(Items, index, item);
        return this;
    }

    protected internal virtual HomeBallsObservableSet<T> AddExisting(
        IList<T> items,
        Int32 index,
        T newValue)
    {
        AddExistingAction.Invoke(items, index, newValue);
        return this;
    }

    public override HomeBallsObservableSet<T> Clear()
    {
        base.Clear();
        return this;
    }

    public override HomeBallsObservableSet<T> CopyTo(T[] array, Int32 arrayIndex)
    {
        base.CopyTo(array, arrayIndex);
        return this;
    }

    public override HomeBallsObservableSet<T> Insert(Int32 index, T item)
    {
        base.Insert(index, item);
        return this;
    }

    public override HomeBallsObservableList<T> RemoveAt(Int32 index)
    {
        base.RemoveAt(index);
        return this;
    }
}