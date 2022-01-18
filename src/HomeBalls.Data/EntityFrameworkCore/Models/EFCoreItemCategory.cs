namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCoreItemCategory :
        EFCoreRecord<Byte>,
        IHomeBallsItemCategory
    {
        #nullable disable
        public EFCoreItemCategory() { }
        #nullable enable
    
        public virtual IEnumerable<EFCoreItem> Items { get; init; } =
            new List<EFCoreItem> { };

        public virtual String Identifier { get; init; }
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