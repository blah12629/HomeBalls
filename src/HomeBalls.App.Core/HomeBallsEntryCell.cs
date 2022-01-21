namespace CEo.Pokemon.HomeBalls.App.Core;

public interface IHomeBallsEntryCell :
    IKeyed<HomeBallsEntryKey>,
    IIdentifiable,
    IHomeBallsNotifyPropertyChanged
{
    Boolean IsObtained { get; set; }

    Boolean IsObtainedWithHiddenAbility { get; set; }

    Boolean IsLegal { get; set; }

    Boolean IsLegalWithHiddenAbility { get; set; }
}

public class HomeBallsEntryCell :
    IHomeBallsEntryCell
{
    Boolean _isObtained, _isObtainedWithHiddenAbility;
    Boolean _isLegal, _isLegalWithHiddenAbility;

    public HomeBallsEntryCell(
        HomeBallsEntryKey id,
        String identifier,
        ILogger? logger = default)
    {
        (Id, Identifier) = (id, identifier);
        (EventRaiser, Logger) = (new EventRaiser().RaisedBy(this), logger);
    }

    public HomeBallsEntryKey Id { get; init; }

    public String Identifier { get; init; }

    public Boolean IsObtained
    {
        get => _isObtained;
        set => EventRaiser.SetField(ref _isObtained, value, PropertyChanged);
    }

    public Boolean IsObtainedWithHiddenAbility
    {
        get => _isObtainedWithHiddenAbility;
        set => EventRaiser.SetField(ref _isObtainedWithHiddenAbility, value, PropertyChanged);
    }

    public Boolean IsLegal
    {
        get => _isLegal;
        set => EventRaiser.SetField(ref _isLegal, value, PropertyChanged);
    }

    public Boolean IsLegalWithHiddenAbility
    {
        get => _isLegalWithHiddenAbility;
        set => EventRaiser.SetField(ref _isLegalWithHiddenAbility, value, PropertyChanged);
    }

    protected internal IEventRaiser EventRaiser { get; }

    protected internal ILogger? Logger { get; }

    public event EventHandler<HomeBallsPropertyChangedEventArgs>? PropertyChanged;
}