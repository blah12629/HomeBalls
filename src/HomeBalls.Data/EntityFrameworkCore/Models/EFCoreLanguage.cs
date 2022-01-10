namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCoreLanguage :
        EFCoreNamedRecord<Byte>,
        IHomeBallsLanguage
    {
        public virtual IEnumerable<EFCoreString> Strings { get; init; } =
            new List<EFCoreString> { };
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public class EFCoreLanguageConfiguration :
        EFCoreRecordConfiguration<EFCoreLanguage>
    {
        public EFCoreLanguageConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureStrings();
        }

        protected internal virtual void ConfigureStrings() =>
            ConfigureLogged(() => Builder
                .HasMany(language => language.Strings)
                .WithOne(@string => @string.Language)
                .HasForeignKey(@string => @string.LanguageId));
    }
}