namespace CEo.Pokemon.HomeBalls.App.Core;

public interface IHomeBallsEntryCell :
    IHomeBallsNotifyPropertyChanged,
    ICloneable
{
    UInt16 BallId { get; }

    HomeBallsEntryObtainedStatus ObtainedStatus { get; set; }

    HomeBallsEntryLegalityStatus LegalityStatus { get; set; }

    new IHomeBallsEntryCell Clone();
}

public enum HomeBallsEntryObtainedStatus
{
    NotObtained = 0,

    ObtainedWithoutHiddenAbility = 1,

    ObtainedWithHiddenAbility = 2
}

public enum HomeBallsEntryLegalityStatus
{
    NotObtainable = 0,

    ObtainableWithoutHiddenAbility = 1,

    ObtainableWithHiddenAbility = 2
}

public class HomeBallsEntryCell :
    IHomeBallsEntryCell
{
    HomeBallsEntryObtainedStatus _obtainedStatus;
    HomeBallsEntryLegalityStatus _legalityStatus;

    public HomeBallsEntryCell()
    {
        EventRaiser = new EventRaiser().RaisedBy(this);
    }

    protected internal IEventRaiser EventRaiser { get; }

    public UInt16 BallId { get; init; }

    public virtual HomeBallsEntryObtainedStatus ObtainedStatus
    {
        get => _obtainedStatus;
        set
        {
            var (oldValue, newValue) = (_obtainedStatus, value);
            _obtainedStatus = value;
            EventRaiser.Raise(PropertyChanged, oldValue, newValue);
        }
    }

    public virtual HomeBallsEntryLegalityStatus LegalityStatus
    {
        get => _legalityStatus;
        set
        {
            var (oldValue, newValue) = (_legalityStatus, value);
            _legalityStatus = value;
            EventRaiser.Raise(PropertyChanged, oldValue, newValue);
        }
    }

    public event EventHandler<HomeBallsPropertyChangedEventArgs>? PropertyChanged;

    public virtual HomeBallsEntryCell Clone() => new HomeBallsEntryCell
    {
        BallId = BallId,
        ObtainedStatus = ObtainedStatus,
        LegalityStatus = LegalityStatus
    };

    IHomeBallsEntryCell IHomeBallsEntryCell.Clone() => Clone();

    Object ICloneable.Clone() => Clone();
}