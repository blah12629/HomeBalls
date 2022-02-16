namespace CEo.Pokemon.HomeBalls;

public class HomeBallsEntryCollection :
    HomeBallsObservableSet<IHomeBallsEntry>
{
    static void ReplaceExistingValue(
        IList<IHomeBallsEntry> entries,
        Int32 index,
        IHomeBallsEntry entry)
    {
        var existingEntry = entries[index];
        existingEntry.HasHiddenAbility.Value = entry.HasHiddenAbility.Value;
        foreach (var id in entry.AvailableEggMoveIds)
            existingEntry.AvailableEggMoveIds.Add(id);
    }

    public HomeBallsEntryCollection(
        IEqualityComparer<IHomeBallsEntry>? comparer = default,
        ILogger? logger = default) :
        base(ReplaceExistingValue, comparer ?? HomeBallsEntityKeyComparer.Instance, logger) { }
}