namespace CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

public interface IRawPokeApiItemConverter
{
    EFCoreItem Convert(RawPokeApiItem item);

    IReadOnlyList<EFCoreItem> Convert(
        IEnumerable<RawPokeApiItem> items);

    IReadOnlyList<EFCoreItem> Convert(
        IEnumerable<RawPokeApiItem> items,
        IEnumerable<RawPokeApiItemName> names);
}

public class RawPokeApiItemConverter :
    RawPokeApiRecordConverter<EFCoreItem, UInt16, RawPokeApiItem, RawPokeApiItemName>,
    IRawPokeApiItemConverter
{
    public RawPokeApiItemConverter(
        IRawPokeApiNameConverter nameConverter,
        ILogger? logger = default) :
        base(nameConverter, logger) { }

    protected internal override Func<RawPokeApiItemName, UInt16> NameForeignKeySelector =>
        name => name.ItemId;

    protected internal override Func<EFCoreItem, IReadOnlyList<EFCoreString>, EFCoreItem> RecordNamesSetter =>
        (item, names) => item with { Names = names };

    public override EFCoreItem Convert(
        RawPokeApiItem rawRecord) =>
        new EFCoreItem
        {
            CategoryId = rawRecord.CategoryId,
            Id = rawRecord.Id,
            Identifier = rawRecord.Identifier
        };
}