namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCoreItemCategory :
        EFCoreRecord<Byte>,
        IHomeBallsItemCategory
    {
        public virtual IEnumerable<EFCoreItem> Items { get; init; } =
            new List<EFCoreItem> { };
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public class EFCoreItemCategoryConfiguration :
        EFCoreRecordConfiguration<EFCoreItemCategory>
    {
        public EFCoreItemCategoryConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureItems();
        }

        protected internal virtual void ConfigureItems() =>
            ConfigureLogged(() => Builder
                .HasMany(category => category.Items)
                .WithOne(item => item.Category)
                .HasForeignKey(item => item.CategoryId));
    }
}