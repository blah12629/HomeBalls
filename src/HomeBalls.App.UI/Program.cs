var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.Services.AddHomeBallsServices(builder.HostEnvironment.BaseAddress);

await builder.Build().RunAsync();