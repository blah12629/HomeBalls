namespace CEo.Pokemon.HomeBalls.App.Components.Settings.Entries.RowIdentifier;

public abstract class HomeBallsRowIdentifierSettingsComponentBase :
    HomeBallsAppSettingsItemComponentBase<IHomeBallsAppEntriesRowIdentifierSettings>
{
    protected internal virtual void RerenderOnUsingDefaultChanged(
        Object? sender,
        PropertyChangedEventArgs<Boolean> e) =>
        StateHasChanged();
}