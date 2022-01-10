namespace CEo.Pokemon.HomeBalls.Data.PokeApi.Converters;

public interface IRawPokeApiLanguageConverter
{
    EFCoreLanguage Convert(RawPokeApiLanguage language);

    IReadOnlyList<EFCoreLanguage> Convert(IEnumerable<RawPokeApiLanguage> languages);

    IReadOnlyList<EFCoreLanguage> Convert(
        IEnumerable<RawPokeApiLanguage> languages,
        IEnumerable<RawPokeApiLanguageName> names);
}

public class RawPokeApiLanguageConverter :
    RawPokeApiRecordConverter<EFCoreLanguage, Byte, RawPokeApiLanguage, RawPokeApiLanguageName>,
    IRawPokeApiLanguageConverter
{
    public RawPokeApiLanguageConverter(
        IRawPokeApiNameConverter nameConverter,
        ILogger? logger = default) :
        base(nameConverter, logger) { }

    protected internal override Func<RawPokeApiLanguageName, Byte> NameForeignKeySelector =>
        name => name.LanguageId;

    protected internal override Func<EFCoreLanguage, IReadOnlyList<EFCoreString>, EFCoreLanguage> RecordNamesSetter =>
        (language, names) => language with { Names = names };

    public override EFCoreLanguage Convert(
        RawPokeApiLanguage language) =>
        new EFCoreLanguage
        {
            Id = language.Id,
            Identifier = language.Identifier
        };
}