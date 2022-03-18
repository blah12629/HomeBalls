using CEo.Pokemon.HomeBalls.App.Entries;

namespace CEo.Pokemon.HomeBalls.App.Components.Entries;

public abstract class HomeBallsEntriesCellComponentBase<TCell> :
    HomeBallsEntriesComponentBase
    where TCell : notnull, IHomeBallsEntryCell
{
    IHomeBallsAppStateContainer? _state;
    TCell? _cell;

    [Inject]
    public IHomeBallsAppStateContainer State
    {
        get => _state ?? throw new NullReferenceException();
        init => _state = value;
    }

    [Parameter, EditorRequired]
    public TCell Cell
    {
        get => _cell ?? throw new NullReferenceException();
        init => _cell = value;
    }

    protected internal virtual IHomeBallsItem Ball =>
        Data.Items.IsLoaded ? Data.Items[Cell.Id] :
        Data.Pokeballs.IsLoaded ? Data.Pokeballs[Cell.Id] :
        throw new NullReferenceException();

    protected internal abstract RenderFragment BallFragment { get; }
}