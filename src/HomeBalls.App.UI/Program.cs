using Blazored.LocalStorage;
using CEo.Pokemon.HomeBalls.App;
using CEo.Pokemon.HomeBalls.App.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
// builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HomeBallsTabsComponent>("#tabs");
builder.RootComponents.Add<HomeBallsLoadingComponent>("#loading");
builder.RootComponents.Add<HomeBallsMenuComponent>("#menu");
builder.RootComponents.Add<HomeBallsEntriesComponent>("#entries");
builder.Services.AddHomeBallsAppServices(builder.HostEnvironment.BaseAddress);

var host = builder.Build();
await PreconfigureHostAsync();
await host.RunAsync();

async Task PreconfigureHostAsync(CancellationToken cancellationToken = default)
{
    var configuration = host.Configuration;

    if (Convert.ToBoolean(configuration.GetSection("ClearLocalStorage").Value))
    {
        Console.WriteLine("Clearing `localStorage`.");
        await GetRequiredService<ILocalStorageService>().ClearAsync(cancellationToken);
    }
}

T GetRequiredService<T>() where T : notnull => host.Services.GetRequiredService<T>();