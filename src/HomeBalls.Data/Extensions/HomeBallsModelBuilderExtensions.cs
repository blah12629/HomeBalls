namespace CEo.Pokemon.HomeBalls.Data;

public static class HomeBallsModelBuilderExtensions
{
    static Lazy<MethodInfo> ApplyConfigurationMethod { get; } =
        new(() => typeof(ModelBuilder)
            .GetMethods(BindingFlags.Instance | BindingFlags.Public)
            .Single(method =>
                method.Name == nameof(ModelBuilder.ApplyConfiguration)));

    public static ModelBuilder ApplyConfiguration(
        this ModelBuilder builder,
        Object configuration)
    {
        foreach (var type in configuration.GetType().GetInterfaces())
            ApplyConfiguration(builder, configuration, type);
        return builder;
    }

    public static ModelBuilder ApplyConfiguration(
        this ModelBuilder builder,
        Object configuration,
        Type configurationInterface)
    {
        if (!(configurationInterface.IsInterface &&
            configurationInterface.IsGenericType &&
            configurationInterface.GetGenericTypeDefinition() ==
                typeof(IEntityTypeConfiguration<>)))
            return builder;

        var entityType = configurationInterface
            .GetGenericArguments()
            .Single();

        ApplyConfigurationMethod.Value
            .MakeGenericMethod(entityType)
            .Invoke(builder, new Object?[] { configuration });
        return builder;
    }
}