namespace CEo.Pokemon.HomeBalls.App.Components.Layout;

public abstract class HomeBallsLayoutComponentBase :
    HomeBallsLoggingComponent
{
    [Parameter]
    public RenderFragment? Body { get; init; }
}