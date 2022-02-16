namespace CEo.Pokemon.HomeBalls.Entities;

#nullable disable

[ProtoContract]
[ProtoInclude(1, typeof(HomeBallsEntry))]
[ProtoInclude(2, typeof(HomeBallsEntryLegality))]
public abstract record HomeBallsEntryEntity :
    HomeBallsEntity,
    IKeyed<HomeBallsEntryKey>
{
    public HomeBallsEntryEntity() => Id = new();

    [ProtoMember(3)]
    public virtual HomeBallsEntryKey Id { get; init; }
}

#nullable enable

[ProtoContract]
public record HomeBallsEntry :
    HomeBallsEntryEntity,
    IHomeBallsEntry
{
    IHomeBallsObservableList<UInt16>? _availableEggMoveIds;
    List<UInt16>? _availableEggMoveIdsCollection;
    DateTime _addedOn;

    public HomeBallsEntry() : this(false) { }

    public HomeBallsEntry(Boolean hasHiddenAbility) :
        this(hasHiddenAbility, new List<UInt16> { }) { }

    public HomeBallsEntry(Boolean hasHiddenAbility, List<UInt16> eggMoveIds) : base()
    {
        EventRaiser = new EventRaiser().RaisedBy(this);
        AddedOn = DateTime.Now;

        HasHiddenAbility = new MutableNotifyingProperty<Boolean>(
            hasHiddenAbility,
            nameof(HasHiddenAbility),
            $"{GetType().GetFullNameNonNull()}.{nameof(HasHiddenAbility)}",
            EventRaiser);
        AvailableEggMoveIdsCollection = eggMoveIds;

        HasHiddenAbility.ValueChanged += OnHasHiddenAbilityChanged;
        AvailableEggMoveIds.CollectionChanged += OnAvailableEggMoveIdsChanged;
    }

    public virtual IMutableNotifyingProperty<Boolean> HasHiddenAbility { get; }

    public virtual IHomeBallsObservableList<UInt16> AvailableEggMoveIds
    {
        get => _availableEggMoveIds!;
        private set => _availableEggMoveIds = value;
    }

    [ProtoMember(3)]
    public virtual DateTime AddedOn
    {
        get => _addedOn;
        init => (_addedOn, LastUpdatedOn) = (value, value);
    }

    [ProtoMember(4)]
    public virtual DateTime LastUpdatedOn
    {
        get => LastUpdatedOnProtected;
        init => LastUpdatedOnProtected = value;
    }

    [ProtoMember(1)]
    protected internal virtual Boolean HasHiddenAbilityValue
    {
        get => HasHiddenAbility.Value;
        init => HasHiddenAbility.Value = value;
    }

    [ProtoMember(2)]
    protected internal virtual List<UInt16> AvailableEggMoveIdsCollection
    {
        get => _availableEggMoveIdsCollection!;
        set
        {
            _availableEggMoveIdsCollection = value;
            AvailableEggMoveIds = new HomeBallsObservableSet<UInt16>(
                AvailableEggMoveIdsCollection);
        }
    }

    protected internal virtual DateTime LastUpdatedOnProtected { get; set; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal virtual void OnHasHiddenAbilityChanged(
        Object? sender,
        PropertyChangedEventArgs<Boolean> e) =>
        OnValueChanged(sender, e);

    protected internal virtual void OnAvailableEggMoveIdsChanged(
        Object? sender,
        NotifyCollectionChangedEventArgs e) =>
        OnValueChanged(sender, e);

    protected internal virtual void OnValueChanged<TEventArgs>(
        Object? sender,
        TEventArgs evenrArgs) =>
        LastUpdatedOnProtected = DateTime.Now;
}

[ProtoContract]
public record HomeBallsEntryLegality :
    HomeBallsEntryEntity,
    IHomeBallsEntryLegality
{
    public HomeBallsEntryLegality() : base() { }

    [ProtoMember(1)]
    public virtual Boolean IsObtainable { get; init; }

    [ProtoMember(2)]
    public virtual Boolean IsObtainableWithHiddenAbility { get; init; }

    String IIdentifiable.Identifier => $"{Id.SpeciesId}-{Id.FormId}-{Id.BallId}";
}