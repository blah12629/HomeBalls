namespace CEo.Pokemon.HomeBalls.App.Entries;

public interface IHomeBallsEntryRowFactoryData
{
    IReadOnlyList<IHomeBallsPokemonForm> Breedables { get; set; }

    IReadOnlyList<HomeBallsPokemonFormKey> BreedableIds { get; }

    IReadOnlyList<IHomeBallsItem> Balls { get; set; }

    IReadOnlyList<UInt16> BallIds { get; }

    IReadOnlyDictionary<UInt16, Int32> BallIndexMap { get; }
}

public class HomeBallsEntryRowFactoryData : IHomeBallsEntryRowFactoryData
{
    IReadOnlyList<IHomeBallsPokemonForm> _breedables;
    IReadOnlyList<HomeBallsPokemonFormKey>? _breedableIds;
    IReadOnlyList<IHomeBallsItem> _balls;
    IReadOnlyList<UInt16>? _ballIds;
    IReadOnlyDictionary<UInt16, Int32>? _ballIndexMap;

    public HomeBallsEntryRowFactoryData()
    {
        _breedables = new List<IHomeBallsPokemonForm> { }.AsReadOnly();
        _balls = new List<IHomeBallsItem> { }.AsReadOnly();
    }

    public IReadOnlyList<IHomeBallsPokemonForm> Breedables
    {
        get => _breedables;
        set => (_breedables, _breedableIds) = (value, default);
    }

    public IReadOnlyList<HomeBallsPokemonFormKey> BreedableIds =>
        _breedableIds ??= CreateBreedableIds();

    public IReadOnlyList<IHomeBallsItem> Balls
    {
        get => _balls;
        set => (_balls, _ballIds, _ballIndexMap) = (value, default, default);
    }

    public IReadOnlyList<UInt16> BallIds =>
        _ballIds ??= CreateBallIds();

    public IReadOnlyDictionary<UInt16, Int32> BallIndexMap =>
        _ballIndexMap ??= CreateBallIndexMap();

    protected internal virtual IReadOnlyList<HomeBallsPokemonFormKey> CreateBreedableIds() =>
        Breedables.Select(form => form.Id).ToList().AsReadOnly();

    protected internal virtual IReadOnlyList<UInt16> CreateBallIds() =>
        Balls.Select(item => item.Id).ToList().AsReadOnly();

    protected internal virtual IReadOnlyDictionary<UInt16, Int32> CreateBallIndexMap() =>
        Balls.Select((ball, index) => new { Id = ball.Id, Index = index })
            .ToDictionary(ball => ball.Id, ball => ball.Index)
            .AsReadOnly();
}
