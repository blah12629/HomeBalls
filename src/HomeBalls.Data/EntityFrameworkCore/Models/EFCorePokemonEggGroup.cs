namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCorePokemonEggGroup :
        EFCoreNamedRecord<Byte>,
        IHomeBallsPokemonEggGroup
    {
        public virtual IEnumerable<EFCorePokemonEggGroupSlot> OnForms { get; init; } =
            new List<EFCorePokemonEggGroupSlot> { };
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public class EFCorePokemonEggGroupConfiguration :
        EFCoreRecordConfiguration<EFCorePokemonEggGroup>
    {
        public EFCorePokemonEggGroupConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureOnForms();
        }

        protected internal virtual void ConfigureOnForms() =>
            ConfigureLogged(() => Builder
                .HasMany(group => group.OnForms)
                .WithOne(form => form.EggGroup)
                .HasForeignKey(form => form.EggGroupId));
    }
}