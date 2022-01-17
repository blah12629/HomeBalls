namespace CEo.Pokemon.HomeBalls.App.Core.DataAccess.Tests;

public class HomeBallsLocalStorageDataSetTests
{
    [Fact]
    public void EnsureLoadedAsync_ShouldDeserializeToDataSet() =>
        throw new NotImplementedException();
}

// public class HomeBallsLocalStorageDataSourceTests
// {
//     public HomeBallsLocalStorageDataSourceTests(ITestOutputHelper output)
//     {
//         Output = output;

//         LocalFileSystem = new FileSystem();
//         LocalStorage = Substitute.For<ILocalStorageService>();
//         Downlaoder = Substitute.For<IHomeBallsLocalStorageDownloader>();
//         TypeMap = Substitute.ForPartsOf<HomeBallsProtobufTypeMap>();
//         Logger = Substitute.For<ILogger<HomeBallsLocalStorageDataSource>>();
//         Sut = new(LocalStorage, Downlaoder, TypeMap, Logger);
//     }

//     protected IFileSystem LocalFileSystem { get; }

//     protected ILocalStorageService LocalStorage { get; }

//     protected IHomeBallsLocalStorageDownloader Downlaoder { get; }

//     protected IHomeBallsProtobufTypeMap TypeMap { get; }

//     protected ILogger Logger { get; }

//     protected HomeBallsLocalStorageDataSource Sut { get; }

//     protected ITestOutputHelper Output { get; }

//     protected async Task EnsureLoadedAsync_ShouldDeserializeToDataSet_NonTest<TKey, TRecord>(
//         Func<TRecord, TKey>? keySelector)
//         where TKey : notnull, IEquatable<TKey>
//         where TRecord : notnull, IKeyed<TKey>, IIdentifiable
//     {
//         var dataSet = new HomeBallsDataSet<TKey, TRecord>();
//         var identifier = dataSet.ElementType.GetFullNameNonNull();
//         var fileName = identifier.AddFileExtension(_Values.DefaultProtobufExtension);
//         var filePath = LocalFileSystem.Path.Join(ProtobufDataRoot, fileName);

//         var bytes = await LocalFileSystem.File.ReadAllBytesAsync(filePath);
//         var returnValue = Convert.ToBase64String(bytes);
//         var returnValueTask = ValueTask.FromResult(returnValue);
//         LocalStorage.GetItemAsync<String>(identifier, Arg.Any<CancellationToken>())
//             .ReturnsForAnyArgs(returnValueTask);
//         LocalStorage.GetItemAsStringAsync(identifier, Arg.Any<CancellationToken>())
//             .ReturnsForAnyArgs(returnValueTask);

//         await Sut.EnsureLoadedAsync(dataSet);
//         dataSet.Should().NotBeEmpty();
//     }

//     [
//         Theory,
//         InlineData(typeof(Byte), typeof(IHomeBallsGameVersion)),
//         InlineData(typeof(Byte), typeof(IHomeBallsGeneration)),
//         InlineData(typeof(UInt16), typeof(IHomeBallsItem)),
//         InlineData(typeof(Byte), typeof(IHomeBallsItemCategory)),
//         InlineData(typeof(Byte), typeof(IHomeBallsLanguage)),
//         InlineData(typeof(UInt16), typeof(IHomeBallsMove)),
//         InlineData(typeof(Byte), typeof(IHomeBallsMoveDamageCategory)),
//         InlineData(typeof(Byte), typeof(IHomeBallsNature)),
//         InlineData(typeof(UInt16), typeof(IHomeBallsPokemonAbility)),
//         InlineData(typeof(Byte), typeof(IHomeBallsPokemonEggGroup)),
//         InlineData(typeof(UInt16), typeof(IHomeBallsPokemonSpecies)),
//         InlineData(typeof(Byte), typeof(IHomeBallsStat)),
//         InlineData(typeof(Byte), typeof(IHomeBallsType)),
//     ]
//     public Task EnsureLoadedAsync_ShouldDeserializeToDataSet(
//         Type key,
//         Type record) =>
//         (this.GetType()
//             .GetMethod(
//                 nameof(EnsureLoadedAsync_ShouldDeserializeToDataSet_NonTest),
//                 BindingFlags.Instance | BindingFlags.NonPublic)!
//             .MakeGenericMethod(key, record)
//             .Invoke(this, new Object?[] { default(Func<Object, Object>) })! as Task)!;

//     [Fact]
//     public Task EnsureLoadedAsync_ShouldDeserializeToDataSet_WhenRecordIsPokemonForm() =>
//         EnsureLoadedAsync_ShouldDeserializeToDataSet_NonTest<(UInt16, Byte), IHomeBallsPokemonForm>(
//             form => (form.SpeciesId, form.FormId));
// }