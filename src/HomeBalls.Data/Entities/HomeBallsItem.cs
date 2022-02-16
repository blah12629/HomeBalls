#nullable disable

namespace CEo.Pokemon.HomeBalls.Data.Entities
{
    public record HomeBallsItem :
        HomeBalls.Entities.HomeBallsItem,
        HomeBalls.Entities.INamed<HomeBallsString>,
        IHomeBallsDataType<HomeBalls.Entities.HomeBallsItem>
    {
        public HomeBallsItem()
        {
            base.Names = new List<HomeBallsString> { };
            BabyTriggerFor = new List<HomeBallsPokemonForm> { };
            LegalOn = new List<HomeBallsEntryLegality> { };
        }

        public static Type BaseEntityType { get; } =
            typeof(HomeBalls.Entities.HomeBallsItem);

        new public virtual IEnumerable<HomeBallsString> Names
        {
            get => (IEnumerable<HomeBallsString>)base.Names;
            init => base.Names = value;
        }

        public virtual HomeBallsItemCategory Category { get; init; }

        public virtual IEnumerable<HomeBallsPokemonForm> BabyTriggerFor { get; init; }

        public virtual IEnumerable<HomeBallsEntryLegality> LegalOn { get; init; }

        public virtual HomeBalls.Entities.HomeBallsItem ToBaseType() =>
            this.AdaptNamed<HomeBalls.Entities.HomeBallsItem>();
    }
}

#nullable enable

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration
{
    public class HomeBallsItemConfiguration :
        HomeBallsEntityConfiguration<HomeBallsItem>
    {
        public HomeBallsItemConfiguration(
            ILogger? logger = default) :
            base(logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureCategory();
            ConfigureBabyTriggerFor();
            ConfigureLegalOn();
        }

        protected internal virtual void ConfigureCategory() =>
            Builder.HasOne(item => item.Category)
                .WithMany(category => category.Items)
                .HasForeignKey(item => item.CategoryId);

        protected internal virtual void ConfigureBabyTriggerFor() =>
            Builder.HasMany(item => item.BabyTriggerFor)
                .WithOne(pokemon => pokemon.BabyTrigger)
                .HasForeignKey(pokemon => pokemon.BabyTriggerId);

        protected internal virtual void ConfigureLegalOn() =>
            Builder.HasMany(item => item.LegalOn)
                .WithOne(legality => legality.Ball)
                .HasForeignKey(legality => legality.BallId);
    }
}