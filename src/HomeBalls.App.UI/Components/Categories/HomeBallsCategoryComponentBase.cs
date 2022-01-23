namespace CEo.Pokemon.HomeBalls.App.UI.Components.Categories;

public abstract class HomeBallsCategoryComponentBase :
    HomeBallsComponentBase
{
    IHomeBallsAppCateogry? _category;
    IHomeBallsStateContainer? _state;

    [Parameter, EditorRequired]
    public IHomeBallsAppCateogry Category { get => _category!; init => _category = value; }

    [Inject]
    protected IHomeBallsStateContainer State { get => _state!; private init => _state = value; }

    protected Boolean IsActive { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        State.ActiveCategoryId.PropertyChanged += OnActiveCategoryIdChanged;
        await State.EnsurePresetsLoaded();
    }

    protected virtual void OnActiveCategoryIdChanged(
        Object? sender,
        HomeBallsPropertyChangedEventArgs e)
    {
        var isNewValue = e.NewValue as String == Category.Identifier;
        if (e.OldValue as String == Category.Identifier || isNewValue)
        {
            IsActive = isNewValue;
            StateHasChanged();
        }
    }
}