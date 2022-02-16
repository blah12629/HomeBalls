namespace CEo.Pokemon.HomeBalls.App;

public interface IHomeBallsBreedablesSpriteService
{
    Uri GetSerebiiSpriteUri(IHomeBallsPokemonForm pokemon);

    Uri GetSpriteUri(IHomeBallsItem item);
}

public class HomeBallsBreedablesSpriteService :
    IHomeBallsBreedablesSpriteService
{
    public HomeBallsBreedablesSpriteService(
        ILogger? logger = default)
    {
        Logger = logger;

        SerebiiIdLookup = new Dictionary<HomeBallsPokemonFormKey, String> { };
    }

    protected internal ILogger? Logger { get; }

    protected internal IDictionary<HomeBallsPokemonFormKey, String> SerebiiIdLookup { get; }

    public virtual Uri GetSerebiiSpriteUri(IHomeBallsPokemonForm pokemon)
    {
        var id = GetSerebiiId(pokemon);
        return new Uri($"https://www.serebii.net/pokedex-swsh/icon/{id}.png");
    }

    public virtual Uri GetSpriteUri(IHomeBallsItem item) => new Uri(
        "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/items/" +
        $"{item.Identifier}.png");

    protected virtual String GetSerebiiId(IHomeBallsPokemonForm pokemon)
    {
        if (!pokemon.IsBreedable) throw new NotSupportedException();

        var key = pokemon.Id;
        if (SerebiiIdLookup.TryGetValue(key, out var value)) return value;

        var idPadded = pokemon.Id.SpeciesId.ToString().PadLeft(3, '0');
        if (pokemon.Id.FormId == 1)
        {
            SerebiiIdLookup.Add(key, idPadded);
            return idPadded;
        }

        if (pokemon.Identifier.Contains("-alola")) idPadded += "-a";
        else if (pokemon.Identifier.Contains("-galar")) idPadded += "-g";
        else if (pokemon.Id.SpeciesId == 422) idPadded += "-e";
        else if (pokemon.Id.SpeciesId == 550) idPadded += "-b";
        else if (pokemon.Id.SpeciesId == 669) idPadded += $@"-{pokemon.Id.FormId switch
        {
            2 => 'y', 3 => 'o', 4 => 'b', 5 => 'w',
            _ => throw new ArgumentException()
        }}";
        else if (pokemon.Id.SpeciesId == 710) idPadded += String.Empty;
        else if (pokemon.Id.SpeciesId == 744) idPadded += String.Empty;
        else if (pokemon.Id.SpeciesId == 774) idPadded += $@"-{pokemon.Id.FormId switch
        {
            8 => 'r', 9 => 'o', 10 => 'y', 11 => 'g', 12 => 'b', 13 => 'i', 14 => 'v',
            _ => throw new ArgumentException()
        }}";
        else if (pokemon.Id.SpeciesId == 876) idPadded += "-f";
        else Logger?.LogWarning($"No `SerebiiId` found for `{(key)}`.");

        SerebiiIdLookup.Add(key, idPadded);
        return idPadded;
    }
}