namespace CEo.Pokemon.HomeBalls.App;

public interface IHomeBallsAppTabList : IReadOnlyList<IHomeBallsAppTab>
{
    Boolean IsAnySelected { get; }

    event EventHandler<PropertyChangedEventArgs<Boolean>>? SelectionChanged;

    IHomeBallsAppTabList DeselectAll();

    IHomeBallsAppTabList DeselectWhere(Func<IHomeBallsAppTab, Boolean> predicate);
}

public class HomeBallsAppTabList : IHomeBallsAppTabList
{
    public HomeBallsAppTabList(
        IHomeBallsAppAbout about,
        IHomeBallsTrade trade,
        IHomeBallsEdit edit,
        IHomeBallsAppSettings settings,
        ILogger? logger = default)
    {
        (About, Trade, Edit, Settings) = (about, trade, edit, settings);
        Logger = logger;

        Tabs = new List<IHomeBallsAppTab> { About, Trade, Edit, Settings };
    }

    public virtual IHomeBallsAppTab this[Int32 index] => Tabs[index];

    public virtual Int32 Count => Tabs.Count;

    public virtual Boolean IsAnySelected
    {
        get
        {
            for (var i = 0; i < Count; i ++) if(this[i].IsSelected.Value) return true;
            return false;
        }
    }

    protected internal IHomeBallsAppAbout About { get; }

    protected internal IHomeBallsTrade Trade { get; }

    protected internal IHomeBallsEdit Edit { get; }

    protected internal IHomeBallsAppSettings Settings { get; }

    protected internal ILogger? Logger { get; }

    protected internal IList<IHomeBallsAppTab> Tabs { get; }

    public event EventHandler<PropertyChangedEventArgs<Boolean>>? SelectionChanged
    {
        add
        {
            for (var i = 0; i < Count; i ++) this[i].IsSelected.ValueChanged += value;
        }

        remove
        {
            for (var i = 0; i < Count; i ++) this[i].IsSelected.ValueChanged -= value;
        }
    }

    public virtual HomeBallsAppTabList DeselectAll() => DeselectWhere(tab => true);

    public virtual HomeBallsAppTabList DeselectWhere(
        Func<IHomeBallsAppTab, Boolean> predicate)
    {
        for (var i = 0; i < Count; i ++)
        {
            var tab = Tabs[i];
            if (predicate(tab)) tab.IsSelected.Value = false;
        }

        return this;
    }

    public virtual IEnumerator<IHomeBallsAppTab> GetEnumerator() => Tabs.GetEnumerator();

    IHomeBallsAppTabList IHomeBallsAppTabList.DeselectAll() => DeselectAll();

    IHomeBallsAppTabList IHomeBallsAppTabList
        .DeselectWhere(Func<IHomeBallsAppTab, Boolean> predicate) =>
        DeselectWhere(predicate);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}