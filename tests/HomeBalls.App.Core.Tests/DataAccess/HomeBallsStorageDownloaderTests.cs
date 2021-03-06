// namespace CEo.Pokemon.HomeBalls.App.DataAccess.Tests;

// public class HomeBallsLocalStorageDownloaderTests
// {
//     public HomeBallsLocalStorageDownloaderTests()
//     {
//         LocalFileSystem = new FileSystem();
//         LocalStorage = Substitute.For<ILocalStorageService>();
//         Logger = Substitute.For<ILogger<HomeBallsLocalStorageDownloader>>();
//     }

//     protected IFileSystem LocalFileSystem { get; }

//     protected ILocalStorageService LocalStorage { get; }

//     protected ILogger Logger { get; }

//     protected HomeBallsLocalStorageDownloader Sut => CreateSut(new HttpClient());

//     protected HomeBallsLocalStorageDownloader CreateSut(HttpClient dataClient) =>
//         new HomeBallsLocalStorageDownloader(dataClient, LocalStorage, Logger);

//     [Fact]
//     public Task DownloadAsync_ShouldRaiseDataDownloading_WhenDownloadFails() =>
//         DownloadAsync_WhenDonwloadFails(monitor => monitor.Should()
//             .Raise(nameof(HomeBallsLocalStorageDownloader.DataDownloading)));

//     [Fact]
//     public Task DownloadAsync_ShouldNotRaiseDataDownloaded_WhenDownloadFails() =>
//         DownloadAsync_WhenDonwloadFails(monitor => monitor.Should()
//             .NotRaise(nameof(HomeBallsLocalStorageDownloader.DataDownloaded)));

//     protected async Task DownloadAsync_WhenDonwloadFails(
//         Action<IMonitor<HomeBallsLocalStorageDownloader>> assertions)
//     {
//         var sut = Sut;
//         var monitor = sut.Monitor();

//         Func<Task> action = () => sut.DownloadAsync(new[] { String.Empty }, default);
//         await action.Should().ThrowAsync<Exception>();

//         assertions(monitor);
//     }

//     [Fact]
//     public Task DownloadAsync_ShouldRaiseAllDataDownloadActionEvents_WhenSuccessful() =>
//         DownloadAsync_WhenSuccessful((identifier, fileName, monitor) =>
//         {
//             monitor.Should().Raise(nameof(HomeBallsLocalStorageDownloader.DataDownloading));
//             monitor.Should().Raise(nameof(HomeBallsLocalStorageDownloader.DataDownloaded));
//         });

//     [Fact]
//     public Task DownloadAsync_ShouldSetLocalStorageItems_WhenSuccessful() =>
//         DownloadAsync_WhenSuccessful((identifier, fileName, monitor) =>
//             LocalStorage.Received(1).SetItemAsync<String>(
//                 identifier,
//                 Arg.Any<String>(),
//                 Arg.Any<CancellationToken>()));

//     protected async Task DownloadAsync_WhenSuccessful(
//         Action<String, String, IMonitor<HomeBallsLocalStorageDownloader>> assertions)
//     {
//         var (identifier, fileName) = (
//             "CEo.Pokemon.HomeBalls.IHomeBallsGameVersion",
//             "CEo.Pokemon.HomeBalls.IHomeBallsGameVersion.bin");
//         var filePath = LocalFileSystem.Path.Join(ProtobufDataRoot, fileName);
//         var cancellationToken = default(CancellationToken);

//         var returnValue = await LocalFileSystem.File
//             .ReadAllBytesAsync(filePath, cancellationToken);
//         var handler = new MockRawDataMessageHandler(new ByteArrayContent(returnValue));
//         var sutClient = new HttpClient(handler);
//         sutClient.BaseAddress = new Uri("https://fake.address.com");
//         var sut = CreateSut(sutClient);
//         var monitor = sut.Monitor();

//         await sut.DownloadAsync(new[] { (identifier, fileName) }, cancellationToken);

//         assertions(identifier, fileName, monitor);
//     }
// }