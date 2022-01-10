namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCoreGeneration :
        EFCoreNamedRecord<Byte>,
        IHomeBallsGeneration
    {
        public virtual IEnumerable<EFCoreGameVersion> Games { get; init; } =
            new List<EFCoreGameVersion> { };
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public class EFCoreGenerationConfiguration :
        EFCoreRecordConfiguration<EFCoreGeneration>
    {
        public EFCoreGenerationConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureGames();
        }

        protected internal virtual void ConfigureGames() =>
            ConfigureLogged(() => Builder
                .HasMany(generation => generation.Games)
                .WithOne(game => game.Generation)
                .HasForeignKey(game => game.GenerationId));
    }
}