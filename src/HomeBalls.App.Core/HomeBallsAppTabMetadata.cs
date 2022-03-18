namespace CEo.Pokemon.HomeBalls.App;

public interface IHomeBallsAppTabMetadata : IHomeBallsAppTab { }

public record HomeBallsAppTabMetadata : IHomeBallsAppTabMetadata
{
    public HomeBallsAppTabMetadata(
        IReadOnlyCollection<IHomeBallsString> names,
        IEventRaiser eventRaiser,
        ILogger? logger)
    {
        (Logger, EventRaiser) = (logger, eventRaiser);

        Names = names;
        IsSelected = new MutableNotifyingProperty<Boolean>(
            false,
            nameof(IsSelected),
            EventRaiser, Logger);
    }

    public IMutableNotifyingProperty<Boolean> IsSelected { get; }

    public IReadOnlyCollection<IHomeBallsString> Names { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    IEnumerable<IHomeBallsString> INamed.Names => Names;
}

public interface IHomeBallsAppAbout : IHomeBallsAppTab { }

public interface IHomeBallsTrade : IHomeBallsAppTab { }

public interface IHomeBallsEdit : IHomeBallsAppTab { }

public abstract class HomeBallsAppTab : IHomeBallsAppTab
{
    protected HomeBallsAppTab(ILogger? logger = default)
    {
        Logger = logger;
        EventRaiser = new EventRaiser(Logger).RaisedBy(this);

        Names = CreateNames();
        IsSelected = new MutableNotifyingProperty<Boolean>(
            false,
            nameof(IsSelected),
            EventRaiser, Logger);
    }

    public IMutableNotifyingProperty<Boolean> IsSelected { get; }

    public IReadOnlyCollection<IHomeBallsString> Names { get; }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    IEnumerable<IHomeBallsString> INamed.Names => Names;

    protected internal abstract IReadOnlyCollection<IHomeBallsString> CreateNames();
}

public class HomeBallsAppAbout : HomeBallsAppTab, IHomeBallsAppAbout
{
    public HomeBallsAppAbout(ILogger? logger = default) : base(logger) { }

    protected internal override IReadOnlyCollection<IHomeBallsString> CreateNames() =>
        new List<IHomeBallsString>
        {
            new HomeBallsString
            {
                LanguageId = EnglishLanguageId,
                Value = AboutTabEnglishName
            }
        }.AsReadOnly();
}

public class HomeBallsTrade : HomeBallsAppTab, IHomeBallsTrade
{
    public HomeBallsTrade(ILogger? logger = default) : base(logger) { }

    protected internal override IReadOnlyCollection<IHomeBallsString> CreateNames() =>
        new List<IHomeBallsString>
        {
            new HomeBallsString
            {
                LanguageId = EnglishLanguageId,
                Value = TradeTabEnglishName
            }
        }.AsReadOnly();
}

public class HomeBallsEdit : HomeBallsAppTab, IHomeBallsEdit
{
    public HomeBallsEdit(ILogger? logger = default) : base(logger) { }

    protected internal override IReadOnlyCollection<IHomeBallsString> CreateNames() =>
        new List<IHomeBallsString>
        {
            new HomeBallsString
            {
                LanguageId = EnglishLanguageId,
                Value = EditTabEnglishName
            }
        }.AsReadOnly();
}