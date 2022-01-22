using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.Services.AddHomeBallsServices(builder.HostEnvironment.BaseAddress);

var host = builder.Build();
await preconfigureHost();
await host.RunAsync();

async Task preconfigureHost(CancellationToken cancellationToken = default)
{
    var configuration = host.Configuration;

    if (Convert.ToBoolean(configuration.GetSection("ClearLocalStorage").Value))
    {
        Console.WriteLine("Clearing `localStorage`.");
        await host.Services.GetRequiredService<ILocalStorageService>()
            .ClearAsync(cancellationToken);
    }
}