namespace CEo.Pokemon.HomeBalls.App.UI.Components;

public abstract class HomeBallsComponentBase : ComponentBase
{
    protected ILogger? Logger { get; set; }

    protected virtual void SetServices() { }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        SetServices();
        Logger?.LogDebug($"Initialized `{GetType().Name}`.");
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        Logger?.LogDebug($"Parameters set on `{GetType().Name}`.");
    }

    protected override async Task OnAfterRenderAsync(Boolean firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        Logger?.LogDebug($"Rendered `{GetType().Name}`.");
    }
}