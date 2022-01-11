String dataClient = nameof(dataClient);

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

var baseAddress = new Uri(builder.HostEnvironment.BaseAddress);

builder.Services
    .AddScoped(sp => new HttpClient { BaseAddress = baseAddress })
    .AddBlazoredLocalStorage()
    .AddScoped<IHomeBallsLocalStorageDataDownloader>(services =>
    {
        var client = services.GetRequiredService<HttpClient>();
        client.BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}/data/");

        return new HomeBallsLocalStorageDataDownloader(
            client,
            services.GetRequiredService<ILocalStorageService>(),
            services.GetRequiredService<ILogger<HomeBallsLocalStorageDataDownloader>>());
    })
    .AddScoped<IHomeBallsProtobufTypeMap>(services =>
        new HomeBallsProtobufTypeMap())
    .AddScoped<IHomeBallsLocalStorageDataSource>(services =>
        new HomeBallsLocalStorageDataSource(
            services.GetRequiredService<ILocalStorageService>(),
            services.GetRequiredService<IHomeBallsLocalStorageDataDownloader>(),
            services.GetRequiredService<IHomeBallsProtobufTypeMap>(),
            services.GetRequiredService<ILogger<HomeBallsLocalStorageDataSource>>()))
    .AddScoped<IHomeBallsEntryTableFactory>(services =>
        new HomeBallsEntryTableFactory(
            services.GetRequiredService<IHomeBallsLocalStorageDataSource>(),
            services.GetRequiredService<ILoggerFactory>()));

await builder.Build().RunAsync();