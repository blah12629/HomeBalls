namespace CEo.Pokemon.HomeBalls;

public class HomeBallsObservableSet<T> :
    HomeBallsObservableList<T>
{
    public HomeBallsObservableSet(
        ILogger? logger = default) :
        this((items, index, item) => { }, logger) { }

    public HomeBallsObservableSet(
        Action<IList<T>, Int32, T> addExistingAction,
        ILogger? logger = default) :
        base(logger) =>
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
}

public class HomeBallsEntryCollection :
    HomeBallsObservableSet<IHomeBallsEntry>
{
    static void ReplaceExistingValue(
        IList<IHomeBallsEntry> entries,
        Int32 index,
        IHomeBallsEntry entry)
    {
        var existingEntry = entries[index];
        existingEntry.HasHiddenAbility = entry.HasHiddenAbility;
        foreach (var id in entry.AvailableEggMoveIds)
            existingEntry.AvailableEggMoveIds.Add(id);
    }

    public HomeBallsEntryCollection(
        ILogger? logger = default) :
        base(ReplaceExistingValue, logger) { }
}