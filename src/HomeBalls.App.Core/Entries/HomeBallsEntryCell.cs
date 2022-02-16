namespace CEo.Pokemon.HomeBalls.App.Entries;

public interface IHomeBallsEntryCell :
    IKeyed<HomeBallsEntryKey>,
    IIdentifiable
{
    IMutableNotifyingProperty<Boolean> IsObtained { get; }

    IMutableNotifyingProperty<Boolean> IsObtainedWithHiddenAbility { get; }

    IMutableNotifyingProperty<Boolean> IsLegal { get; }

    IMutableNotifyingProperty<Boolean> IsLegalWithHiddenAbility { get; }
}

public class HomeBallsEntryCell :
    IHomeBallsEntryCell
{
    public HomeBallsEntryCell(
        HomeBallsEntryKey id,
        String identifier,
        ILogger? logger = default)
    {
        (Id, Identifier) = (id, identifier);
        Logger = logger;
        EventRaiser = new EventRaiser(Logger).RaisedBy(this);

        IsObtained = new MutableNotifyingProperty<Boolean>(false, nameof(IsObtained), EventRaiser, Logger);
        IsObtainedWithHiddenAbility = new MutableNotifyingProperty<Boolean>(false, nameof(IsObtainedWithHiddenAbility), EventRaiser, Logger);
        IsLegal = new MutableNotifyingProperty<Boolean>(false, nameof(IsLegal), EventRaiser, Logger);
        IsLegalWithHiddenAbility = new MutableNotifyingProperty<Boolean>(false, nameof(IsLegalWithHiddenAbility), EventRaiser, Logger);
    }

    public HomeBallsEntryKey Id { get; init; }

    public String Identifier { get; init; }

    public IMutableNotifyingProperty<Boolean> IsObtained { get; }

    public IMutableNotifyingProperty<Boolean> IsObtainedWithHiddenAbility { get; }

    public IMutableNotifyingProperty<Boolean> IsLegal { get; }

    public IMutableNotifyingProperty<Boolean> IsLegalWithHiddenAbility { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }
}