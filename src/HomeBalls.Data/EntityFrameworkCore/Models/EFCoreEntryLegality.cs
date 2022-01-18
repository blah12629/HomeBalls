namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore
{
    public record EFCoreEntryLegality :
        EFCoreRecord<HomeBallsEntryKey>,
        IHomeBallsEntryLegality
    {
        #nullable disable
        public EFCoreEntryLegality() { }
        #nullable enable

        public virtual UInt16 SpeciesId
        {
            get => Id.SpeciesId;
            init => Id = new(value, FormId, BallId);
        }

        public virtual Byte FormId
        {
            get => Id.FormId;
            init => Id = new(SpeciesId, value, BallId);
        }

        public virtual EFCorePokemonForm Form { get; init; }

        public virtual UInt16 BallId
        {
            get => Id.BallId;
            init => Id = new(SpeciesId, FormId, value);
        }

        public virtual EFCoreItem Ball { get; init; }

        public virtual Boolean IsObtainable { get; init; }

        public virtual Boolean IsObtainableWithHiddenAbility { get; init; }

        String IIdentifiable.Identifier => Id.ToString();
    }
}

namespace CEo.Pokemon.HomeBalls.Data.EntityFrameworkCore.Configurations
{
    public class EFCoreEntryLegalityConfiguration :
        EFCoreRecordConfiguration<EFCoreEntryLegality>
    {
        public EFCoreEntryLegalityConfiguration(
            IList<Expression<Action>>? configurations,
            ILogger? logger) :
            base(configurations, logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureId();
            ConfigureSpecies();
            ConfigureBall();
        }

        protected internal override void ConfigureKey() =>
            ConfigureLogged(() => Builder
                .HasKey(legality => new { legality.SpeciesId, legality.FormId, legality.BallId }));

        protected internal override void ConfigureIdentifier() { }

        protected internal virtual void ConfigureId() =>
            ConfigureLogged(() => Builder
                .Ignore(legality => legality.Id));

        protected internal virtual void ConfigureSpecies() =>
            ConfigureLogged(() => Builder
                .HasOne(legality => legality.Form)
                .WithMany(form => form.LegalOn)
                .HasForeignKey(legality => new { legality.SpeciesId, legality.FormId }));

        protected internal virtual void ConfigureBall() =>
            ConfigureLogged(() => Builder
                .HasOne(legality => legality.Ball)
                .WithMany(item => item.LegalOn)
                .HasForeignKey(legality => legality.BallId));
    }
}