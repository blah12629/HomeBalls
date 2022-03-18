using CEo.Pokemon.HomeBalls.App.Entries;

namespace CEo.Pokemon.HomeBalls.App.Components.Entries;

public abstract class HomeBallsEntriesComponentBase :
    HomeBallsLoggingComponent
{
    IHomeBallsAppSettings? _settings;
    IHomeBallsEntryTable? _table;
    IHomeBallsLoadableDataSource? _data;

    [Inject]
    protected internal IHomeBallsAppSettings Settings
    {
        get => _settings ?? throw new NullReferenceException();
        init => _settings = value;
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
}