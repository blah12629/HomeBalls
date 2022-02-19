namespace CEo.Pokemon.HomeBalls.App.Categories;

public interface IHomeBallsAppEdit :
    IHomeBallsAppCateogry
{
}

public class HomeBallsAppEdit :
    HomeBallsAppCategory,
    IHomeBallsAppEdit
{
    public HomeBallsAppEdit(ILogger? logger = default) : base(default, logger) { }

    protected internal override void GenerateIconSvgPaths(List<String> paths) { }
}