namespace CEo.Pokemon.HomeBalls.Data;

public class Program
{
    static Program DefaultInstance { get; } = new();

    public static async Task Main(params String[] arguments)
    {
        var program = DefaultInstance;
        var services = program.Services;
        var start = DateTime.Now;

        var ensureSprites = arguments.Contains("ensure-sprites");

        try
        {
            await program.StartupAsync(arguments);
            await program.MigratePokeApiDataAsync();
            await program.SeedEntryLegalitiesAsync();
            if (ensureSprites) await program.EnsureSpriteIdsExistAsync();
            await program.CommitCacheAsync();
            await program.ExportDataAsProtoBufAsync();
            await program.ExportEntryCollectionAsProtoBufAsync();
        }
        catch(Exception exception)
        {
            program.Logger.LogError(exception, default);
        }

        program.Logger?.LogInformation($"Application ended in {DateTime.Now - start}.");
        await program.Services.DisposeAsync();
        Console.ReadKey();
    }

    public Program()
    {
        Services = new ServiceCollection()
            .AddHomeBallsDataServices()
            .BuildServiceProvider();

        Options = new ProgramOptions();
    }

    protected internal ServiceProvider Services { get; }

    protected internal IProgramOptions Options { get; set; }

    protected internal ILogger Logger => Services.GetRequiredService<ILogger<Program>>();

    protected internal virtual Task StartupAsync(
        String[] arguments,
        CancellationToken cancellationToken = default)
    {
        Parser.Default
            .ParseArguments<ProgramOptions>(arguments)
            .WithParsed<ProgramOptions>(setOptions);

        return Task.CompletedTask;

        void setOptions(ProgramOptions options)
        {
            Options = options;
            Logger.LogDebug($"Running with `{Options.ToString()}`.");
        }
    }

    protected internal virtual async Task MigratePokeApiDataAsync(
        CancellationToken cancellationToken = default)
    {
        var rawData = Services.GetRequiredService<IRawPokeApiDataSource>();
        var repository = Services.GetRequiredService<IHomeBallsDataRepository>();
        var getCache = Services.GetRequiredService<HomeBallsDataDbContextCache>;
        var converter = new RawPokeApiConverter(Logger);
        var spriteIds = new ProjectPokemonHomeSpriteIdService(Logger);
        var pokeBalls = new HomeBallsPokeBallService(Logger);

        await using (var cache = getCache())
        {
            await cache.Database.EnsureDeletedAsync(cancellationToken);
            await cache.Database.EnsureCreatedAsync(cancellationToken);
        }

        var rawMigrator = new RawPokeApiDataDbContextMigrator(rawData, converter, Logger);
        var migrator = new PokeApiDataDbContextMigrator(rawMigrator, converter, repository, spriteIds, pokeBalls, Logger);
        await migrator.MigratePokeApiDataAsync(getCache, cancellationToken);
    }

    protected internal virtual async Task SeedEntryLegalitiesAsync(
        CancellationToken cancellationToken = default)
    {
        var legalities = new HomeBallsEntryLegalityCollectionFactory(
            Services.GetRequiredService<ILoggerFactory>());
        var seeder = new HomeBallsEntryLegalitySeeder(
            legalities,
            Services.GetRequiredService<ILogger<HomeBallsEntryLegalitySeeder>>());

        await legalities.EnsureBallsLoadedAsync(
            () => Services.GetRequiredService<HomeBallsDataDbContextCache>(),
            cancellationToken);
        await seeder.SeedEntriesAsync(
            Services.GetRequiredService<HomeBallsDataDbContextCache>,
            cancellationToken);
    }

    protected internal virtual async Task EnsureSpriteIdsExistAsync(
        Byte chunkSize = 100,
        CancellationToken cancellationToken = default)
    {
        var getData = Services.GetRequiredService<HomeBallsDataDbContextCache>;
        var getClient = () => Services
            .GetRequiredService<IHttpClientFactory>()
            .CreateClient(ProjectPokemonHomeSpriteClientKey);
        var fileSystem = Services.GetRequiredService<IFileSystem>();
        var errorForms = new List<HomeBallsPokemonForm> { };
        await using var data = getData();

        var ensureTaskChunks = (await data.PokemonForms.ToListAsync(cancellationToken))
            .Select(form => EnsureSpriteIdExistsAsync(
                form,
                getClient,
                errorForms,
                fileSystem,
                cancellationToken))
            .Chunk(chunkSize);
        foreach (var chunk in ensureTaskChunks)
        {
            if (cancellationToken.IsCancellationRequested) break;
            await Task.WhenAll(chunk);
            await Task.Delay(1_500);
        }

        if (errorForms.Count == 0) return;
        Logger.LogWarning(
            $"The following `{nameof(HomeBallsPokemonForm)}` " +
            "have no valid sprite IDs:\n\t" +
            String.Join("\n\t", errorForms
                .OrderBy(form => form.SpeciesId)
                .ThenBy(form => form.FormId)
                .Select(form => $"{form.Identifier}\t{form.Id}")));
    }

    protected internal virtual async Task EnsureSpriteIdExistsAsync(
        HomeBallsPokemonForm form,
        Func<HttpClient> getClient,
        ICollection<HomeBallsPokemonForm> errorForms,
        IFileSystem fileSystem,
        CancellationToken cancellationToken = default)
    {
        using var client = getClient();
        var header = await client.HeadAsync(
            $"poke_capture_{form.ProjectPokemonHomeSpriteId}.png",
            cancellationToken);

        if (header.IsSuccessStatusCode) return;
        errorForms.Add(form);
    }

    protected internal virtual async Task CommitCacheAsync(
        Boolean overwriteData = true,
        CancellationToken cancellationToken = default)
    {
        var committer = new HomeBallsDataCacheCommiter(
            Services.GetService<ILogger<HomeBallsDataCacheCommiter>>());
        await committer.CommitEntitiesAsync(
            Services.GetRequiredService<HomeBallsDataDbContext>,
            Services.GetRequiredService<HomeBallsDataDbContextCache>,
            overwriteData,
            cancellationToken);
    }

    protected internal virtual async Task ExportDataAsProtoBufAsync(
        CancellationToken cancellationToken = default)
    {
        var dataGenerator = new HomeBallsDataSourceGenerator(
            Services.GetRequiredService<IHomeBallsPokemonFormKeyComparer>(),
            Services.GetRequiredService<IHomeBallsItemIdComparer>(),
            Services.GetService<ILogger<HomeBallsDataSourceGenerator>>());
        var data = await dataGenerator.GenerateDataSourceAsync(
            Services.GetRequiredService<HomeBallsDataDbContext>,
            cancellationToken);
        var exporter = Services.GetRequiredService<IHomeBallsDataSourceExporter>();

        if (!String.IsNullOrWhiteSpace(Options.ExportRoot))
            exporter.InDirectory(Options.ExportRoot);

        await exporter.ExportAsync(data, cancellationToken);
    }

    protected internal virtual async Task ExportEntryCollectionAsProtoBufAsync(
        CancellationToken cancellationToken = default)
    {
        var exporter = Services.GetRequiredService<IHomeBallsDataSourceExporter>();
        var initializer = new HomeBallsEntryCollectionInitializer(Logger);
        var entries = await initializer.InitializeAsync(
            Services.GetRequiredService<HomeBallsDataDbContext>,
            cancellationToken);

        await exporter.ExportAsync<HomeBalls.Entities.HomeBallsEntryKey, HomeBallsEntry>(
            entries.AsReadOnly(),
            "Entries",
            cancellationToken);
    }
}