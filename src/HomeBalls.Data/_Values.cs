namespace CEo.Pokemon.HomeBalls.Data;

internal static class _Values
{
    static IPluralize? _pluralizer;

    public static IPluralize Pluralizer
    {
        get
        {
            if (_pluralizer == default)
            {
                _pluralizer = new Pluralizer();
                Pluralizer.AddIrregularRule("Evolution", "Evolution");
                Pluralizer.AddIrregularRule("Prose", "Prose");
                Pluralizer.AddIrregularRule("Pokemon", "Pokemon");
            }
            return _pluralizer;
        }
    }

    public const String DefaultDataRoot = @".\data\";

    public const String DefaultCsvExtension = ".csv";

    public const String DefaultProtobufExportRoot = @".\data_protobuf\";
}