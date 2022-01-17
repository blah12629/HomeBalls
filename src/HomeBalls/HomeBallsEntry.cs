namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsEntry :
    IHomeBallsEntity,
    IKeyed<HomeBallsEntryKey>,
    IHomeBallsNotifyPropertyChanged,
    IEquatable<IHomeBallsEntry>
{
    UInt16 SpeciesId { get; }

    Byte FormId { get; }

    UInt16 BallId { get; }

    Boolean HasHiddenAbility { get; set; }

    ICollection<UInt16> AvailableEggMoveIds { get; }

    DateTime AddedOn { get; }

    DateTime LastUpdatedOn { get; }
}

public record HomeBallsEntry :
    HomeBallsRecord,
    IHomeBallsEntry
{
    Boolean _hasHiddenAbility;
    HomeBallsEntryKey? _id;
    DateTime _addedOn;

    public HomeBallsEntry()
    {
        EventRaiser = new EventRaiser().RaisedBy(this);

        AddedOn = DateTime.Now;
        PropertyChanged += OnPropertyChanged;

        var availableEggMoveIds = new HomeBallsObservableSet<UInt16> { };
        availableEggMoveIds.CollectionChanged += OnAvailableEggMoveIdsChanged;
        AvailableEggMoveIds = availableEggMoveIds;
    }

    protected internal IEventRaiser EventRaiser { get; }

    public UInt16 SpeciesId { get; init; }

    public Byte FormId { get; init; }

    public UInt16 BallId { get; init; }

    public virtual Boolean HasHiddenAbility
    {
        get => _hasHiddenAbility;
        set
        {
            var (oldValue, newValue) = (_hasHiddenAbility, value);
            _hasHiddenAbility = value;
            EventRaiser.Raise(PropertyChanged, oldValue, newValue);
        }
    }

    public virtual ICollection<UInt16> AvailableEggMoveIds { get; }

    public virtual DateTime AddedOn
    {
        get => _addedOn;
        init => (_addedOn, LastUpdatedOn) = (value, value);
    }

    public virtual DateTime LastUpdatedOn { get; set; }

    public virtual HomeBallsEntryKey Id => _id ??= new(SpeciesId, FormId, BallId);

    public event EventHandler<HomeBallsPropertyChangedEventArgs>? PropertyChanged;

    public virtual Boolean Equals(IHomeBallsEntry? other) =>
        SpeciesId == other?.SpeciesId &&
        FormId == other?.FormId &&
        BallId == other?.BallId;

    protected internal virtual void OnPropertyChanged(
        Object? sender,
        HomeBallsPropertyChangedEventArgs e) =>
        LastUpdatedOn = DateTime.Now;

    protected internal virtual void OnAvailableEggMoveIdsChanged(
        Object? sender,
        NotifyCollectionChangedEventArgs e)
    {
        var name = nameof(AvailableEggMoveIds);

        if (e.Action == NotifyCollectionChangedAction.Move) return;
        if (e.Action == NotifyCollectionChangedAction.Reset)
            EventRaiser.Raise(PropertyChanged, default, default, name);

        EventRaiser.Raise(
            PropertyChanged,
            e.OldItems?.Cast<Object?>().FirstOrDefault(),
            e.NewItems?.Cast<Object?>().FirstOrDefault(),
            name);
    }
}