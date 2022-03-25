namespace CEo.Pokemon.HomeBalls.App.Components.Settings.Entries.IllegalEntries;

public abstract class HomeBallsIllegalEntriesSettingsComponentBase :
    HomeBallsAppSettingsItemComponentBase<IHomeBallsAppEntriesSettings>
{
    new protected internal IMutableNotifyingProperty<Boolean> Settings =>
        base.Settings.IsShowingIllegalEntries;

    protected internal virtual void RerenderOnValueChanged(
        Object? sender,
        PropertyChangedEventArgs<Boolean> e) =>
        StateHasChanged();
}