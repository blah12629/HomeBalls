String dataClient = nameof(dataClient);

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

var baseAddress = new Uri(builder.HostEnvironment.BaseAddress);

builder.Services
    .AddScoped(sp => new HttpClient { BaseAddress = baseAddress })
    .AddBlazoredLocalStorage()
    .AddScoped<IHomeBallsProtobufTypeMap>(services =>
        new HomeBallsProtobufTypeMap())
    .AddScoped<IHomeBallsLocalStorageDownloader>(services =>
    {
        var client = services.GetRequiredService<HttpClient>();
        client.BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}/data/");

        return new HomeBallsLocalStorageDownloader(
            client,
            services.GetRequiredService<ILocalStorageService>(),
            services.GetRequiredService<ILogger<HomeBallsLocalStorageDownloader>>());
    })

    .AddScoped<IHomeBallsLocalStorageDataSource>(services =>
        new HomeBallsLocalStorageDataSource(
            services.GetRequiredService<ILocalStorageService>(),
            services.GetRequiredService<IHomeBallsProtobufTypeMap>(),
            services.GetRequiredService<IHomeBallsLocalStorageDownloader>(),
            services.GetRequiredService<ILoggerFactory>()))
    .AddScoped<IHomeBallsDataSource>(services =>
        services.GetRequiredService<IHomeBallsLocalStorageDataSource>())

    .AddScoped<IHomeBallsLocalStorageEntryCollection>(services =>
        new HomeBallsLocalStorageEntryCollection(
            services.GetRequiredService<ILocalStorageService>(),
            services.GetRequiredService<IHomeBallsLocalStorageDownloader>(),
            services.GetRequiredService<ILogger<HomeBallsEntryCollection>>()))
    .AddScoped<IHomeBallsReadOnlyCollection<IHomeBallsEntry>>(services =>
        services.GetRequiredService<IHomeBallsLocalStorageEntryCollection>())

    .AddScoped<IHomeBallsEntryTableFactory>(services =>
        new HomeBallsEntryTableFactory(
            services.GetRequiredService<IHomeBallsLocalStorageDataSource>(),
            services.GetRequiredService<ILoggerFactory>()))

    .AddScoped<IHomeBallsBreedablesSpriteService>(services =>
        new HomeBallsBreedablesSpriteService(
            services.GetRequiredService<ILogger<HomeBallsBreedablesSpriteService>>()))
    .AddScoped<IHomeBallsBreedablesFormIdentifierService>(services =>
        new HomeBallsBreedablesFormIdentifierService(
            services.GetRequiredService<ILogger<HomeBallsBreedablesFormIdentifierService>>()));

builder.Logging.SetMinimumLevel(LogLevel.Information)
    .AddFilter(nameof(Microsoft.Extensions.Http), LogLevel.Information)
    .AddFilter(nameof(System), LogLevel.Information);

await builder.Build().RunAsync();