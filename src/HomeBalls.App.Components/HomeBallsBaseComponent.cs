namespace CEo.Pokemon.HomeBalls.App.Components;

public abstract class HomeBallsBaseComponent :
    ComponentBase
{
    IHomeBallsComponentIdService? _componentIds;

    [Inject]
    protected internal IHomeBallsComponentIdService ComponentIds
    {
        get => _componentIds ?? throw new NullReferenceException();
        init => _componentIds = value;
    }

    public virtual Int32 Id { get; protected internal set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        Id = ComponentIds.CreateNew();
    }
}