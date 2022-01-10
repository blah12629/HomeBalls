namespace CEo.Pokemon.HomeBalls.Data.PokeApi;

public interface IPokeApiDataInitializer :
    IHomeBallsDataInitializer<IPokeApiDataInitializer> { }

public class PokeApiDataInitializer :
    RawPokeApiDataInitializer,
    IPokeApiDataInitializer
{
    public PokeApiDataInitializer(
        IRawPokeApiDataSource rawData,
        IRawPokeApiHomeBallsConverter converter,
        ILogger? logger = default) :
        base(rawData, converter, logger) { }

    new public virtual async Task<PokeApiDataInitializer> StartConversionAsync(
        CancellationToken cancellationToken = default)
    {
        await base.StartConversionAsync(cancellationToken);
        return this;
    }

    new public virtual async Task<PokeApiDataInitializer> PostProcessDataAsync(
        CancellationToken cancellationToken = default)
    {
        // STOPPED HERE. OPTED TO WORK ON PROTOBUF INSTEAD IF CLEARNING THE DB
        // await RemoveUnnecessaryLanguagesAsync(cancellationToken);
        // await LabelBreedablesAsync(cancellationToken);

        await Task.CompletedTask;
        return this;
    }

    new public virtual async Task<PokeApiDataInitializer> SaveToDataDbContextAsync(
        HomeBallsDataDbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        await base.SaveToDataDbContextAsync(dbContext, cancellationToken);
        return this;
    }
}