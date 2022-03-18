namespace CEo.Pokemon.HomeBalls.App.Entries;

public interface IHomeBallsEntryRowFactory
{
    IHomeBallsEntryRowFactory UsingData(IHomeBallsLoadableDataSource data);

    IHomeBallsEntryRowFactory UsingPokemonForms(IEnumerable<IHomeBallsPokemonForm> forms);

    IHomeBallsEntryRowFactory UsingBalls(IEnumerable<IHomeBallsItem> balls);

    IHomeBallsEntryHeadRow CreateHeader();

    IEnumerable<IHomeBallsEntryBodyRow> CreateRows();

    IHomeBallsEntryBodyRow CreateRow(HomeBallsPokemonFormKey formKey);
}

public class HomeBallsEntryRowFactory : IHomeBallsEntryRowFactory
{
    public HomeBallsEntryRowFactory(
        IHomeBallsPokemonFormKeyComparer pokemonComparer,
        IHomeBallsItemIdComparer itemComparer,
        ILoggerFactory? loggerFactory = default)
    {
        (PokemonComparer, ItemComparer) = (pokemonComparer, itemComparer);
        LoggerFactory = loggerFactory;
        Logger = LoggerFactory?.CreateLogger<HomeBallsEntryRowFactory>();
        Data = new HomeBallsEntryRowFactoryData();
    }

    protected internal IHomeBallsEntryRowFactoryData Data { get; }

    protected internal IHomeBallsPokemonFormKeyComparer PokemonComparer { get; }

    protected internal IHomeBallsItemIdComparer ItemComparer { get; }

    protected internal ILoggerFactory? LoggerFactory { get; }

    protected internal ILogger? Logger { get; }

    public virtual IHomeBallsEntryHeadRow CreateHeader() =>
        new HomeBallsEntryHeadRow(
            Data.BallIds.Select(CreateHeaderCell).ToList().AsReadOnly(),
            Data.BallIndexMap,
            LoggerFactory?.CreateLogger<HomeBallsEntryHeadRow>());

    protected internal virtual IHomeBallsEntryHeadCell CreateHeaderCell(UInt16 ballId) =>
        new HomeBallsEntryHeadCell(
            ballId,
            LoggerFactory?.CreateLogger<HomeBallsEntryHeadCell>());

    public virtual IHomeBallsEntryBodyRow CreateRow(HomeBallsPokemonFormKey formKey) =>
        new HomeBallsEntryBodyRow(
            formKey,
            Data.BallIds.Select(id => CreateRowCell(formKey, id)).ToList().AsReadOnly(),
            Data.BallIndexMap,
            LoggerFactory?.CreateLogger<HomeBallsEntryBodyRow>());

    protected internal virtual IHomeBallsEntryBodyCell CreateRowCell(
        HomeBallsPokemonFormKey formId,
        UInt16 ballId) =>
        new HomeBallsEntryBodyCell(formId, ballId, logger: LoggerFactory?.CreateLogger<HomeBallsEntryBodyCell>());

    public virtual IEnumerable<IHomeBallsEntryBodyRow> CreateRows()
    {
        foreach (var form in Data.Breedables) yield return CreateRow(form.Id);
    }

    public virtual HomeBallsEntryRowFactory UsingBalls(IEnumerable<IHomeBallsItem> balls)
    {
        Data.Balls = balls.OrderBy(ball => ball.Id, ItemComparer).ToList().AsReadOnly();
        return this;
    }

    public virtual HomeBallsEntryRowFactory UsingData(IHomeBallsLoadableDataSource data)
    {
        UsingPokemonForms(
            data.BreedablePokemonForms.IsLoaded ? data.BreedablePokemonForms :
            data.PokemonForms.IsLoaded ? data.PokemonForms.Where(form => form.IsBreedable) :
            throw new ArgumentNullException());

        UsingBalls(
            data.Pokeballs.IsLoaded ? data.Pokeballs :
            data.Items.IsLoaded ? data.Items.Where(item => item.Identifier.Contains("ball")) :
            throw new ArgumentNullException());

        return this;
    }

    public virtual HomeBallsEntryRowFactory UsingPokemonForms(IEnumerable<IHomeBallsPokemonForm> forms)
    {
        Data.Breedables = forms.OrderBy(form => form.Id, PokemonComparer).ToList().AsReadOnly();
        return this;
    }

    IHomeBallsEntryRowFactory IHomeBallsEntryRowFactory.UsingData(IHomeBallsLoadableDataSource data) => UsingData(data);

    IHomeBallsEntryRowFactory IHomeBallsEntryRowFactory.UsingPokemonForms(IEnumerable<IHomeBallsPokemonForm> forms) => UsingPokemonForms(forms);

    IHomeBallsEntryRowFactory IHomeBallsEntryRowFactory.UsingBalls(IEnumerable<IHomeBallsItem> balls) => UsingBalls(balls);
}