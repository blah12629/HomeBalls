namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCorePokemonAbilitySlot :
        EFCorePokemonFormComponent,
        IHomeBallsPokemonAbilitySlot
    {
        public virtual UInt16 AbilityId { get; init; }

        public virtual EFCorePokemonAbility Ability { get; init; }

        public virtual Boolean IsHidden { get; init; }
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public class EFCorePokemonAbilitySlotConfiguration :
        EFCorePokemonFormComponentConfiguration<EFCorePokemonAbilitySlot>
    {
        public EFCorePokemonAbilitySlotConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal override Expression<Func<EFCorePokemonForm, IEnumerable<EFCorePokemonAbilitySlot>?>> PokemonFormNavigation =>
            form => form.Abilities;

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureAbility();
        }

        protected internal virtual void ConfigureAbility() =>
            ConfigureLogged(() => Builder
                .HasOne(slot => slot.Ability)
                .WithMany(ability => ability.OnForms)
                .HasForeignKey(slot => slot.AbilityId));
    }
}