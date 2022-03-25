// namespace CEo.Pokemon.HomeBalls.App.DataAccess.Tests;

// public class HomeBallsLocalStorageDataSetTests :
//     HomeBallsLocalStorageDataSetTests<HomeBallsEntryKey, IHomeBallsEntryLegality> { }

// public abstract class HomeBallsLocalStorageDataSetTests<TKey, TRecord> :
//     HomeBallsLoadableDataSetTests<TKey, TRecord>
//     where TKey : notnull, IEquatable<TKey>
//     where TRecord : notnull, IKeyed<TKey>, IIdentifiable
// {
//     public HomeBallsLocalStorageDataSetTests() : base()
//     {
//         FileSystem = new FileSystem();
//         LocalStorage = Substitute.For<ILocalStorageService>();
//         TypeMap = new HomeBallsProtobufTypeMap();
//         Downloader = Substitute.For<IHomeBallsLocalStorageDownloader>();
//         Sut = new(LocalStorage, TypeMap, Downloader, DataSet, default);
//     }

//     protected IFileSystem FileSystem { get; }

//     protected ILocalStorageService LocalStorage { get; }

//     protected IHomeBallsProtobufTypeMap TypeMap { get; }

//     protected IHomeBallsLocalStorageDownloader Downloader { get; }

//     new protected HomeBallsLocalStorageDataSet<TKey, TRecord> Sut { get; }

//     [Fact]
//     public async Task EnsureLoadedAsync_ShouldDeserializeToDataSet()
//     {
//         var identifier = Sut.ElementType.GetFullNameNonNull();
//         var fileName = identifier.AddFileExtension(DefaultProtobufExtension);
//         var filePath = FileSystem.Path.Join(ProtobufDataRoot, fileName);
//         var returnValue = Convert.ToBase64String(await FileSystem.File
//             .ReadAllBytesAsync(filePath));

//         LocalStorage.Configure()
//             .ContainKeyAsync(identifier, Arg.Any<CancellationToken>())
//             .Returns(ValueTask.FromResult(true));

//         LocalStorage.Configure()
//             .GetItemAsync<String>(identifier, Arg.Any<CancellationToken>())
//             .Returns(ValueTask.FromResult(returnValue));

//         await Sut.EnsureLoadedAsync();
//         Sut.Should().NotBeEmpty();
//     }
// }