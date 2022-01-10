namespace CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

public abstract class RawPokeApiBaseConverter
{
    protected RawPokeApiBaseConverter(ILogger? logger) => Logger = logger;

    protected internal ILogger? Logger { get; }

    protected internal virtual IReadOnlyList<TRecord> ConvertCollection<TRecord, TRaw>(
        IEnumerable<TRaw> rawRecords,
        Func<TRaw, Int32, TRecord> conversion) =>
        rawRecords.Select(conversion).ToList();

    protected internal virtual IReadOnlyList<TRecord> ConvertCollection<TRecord, TRaw>(
        IEnumerable<TRaw> rawRecords,
        Func<TRaw, TRecord> conversion) =>
        rawRecords.Select(conversion).ToList();

    protected internal virtual TNumber Convert<TNumber>(Int32 number)
        where TNumber : struct, INumber<TNumber> =>
        TNumber.Create(number);

    protected internal virtual IReadOnlyDictionary<TKey, IReadOnlyList<TRecord>> ToDictionary<TRaw, TKey, TRecord>(
        IEnumerable<TRaw> rawRecords,
        Func<TRaw, TKey> keySelector,
        Func<IEnumerable<TRaw>, IReadOnlyList<TRecord>> conversion)
        where TKey : notnull =>
        rawRecords.GroupBy(keySelector).ToDictionary(
            (IGrouping<TKey, TRaw> group) => group.Key,
            conversion);

    protected internal virtual Byte ToTypeId(UInt16 id) =>
        Convert<Byte>(id <= Byte.MaxValue ? id : Byte.MaxValue - id % 10_000);
}

public abstract class RawPokeApiBaseNamedConverter : RawPokeApiBaseConverter
{
    protected RawPokeApiBaseNamedConverter(
        IRawPokeApiNameConverter nameConverter,
        ILogger? logger) :
        base(logger) =>
        NameConverter = nameConverter;

    protected internal IRawPokeApiNameConverter NameConverter { get; }

    protected internal IReadOnlyList<TRecord> AddNames<TRecord, TKey, TRawName>(
        IReadOnlyList<TRecord> records,
        IEnumerable<TRawName> names,
        Func<TRawName, TKey> keySelector,
        Func<TRecord, IReadOnlyList<EFCoreString>, TRecord> nameSetter)
        where TRecord : notnull, EFCoreRecord<TKey>, INamed
        where TKey : struct, INumber<TKey>
        where TRawName : notnull, RawPokeApiName =>
        AddNames<TRecord, TKey, TRawName>(
            records, names,
            keySelector,
            record => record.Id,
            names => NameConverter.Convert(names),
            nameSetter);

    protected internal IReadOnlyList<TRecord> AddNames<TRecord, TKey, TRawName>(
        IReadOnlyList<TRecord> records,
        IEnumerable<TRawName> names,
        Func<TRawName, TKey> nameKeySelector,
        Func<TRecord, TKey> recordKeySelector,
        Func<IEnumerable<TRawName>, IReadOnlyList<EFCoreString>> namesConversion,
        Func<TRecord, IReadOnlyList<EFCoreString>, TRecord> nameSetter)
        where TRecord : notnull, EFCoreBaseRecord, INamed
        where TKey : notnull
    {
        var namesLookup = ToDictionary(names, nameKeySelector, namesConversion);
        return ConvertCollection(records, setNames);

        TRecord setNames(TRecord record) =>
            namesLookup.TryGetValue(recordKeySelector(record), out var names) ?
                nameSetter(record, names) :
                record;
    }
}

public abstract class RawPokeApiRecordConverter<TRecord, TKey, TRaw, TRawName> :
    RawPokeApiBaseNamedConverter
    where TRecord : notnull, EFCoreRecord<TKey>, INamed
    where TKey : struct, INumber<TKey>
    where TRaw : notnull
    where TRawName : notnull, RawPokeApiName
{
    protected RawPokeApiRecordConverter(
        IRawPokeApiNameConverter nameConverter,
        ILogger? logger) :
        base(nameConverter, logger) { }

    protected internal abstract Func<TRawName, TKey> NameForeignKeySelector { get; }

    protected internal abstract Func<TRecord, IReadOnlyList<EFCoreString>, TRecord> RecordNamesSetter { get; }

    public abstract TRecord Convert(TRaw rawRecord);

    public virtual IReadOnlyList<TRecord> Convert(
        IEnumerable<TRaw> rawRecords) =>
        ConvertCollection(rawRecords, Convert);

    public virtual IReadOnlyList<TRecord> Convert(
        IEnumerable<TRaw> rawRecords,
        IEnumerable<TRawName> names) =>
        AddNames(Convert(rawRecords), names, NameForeignKeySelector, RecordNamesSetter);
}