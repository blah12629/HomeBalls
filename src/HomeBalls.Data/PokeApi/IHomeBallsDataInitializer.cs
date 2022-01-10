namespace CEo.Pokemon.HomeBalls.Data.PokeApi;

public interface IHomeBallsDataInitializer
{
    Task StartConversionAsync(
        CancellationToken cancellationToken = default);

    Task PostProcessDataAsync(
        CancellationToken cancellationToken = default);

    Task SaveToDataDbContextAsync(
        HomeBallsDataDbContext dbContext,
        CancellationToken cancellationToken = default);
}

public interface IHomeBallsDataInitializer<T> :
    IHomeBallsDataInitializer
{
    new Task<T> StartConversionAsync(
        CancellationToken cancellationToken = default);

    new Task<T> PostProcessDataAsync(
        CancellationToken cancellationToken = default);

    new Task<T> SaveToDataDbContextAsync(
        HomeBallsDataDbContext dbContext,
        CancellationToken cancellationToken = default);
}
