using CEo.Pokemon.HomeBalls.App.Entries;

namespace CEo.Pokemon.HomeBalls.App.Components.Entries;

public abstract class HomeBallsEntriesRowComponentBase<TRow, TCell> :
    HomeBallsEntriesComponentBase
    where TRow : notnull, IHomeBallsEntryRow<TCell>
    where TCell : notnull, IHomeBallsEntryCell
{
    protected abstract String ElementIdPrefix { get; }

    protected virtual String ElementIdLeftPrefix => $"{ElementIdPrefix}-left";

    protected virtual Boolean IsCellRenderedWhen(TCell cell) =>
        Settings.EntryTable.BallIdsShown.IsUsingDefault.Value ?
            Settings.EntryTable.BallIdsShown.DefaultValues.Contains(cell.Id) :
            Settings.EntryTable.BallIdsShown.Collection.Contains(cell.Id);

    protected virtual Task OnCellInitializedAsync(Action rerenderCell, TCell cell)
    {
        Settings.EntryTable.BallIdsShown.IsUsingDefault.ValueChanged +=
            (sender, e) => OnUsingDefaultBallIdsShownChanged(sender, e, rerenderCell);
        Settings.EntryTable.BallIdsShown.Collection.CollectionChanged +=
            (sender, e) => OnBallIdsShownChanged(sender, e, rerenderCell, cell);

        return Task.CompletedTask;
    }

    protected virtual void OnUsingDefaultBallIdsShownChanged(
        Object? sender,
        PropertyChangedEventArgs<Boolean> e,
        Action rerenderCell) =>
        rerenderCell();

    protected virtual void OnBallIdsShownChanged(
        Object? sender,
        NotifyCollectionChangedEventArgs e,
        Action rerenderCell,
        TCell cell)
    {
        if (IsIdInGenericList(e.OldItems, cell) ||
            IsIdInGenericList(e.NewItems, cell))
            rerenderCell();
    }

    protected virtual Boolean IsIdInGenericList(IList? list, TCell cell) =>
        IsInGenericList(list, cell.Id);

    protected virtual Boolean IsInGenericList<T>(IList? list, T value) =>
        list?.Cast<T>().Contains(value) ?? false;

    protected virtual String CreateElementId(Int32 id) => $"{ElementIdPrefix}-{id}";

    protected virtual String CreateLeftElementId(Int32 id) => $"{ElementIdLeftPrefix}-{id}";
}