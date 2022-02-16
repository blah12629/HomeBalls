namespace CEo.Pokemon.HomeBalls;

public static class HomeBallsServiceCollectionExtensions
{
    public static IServiceCollection Add<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, TService> factory,
        ServiceLifetime lifetime)
        where TService : class
    {
        services.Add(new ServiceDescriptor(
            typeof(TService),
            services => factory(services),
            lifetime));
        return services;
    }

    public static IServiceCollection Add<TService, TImplementation>(
        this IServiceCollection collection,
        Func<IServiceProvider, TImplementation> factory,
        ServiceLifetime lifetime)
        where TService : class
        where TImplementation : class, TService =>
        Add(collection, services => (TService)factory(services), lifetime);

    public static IServiceCollection Add<TService, TImplementation>(
        this IServiceCollection collection,
        ServiceLifetime lifetime)
        where TService : class
        where TImplementation : class, TService, new() =>
        Add(collection, services => new TImplementation(), lifetime);

    public static IServiceCollection AddHomeBallsServices(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Scoped) =>
        services.AddHomeBallsComparers(lifetime)
            .AddProtoBufSerializationServices(lifetime);
            // .AddHomeBallIdentifierServices(lifetime);

    public static IServiceCollection AddHomeBallsComparers(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Scoped) =>
        services
            .Add<IHomeBallsItemIdComparer>(services => HomeBallsItemIdComparer.Instance, lifetime)
            .Add<IHomeBallsPokemonFormKeyComparer>(services => HomeBallsPokemonFormKeyComparer.Instance, lifetime)
            .Add<IHomeBallsEntryKeyComparer>(services => HomeBallsEntryKeyComparer.Instance, lifetime)
            .Add<IHomeBallsEntityKeyComparer>(
                services => new HomeBallsEntityKeyComparer(
                    services.GetRequiredService<IHomeBallsItemIdComparer>(),
                    services.GetRequiredService<IHomeBallsPokemonFormKeyComparer>(),
                    services.GetRequiredService<IHomeBallsEntryKeyComparer>()),
                lifetime)
            .AddHomeBallsEntityKeyComparer<IHomeBallsEntry>(lifetime)
            .AddHomeBallsEntityKeyComparer<IHomeBallsEntryLegality>(lifetime)
            .AddHomeBallsEntityKeyComparer<IHomeBallsGameVersion>(lifetime)
            .AddHomeBallsEntityKeyComparer<IHomeBallsGeneration>(lifetime)
            .AddHomeBallsEntityKeyComparer<IHomeBallsItem>(lifetime)
            .AddHomeBallsEntityKeyComparer<IHomeBallsItemCategory>(lifetime)
            .AddHomeBallsEntityKeyComparer<IHomeBallsLanguage>(lifetime)
            .AddHomeBallsEntityKeyComparer<IHomeBallsMove>(lifetime)
            .AddHomeBallsEntityKeyComparer<IHomeBallsMoveDamageCategory>(lifetime)
            .AddHomeBallsEntityKeyComparer<IHomeBallsNature>(lifetime)
            .AddHomeBallsEntityKeyComparer<IHomeBallsPokemonAbility>(lifetime)
            .AddHomeBallsEntityKeyComparer<IHomeBallsPokemonEggGroup>(lifetime)
            .AddHomeBallsEntityKeyComparer<IHomeBallsPokemonForm>(lifetime)
            .AddHomeBallsEntityKeyComparer<IHomeBallsPokemonSpecies>(lifetime)
            .AddHomeBallsEntityKeyComparer<IHomeBallsStat>(lifetime)
            .AddHomeBallsEntityKeyComparer<IHomeBallsType>(lifetime);

    internal static IServiceCollection AddHomeBallsEntityKeyComparer<TEntity>(
        this IServiceCollection services,
        ServiceLifetime lifetime)
        where TEntity : class, IHomeBallsEntity =>
        services
            .Add<IHomeBallsBaseClassComparer<TEntity>>(
                services => (IHomeBallsBaseClassComparer<TEntity>)services.GetRequiredService<IHomeBallsEntityKeyComparer>(),
                lifetime)
            .Add<IComparer<TEntity>>(
                services => services.GetRequiredService<IHomeBallsBaseClassComparer<TEntity>>(),
                lifetime)
            .Add<IEqualityComparer<TEntity>>(
                services => services.GetRequiredService<IHomeBallsBaseClassComparer<TEntity>>(),
                lifetime);

    public static IServiceCollection AddProtoBufSerializationServices(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Scoped) =>
        services
            .Add<IProtoBufSerializer>(
                services => new ProtoBufSerializer(
                    services.GetService<ILogger<ProtoBufSerializer>>()),
                lifetime);

    // public static IServiceCollection AddHomeBallIdentifierServices(
    //     this IServiceCollection services,
    //     ServiceLifetime lifetime = ServiceLifetime.Scoped) =>
    //     services
    //         .Add<IHomeBallsDataIdentifierService>(services =>
    //             new HomeBallsDataIdentifierService(
    //                 services.GetRequiredService<IProtoBufSerializer>(),
    //                 services.GetService<ILogger<HomeBallsDataIdentifierService>>()),
    //             lifetime)
    //         .Add<IHomeBallsIdentifierService>(services =>
    //             services.GetRequiredService<IHomeBallsDataIdentifierService>(),
    //             lifetime);
}