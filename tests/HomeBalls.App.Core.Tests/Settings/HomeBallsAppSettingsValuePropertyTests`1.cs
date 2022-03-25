namespace CEo.Pokemon.HomeBalls.App.Settings.Tests;

public class HomeBallsAppSettingsValuePropertyTests :
    HomeBallsAppSettingsValuePropertyTests<Boolean>
{
    public HomeBallsAppSettingsValuePropertyTests(
        ITestOutputHelper output) :
        base(output) { }
}

public abstract class HomeBallsAppSettingsValuePropertyTests<TValue> :
    HomeBallsAppSettingsPropertyTests
{
    protected HomeBallsAppSettingsValuePropertyTests(
        ITestOutputHelper output) :
        base(output)
    {
        Property = Substitute.For<IMutableNotifyingProperty<TValue>>();
        Sut = new HomeBallsAppSettingsValueProperty<TValue>(
            Property,
            nameof(Sut), nameof(Sut),
            LocalStorage, JSRuntime, new EventRaiser());
    }

    protected IMutableNotifyingProperty<TValue> Property { get; }

    new protected HomeBallsAppSettingsValueProperty<TValue> Sut
    {
        get => (HomeBallsAppSettingsValueProperty<TValue>)base.Sut;
        set => base.Sut = value;
    }
}