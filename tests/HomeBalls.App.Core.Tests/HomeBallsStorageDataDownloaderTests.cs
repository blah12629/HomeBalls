namespace CEo.Pokemon.HomeBalls.App.Core.DataAccess.Tests;

public class HomeBallsLocalStorageDataDownloaderTests
{
    public HomeBallsLocalStorageDataDownloaderTests()
    {
        LocalFileSystem = new FileSystem();
        LocalStorage = Substitute.For<ILocalStorageService>();
        Logger = Substitute.For<ILogger<HomeBallsLocalStorageDataDownloader>>();
    }

    protected IFileSystem LocalFileSystem { get; }

    protected ILocalStorageService LocalStorage { get; }

    protected ILogger Logger { get; }

    protected HomeBallsLocalStorageDataDownloader Sut => CreateSut(new HttpClient());

    protected HomeBallsLocalStorageDataDownloader CreateSut(HttpClient dataClient) =>
        new HomeBallsLocalStorageDataDownloader(dataClient, LocalStorage, Logger);

    [Fact]
    public Task DownloadAsync_ShouldRaiseDataDownloading_WhenDownloadFails() =>
        DownloadAsync_WhenDonwloadFails(monitor => monitor.Should()
            .Raise(nameof(HomeBallsLocalStorageDataDownloader.DataDownloading)));

    [Fact]
    public Task DownloadAsync_ShouldNotRaiseDataDownloaded_WhenDownloadFails() =>
        DownloadAsync_WhenDonwloadFails(monitor => monitor.Should()
            .NotRaise(nameof(HomeBallsLocalStorageDataDownloader.DataDownloaded)));

    protected async Task DownloadAsync_WhenDonwloadFails(
        Action<IMonitor<HomeBallsLocalStorageDataDownloader>> assertions)
    {
        var sut = Sut;
        var monitor = sut.Monitor();

        Func<Task> action = () => sut.DownloadAsync(new[] { String.Empty }, default);
        await action.Should().ThrowAsync<Exception>();

        assertions(monitor);
    }

    [Fact]
    public Task DownloadAsync_ShouldRaiseAllDataDownloadActionEvents_WhenSuccessful() =>
        DownloadAsync_WhenSuccessful((identifier, fileName, monitor) =>
        {
            monitor.Should().Raise(nameof(HomeBallsLocalStorageDataDownloader.DataDownloading));
            monitor.Should().Raise(nameof(HomeBallsLocalStorageDataDownloader.DataDownloaded));
        });

    [Fact]
    public Task DownloadAsync_ShouldSetLocalStorageItems_WhenSuccessful() =>
        DownloadAsync_WhenSuccessful((identifier, fileName, monitor) =>
            LocalStorage.Received(1).SetItemAsync<String>(
                identifier,
                Arg.Any<String>(),
                Arg.Any<CancellationToken>()));

    protected async Task DownloadAsync_WhenSuccessful(
        Action<String, String, IMonitor<HomeBallsLocalStorageDataDownloader>> assertions)
    {
        var (identifier, fileName) = (
            "CEo.Pokemon.HomeBalls.IHomeBallsGameVersion",
            "CEo.Pokemon.HomeBalls.IHomeBallsGameVersion.bin");
        var filePath = LocalFileSystem.Path.Join(ProtobufDataRoot, fileName);
        var cancellationToken = default(CancellationToken);

        var returnValue = await LocalFileSystem.File
            .ReadAllBytesAsync(filePath, cancellationToken);
        var handler = new MockRawDataMessageHandler(new ByteArrayContent(returnValue));
        var sutClient = new HttpClient(handler);
        sutClient.BaseAddress = new Uri("https://fake.address.com");
        var sut = CreateSut(sutClient);
        var monitor = sut.Monitor();

        await sut.DownloadAsync(new[] { (identifier, fileName) }, cancellationToken);

        assertions(identifier, fileName, monitor);
    }
}