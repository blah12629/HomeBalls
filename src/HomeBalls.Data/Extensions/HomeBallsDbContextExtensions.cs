namespace CEo.Pokemon.HomeBalls.Data;

public static class HomeBallsDbContextExtensions
{
    public static Task VacuumAsync(
        this DatabaseFacade database,
        CancellationToken cancellationToken = default) =>
        database.ExecuteSqlRawAsync("VACUUM;", cancellationToken);
}