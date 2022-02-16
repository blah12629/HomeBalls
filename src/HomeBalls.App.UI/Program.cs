using Blazored.LocalStorage;
using CEo.Pokemon.HomeBalls.App;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.Services.AddHomeBallsAppServices(builder.HostEnvironment.BaseAddress);

var host = builder.Build();
await PreconfigureHost();
await host.RunAsync();

async Task PreconfigureHost(CancellationToken cancellationToken = default)
{
    var configuration = host.Configuration;

    if (Convert.ToBoolean(configuration.GetSection("ClearLocalStorage").Value))
    {
        Console.WriteLine("Clearing `localStorage`.");
        await GetRequiredService<ILocalStorageService>().ClearAsync(cancellationToken);
    }
}

T GetRequiredService<T>() where T : notnull => host.Services.GetRequiredService<T>();