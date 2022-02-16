namespace CEo.Pokemon.HomeBalls.App;

public interface IHomeBallsBreedablesFormIdentifierService
{
    String GetIdentifier(IHomeBallsPokemonForm pokemon);
}

public class HomeBallsBreedablesFormIdentifierService :
    IHomeBallsBreedablesFormIdentifierService
{
    public HomeBallsBreedablesFormIdentifierService(
        ILogger? logger = default)
    {
        Logger = logger;

        IdentifierLookup = new Dictionary<HomeBallsPokemonFormKey, String> { };
    }

    protected internal ILogger? Logger { get; }

    protected internal IDictionary<HomeBallsPokemonFormKey, String> IdentifierLookup { get; }

    public virtual String GetIdentifier(IHomeBallsPokemonForm pokemon)
    {
        var key = pokemon.Id;
        if (IdentifierLookup.TryGetValue(key, out var existingValue))
            return existingValue;

        var identifier = pokemon.Identifier;
        String value = String.Empty;

        if (identifier.EndsWith("-alola")) value = "alola";
        else if (identifier.EndsWith("-galar")) value = "galar";
        else value = key.SpeciesId switch
        {
            422 => fromHyphen(),
            550 => key.FormId switch
            {
                1 => "red", 2 => "blue", _ => throw new ArgumentException()
            },
            669 => fromHyphen(),
            710 => fromHyphen(),
            744 => fromHyphen(),
            774 => fromHyphen(),
            876 => key.FormId switch
            {
                1 => "♂", 2 => "♀", _ => throw new ArgumentException()
            },
            _ => String.Empty
        };

        IdentifierLookup.Add(key, value);
        return value;

        String fromHyphen()
        {
            var i = identifier.IndexOf('-');
            return i < 0 ? String.Empty : identifier[(i + 1) ..];
        }
    }
}