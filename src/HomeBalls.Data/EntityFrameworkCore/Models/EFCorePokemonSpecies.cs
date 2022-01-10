namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCorePokemonSpecies :
        EFCoreNamedRecord<UInt16>,
        IHomeBallsPokemonSpecies
    {
        public virtual SByte GenderRate { get; init; }

        public virtual Boolean IsFormSwitchable { get; init; }

        public virtual IEnumerable<EFCorePokemonForm> Forms { get; init; } =
            new List<EFCorePokemonForm> { };
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public class EFCorePokemonSpeciesConfiguration :
        EFCoreRecordConfiguration<EFCorePokemonSpecies>
    {
        public EFCorePokemonSpeciesConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureForms();
        }

        protected internal virtual void ConfigureForms() =>
            ConfigureLogged(() => Builder
                .HasMany(species => species.Forms)
                .WithOne(form => form.Species)
                .HasForeignKey(form => form.SpeciesId));
    }
}