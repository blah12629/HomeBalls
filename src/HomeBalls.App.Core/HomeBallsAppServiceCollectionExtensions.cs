namespace CEo.Pokemon.HomeBalls.App.Core;

public static class HomeBallsAppServiceCollectionExtensions
{
    public static IServiceCollection AddHomeBallsServices(
        this IServiceCollection services,
        String baseAddress) =>
        services.AddHomeBallsComparers()
            .AddStateContainer()
            .AddCategories()
            .AddHttpClients(baseAddress)
            .AddDataAccessServices()
            .AddEntryTableServices()
            .AddUIServices();

    internal static IServiceCollection AddHomeBallsComparers(
        this IServiceCollection services) =>
        services.AddScoped<HomeBallsPokemonFormComparer>()
            .AddScoped<IComparer<IHomeBallsPokemonForm>>(services =>
                services.GetRequiredService<HomeBallsPokemonFormComparer>())
            .AddScoped<IComparer<HomeBallsPokemonFormKey>>(services =>
                services.GetRequiredService<HomeBallsPokemonFormComparer>())
            .AddScoped<IComparer<HomeBallsPokemonFormKey?>>(services =>
                services.GetRequiredService<HomeBallsPokemonFormComparer>())
            .AddScoped<IHomeBallsPokeballComparer, HomeBallsPokeballComparer>()
            .AddScoped<IComparer<IHomeBallsItem>>(services =>
                services.GetRequiredService<IHomeBallsPokeballComparer>());

    internal static IServiceCollection AddStateContainer(
        this IServiceCollection services) =>
        services.AddScoped<IHomeBallsStateContainer, HomeBallsStateContainer>();

    internal static IServiceCollection AddCategories(
        this IServiceCollection services) =>
        services
            .AddScoped<IHomeBallsAppNavigation>(services =>
                new HomeBallsAppNavigation(
                    services.GetRequiredService<ILogger<HomeBallsAppNavigation>>()))
            .AddScoped<IHomeBallsAppSettings, HomeBallsAppSettings>()
            .AddScoped<IReadOnlyList<IHomeBallsAppCateogry>>(services =>
                new List<IHomeBallsAppCateogry>
                {
                    services.GetRequiredService<IHomeBallsAppNavigation>(),
                    services.GetRequiredService<IHomeBallsAppSettings>(),
                }.AsReadOnly())
            .AddScoped<IReadOnlyCollection<IHomeBallsAppCateogry>>(services =>
                services.GetRequiredService<IReadOnlyList<IHomeBallsAppCateogry>>());

    internal static IServiceCollection AddHttpClients(
        this IServiceCollection services,
        String baseAddress)
    {
        services.AddScoped(services => new HttpClient
        {
            BaseAddress = new Uri(baseAddress)
        });

        services.AddHttpClient(_Values.DataClientKey, (services, client) =>
        {
            var (address, data) = (baseAddress, "/data/");
            address = trim(address, address.TrimEnd, '/', '\\');
            data = trim(data, data.TrimStart, '/', '\\');
            client.BaseAddress = new Uri($"{address}/{data}");
        });

        return services;

        static String trim(
            String @string,
            Func<Char[]?, String> trimFunction,
            params Char[] trimCharacters) =>
            trimCharacters.Any(character => @string.EndsWith(character)) ?
                trimFunction(trimCharacters) :
                @string;
    }

    internal static IServiceCollection AddDataAccessServices(
        this IServiceCollection services) =>
        services.AddBlazoredLocalStorage()
            .AddDataSource()
            .AddEntries();

    internal static IServiceCollection AddDataSource(
        this IServiceCollection services) =>
        services.AddScoped<IHomeBallsProtobufTypeMap, HomeBallsProtobufTypeMap>()
            .AddScoped<IHomeBallsLocalStorageDownloader>(services =>
                new HomeBallsLocalStorageDownloader(
                    services.GetRequiredService<IHttpClientFactory>()
                        .CreateClient(_Values.DataClientKey),
                    services.GetRequiredService<ILocalStorageService>(),
                    services.GetService<ILogger<HomeBallsLocalStorageDownloader>>()))
            .AddScoped<IHomeBallsLocalStorageDataSource, HomeBallsLocalStorageDataSource>()
            .AddScoped<IHomeBallsLoadableDataSource>(services =>
                services.GetRequiredService<IHomeBallsLocalStorageDataSource>())
            .AddScoped<IHomeBallsDataSource>(services =>
                services.GetRequiredService<IHomeBallsLocalStorageDataSource>());

    internal static IServiceCollection AddEntries(
        this IServiceCollection services) =>
        services
            .AddScoped<IHomeBallsLocalStorageEntryCollection>(services =>
                new HomeBallsLocalStorageEntryCollection(
                    services.GetRequiredService<ILocalStorageService>(),
                    services.GetRequiredService<IHomeBallsLocalStorageDownloader>(),
                    services.GetRequiredService<ILogger<HomeBallsEntryCollection>>()))
            .AddScoped<IHomeBallsReadOnlyCollection<IHomeBallsEntry>>(services =>
                services.GetRequiredService<IHomeBallsLocalStorageEntryCollection>());

    internal static IServiceCollection AddEntryTableServices(
        this IServiceCollection services) =>
        services.AddScoped<IHomeBallsEntryTable, HomeBallsEntryTable>()
            .AddScoped<IHomeBallsEntryColumnFactory, HomeBallsEntryColumnFactory>();

    internal static IServiceCollection AddUIServices(
        this IServiceCollection services) =>
        services
            .AddScoped<IHomeBallsBreedablesSpriteService>(services =>
                new HomeBallsBreedablesSpriteService(
                    services.GetService<ILogger<HomeBallsBreedablesSpriteService>>()))
            .AddScoped<IHomeBallsBreedablesFormIdentifierService>(services =>
                new HomeBallsBreedablesFormIdentifierService(
                    services.GetService<ILogger<HomeBallsBreedablesFormIdentifierService>>()));
}