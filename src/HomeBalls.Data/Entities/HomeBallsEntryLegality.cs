#nullable disable

namespace CEo.Pokemon.HomeBalls.Data.Entities
{
    public record HomeBallsEntryLegality :
        HomeBalls.Entities.HomeBallsEntryLegality,
        IHomeBallsDataType<HomeBalls.Entities.HomeBallsEntryLegality>
    {
        public static Type BaseEntityType { get; } =
            typeof(HomeBalls.Entities.HomeBallsEntryLegality);

        public virtual UInt16 SpeciesId
        {
            get => Id.SpeciesId;
            init => Id = (value, Id.FormId, Id.BallId);
        }

        public virtual Byte FormId
        {
            get => Id.FormId;
            init => Id = (Id.SpeciesId, value, Id.BallId);
        }

        public virtual HomeBallsPokemonForm PokemonForm { get; init; }

        public virtual UInt16 BallId
        {
            get => Id.BallId;
            init => Id = (Id.SpeciesId, Id.FormId, value);
        }

        public virtual HomeBallsItem Ball { get; init; }

        public virtual HomeBalls.Entities.HomeBallsEntryLegality ToBaseType() =>
            this.Adapt<HomeBalls.Entities.HomeBallsEntryLegality>();
    }
}

#nullable enable

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration
{
    public class HomeBallsEntryLegalityConfiguration :
        HomeBallsEntityConfiguration<HomeBallsEntryLegality>
    {
        public HomeBallsEntryLegalityConfiguration(
            ILogger? logger = default) :
            base(logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureId();
            ConfigurePokemonForm();
            ConfigureBall();
        }

        protected internal override void ConfigureKey() =>
            Builder.HasKey(legality => new
            {
                legality.SpeciesId,
                legality.FormId,
                legality.BallId
            });

        protected internal override void ConfiugreIdentifier() { }

        protected internal virtual void ConfigureId() =>
            Builder.Ignore(legality => legality.Id);

        protected internal virtual void ConfigurePokemonForm() =>
            Builder.HasOne(legality => legality.PokemonForm)
                .WithMany(form => form.LegalOn)
                .HasForeignKey(legality => new
                {
                    legality.SpeciesId,
                    legality.FormId
                });

        protected internal virtual void ConfigureBall() =>
            Builder.HasOne(legality => legality.Ball)
                .WithMany(ball => ball.LegalOn)
                .HasForeignKey(legality => legality.BallId);
    }
}