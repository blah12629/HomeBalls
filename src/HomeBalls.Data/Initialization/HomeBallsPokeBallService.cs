namespace CEo.Pokemon.HomeBalls.Data.Initialization;

public interface IHomeBallsPokeBallService
{
    HomeBallsItem MarkItem(HomeBallsItem item);

    HomeBallsItem MarkWhenItemIsPokeBall(HomeBallsItem item);

    HomeBallsItem MarkWhenItemIsDefaultBreedableBall(HomeBallsItem item);
}

public class HomeBallsPokeBallService : IHomeBallsPokeBallService
{
    public HomeBallsPokeBallService(
        ILogger? logger = default) =>
        Logger = logger;

    protected internal ILogger? Logger { get; }

    public virtual HomeBallsItem MarkItem(HomeBallsItem item) =>
        MarkWhenItemIsDefaultBreedableBall(item);

    public virtual HomeBallsItem MarkWhenItemIsDefaultBreedableBall(HomeBallsItem item)
    {
        item = MarkWhenItemIsPokeBall(item) with
        {
            IsDefaultBreedableBall = item.CategoryId == 39
        };

        return item.IsDefaultBreedableBall ? item : item with
        {
            IsDefaultBreedableBall = new UInt16[] { 5, 457, 617, 887 }.Contains(item.Id)
        };
    }

    public virtual HomeBallsItem MarkWhenItemIsPokeBall(HomeBallsItem item) =>
        item with { IsPokeBall = item.Identifier.Contains("ball") };
}