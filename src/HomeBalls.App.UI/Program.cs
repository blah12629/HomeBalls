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
            services.GetRequiredService<IHomeBallsLocalStorageDownloader>(),
            services.GetRequiredService<IHomeBallsProtobufTypeMap>(),
            services.GetRequiredService<ILogger<HomeBallsLocalStorageDataSource>>()))
    .AddScoped<IHomeBallsDataSource>(services =>
        services.GetRequiredService<IHomeBallsLocalStorageDataSource>())

    .AddScoped<IHomeBallsLocalStorageEntryCollection>(services =>
        new HomeBallsLocalStorageEntryCollection(
            services.GetRequiredService<ILocalStorageService>(),
            services.GetRequiredService<IHomeBallsLocalStorageDownloader>(),
            services.GetRequiredService<ILogger<HomeBallsEntryCollection>>()))
    .AddScoped<IHomeBallsEntryCollection>(services =>
        services.GetRequiredService<IHomeBallsEntryCollection>())

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

await builder.Build().RunAsync();