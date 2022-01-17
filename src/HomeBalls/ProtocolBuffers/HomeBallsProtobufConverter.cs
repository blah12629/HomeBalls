namespace CEo.Pokemon.HomeBalls.ProtocolBuffers;

public interface IHomeBallsProtobufConverter :
    IHomeBallsProtobufConverter<IHomeBallsEntryLegality, ProtobufEntryLegality>,
    IHomeBallsProtobufConverter<IHomeBallsEntry, ProtobufEntry>,
    IHomeBallsProtobufConverter<IHomeBallsGameVersion, ProtobufGameVersion>,
    IHomeBallsProtobufConverter<IHomeBallsGeneration, ProtobufGeneration>,
    IHomeBallsProtobufConverter<IHomeBallsItem, ProtobufItem>,
    IHomeBallsProtobufConverter<IHomeBallsItemCategory, ProtobufItemCategory>,
    IHomeBallsProtobufConverter<IHomeBallsLanguage, ProtobufLanguage>,
    IHomeBallsProtobufConverter<IHomeBallsMove, ProtobufMove>,
    IHomeBallsProtobufConverter<IHomeBallsMoveDamageCategory, ProtobufMoveDamageCategory>,
    IHomeBallsProtobufConverter<IHomeBallsNature, ProtobufNature>,
    IHomeBallsProtobufConverter<IHomeBallsPokemonAbility, ProtobufPokemonAbility>,
    IHomeBallsProtobufConverter<IHomeBallsPokemonEggGroup, ProtobufPokemonEggGroup>,
    IHomeBallsProtobufConverter<IHomeBallsPokemonForm, ProtobufPokemonForm>,
    IHomeBallsProtobufConverter<IHomeBallsPokemonSpecies, ProtobufPokemonSpecies>,
    IHomeBallsProtobufConverter<IHomeBallsStat, ProtobufStat>,
    IHomeBallsProtobufConverter<IHomeBallsType, ProtobufType>
{
    TResult Convert<TSource, TResult>(
        TSource source)
        where TSource : notnull, IHomeBallsEntity
        where TResult : notnull, ProtobufRecord, TSource;
        
    IReadOnlyList<TResult> Convert<TSource, TResult>(
        IEnumerable<TSource> source)
        where TSource : notnull, IHomeBallsEntity
        where TResult : notnull, ProtobufRecord, TSource;
}

public interface IHomeBallsProtobufConverter<TSource, TResult>
    where TSource : notnull, IHomeBallsEntity
    where TResult : notnull, ProtobufRecord, TSource
{
    TResult Convert(TSource source);

    IReadOnlyList<TResult> Convert(IEnumerable<TSource> sources);
}

