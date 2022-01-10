namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCorePokemonEggGroupSlot :
        EFCorePokemonFormComponent,
        IHomeBallsPokemonEggGroupSlot
    {
        public virtual Byte EggGroupId { get; init; }

        public virtual EFCorePokemonEggGroup EggGroup { get; init; }
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public class EFCorePokemonEggGroupSlotConfiguration :
        EFCorePokemonFormComponentConfiguration<EFCorePokemonEggGroupSlot>
    {
        public EFCorePokemonEggGroupSlotConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal override Expression<Func<EFCorePokemonForm, IEnumerable<EFCorePokemonEggGroupSlot>?>> PokemonFormNavigation =>
            form => form.EggGroups;

        protected internal override void ConfigureKey()
        {
            base.ConfigureKey();
            ConfigureEggGroup();
        }

        protected internal virtual void ConfigureEggGroup() =>
            ConfigureLogged(() => Builder
                .HasOne(slot => slot.EggGroup)
                .WithMany(group => group.OnForms)
                .HasForeignKey(slot => slot.EggGroupId));
    }
}