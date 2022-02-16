namespace CEo.Pokemon.HomeBalls.App;

public static class HomeBallsAppServiceCollectionExtensions
{
    public static IServiceCollection AddHomeBallsAppServices(
        this IServiceCollection services,
        String baseAddress,
        ServiceLifetime lifetime = ServiceLifetime.Scoped) =>
        services.AddHomeBallsServices(lifetime)
            .AddStateContainer(lifetime)
            .AddCategories(lifetime)
            .AddHttpClients(baseAddress, lifetime)
            .AddDataAccessServices(lifetime)
            .AddEntryTableServices(lifetime)
            .AddUIServices(lifetime)
            .AddAppStartupServices(lifetime)
            .AddComponentServices(lifetime);

    public static IServiceCollection AddStateContainer(
        this IServiceCollection services,
        ServiceLifetime lifetime) =>
        services
            .Add<IHomeBallsAppStateContainer>(
                services => new HomeBallsAppStateContainer(
                    services.GetService<ILogger<HomeBallsAppStateContainer>>()),
                lifetime);

    public static IServiceCollection AddCategories(
        this IServiceCollection services,
        ServiceLifetime lifetime) =>
        services
            .Add<IHomeBallsAppNavigation>(
                services => new HomeBallsAppNavigation(
                    services.GetService<ILogger<HomeBallsAppNavigation>>()),
                lifetime)
            .Add<IHomeBallsAppSettings>(
                services => new HomeBallsAppSettings(
                    services.GetRequiredService<ILocalStorageService>(),
                    services.GetService<ILoggerFactory>()),
                lifetime)
            .Add<IReadOnlyList<IHomeBallsAppCateogry>>(services =>
                new List<IHomeBallsAppCateogry>
                {
                    services.GetRequiredService<IHomeBallsAppNavigation>(),
                    services.GetRequiredService<IHomeBallsAppSettings>(),
                }.AsReadOnly(),
                lifetime)
            .Add<IReadOnlyCollection<IHomeBallsAppCateogry>>(
                services => services
                    .GetRequiredService<IReadOnlyList<IHomeBallsAppCateogry>>(),
                lifetime);

    public static IServiceCollection AddHttpClients(
        this IServiceCollection services,
        String baseAddress,
        ServiceLifetime lifetime)
    {
        services.AddScoped(services => new HttpClient
        {
            BaseAddress = new Uri(baseAddress)
        });

        services.AddHttpClient(DataClientKey, (services, client) =>
        {
            var (address, data) = (baseAddress, "/data/");
            address = trim(address, address.TrimEnd, '/', '\\');
            data = trim(data, data.TrimStart, '/', '\\');
            client.BaseAddress = new Uri($"{address}/{data}");
        });

        services.AddHttpClient(HomeSpriteClientKey, (services, client) =>
            client.BaseAddress = new Uri(HomeSpriteBaseAddress));

        return services;

        static String trim(
            String @string,
            Func<Char[]?, String> trimFunction,
            params Char[] trimCharacters) =>
            trimCharacters.Any(character => @string.EndsWith(character)) ?
                trimFunction(trimCharacters) :
                @string;
    }

    public static IServiceCollection AddDataAccessServices(
        this IServiceCollection services,
        ServiceLifetime lifetime) =>
        services.AddBlazoredLocalStorage()
            .AddDataSource(lifetime)
            .AddEntries(lifetime);

    internal static IServiceCollection AddDataSource(
        this IServiceCollection services,
        ServiceLifetime lifetime) =>
        services
            .Add<IHomeBallsLocalStorageDownloader>(
                services => new HomeBallsLocalStorageDownloader(
                    services.GetRequiredService<IHttpClientFactory>().CreateClient(DataClientKey),
                    services.GetRequiredService<ILocalStorageService>(),
                    services.GetService<ILogger<HomeBallsLocalStorageDownloader>>()),
                lifetime)
            .Add<IHomeBallsLocalStorageDataSource>(
                services => new HomeBallsLocalStorageDataSource(
                    services.GetRequiredService<ILocalStorageService>(),
                    services.GetRequiredService<IHomeBallsLocalStorageDownloader>(),
                    services.GetRequiredService<IProtoBufSerializer>(),
                    services.GetRequiredService<IHomeBallsEntryKeyComparer>(),
                    services.GetRequiredService<IHomeBallsPokemonFormKeyComparer>(),
                    services.GetRequiredService<IHomeBallsItemIdComparer>(),
                    services.GetService<ILoggerFactory>()),
                lifetime)
            .Add<IHomeBallsLoadableDataSource>(
                services => services.GetRequiredService<IHomeBallsLocalStorageDataSource>(),
                lifetime)
            .Add<IHomeBallsDataSource>(
                services => services.GetRequiredService<IHomeBallsLocalStorageDataSource>(),
                lifetime);

    internal static IServiceCollection AddEntries(
        this IServiceCollection services,
        ServiceLifetime lifetime) =>
        services
            .Add<IHomeBallsLocalStorageEntryCollection>(
                services => new HomeBallsLocalStorageEntryCollection(
                    services.GetRequiredService<ILocalStorageService>(),
                    services.GetRequiredService<IHomeBallsLocalStorageDownloader>(),
                    services.GetRequiredService<IProtoBufSerializer>(),
                    services.GetRequiredService<ILogger<HomeBallsEntryCollection>>()),
                lifetime)
            .Add<IHomeBallsReadOnlyCollection<IHomeBallsEntry>>(
                services => services
                    .GetRequiredService<IHomeBallsLocalStorageEntryCollection>(),
                lifetime);

    public static IServiceCollection AddEntryTableServices(
        this IServiceCollection services,
        ServiceLifetime lifetime) =>
        services
            .Add<IHomeBallsEntryTable>(
                services => new HomeBallsEntryTable(
                    services.GetService<ILoggerFactory>()),
                lifetime)
            .Add<IHomeBallsEntryColumnFactory>(
                services => new HomeBallsEntryColumnFactory(
                    services.GetRequiredService<IComparer<IHomeBallsItem>>(),
                    services.GetRequiredService<IComparer<IHomeBallsPokemonForm>>(),
                    services.GetService<ILoggerFactory>()),
                lifetime);

    public static IServiceCollection AddUIServices(
        this IServiceCollection services,
        ServiceLifetime lifetime) =>
        services
            .Add<IHomeBallsBreedablesSpriteService>(
                services => new HomeBallsBreedablesSpriteService(
                    services.GetService<ILogger<HomeBallsBreedablesSpriteService>>()),
                lifetime)
            .Add<IHomeBallsBreedablesFormIdentifierService>(
                services => new HomeBallsBreedablesFormIdentifierService(
                    services.GetService<ILogger<HomeBallsBreedablesFormIdentifierService>>()),
                lifetime);

    public static IServiceCollection AddAppStartupServices(
        this IServiceCollection services,
        ServiceLifetime lifetime) =>
        services
            .Add<IHomeBallsAppSettingsLoader>(
                services => new HomeBallsAppSettingsLoader(
                    services.GetService<ILogger<HomeBallsAppSettingsLoader>>()),
                lifetime)
            .Add<IHomeBallsTableGenerator>(
                services => new HomeBallsTableGenerator(
                    services.GetService<ILogger<HomeBallsTableGenerator>>()),
                lifetime);

    public static IServiceCollection AddComponentServices(
        this IServiceCollection services,
        ServiceLifetime lifetime) =>
        services
            .Add<IHomeBallsComponentIdService>(
                services => new HomeBallsComponentIdServices(
                    services.GetService<ILogger<HomeBallsComponentIdServices>>()),
                lifetime);
}