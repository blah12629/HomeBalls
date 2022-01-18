namespace CEo.Pokemon.HomeBalls.App.Core.DataAccess.Tests;

public class HomeBallsLocalStorageDataSetTests
{
    public HomeBallsLocalStorageDataSetTests()
    {
        FileSystem = new FileSystem();
        LocalStorage = Substitute.For<ILocalStorageService>();
        TypeMap = new HomeBallsProtobufTypeMap();
        Downloader = Substitute.For<IHomeBallsLocalStorageDownloader>();
    }

    protected IFileSystem FileSystem { get; }

    protected ILocalStorageService LocalStorage { get; }

    protected IHomeBallsProtobufTypeMap TypeMap { get; }

    protected IHomeBallsLocalStorageDownloader Downloader { get; }

    [Fact]
    public async Task EnsureLoadedAsync_ShouldDeserializeToDataSet()
    {
        var sut = new HomeBallsLocalStorageDataSet<HomeBallsEntryKey, IHomeBallsEntryLegality>(
            LocalStorage,
            TypeMap,
            Downloader);

        var identifier = sut.ElementType.GetFullNameNonNull();
        var fileName = identifier.AddFileExtension(_Values.DefaultProtobufExtension);
        var filePath = FileSystem.Path.Join(ProtobufDataRoot, fileName);
        var returnValue = Convert.ToBase64String(await FileSystem.File
            .ReadAllBytesAsync(filePath));

        LocalStorage.Configure()
            .ContainKeyAsync(identifier, Arg.Any<CancellationToken>())
            .Returns(ValueTask.FromResult(true));

        LocalStorage.Configure()
            .GetItemAsync<String>(identifier, Arg.Any<CancellationToken>())
            .Returns(ValueTask.FromResult(returnValue));

        await sut.EnsureLoadedAsync();
        sut.Should().NotBeEmpty();
    }
}