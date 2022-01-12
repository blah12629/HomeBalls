String dataClient = nameof(dataClient);

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

var baseAddress = new Uri(builder.HostEnvironment.BaseAddress);

builder.Services
    .AddScoped(sp => new HttpClient { BaseAddress = baseAddress })
    .AddBlazoredLocalStorage()
    .AddScoped<IHomeBallsProtobufTypeMap>(services =>
        new HomeBallsProtobufTypeMap())
    .AddScoped<IHomeBallsLocalStorageDataDownloader>(services =>
    {
        var client = services.GetRequiredService<HttpClient>();
        client.BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}/data/");

        return new HomeBallsLocalStorageDataDownloader(
            client,
            services.GetRequiredService<ILocalStorageService>(),
            services.GetRequiredService<ILogger<HomeBallsLocalStorageDataDownloader>>());
    })
    .AddScoped<IHomeBallsLocalStorageDataSource>(services =>
        new HomeBallsLocalStorageDataSource(
            services.GetRequiredService<ILocalStorageService>(),
            services.GetRequiredService<IHomeBallsLocalStorageDataDownloader>(),
            services.GetRequiredService<IHomeBallsProtobufTypeMap>(),
            services.GetRequiredService<ILogger<HomeBallsLocalStorageDataSource>>()))
    .AddScoped<IHomeBallsDataSource>(services =>
        services.GetRequiredService<IHomeBallsLocalStorageDataSource>())
    .AddScoped<IHomeBallsEntryTableFactory>(services =>
        new HomeBallsEntryTableFactory(
            services.GetRequiredService<IHomeBallsLocalStorageDataSource>(),
            services.GetRequiredService<ILoggerFactory>()))
    .AddScoped<IPokemonSpriteService>(services =>
        new PokemonSpriteService(
            services.GetRequiredService<ILogger<PokemonSpriteService>>()));

await builder.Build().RunAsync();