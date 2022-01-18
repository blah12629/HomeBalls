var githubKey = "raw.github/pokeapi";
var serviceCollection = new ServiceCollection()
    .AddScoped<IFileSystem, FileSystem>()
    .AddDbContextFactory<HomeBallsDataDbContext>(
        options => options
            .UseSqlite(@"Data Source=.\data\homeballs.data.db;")
            .EnableSensitiveDataLogging(),
        ServiceLifetime.Transient)
    .AddSingleton<CsvConfiguration>(services =>
        new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToSnakeCase()
        })
    .AddScoped<ICsvHelperFactory, CsvHelperFactory>(services =>
        new CsvHelperFactory().UseConfiguration(
            services.GetRequiredService<CsvConfiguration>()))
    .AddSingleton<IPluralize>(services => _Values.Pluralizer)
    .AddScoped<IRawPokeApiFileNameService>(services =>
        new RawPokeApiFileNameService(
            services.GetRequiredService<IPluralize>(),
            services.GetRequiredService<ILogger<RawPokeApiFileNameService>>()))
    .AddScoped<IRawPokeApiHomeBallsConverter>(services =>
        new RawPokeApiHomeBallsConverter(
            logger: services.GetRequiredService<ILogger<RawPokeApiHomeBallsConverter>>()))
    .AddLogging(builder => builder
        .AddFilter(nameof(CEo), LogLevel.Information)
        .AddFilter(nameof(Microsoft), LogLevel.Warning)
        .AddFilter(nameof(System), LogLevel.Warning)
        .AddConsole());
serviceCollection.AddHttpClient(githubKey, client =>
    client.BaseAddress = new Uri(@"https://raw.githubusercontent.com/PokeAPI/pokeapi/master/data/v2/csv/"));

var services = serviceCollection.BuildServiceProvider();
var fileSystem = services.GetRequiredService<IFileSystem>();
var httpClient = services.GetRequiredService<IHttpClientFactory>().CreateClient(githubKey);
var fileNameService = services.GetRequiredService<IRawPokeApiFileNameService>();
var csvFactory = services.GetRequiredService<ICsvHelperFactory>();
var converter = services.GetRequiredService<IRawPokeApiHomeBallsConverter>();
// var pluralizer = services.GetRequiredService<IPluralize>();
var loggerFactory = services.GetRequiredService<ILoggerFactory>();
var logger = loggerFactory.CreateLogger("_Program");

HomeBallsDataDbContext createDataContext()
{
    fileSystem.Directory.CreateDirectory(@".\data\");
    return services.GetRequiredService<HomeBallsDataDbContext>();
}

async Task initializeDataContextAsync(
    IHomeBallsDataInitializer dataInitializer,
    IHomeBallsEntryLegalityInitializer legalityInitializer,
    CancellationToken cancellationToken = default)
{
    await dataInitializer.StartConversionAsync(cancellationToken);
    await dataInitializer.PostProcessDataAsync(cancellationToken);

    await using (var context = createDataContext())
    {
        await context.Database.EnsureDeletedAsync(cancellationToken);
        await context.Database.EnsureCreatedAsync(cancellationToken);
    }

    await using (var context = createDataContext())
    try { await dataInitializer.SaveToDataDbContextAsync(context, cancellationToken); }
    catch(Exception exception) { logger.LogError(exception, default); }

    await using (var context = createDataContext())
    try { await legalityInitializer.SaveToDataDbContextAsync(context, cancellationToken); }
    catch(Exception exception) { logger.LogError(exception, default); }
}

async Task exportDataContextAsync(
    IHomeBallsDataProtobufExporter exporter,
    CancellationToken cancellationToken = default)
{
    await using (var data = createDataContext())
    {
        await data.EnsureLoadedAsync(cancellationToken);
        await exporter.ExportDataAsync(data, cancellationToken);
    }
}

async Task exportEntriesAsync(
    IFileSystem fileSystem,
    IHomeBallsProtobufConverter converter,
    CancellationToken cancellationToken = default)
{
    ICollection<IHomeBallsEntry> entries;
    var initializer = new HomeBallsEntryCollectionInitializer(logger);
    await using (var dataContext = await createDataContext()
        .EnsureLoadedAsync(cancellationToken))
        entries = await initializer.InitializeAsync(dataContext, cancellationToken);
        
    var exporter = new HomeBallsEntriesProtobufExporter(fileSystem, converter, logger);
    await exporter.ExportEntriesAsync(entries, cancellationToken);
}

var startTime = DateTime.UtcNow;
var dataSource = new RawPokeApiDataSource(fileSystem, httpClient, fileNameService, csvFactory, logger);
var initializer = new PokeApiDataInitializer(dataSource, converter, logger);
var protobufConverter = new HomeBallsProtobufConverter();
var exporter = new HomeBallsDataProtobufExporter(
    fileSystem,
    protobufConverter,
    loggerFactory.CreateLogger<HomeBallsDataProtobufExporter>());
var legalityInitializer = new HomeBallsEntryLegalityInitializer(logger);

await initializeDataContextAsync(initializer, legalityInitializer);
await exportDataContextAsync(exporter);
await exportEntriesAsync(fileSystem, protobufConverter);

// await using (var dataContext = await createDataContext().EnsureLoadedAsync())
// {
//     // foreach (var breedable in await dataContext.PokemonFormsLoadable.EnsureLoadedAsync())
//     foreach (var breedable in ((IHomeBallsDataSource)dataContext).PokemonForms)
//     {
//         if (!breedable.IsBreedable) continue;
//         if (breedable.SpeciesId < 151) continue;
//         if (breedable.SpeciesId > 251) break;
//         Console.WriteLine($"/* {breedable.Identifier.PadRight(15)} */ legalities.AddRange(factory.Pokemon({breedable.SpeciesId}, {breedable.FormId}).CreateLegalities());");
//     }
// }

var runTime = DateTime.UtcNow - startTime;
logger.LogInformation($"Application completed after `{runTime}`.");
Console.ReadLine();