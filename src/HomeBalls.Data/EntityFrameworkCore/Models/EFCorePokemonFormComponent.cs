namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public abstract record EFCorePokemonFormComponent :
        EFCoreBaseRecord,
        IKeyed<UInt32>,
        IHomeBallsEntityComponent
    {
        #nullable disable
        protected EFCorePokemonFormComponent() { }
        #nullable enable

        public virtual UInt32 Id { get; init; }

        public virtual UInt16 SpeciesId { get; init; }

        public virtual Byte FormId { get; init; }

        public virtual EFCorePokemonForm Form { get; init; }

        public virtual Byte Slot { get; init; }

        dynamic IKeyed.Id => Id;
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public class EFCorePokemonFormComponentConfiguration :
        EFCoreRecordConfiguration<EFCorePokemonFormComponent>
    {
        public EFCorePokemonFormComponentConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureDiscriminator();
        }

        protected internal virtual void ConfigureDiscriminator() =>
            ConfigureLogged(() => Builder
                .HasDiscriminator<Byte>("Discriminator")
                .HasValue<EFCorePokemonAbilitySlot>(1)
                .HasValue<EFCorePokemonEggGroupSlot>(2)
                .HasValue<EFCorePokemonTypeSlot>(3));
    }

    public abstract class EFCorePokemonFormComponentConfiguration<TRecord> :
        EFCoreRecordConfiguration<TRecord>
        where TRecord : notnull, EFCorePokemonFormComponent
    {
        protected EFCorePokemonFormComponentConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal abstract Expression<Func<EFCorePokemonForm, IEnumerable<TRecord>?>> PokemonFormNavigation { get; }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureForm();
        }

        protected internal override void ConfigureKey() { }

        protected internal override void ConfigureIdentifier() { }

        protected internal virtual void ConfigureForm() =>
            ConfigureLogged(() => Builder
                .HasOne(component => component.Form)
                .WithMany(PokemonFormNavigation)
                .HasForeignKey(component => new { component.SpeciesId, component.FormId }));
    }
}