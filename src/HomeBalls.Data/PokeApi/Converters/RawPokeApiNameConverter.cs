namespace CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

public interface IRawPokeApiNameConverter
{
    EFCoreString Convert(RawPokeApiName name);

    IReadOnlyList<EFCoreString> Convert(IEnumerable<RawPokeApiName> name);

    EFCoreString? Convert(RawPokeApiPokemonFormName name);

    IReadOnlyList<EFCoreString> Convert(IEnumerable<RawPokeApiPokemonFormName> names);
}

public class RawPokeApiNameConverter :
    RawPokeApiBaseConverter,
    IRawPokeApiNameConverter
{
    public RawPokeApiNameConverter(ILogger? logger = default) : base(logger) { }

    public virtual EFCoreString Convert(RawPokeApiName name) =>
        new EFCoreString
        {
            Value = name.Name,
            LanguageId = name.LocalLanguageId
        };

    public virtual IReadOnlyList<EFCoreString> Convert(
        IEnumerable<RawPokeApiName> name) =>
        ConvertCollection(name, Convert);

    public virtual EFCoreString? Convert(RawPokeApiPokemonFormName name) =>
        name.FormName == default ? default : Convert(new RawPokeApiName
        {
            Name = name.FormName,
            LocalLanguageId = name.LocalLanguageId
        });

    public virtual IReadOnlyList<EFCoreString> Convert(
        IEnumerable<RawPokeApiPokemonFormName> names) =>
        ConvertCollection(names, Convert)
            .Where(name => !String.IsNullOrWhiteSpace(name?.Value))
            .Select(name => name!)
            .ToList();
}