namespace CEo.Pokemon.HomeBalls.App;

public interface IHomeBallsAppTab : INamed
{
    IMutableNotifyingProperty<Boolean> IsSelected { get; }

    Boolean IsDisabled { get; }
}