namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCorePokemonAbility :
        EFCoreNamedRecord<UInt16>,
        IHomeBallsPokemonAbility
    {
        public virtual IEnumerable<EFCorePokemonAbilitySlot> OnForms { get; init; } =
            new List<EFCorePokemonAbilitySlot> { };
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public class EFCorePokemonAbilityConfiguration :
        EFCoreRecordConfiguration<EFCorePokemonAbility>
    {
        public EFCorePokemonAbilityConfiguration(
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
                .HasMany(ability => ability.OnForms)
                .WithOne(form => form.Ability)
                .HasForeignKey(form => form.AbilityId));
    }
}