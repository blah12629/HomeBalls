namespace CEo.Pokemon.HomeBalls.App.UI.Components;

public abstract class HomeBallsComponentBase : OwningComponentBase
{
    protected internal ILogger? Logger { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await InitializeServicesAsync();
    }

    protected virtual Task InitializeServicesAsync(
        CancellationToken cancellationToken = default)
    {
        Logger = ScopedServices.GetRequiredService<ILoggerFactory>()
            .CreateLogger(GetType());

        return Task.CompletedTask;
    }
}