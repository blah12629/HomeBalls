namespace CEo.Pokemon.HomeBalls.App.Core.Categories;

public interface IHomeBallsAppNavigation :
    IHomeBallsAppCateogry { }

public class HomeBallsAppNavigation :
    HomeBallsAppCategory,
    IHomeBallsAppNavigation
{
    public HomeBallsAppNavigation(ILogger? logger = default) : base(default, logger) { }

    protected internal override void GenerateIconSvgPaths(List<String> paths) { }
}