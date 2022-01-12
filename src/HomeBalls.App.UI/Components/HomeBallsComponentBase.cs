namespace CEo.Pokemon.HomeBalls.App.UI.Components;

public abstract class HomeBallsComponentBase : ComponentBase
{
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        Console.WriteLine($"Initialized `{GetType().Name}`.");
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        Console.WriteLine($"Parameters set on `{GetType().Name}`.");
    }

    protected override async Task OnAfterRenderAsync(Boolean firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        Console.WriteLine($"Rendered `{GetType().Name}`.");
    }
}