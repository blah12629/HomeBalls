namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCoreGameVersion :
        EFCoreNamedRecord<Byte>,
        IHomeBallsGameVersion
    {
        public virtual Byte GenerationId { get; init; }

        public virtual EFCoreGeneration Generation { get; init; }
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public class EFCoreGameVersionConfiguration :
        EFCoreRecordConfiguration<EFCoreGameVersion>
    {
        public EFCoreGameVersionConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureGeneration();
        }

        protected internal virtual void ConfigureGeneration() =>
            ConfigureLogged(() => Builder
                .HasOne(game => game.Generation)
                .WithMany(generation => generation.Games)
                .HasForeignKey(game => game.GenerationId));
    }
}