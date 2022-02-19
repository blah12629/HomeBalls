namespace CEo.Pokemon.HomeBalls.App.Categories;

public interface IHomeBallsAppNavigation :
    IHomeBallsAppCateogry { }

public class HomeBallsAppNavigation :
    HomeBallsAppCategory,
    IHomeBallsAppNavigation
{
    public HomeBallsAppNavigation(ILogger? logger = default) : base(default, logger) { }

    protected internal override void GenerateIconSvgPaths(List<String> paths)
    {
        paths.Add("M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6");
    }
}