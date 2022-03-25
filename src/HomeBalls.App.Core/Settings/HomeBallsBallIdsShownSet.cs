namespace CEo.Pokemon.HomeBalls.App.Settings;

public class HomeBallsBallIdsShownSet :
    IHomeBallsObservableCollection<UInt16>
{
    public HomeBallsBallIdsShownSet(
        IEventRaiser? eventRaiser = default,
        ILogger? logger = default)
    {
        ElementType = typeof(UInt16);
        IsIdToggled = new Dictionary<UInt16, Boolean> { };

        Logger = logger;
        EventRaiser = eventRaiser ??= new EventRaiser(Logger).RaisedBy(this);
    }

    public Type ElementType { get; }

    public Int32 Count { get; protected internal set; }

    public virtual Boolean IsReadOnly => false;

    protected internal IDictionary<UInt16, Boolean> IsIdToggled { get; }

    protected internal virtual IEnumerable<UInt16> ToggledIds =>
        IsIdToggled.Where(pair => pair.Value).Select(pair => pair.Key);

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    public virtual HomeBallsBallIdsShownSet Add(UInt16 item)
    {
        if (IsIdToggled.ContainsKey(item))
        {
            var previousValue = IsIdToggled[item];
            IsIdToggled[item] = true;
            if (!previousValue) ToggleAdded(item);
        }

        else
        {
            IsIdToggled.Add(item, true);
            ToggleAdded(item);
        }

        return this;
    }

    public virtual HomeBallsBallIdsShownSet Clear()
    {
        foreach (var key in IsIdToggled.Keys) IsIdToggled[key] = false;
        ToggleCleared();
        return this;
    }

    public virtual Boolean Contains(UInt16 item) =>
        IsIdToggled.TryGetValue(item, out var isInLookup) && isInLookup;

    public virtual HomeBallsBallIdsShownSet CopyTo(UInt16[] array, Int32 arrayIndex)
    {
        ToggledIds.ToList().CopyTo(array, arrayIndex);
        return this;
    }

    public virtual IEnumerator<UInt16> GetEnumerator() => ToggledIds.GetEnumerator();

    public virtual Boolean Remove(UInt16 item)
    {
        if (!(IsIdToggled.ContainsKey(item) || IsIdToggled[item])) return false;

        IsIdToggled[item] = false;
        ToggleRemoved(item);
        return true;
    }

    protected internal virtual NotifyCollectionChangedEventArgs CreateAddEventArgs(UInt16 id) =>
        new(NotifyCollectionChangedAction.Add, changedItem: id);

    protected internal virtual NotifyCollectionChangedEventArgs CreateClearEventArgs() =>
        new(NotifyCollectionChangedAction.Reset);

    protected internal virtual NotifyCollectionChangedEventArgs CreateRemovedEventArgs(UInt16 id) =>
        new(NotifyCollectionChangedAction.Remove, changedItem: id);

    protected internal virtual void ToggleAdded(UInt16 id)
    {
        Count += 1;
        ToggleChanged(CreateAddEventArgs(id));
    }

    protected internal virtual void ToggleChanged(NotifyCollectionChangedEventArgs e) =>
        EventRaiser.Raise(CollectionChanged, e);

    protected internal virtual void ToggleCleared()
    {
        Count = 0;
        ToggleChanged(CreateClearEventArgs());
    }

    protected internal virtual void ToggleRemoved(UInt16 id)
    {
        Count -= 1;
        ToggleChanged(CreateRemovedEventArgs(id));
    }

    IHomeBallsObservableCollection<UInt16> IHomeBallsObservableCollection<UInt16>.Add(UInt16 item) => Add(item);

    void ICollection<UInt16>.Add(UInt16 item) => Add(item);

    IHomeBallsObservableCollection<UInt16> IHomeBallsObservableCollection<UInt16>.Clear() => Clear();

    void ICollection<UInt16>.Clear() => Clear();

    IHomeBallsObservableCollection<UInt16> IHomeBallsObservableCollection<UInt16>.CopyTo(UInt16[] array, Int32 arrayIndex) => CopyTo(array, arrayIndex);

    void ICollection<UInt16>.CopyTo(UInt16[] array, Int32 arrayIndex) => CopyTo(array, arrayIndex);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}