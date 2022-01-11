namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCorePokemonTypeSlot :
        EFCorePokemonFormComponent,
        IHomeBallsPokemonTypeSlot
    {
        #nullable disable
        public EFCorePokemonTypeSlot() { }
        #nullable enable

        public virtual Byte TypeId { get; init; }

        public virtual EFCoreType Type { get; init; }
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public class EFCorePokemonTypeSlotConfiguration :
        EFCorePokemonFormComponentConfiguration<EFCorePokemonTypeSlot>
    {
        public EFCorePokemonTypeSlotConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal override Expression<Func<EFCorePokemonForm, IEnumerable<EFCorePokemonTypeSlot>?>> PokemonFormNavigation =>
            form => form.Types;

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureType();
        }

        protected internal virtual void ConfigureType() =>
            ConfigureLogged(() => Builder
                .HasOne(slot => slot.Type)
                .WithMany(type => type.OnForms)
                .HasForeignKey(slot => slot.TypeId));
    }
}