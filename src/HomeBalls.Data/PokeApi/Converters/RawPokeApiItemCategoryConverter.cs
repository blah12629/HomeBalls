namespace CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

public interface IRawPokeApiItemCategoryConverter
{
    EFCoreItemCategory Convert(RawPokeApiItemCategory item);

    IReadOnlyList<EFCoreItemCategory> Convert(
        IEnumerable<RawPokeApiItemCategory> itemCategories);
}

public class RawPokeApiItemCategoryConverter :
    RawPokeApiBaseConverter,
    IRawPokeApiItemCategoryConverter
{
    public RawPokeApiItemCategoryConverter(
        ILogger? logger = default) :
        base(logger) { }

    public virtual EFCoreItemCategory Convert(
        RawPokeApiItemCategory item) =>
        new EFCoreItemCategory { Id = item.Id, Identifier = item.Identifier };

    public IReadOnlyList<EFCoreItemCategory> Convert(
        IEnumerable<RawPokeApiItemCategory> itemCategories) =>
        ConvertCollection(itemCategories, Convert);
}
