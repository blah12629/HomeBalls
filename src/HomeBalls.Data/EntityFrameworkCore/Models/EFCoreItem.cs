namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCoreItem :
        EFCoreNamedRecord<UInt16>,
        IHomeBallsItem
    {
        public virtual Byte CategoryId { get; init; }

        public virtual EFCoreItemCategory Category { get; init; }

        public virtual IEnumerable<EFCorePokemonForm> BabyTriggerFor { get; init; } =
            new List<EFCorePokemonForm> { };
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
        }

        protected internal virtual void ConfigureCategory() =>
            ConfigureLogged(() => Builder
                .HasOne(item => item.Category)
                .WithMany(category => category.Items)
                .HasForeignKey(item => item.CategoryId));
    }
}