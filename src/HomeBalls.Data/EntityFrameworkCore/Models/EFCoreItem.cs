namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCoreItem :
        EFCoreNamedRecord<UInt16>,
        IHomeBallsItem
    {
        #nullable disable
        public EFCoreItem() { }
        #nullable enable

        public virtual Byte CategoryId { get; init; }

        public virtual EFCoreItemCategory Category { get; init; }

        public virtual IEnumerable<EFCorePokemonForm> BabyTriggerFor { get; init; } =
            new List<EFCorePokemonForm> { };

        public virtual IEnumerable<EFCoreEntryLegality> LegalOn { get; init; } =
            new List<EFCoreEntryLegality> { };
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public class EFCoreItemConfiguration :
        EFCoreRecordConfiguration<EFCoreItem>
    {
        public EFCoreItemConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureCategory();
            ConfigureLegalOn();
        }

        protected internal virtual void ConfigureCategory() =>
            ConfigureLogged(() => Builder
                .HasOne(item => item.Category)
                .WithMany(category => category.Items)
                .HasForeignKey(item => item.CategoryId));

        protected internal virtual void ConfigureLegalOn() =>
            ConfigureLogged(() => Builder
                .HasMany(item => item.LegalOn)
                .WithOne(legality => legality.Ball)
                .HasForeignKey(legality => legality.BallId));
    }
}