namespace CEo.Pokemon.HomeBalls.Data;

internal static class _Values
{
    static IPluralize? _pluralizer;

    public static IPluralize StaticPluralizer
    {
        get
        {
            if (_pluralizer == default)
            {
                _pluralizer = new Pluralizer();
                StaticPluralizer.AddIrregularRule("Evolution", "Evolution");
                StaticPluralizer.AddIrregularRule("Prose", "Prose");
                StaticPluralizer.AddIrregularRule("Pokemon", "Pokemon");
            }
            return _pluralizer;
        }
    }

    public const String PokeApiDataClientKey = "raw.github/pokeapi";

    public const String PokeApiDataBaseAddress =
        "https://raw.githubusercontent.com/" +
        "PokeAPI/pokeapi/master/data/v2/csv/";

    public const String ProjectPokemonHomeSpriteClientKey =
        "projectpokemon/sprites-models/homeimg";

    public const String ProjectPokemonHomeSpriteBaseAddress =
        "https://projectpokemon.org/images/sprites-models/homeimg/";

    public const String DefaultDataRoot = @".\data\";

    public const String DefaultCsvExtension = ".csv";

    public const String DefaultProtoBufExtension = ".bin";

    public const String DefaultSqliteRoot = $@"{DefaultDataRoot}sqlite\";

    public const String DefaultProtobufExportRoot = $@"{DefaultDataRoot}protobuf\";

    public const String DataConnectionString =
        $@"Data Source={DefaultSqliteRoot}homeballs.data.db;";

    public const String DataCacheConnectionString =
        $@"Data Source={DefaultSqliteRoot}homeballs.data.cache.db;";

    public const Byte PokeApiEnglishLanguageId = 9;

    public const UInt16 PokeApiPokeBallId = 4;
}