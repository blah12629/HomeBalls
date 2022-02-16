namespace CEo.Pokemon.HomeBalls.Comparers;

public interface IHomeBallsEntityKeyComparer :
    IHomeBallsBaseClassComparer<IHomeBallsEntry>,
    IHomeBallsBaseClassComparer<IHomeBallsEntryLegality>,
    IHomeBallsBaseClassComparer<IHomeBallsGameVersion>,
    IHomeBallsBaseClassComparer<IHomeBallsGeneration>,
    IHomeBallsBaseClassComparer<IHomeBallsItem>,
    IHomeBallsBaseClassComparer<IHomeBallsItemCategory>,
    IHomeBallsBaseClassComparer<IHomeBallsLanguage>,
    IHomeBallsBaseClassComparer<IHomeBallsMove>,
    IHomeBallsBaseClassComparer<IHomeBallsMoveDamageCategory>,
    IHomeBallsBaseClassComparer<IHomeBallsNature>,
    IHomeBallsBaseClassComparer<IHomeBallsPokemonAbility>,
    IHomeBallsBaseClassComparer<IHomeBallsPokemonEggGroup>,
    IHomeBallsBaseClassComparer<IHomeBallsPokemonForm>,
    IHomeBallsBaseClassComparer<IHomeBallsPokemonSpecies>,
    IHomeBallsBaseClassComparer<IHomeBallsStat>,
    IHomeBallsBaseClassComparer<IHomeBallsType>,
    IPredefinedInstance<IHomeBallsEntityKeyComparer> { }

