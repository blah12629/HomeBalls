using CEo.Pokemon.HomeBalls.Entities;

namespace CEo.Pokemon.HomeBalls.Data.Entities.Configuration;

public abstract class HomeBallsEntityConfiguration
{
    String? _entityFullName;
    IReadOnlyList<Type>? _interfaces;

    protected HomeBallsEntityConfiguration(
        ILogger? logger = default) =>
        Logger = logger;

    protected internal ILogger? Logger { get; }

    protected internal abstract Type EntityType { get; }

    protected internal virtual String EntityFullName =>
        _entityFullName ??= EntityType.GetFullNameNonNull();

    protected internal virtual IReadOnlyList<Type> Interfaces =>
        _interfaces ??= Array.AsReadOnly(EntityType.GetInterfaces());
}

public abstract class HomeBallsEntityConfiguration<TEntity> :
    HomeBallsEntityConfiguration,
    IEntityTypeConfiguration<TEntity>
    where TEntity : class
{
    Type? _entityType;
    EntityTypeBuilder<TEntity>? _builder;

    protected HomeBallsEntityConfiguration(
        ILogger? logger = default) :
        base(logger) { }

    protected internal override Type EntityType  => _entityType ??= typeof(TEntity);

    protected internal EntityTypeBuilder<TEntity> Builder
    {
        get => _builder ?? throw new ArgumentException(
            $"`{GetType().GetFullNameNonNull()}.{nameof(Configure)}" +
            $"({nameof(EntityTypeBuilder)}<{EntityFullName}>)` " +
            "has not been called.");
        set => _builder = value;
    }

    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        Builder = builder;
        ConfigureCore();
    }

    protected internal virtual void ConfigureCore()
    {
        ConfigureKey();
        ConfiugreIdentifier();
        ConfigureNames();
    }

    protected internal virtual void ConfigureKey()
    {
        var hasKey = TryConfigureKey<Byte>() &&
            TryConfigureKey<UInt16>() &&
            TryConfigureKey<UInt32>();
    }

    protected internal virtual Boolean TryConfigureKey<TKey>()
        where TKey : notnull, IEquatable<TKey>
    {
        if (EntityType.IsAssignableTo(typeof(IKeyed<TKey>)))
        {
            Builder.HasKey(record => ((IKeyed<TKey>)record).Id);
            return true;
        }
        return false;
    }

    protected internal virtual void ConfiugreIdentifier()
    {
        if (EntityType.IsAssignableTo(typeof(IIdentifiable)))
            Builder.HasAlternateKey(record => ((IIdentifiable)record).Identifier);
    }

    protected internal virtual void ConfigureNames()
    {
        if (EntityType.IsAssignableTo(typeof(INamed<HomeBallsString>)))
            Builder.HasMany(record => ((INamed<HomeBallsString>)record).Names);
    }
}