namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCoreString :
        EFCoreBaseRecord,
        IKeyed<UInt32>,
        IHomeBallsString
    {
        #nullable disable
        public EFCoreString() { }
        #nullable enable

        public virtual UInt32 Id { get; init; }

        public virtual Byte LanguageId { get; init; }

        public virtual EFCoreLanguage Language { get; init; }

        public virtual String Value { get; init; }
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public class EFCoreStringConfiguration :
        EFCoreRecordConfiguration<EFCoreString>
    {
        public EFCoreStringConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureLanguage();
        }

        protected internal virtual void ConfigureLanguage() =>
            ConfigureLogged(() => Builder
                .HasOne(@string => @string.Language)
                .WithMany(language => language.Strings)
                .HasForeignKey(@string => @string.LanguageId));
    }
}