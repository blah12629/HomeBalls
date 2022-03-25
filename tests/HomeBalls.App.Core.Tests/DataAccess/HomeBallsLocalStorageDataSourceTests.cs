namespace CEo.Pokemon.HomeBalls.App.DataAccess.Tests;

public class HomeBallsLocalStorageDataSourceTests
{
    public HomeBallsLocalStorageDataSourceTests()
    {
        LocalStorage = Substitute.For<ILocalStorageService>();
        Downloader = Substitute.For<IHomeBallsLocalStorageDownloader>();
        Serializer = Substitute.For<IProtoBufSerializer>();
        EntryComparer = Substitute.For<IHomeBallsEntryKeyComparer>();
        PokemonComparer = Substitute.For<IHomeBallsPokemonFormKeyComparer>();
        ItemComparer = Substitute.For<IHomeBallsItemIdComparer>();
        Sut = new(LocalStorage, Downloader, Serializer, EntryComparer, PokemonComparer, ItemComparer);
    }

    protected ILocalStorageService LocalStorage { get; }

    protected IHomeBallsLocalStorageDownloader Downloader { get; }

    protected IProtoBufSerializer Serializer { get; }

    protected IHomeBallsEntryKeyComparer EntryComparer { get; }

    protected IHomeBallsPokemonFormKeyComparer PokemonComparer { get; }

    protected IHomeBallsItemIdComparer ItemComparer { get; }

    protected HomeBallsLocalStorageDataSource Sut { get; }

    // [Fact]
    // public void Property_ShouldHaveProtoContractElementType()
    // {
    //     var binding = BindingFlags.Instance | BindingFlags.NonPublic;
    //     var method = GetType().GetMethod(
    //         nameof(Property_ShouldHaveProtoContractElementType_Protected),
    //         binding);

    //     var entityTypes = Sut.GetType().GetProperties(binding)
    //         .Where(property => property.PropertyType
    //             .GetInterfaces()
    //             .Append(property.PropertyType)
    //             .Any(type =>
    //                 type.IsGenericType &&
    //                 type.GetGenericTypeDefinition() ==
    //                     typeof(IHomeBallsReadOnlyDataSet<, >)))
    //         .ToList().AsReadOnly();
    //     entityTypes.Should().NotBeEmpty();

    //     foreach (var property in entityTypes)
    //         method?.Invoke(this, new Object?[] { property.Name });
    // }

    // protected virtual void Property_ShouldHaveProtoContractElementType_Protected(
    //     String propertyName)
    // {
    //     var property = (dynamic)Sut.GetType()
    //         .GetProperty(
    //             propertyName,
    //             BindingFlags.Instance | BindingFlags.NonPublic)!
    //         .GetValue(Sut)!;

    //     (property.ElementType as Type)?
    //         .GetCustomAttribute<ProtoContractAttribute>()
    //         .Should().NotBeNull();
    // }
}