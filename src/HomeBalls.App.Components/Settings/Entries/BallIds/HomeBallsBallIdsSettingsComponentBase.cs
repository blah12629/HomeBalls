namespace CEo.Pokemon.HomeBalls.App.Components.Settings.Entries.BallIds;

public abstract class HomeBallsBallIdsSettingsComponentBase :
    HomeBallsAppSettingsItemComponentBase<IHomeBallsAppEntriesBallIdsSettings>
{
    IHomeBallsLoadableDataSource? _data;
    IHomeBallsItemIdComparer? _itemIdComparer;
    IHomeBallsBreedablesSpriteService? _sprites;

    [Inject]
    protected internal IHomeBallsLoadableDataSource Data
    {
        get => _data ?? throw new NullReferenceException();
        init => _data = value;
    }

    [Inject]
    protected internal IHomeBallsItemIdComparer ItemIdComparer
    {
        get => _itemIdComparer ?? throw new NullReferenceException();
        init => _itemIdComparer = value;
    }

    [Inject]
    protected internal IHomeBallsBreedablesSpriteService Sprites
    {
        get => _sprites ?? throw new NullReferenceException();
        init => _sprites = value;
    }

    protected internal virtual void RerenderOnUsingDefaultChanged(
        Object? sender,
        PropertyChangedEventArgs<Boolean> e) =>
        StateHasChanged();
}