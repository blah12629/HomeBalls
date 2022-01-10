namespace CEo.Pokemon.HomeBalls.App.Core;

public interface IHomeBallsEntryCell :
    IHomeBallsNotifyPropertyChanged,
    ICloneable
{
    UInt16 BallId { get; }

    HomeBallsEntryObtainedStatus ObtainedStatus { get; set; }

    new IHomeBallsEntryCell Clone();
}

public class HomeBallsEntryCell :
    IHomeBallsEntryCell
{
    HomeBallsEntryObtainedStatus _obtainedStatus;

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

    public event EventHandler<HomeBallsPropertyChangedEventArgs>? PropertyChanged;

    public virtual HomeBallsEntryCell Clone() => new HomeBallsEntryCell
    {
        BallId = BallId,
        ObtainedStatus = ObtainedStatus
    };

    IHomeBallsEntryCell IHomeBallsEntryCell.Clone() => Clone();

    Object ICloneable.Clone() => Clone();
}