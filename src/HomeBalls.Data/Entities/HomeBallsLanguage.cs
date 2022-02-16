namespace CEo.Pokemon.HomeBalls.Data.Entities
{
    public record HomeBallsLanguage :
        HomeBalls.Entities.HomeBallsLanguage,
        HomeBalls.Entities.INamed<HomeBallsString>,
        IHomeBallsDataType<HomeBalls.Entities.HomeBallsLanguage>
    {
        public HomeBallsLanguage()
        {
            base.Names = new List<HomeBallsString> { };
            Strings = new List<HomeBallsString> { };
        }

        public static Type BaseEntityType { get; } =
            typeof(HomeBalls.Entities.HomeBallsLanguage);

        new public virtual IEnumerable<HomeBallsString> Names
        {
            get => (IEnumerable<HomeBallsString>)base.Names;
            init => base.Names = value;
        }

        public virtual IEnumerable<HomeBallsString> Strings { get; init; }

        public virtual HomeBalls.Entities.HomeBallsLanguage ToBaseType() =>
            this.AdaptNamed<HomeBalls.Entities.HomeBallsLanguage>();
    }
}

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration
{
    public class HomeBallsLanguageConfiguration :
        HomeBallsEntityConfiguration<HomeBallsLanguage>
    {
        public HomeBallsLanguageConfiguration(
            ILogger? logger = default) :
            base(logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureStrings();
        }

        protected internal virtual void ConfigureStrings() =>
            Builder.HasMany(language => language.Strings)
                .WithOne(@string => @string.Language)
                .HasForeignKey(@string => @string.LanguageId);
    }
}