namespace CEo.Pokemon.HomeBalls.App.Entries;

public interface IHomeBallsEntryBodyCell :
    IHomeBallsEntryCell,
    IKeyed<HomeBallsEntryKey>
{
    new HomeBallsEntryKey Id { get; }

    IMutableNotifyingProperty<Boolean> IsLegal { get; }

    IMutableNotifyingProperty<Boolean> IsLegalWithHiddenAbility { get; }

    IMutableNotifyingProperty<Boolean> IsObtained { get; }

    IMutableNotifyingProperty<Boolean> IsObtainedWithHiddenAbility { get; }
}

public class HomeBallsEntryBodyCell :
    HomeBallsEntryCell,
    IHomeBallsEntryBodyCell
{
    public HomeBallsEntryBodyCell(
        HomeBallsPokemonFormKey formKey,
        UInt16 ballId,
        IEventRaiser? eventRaiser = default,
        ILogger? logger = default) :
        this(new(formKey.SpeciesId, formKey.FormId, ballId), eventRaiser, logger) { }

    public HomeBallsEntryBodyCell(
        HomeBallsEntryKey id,
        IEventRaiser? eventRaiser = default,
        ILogger? logger = default) :
        base(id.BallId, logger)
    {
        Id = id;
        EventRaiser = eventRaiser ?? new EventRaiser(Logger).RaisedBy(this);
        IsLegal = new MutableNotifyingProperty<Boolean>(false, nameof(IsLegal), EventRaiser, Logger);
        IsLegalWithHiddenAbility = new MutableNotifyingProperty<Boolean>(false, nameof(IsLegalWithHiddenAbility), EventRaiser, Logger);
        IsObtained = new MutableNotifyingProperty<Boolean>(false, nameof(IsObtained), EventRaiser, Logger);
        IsObtainedWithHiddenAbility = new MutableNotifyingProperty<Boolean>(false, nameof(IsObtainedWithHiddenAbility), EventRaiser, Logger);
    }

    new public HomeBallsEntryKey Id { get; }

    public IMutableNotifyingProperty<Boolean> IsLegal { get; }

    public IMutableNotifyingProperty<Boolean> IsLegalWithHiddenAbility { get; }

    public IMutableNotifyingProperty<Boolean> IsObtained { get; }

    public IMutableNotifyingProperty<Boolean> IsObtainedWithHiddenAbility { get; }

    protected internal IEventRaiser EventRaiser { get; }
}