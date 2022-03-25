namespace CEo.Pokemon.HomeBalls.App.Components;

public delegate String CreateComponentId(Int32 componentId);

public abstract class HomeBallsBaseComponent :
    ComponentBase,
    IHomeBallsComponent
{
    IHomeBallsComponentIdService? _componentIds;
    Int32 _id;

    [Inject]
    protected internal virtual IHomeBallsComponentIdService ComponentIds
    {
        get => _componentIds ?? throw new NullReferenceException();
        init
        {
            _componentIds = value;
            _id = ComponentIds.CreateNew();
        }
    }

    public virtual Int32 Id => _id;
}