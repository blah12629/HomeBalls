namespace CEo.Pokemon.HomeBalls.Data;

public static class HomeBallsDataServiceCollectionExtensions
{
    public static IServiceCollection AddHomeBallsDataServices(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Scoped,
        ServiceLifetime dbContextLifetime = ServiceLifetime.Transient) =>
        services.AddHomeBallsServices(lifetime)
            .AddFileSystem(lifetime)
            .AddHttpClients(lifetime)
            .AddDataDbContext(dbContextLifetime)
            .AddRawPokeApiDataSource(lifetime)
            .AddCsvHelperServices(lifetime)
            .AddPluralizerServices(lifetime)
            .AddProtoBufExporter(lifetime)
            .AddLogging(lifetime);

    public static IServiceCollection AddFileSystem(
        this IServiceCollection services,
        ServiceLifetime lifetime) =>
        services.Add<IFileSystem>(services => new FileSystem(), lifetime);

    public static IServiceCollection AddHttpClients(
        this IServiceCollection services,
        ServiceLifetime lifetime)
    {
        services.AddHttpClient(PokeApiDataClientKey, client =>
            client.BaseAddress = new Uri(PokeApiDataBaseAddress));

        services.AddHttpClient(ProjectPokemonHomeSpriteClientKey, client =>
            client.BaseAddress = new Uri(ProjectPokemonHomeSpriteBaseAddress));

        return services;
    }

    public static IServiceCollection AddDataDbContext(
        this IServiceCollection services,
        ServiceLifetime lifetime) =>
        services.AddDataDbContext<HomeBallsDataDbContext>(DataConnectionString, lifetime)
            .AddDataDbContext<HomeBallsDataDbContextCache>(DataCacheConnectionString, lifetime);

    internal static IServiceCollection AddDataDbContext<TDbContext>(
        this IServiceCollection services,
        String connectionString,
        ServiceLifetime lifetime)
        where TDbContext : notnull, DbContext =>
        services.AddDbContextFactory<TDbContext>(
            (services, options) =>
            {
                var fileSystem = services.GetRequiredService<IFileSystem>();
                options
                    .UseSqlite(connectionString, sqliteOptions => sqliteOptions
                        .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                    .EnableSensitiveDataLogging();
                fileSystem.Directory.CreateDirectory(DefaultSqliteRoot);
            },
            lifetime);

    public static IServiceCollection AddRawPokeApiDataSource(
        this IServiceCollection services,
        ServiceLifetime lifetime) =>
        services
            .Add<IRawPokeApiDownloader>(
                services => new RawPokeApiDownloader(
                    services.GetRequiredService<IHttpClientFactory>()
                        .CreateClient(PokeApiDataClientKey),
                    services.GetRequiredService<IFileSystem>(),
                    DefaultDataRoot,
                    services.GetRequiredService<ILogger<RawPokeApiDownloader>>()),
                lifetime)
            .Add<IHomeBallsIdentifierService>(
                services => new PokeApiIdentifierService(
                    services.GetRequiredService<IPluralize>(),
                    services.GetService<ILogger<PokeApiIdentifierService>>()),
                lifetime)
            .Add<IRawPokeApiDataSource>(
                services => new RawPokeApiDataSource(
                    services.GetRequiredService<IFileSystem>(),
                    services.GetRequiredService<IRawPokeApiDownloader>(),
                    services.GetRequiredService<ICsvHelperFactory>(),
                    services.GetRequiredService<IHomeBallsIdentifierService>(),
                    DefaultDataRoot,
                    services.GetService<ILoggerFactory>()),
                lifetime);

    public static IServiceCollection AddCsvHelperServices(
        this IServiceCollection services,
        ServiceLifetime lifetime) =>
        services
            .Add<CsvConfiguration>(
                services => new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    PrepareHeaderForMatch = args => args.Header.ToSnakeCase()
                },
                lifetime)
            .Add<ICsvHelperFactory>(
                services => new CsvHelperFactory()
                    .UseConfiguration(services.GetRequiredService<CsvConfiguration>()),
                lifetime);

    public static IServiceCollection AddPluralizerServices(
        this IServiceCollection services,
        ServiceLifetime lifetime) =>
        services.Add<IPluralize>(services => StaticPluralizer, lifetime);

    public static IServiceCollection AddProtoBufExporter(
        this IServiceCollection services,
        ServiceLifetime lifetime) =>
        services.Add<IHomeBallsDataSourceExporter>(
            services => new HomeBallsDataSourceExporter(
                services.GetRequiredService<IFileSystem>(),
                services.GetRequiredService<IProtoBufSerializer>(),
                DefaultProtobufExportRoot,
                services.GetService<ILogger<HomeBallsDataSourceExporter>>()),
            lifetime);

    public static IServiceCollection AddLogging(
        this IServiceCollection services,
        ServiceLifetime lifetime)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        return services.AddLogging(builder => builder
            .AddFilter(nameof(CEo), LogLevel.Information)
            .AddFilter(nameof(Microsoft), LogLevel.Warning)
            .AddFilter(nameof(System), LogLevel.Information)
            .AddConsole());
    }
}