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

    protected internal override void GenerateIconSvgPaths(List<String> paths)
    {
        paths.Add("M15.232 5.232l3.536 3.536m-2.036-5.036a2.5 2.5 0 113.536 3.536L6.5 21.036H3v-3.572L16.732 3.732z");
    }
}