public class HomeBallsEntityKeyComparer :
    IHomeBallsEntityKeyComparer,
    IPredefinedInstance<HomeBallsEntityKeyComparer>
{
    public static HomeBallsEntityKeyComparer Instance { get; } = new(
        HomeBallsItemIdComparer.Instance,
        HomeBallsPokemonFormKeyComparer.Instance,
        HomeBallsEntryKeyComparer.Instance);

    static IHomeBallsEntityKeyComparer IPredefinedInstance<IHomeBallsEntityKeyComparer>.Instance => Instance;

    public HomeBallsEntityKeyComparer(
        IHomeBallsBaseStructComparer<UInt16> itemIdComparer,
        IHomeBallsBaseClassComparer<HomeBallsPokemonFormKey> pokemonFormKeyComparer,
        IHomeBallsBaseClassComparer<HomeBallsEntryKey> entryKeyComparer)
    {
        ItemIdComparer = itemIdComparer;
        PokemonFormKeyComparer = pokemonFormKeyComparer;
        EntryKeyComparer = entryKeyComparer;
    }

    protected internal IHomeBallsBaseStructComparer<UInt16> ItemIdComparer { get; }

    protected internal IHomeBallsBaseClassComparer<HomeBallsPokemonFormKey> PokemonFormKeyComparer { get; }

    protected internal IHomeBallsBaseClassComparer<HomeBallsEntryKey> EntryKeyComparer { get; }

    public virtual Int32 Compare(IHomeBallsEntry? x, IHomeBallsEntry? y) => Compare(x?.Id, y?.Id);

    public virtual Boolean Equals(IHomeBallsEntry? x, IHomeBallsEntry? y) => Equals(x?.Id, y?.Id);

    public virtual Int32 GetHashCode([DisallowNull] IHomeBallsEntry? obj) => GetHashCode(obj?.Id);

    public virtual Int32 Compare(IHomeBallsEntryLegality? x, IHomeBallsEntryLegality? y) => Compare(x?.Id, y?.Id);

    public virtual Boolean Equals(IHomeBallsEntryLegality? x, IHomeBallsEntryLegality? y) => Equals(x?.Id, y?.Id);

    public virtual Int32 GetHashCode([DisallowNull] IHomeBallsEntryLegality? obj) => GetHashCode(obj?.Id);

    public virtual Int32 Compare(IHomeBallsGameVersion? x, IHomeBallsGameVersion? y) => Compare(x?.Id, y?.Id);

    public virtual Boolean Equals(IHomeBallsGameVersion? x, IHomeBallsGameVersion? y) => Equals(x?.Id, y?.Id);

    public virtual Int32 GetHashCode([DisallowNull] IHomeBallsGameVersion? obj) => GetHashCode(obj?.Id);

    public virtual Int32 Compare(IHomeBallsGeneration? x, IHomeBallsGeneration? y) => Compare(x?.Id, y?.Id);

    public virtual Boolean Equals(IHomeBallsGeneration? x, IHomeBallsGeneration? y) => Equals(x?.Id, y?.Id);

    public virtual Int32 GetHashCode([DisallowNull] IHomeBallsGeneration? obj) => GetHashCode(obj?.Id);

    public virtual Int32 Compare(IHomeBallsItem? x, IHomeBallsItem? y) => ItemIdComparer.Compare(x?.Id, y?.Id);

    public virtual Boolean Equals(IHomeBallsItem? x, IHomeBallsItem? y) => ItemIdComparer.Equals(x?.Id, y?.Id);

    public virtual Int32 GetHashCode([DisallowNull] IHomeBallsItem? obj) => obj == default ? 0 : ItemIdComparer.GetHashCode(obj.Id);

    public virtual Int32 Compare(IHomeBallsItemCategory? x, IHomeBallsItemCategory? y) => Compare(x?.Id, y?.Id);

    public virtual Boolean Equals(IHomeBallsItemCategory? x, IHomeBallsItemCategory? y) => Equals(x?.Id, y?.Id);

    public virtual Int32 GetHashCode([DisallowNull] IHomeBallsItemCategory? obj) => GetHashCode(obj?.Id);

    public virtual Int32 Compare(IHomeBallsLanguage? x, IHomeBallsLanguage? y) => Compare(x?.Id, y?.Id);

    public virtual Boolean Equals(IHomeBallsLanguage? x, IHomeBallsLanguage? y) => Equals(x?.Id, y?.Id);

    public virtual Int32 GetHashCode([DisallowNull] IHomeBallsLanguage? obj) => GetHashCode(obj?.Id);

    public virtual Int32 Compare(IHomeBallsMove? x, IHomeBallsMove? y) => Compare(x?.Id, y?.Id);

    public virtual Boolean Equals(IHomeBallsMove? x, IHomeBallsMove? y) => Equals(x?.Id, y?.Id);

    public virtual Int32 GetHashCode([DisallowNull] IHomeBallsMove? obj) => GetHashCode(obj?.Id);

    public virtual Int32 Compare(IHomeBallsMoveDamageCategory? x, IHomeBallsMoveDamageCategory? y) => Compare(x?.Id, y?.Id);

    public virtual Boolean Equals(IHomeBallsMoveDamageCategory? x, IHomeBallsMoveDamageCategory? y) => Equals(x?.Id, y?.Id);

    public virtual Int32 GetHashCode([DisallowNull] IHomeBallsMoveDamageCategory? obj) => GetHashCode(obj?.Id);

    public virtual Int32 Compare(IHomeBallsNature? x, IHomeBallsNature? y) => Compare(x?.Id, y?.Id);

    public virtual Boolean Equals(IHomeBallsNature? x, IHomeBallsNature? y) => Equals(x?.Id, y?.Id);

    public virtual Int32 GetHashCode([DisallowNull] IHomeBallsNature? obj) => GetHashCode(obj?.Id);

    public virtual Int32 Compare(IHomeBallsPokemonAbility? x, IHomeBallsPokemonAbility? y) => Compare(x?.Id, y?.Id);

    public virtual Boolean Equals(IHomeBallsPokemonAbility? x, IHomeBallsPokemonAbility? y) => Equals(x?.Id, y?.Id);

    public virtual Int32 GetHashCode([DisallowNull] IHomeBallsPokemonAbility? obj) => GetHashCode(obj?.Id);

    public virtual Int32 Compare(IHomeBallsPokemonEggGroup? x, IHomeBallsPokemonEggGroup? y) => Compare(x?.Id, y?.Id);

    public virtual Boolean Equals(IHomeBallsPokemonEggGroup? x, IHomeBallsPokemonEggGroup? y) => Equals(x?.Id, y?.Id);

    public virtual Int32 GetHashCode([DisallowNull] IHomeBallsPokemonEggGroup? obj) => GetHashCode(obj?.Id);

    public virtual Int32 Compare(IHomeBallsPokemonForm? x, IHomeBallsPokemonForm? y) => Compare(x?.Id, y?.Id);

    public virtual Boolean Equals(IHomeBallsPokemonForm? x, IHomeBallsPokemonForm? y) => Equals(x?.Id, y?.Id);

    public virtual Int32 GetHashCode([DisallowNull] IHomeBallsPokemonForm? obj) => GetHashCode(obj?.Id);

    public virtual Int32 Compare(IHomeBallsPokemonSpecies? x, IHomeBallsPokemonSpecies? y) => Compare(x?.Id, y?.Id);

    public virtual Boolean Equals(IHomeBallsPokemonSpecies? x, IHomeBallsPokemonSpecies? y) => Equals(x?.Id, y?.Id);

    public virtual Int32 GetHashCode([DisallowNull] IHomeBallsPokemonSpecies? obj) => GetHashCode(obj?.Id);

    public virtual Int32 Compare(IHomeBallsStat? x, IHomeBallsStat? y) => Compare(x?.Id, y?.Id);

    public virtual Boolean Equals(IHomeBallsStat? x, IHomeBallsStat? y) => Equals(x?.Id, y?.Id);

    public virtual Int32 GetHashCode([DisallowNull] IHomeBallsStat? obj) => GetHashCode(obj?.Id);

    public virtual Int32 Compare(IHomeBallsType? x, IHomeBallsType? y) => Compare(x?.Id, y?.Id);

    public virtual Boolean Equals(IHomeBallsType? x, IHomeBallsType? y) => Equals(x?.Id, y?.Id);

    public virtual Int32 GetHashCode([DisallowNull] IHomeBallsType? obj) => GetHashCode(obj?.Id);

    protected internal virtual Int32 Compare<TStruct>(TStruct? x, TStruct? y) where TStruct : struct => Nullable.Compare(x, y);

    protected internal virtual Int32 Compare(HomeBallsEntryKey? x, HomeBallsEntryKey? y) => EntryKeyComparer.Compare(x, y);

    protected internal virtual Int32 Compare(HomeBallsPokemonFormKey? x, HomeBallsPokemonFormKey? y) => PokemonFormKeyComparer.Compare(x, y);

    protected internal virtual Boolean Equals<TStruct>(TStruct? x, TStruct? y) where TStruct : struct => Nullable.Equals(x, y);

    protected internal virtual Boolean Equals(HomeBallsEntryKey? x, HomeBallsEntryKey? y) => EntryKeyComparer.Equals(x, y);

    protected internal virtual Boolean Equals(HomeBallsPokemonFormKey? x, HomeBallsPokemonFormKey? y) => PokemonFormKeyComparer.Equals(x, y);

    protected internal virtual Int32 GetHashCode<TStruct>(TStruct? id) where TStruct : struct => id.HasValue ? id.Value.GetHashCode() : 0;

    protected internal virtual Int32 GetHashCode(HomeBallsEntryKey? id) => id == default ? 0 : EntryKeyComparer.GetHashCode(id);

    protected internal virtual Int32 GetHashCode(HomeBallsPokemonFormKey? id) => id == default ? 0 : PokemonFormKeyComparer.GetHashCode(id);
}