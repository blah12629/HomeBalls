namespace CEo.Pokemon.HomeBalls.Data.Entities
{
    public record HomeBallsString :
        HomeBalls.Entities.HomeBallsString,
        HomeBalls.Entities.IKeyed<UInt32>,
        IHomeBallsDataType<HomeBalls.Entities.HomeBallsString>
    {
        #nullable disable
        public HomeBallsString() { }
        #nullable enable

        public static Type BaseEntityType { get; } =
            typeof(HomeBalls.Entities.HomeBallsString);

        public virtual UInt32 Id { get; init; }

        public virtual HomeBallsLanguage Language { get; init; }

        public virtual HomeBallsGameVersion? NameOfGameVersion { get; init; }

        public virtual Byte? NameOfGameVersionId { get; init; }

        public virtual HomeBallsGeneration? NameOfGeneration { get; init;}

        public virtual Byte? NameOfGenerationId { get; init; }

        public virtual HomeBallsItem? NameOfItem { get; init; }

        public virtual UInt16? NameOfItemId { get; init; }

        public virtual HomeBallsLanguage? NameOfLanguage { get; init; }

        public virtual Byte? NameOfLanguageId { get; init; }

        public virtual HomeBallsMove? NameOfMove { get; init; }

        public virtual UInt16? NameOfMoveId { get; init; }

        public virtual HomeBallsMoveDamageCategory? NameOfMoveDamageCategory { get; init; }

        public virtual Byte? NameOfMoveDamageCategoryId { get; init; }

        public virtual HomeBallsNature? NameOfNature { get; init; }

        public virtual Byte? NameOfNatureId { get; init; }

        public virtual HomeBallsPokemonAbility? NameOfPokemonAbility { get; init; }

        public virtual UInt16? NameOfPokemonAbilityId { get; init; }

        public virtual HomeBallsPokemonEggGroup? NameOfPokemonEggGroup { get; init; }

        public virtual Byte? NameOfPokemonEggGroupId { get; init; }

        public virtual HomeBallsPokemonForm? NameOfPokemonForm { get; init; }

        public virtual UInt16? NameOfPokemonFormSpeciesId { get; init; }

        public virtual Byte? NameOfPokemonFormFormId { get; init; }

        public virtual HomeBallsPokemonSpecies? NameOfPokemonSpecies { get; init; }

        public virtual UInt16? NameOfPokemonSpeciesId { get; init; }

        public virtual HomeBallsStat? NameOfStat { get; init; }

        public virtual Byte? NameOfStatId { get; init; }

        public virtual HomeBallsType? NameOfType { get; init; }

        public virtual Byte? NameOfTypeId { get; init; }

        public virtual HomeBalls.Entities.HomeBallsString ToBaseType() =>
            this.Adapt<HomeBalls.Entities.HomeBallsString>();
    }
}

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration
{
    public class HomeBallsStringConfiguration :
        HomeBallsEntityConfiguration<HomeBallsString>
    {
        public HomeBallsStringConfiguration(
            ILogger? logger = default) :
            base(logger) { }

        protected internal override void ConfigureCore()
        {
            base.ConfigureCore();
            ConfigureLanguage();
            // ConfigureNameOfGameVersion();
            ConfigureNameOfGeneration();
            ConfigureNameOfItem();
            ConfigureNameOfLanguage();
            ConfigureNameOfMove();
            ConfigureNameOfMoveDamageCategory();
            ConfigureNameOfNature();
            ConfigureNameOfPokemonAbility();
            ConfigureNameOfPokemonEggGroup();
            ConfigureNameOfPokemonForm();
            ConfigureNameOfPokemonSpecies();
            ConfigureNameOfStat();
            ConfigureNameOfType();
        }

        protected internal virtual void ConfigureNameOf<TForeignEntity>(
            Expression<Func<HomeBallsString, TForeignEntity?>> hasOne,
            Expression<Func<HomeBallsString, Object?>> hasForeignKey)
            where TForeignEntity : class, HomeBalls.Entities.INamed<HomeBallsString> =>
            Builder.HasOne(hasOne)
                .WithMany(entity => entity.Names)
                .HasForeignKey(hasForeignKey);

        protected internal virtual void ConfigureLanguage() =>
            Builder.HasOne(@string => @string.Language)
                .WithMany(language => language.Strings)
                .HasForeignKey(@string => @string.LanguageId);

        // protected internal virtual void ConfigureNameOfGameVersion() =>
        //     ConfigureNameOf(
        //         name => name.NameOfGameVersion,
        //         name => name.NameOfGameVersionId);

        protected internal virtual void ConfigureNameOfGeneration() =>
            ConfigureNameOf(
                name => name.NameOfGeneration,
                name => name.NameOfGenerationId);

        protected internal virtual void ConfigureNameOfItem() =>
            ConfigureNameOf(
                name => name.NameOfItem,
                name => name.NameOfItemId);

        protected internal virtual void ConfigureNameOfLanguage() =>
            ConfigureNameOf(
                name => name.NameOfLanguage,
                name => name.NameOfLanguageId);

        protected internal virtual void ConfigureNameOfMove() =>
            ConfigureNameOf(
                name => name.NameOfMove,
                name => name.NameOfMoveId);

        protected internal virtual void ConfigureNameOfMoveDamageCategory() =>
            ConfigureNameOf(
                name => name.NameOfMoveDamageCategory,
                name => name.NameOfMoveDamageCategoryId);

        protected internal virtual void ConfigureNameOfNature() =>
            ConfigureNameOf(
                name => name.NameOfNature,
                name => name.NameOfNatureId);

        protected internal virtual void ConfigureNameOfPokemonAbility() =>
            ConfigureNameOf(
                name => name.NameOfPokemonAbility,
                name => name.NameOfPokemonAbilityId);

        protected internal virtual void ConfigureNameOfPokemonEggGroup() =>
            ConfigureNameOf(
                name => name.NameOfPokemonEggGroup,
                name => name.NameOfPokemonEggGroupId);

        protected internal virtual void ConfigureNameOfPokemonForm() =>
            ConfigureNameOf(
                name => name.NameOfPokemonForm,
                name => new
                {
                    name.NameOfPokemonFormSpeciesId,
                    name.NameOfPokemonFormFormId
                });

        protected internal virtual void ConfigureNameOfPokemonSpecies() =>
            ConfigureNameOf(
                name => name.NameOfPokemonSpecies,
                name => name.NameOfPokemonSpeciesId);

        protected internal virtual void ConfigureNameOfStat() =>
            ConfigureNameOf(
                name => name.NameOfStat,
                name => name.NameOfStatId);

        protected internal virtual void ConfigureNameOfType() =>
            ConfigureNameOf(
                name => name.NameOfType,
                name => name.NameOfTypeId);
    }
}