public class HomeBallsProtobufConverter :
    IHomeBallsProtobufConverter
{
    public virtual ProtobufEntry Convert(IHomeBallsEntry source) => Convert<IHomeBallsEntry, ProtobufEntry>(source);

    public virtual IReadOnlyList<ProtobufEntry> Convert(IEnumerable<IHomeBallsEntry> sources) => Convert<IHomeBallsEntry, ProtobufEntry>(sources);

    public virtual ProtobufEntryLegality Convert(IHomeBallsEntryLegality source) => Convert<IHomeBallsEntryLegality, ProtobufEntryLegality>(source);

    public virtual IReadOnlyList<ProtobufEntryLegality> Convert(IEnumerable<IHomeBallsEntryLegality> sources) => Convert<IHomeBallsEntryLegality, ProtobufEntryLegality>(sources);

    public virtual ProtobufGameVersion Convert(IHomeBallsGameVersion source) => Convert<IHomeBallsGameVersion, ProtobufGameVersion>(source);

    public virtual IReadOnlyList<ProtobufGameVersion> Convert(IEnumerable<IHomeBallsGameVersion> sources) => Convert<IHomeBallsGameVersion, ProtobufGameVersion>(sources);

    public virtual ProtobufGeneration Convert(IHomeBallsGeneration source) => Convert<IHomeBallsGeneration, ProtobufGeneration>(source);

    public virtual IReadOnlyList<ProtobufGeneration> Convert(IEnumerable<IHomeBallsGeneration> sources) => Convert<IHomeBallsGeneration, ProtobufGeneration>(sources);

    public virtual ProtobufItem Convert(IHomeBallsItem source) => Convert<IHomeBallsItem, ProtobufItem>(source);

    public virtual IReadOnlyList<ProtobufItem> Convert(IEnumerable<IHomeBallsItem> sources) => Convert<IHomeBallsItem, ProtobufItem>(sources);

    public virtual ProtobufItemCategory Convert(IHomeBallsItemCategory source) => Convert<IHomeBallsItemCategory, ProtobufItemCategory>(source);

    public virtual IReadOnlyList<ProtobufItemCategory> Convert(IEnumerable<IHomeBallsItemCategory> sources) => Convert<IHomeBallsItemCategory, ProtobufItemCategory>(sources);

    public virtual ProtobufLanguage Convert(IHomeBallsLanguage source) => Convert<IHomeBallsLanguage, ProtobufLanguage>(source);

    public virtual IReadOnlyList<ProtobufLanguage> Convert(IEnumerable<IHomeBallsLanguage> sources) => Convert<IHomeBallsLanguage, ProtobufLanguage>(sources);

    public virtual ProtobufMove Convert(IHomeBallsMove source) => Convert<IHomeBallsMove, ProtobufMove>(source);

    public virtual IReadOnlyList<ProtobufMove> Convert(IEnumerable<IHomeBallsMove> sources) => Convert<IHomeBallsMove, ProtobufMove>(sources);

    public virtual ProtobufMoveDamageCategory Convert(IHomeBallsMoveDamageCategory source) => Convert<IHomeBallsMoveDamageCategory, ProtobufMoveDamageCategory>(source);

    public virtual IReadOnlyList<ProtobufMoveDamageCategory> Convert(IEnumerable<IHomeBallsMoveDamageCategory> sources) => Convert<IHomeBallsMoveDamageCategory, ProtobufMoveDamageCategory>(sources);

    public virtual ProtobufNature Convert(IHomeBallsNature source) => Convert<IHomeBallsNature, ProtobufNature>(source);

    public virtual IReadOnlyList<ProtobufNature> Convert(IEnumerable<IHomeBallsNature> sources) => Convert<IHomeBallsNature, ProtobufNature>(sources);

    public virtual ProtobufPokemonAbility Convert(IHomeBallsPokemonAbility source) => Convert<IHomeBallsPokemonAbility, ProtobufPokemonAbility>(source);

    public virtual IReadOnlyList<ProtobufPokemonAbility> Convert(IEnumerable<IHomeBallsPokemonAbility> sources) => Convert<IHomeBallsPokemonAbility, ProtobufPokemonAbility>(sources);

    public virtual ProtobufPokemonEggGroup Convert(IHomeBallsPokemonEggGroup source) => Convert<IHomeBallsPokemonEggGroup, ProtobufPokemonEggGroup>(source);

    public virtual IReadOnlyList<ProtobufPokemonEggGroup> Convert(IEnumerable<IHomeBallsPokemonEggGroup> sources) => Convert<IHomeBallsPokemonEggGroup, ProtobufPokemonEggGroup>(sources);

    public virtual ProtobufPokemonForm Convert(IHomeBallsPokemonForm source) => Convert<IHomeBallsPokemonForm, ProtobufPokemonForm>(source);

    public virtual IReadOnlyList<ProtobufPokemonForm> Convert(IEnumerable<IHomeBallsPokemonForm> sources) => Convert<IHomeBallsPokemonForm, ProtobufPokemonForm>(sources);

    public virtual ProtobufPokemonSpecies Convert(IHomeBallsPokemonSpecies source) => Convert<IHomeBallsPokemonSpecies, ProtobufPokemonSpecies>(source);

    public virtual IReadOnlyList<ProtobufPokemonSpecies> Convert(IEnumerable<IHomeBallsPokemonSpecies> sources) => Convert<IHomeBallsPokemonSpecies, ProtobufPokemonSpecies>(sources);

    public virtual ProtobufStat Convert(IHomeBallsStat source) => Convert<IHomeBallsStat, ProtobufStat>(source);

    public virtual IReadOnlyList<ProtobufStat> Convert(IEnumerable<IHomeBallsStat> sources) => Convert<IHomeBallsStat, ProtobufStat>(sources);

    public virtual ProtobufType Convert(IHomeBallsType source) => Convert<IHomeBallsType, ProtobufType>(source);

    public virtual IReadOnlyList<ProtobufType> Convert(IEnumerable<IHomeBallsType> sources) => Convert<IHomeBallsType, ProtobufType>(sources);

    public virtual TResult Convert<TSource, TResult>(
        TSource source)
        where TSource : notnull, IHomeBallsEntity
        where TResult : notnull, ProtobufRecord, TSource =>
        source.Adapt<TResult>();

    public virtual IReadOnlyList<TResult> Convert<TSource, TResult>(
        IEnumerable<TSource> source)
        where TSource : notnull, IHomeBallsEntity
        where TResult : notnull, ProtobufRecord, TSource =>
        source.Select(Convert<TSource, TResult>).ToList().AsReadOnly();
}