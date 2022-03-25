using CEo.Pokemon.HomeBalls.App.Entries;

namespace CEo.Pokemon.HomeBalls.App.Components.Entries;

public abstract class HomeBallsEntriesRowComponentBase<TRow, TCell> :
    HomeBallsEntriesComponentBase
    where TRow : notnull, IHomeBallsEntryRow<TCell>
    where TCell : notnull, IHomeBallsEntryCell
{
    protected virtual IHomeBallsAppEntriesRowIdentifierSettings RowIdentifierSettings => EntriesSettings.RowIdentifier;

    protected virtual IHomeBallsAppEntriesBallIdsSettings BallIdsSettings => EntriesSettings.BallIds;

    protected virtual Task OnRowIdentifierInitializedAsync(
        Action rerenderAction,
        INotifyingProperty<Boolean> identifierToggle)
    {
        RowIdentifierSettings.IsUsingDefault.ValueChanged += (sender, e) => rerenderAction();
        identifierToggle.ValueChanged += (sender, e) => rerenderAction();
        return Task.CompletedTask;
    }

    protected abstract Task OnCellRenderedAsync(String containerId, String functionKey);

    protected virtual Task OnBallCellInitializedAsync(Action rerenderAction, TCell cell)
    {
        BallIdsSettings.IsUsingDefault.ValueChanged += (sender, e) => rerenderAction();
        BallIdsSettings.Collection.CollectionChanged += (sender, e) =>
        {
            if (e.IsChanged(cell.Id)) rerenderAction();
        };
        return Task.CompletedTask;
    }

    protected virtual Boolean IsBallCellShown(TCell cell) =>
        (BallIdsSettings.IsUsingDefault.Value ? (IEnumerable<UInt16>)
            BallIdsSettings.DefaultValues :
            BallIdsSettings.Collection)
            .Contains(cell.Id);
}