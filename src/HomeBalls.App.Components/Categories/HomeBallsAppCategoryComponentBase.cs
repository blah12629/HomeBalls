namespace CEo.Pokemon.HomeBalls.App.Components.Categories;

public abstract class HomeBallsAppCategoryComponentBase : ComponentBase
{
    IHomeBallsAppCateogry? _category;
    IHomeBallsAppStateContainer? _state;

    [Parameter, EditorRequired]
    public IHomeBallsAppCateogry Category { get => _category!; init => _category = value; }

    [Inject]
    protected IHomeBallsAppStateContainer State { get => _state!; private init => _state = value; }

    protected Boolean IsActive { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        State.ActiveCategoryId.ValueChanged += OnActiveCategoryIdChanged;
    }

    protected virtual void OnActiveCategoryIdChanged(
        Object? sender,
        PropertyChangedEventArgs<String?> e)
    {
        var isNewValue = e.NewValue as String == Category.Identifier;
        if (e.OldValue as String == Category.Identifier || isNewValue)
        {
            IsActive = isNewValue;
            StateHasChanged();
        }
    }
}