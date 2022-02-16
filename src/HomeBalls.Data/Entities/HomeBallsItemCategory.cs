namespace CEo.Pokemon.HomeBalls.Data.Entities
{
    public record HomeBallsItemCategory :
        HomeBalls.Entities.HomeBallsItemCategory,
        IHomeBallsDataType<HomeBalls.Entities.HomeBallsItemCategory>
    {
        public HomeBallsItemCategory() =>
            Items = new List<HomeBallsItem> { };

        public static Type BaseEntityType { get; } =
            typeof(HomeBalls.Entities.HomeBallsItemCategory);

        public virtual IEnumerable<HomeBallsItem> Items { get; init; }

        public virtual HomeBalls.Entities.HomeBallsItemCategory ToBaseType() =>
            this.Adapt<HomeBalls.Entities.HomeBallsItemCategory>();
    }
}

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration
{
    public class HomeBallsItemCategoryConfiguration :
        HomeBallsEntityConfiguration<HomeBallsItemCategory>
    {
        public HomeBallsItemCategoryConfiguration(
            ILogger? logger = default) :
            base(logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureItems();
        }

        protected internal virtual void ConfigureItems() =>
            Builder.HasMany(category => category.Items)
                .WithOne(item => item.Category)
                .HasForeignKey(item => item.CategoryId);
    }
}