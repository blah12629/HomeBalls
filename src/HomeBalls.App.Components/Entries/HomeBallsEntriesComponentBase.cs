using CEo.Pokemon.HomeBalls.App.Entries;

namespace CEo.Pokemon.HomeBalls.App.Components.Entries;

public abstract class HomeBallsEntriesComponentBase :
    HomeBallsLoggingComponent
{
    IHomeBallsAppEntriesSettings? _entriesSettings;
    IHomeBallsAppStateContainer? _state;
    IHomeBallsEntryTable? _table;
    IHomeBallsLoadableDataSource? _data;
    IJSRuntime? _jsRuntime;

    [Inject]
    protected internal IHomeBallsAppEntriesSettings EntriesSettings
    {
        get => _entriesSettings ?? throw new NullReferenceException();
        init => _entriesSettings = value;
    }

    [Inject]
    protected internal IHomeBallsAppStateContainer State
    {
        get => _state ?? throw new NullReferenceException();
        init => _state = value;
    }

    [Inject]
    protected internal IHomeBallsEntryTable Table
    {
        get => _table ?? throw new NullReferenceException();
        init => _table = value;
    }

    [Inject]
    protected internal IHomeBallsLoadableDataSource Data
    {
        get => _data ?? throw new NullReferenceException();
        init => _data = value;
    }

    [Inject]
    protected internal IJSRuntime JSRuntime
    {
        get => _jsRuntime ?? throw new NullReferenceException();
        init => _jsRuntime = value;
    }
}