namespace CEo.Pokemon.HomeBalls.App.Settings;

public interface IHomeBallsAppSettingsProperty :
    IAsyncLoadable,
    INotifyDataLoading
{
    Task SaveAsync(CancellationToken cancellationToken = default);
}
