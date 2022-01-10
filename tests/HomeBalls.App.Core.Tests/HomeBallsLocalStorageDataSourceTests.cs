namespace CEo.Pokemon.HomeBalls.App.Core.DataAccess.Tests;

public class HomeBallsLocalStorageDataSourceTests
{
    public HomeBallsLocalStorageDataSourceTests()
    {
        LocalFileSystem = new FileSystem();
        LocalStorage = Substitute.For<ILocalStorageService>();
        Downlaoder = Substitute.For<IHomeBallsLocalStorageDataDownloader>();
        TypeMap = Substitute.ForPartsOf<HomeBallsProtobufTypeMap>();
        Logger = Substitute.For<ILogger<HomeBallsLocalStorageDataSourceTests>>();
        Sut = new(LocalStorage, Downlaoder, TypeMap, Logger);
    }

    protected IFileSystem LocalFileSystem { get; }

    protected ILocalStorageService LocalStorage { get; }

    protected IHomeBallsLocalStorageDataDownloader Downlaoder { get; }

    protected IHomeBallsProtobufTypeMap TypeMap { get; }

    protected ILogger Logger { get; }

    protected HomeBallsLocalStorageDataSource Sut { get; }

    protected String DataRootDirectory => @"..\..\..\..\..\src\Pokemon.HomeBalls.Data\data_protobuf";

    protected async Task EnsureLoadedAsync_ShouldDeserializeToDataSet_NonTest<TKey, TRecord>(
        Func<TRecord, TKey>? keySelector)
        where TKey : notnull
        where TRecord : notnull, IKeyed, IIdentifiable
    {
        var dataSet =
            keySelector == default ?
                new HomeBallsDataSet<TKey, TRecord>() :
                new HomeBallsDataSet<TKey, TRecord>(keySelector, record =>
                    ((dynamic)record).Identifier);
        var cancellationToken = default(CancellationToken);
        var identifier = dataSet.ElementType.GetFullNameNonNull();
        var fileName = identifier.AddFileExtension(_Values.DefaultProtobufExtension);
        var filePath = LocalFileSystem.Path.Join(DataRootDirectory, fileName);

        var returnValue = Convert.ToBase64String(
            await LocalFileSystem.File.ReadAllBytesAsync(filePath));
        LocalStorage.GetItemAsStringAsync(identifier, cancellationToken)
            .ReturnsForAnyArgs(ValueTask.FromResult(returnValue));

        await Sut.EnsureLoadedAsync(dataSet, cancellationToken);
        dataSet.Should().NotBeEmpty();
    }

    [
        Theory,
        InlineData(typeof(Byte), typeof(IHomeBallsGameVersion)),
        InlineData(typeof(Byte), typeof(IHomeBallsGeneration)),
        InlineData(typeof(UInt16), typeof(IHomeBallsItem)),
        InlineData(typeof(Byte), typeof(IHomeBallsItemCategory)),
        InlineData(typeof(Byte), typeof(IHomeBallsLanguage)),
        InlineData(typeof(UInt16), typeof(IHomeBallsMove)),
        InlineData(typeof(Byte), typeof(IHomeBallsMoveDamageCategory)),
        InlineData(typeof(Byte), typeof(IHomeBallsNature)),
        InlineData(typeof(UInt16), typeof(IHomeBallsPokemonAbility)),
        InlineData(typeof(Byte), typeof(IHomeBallsPokemonEggGroup)),
        InlineData(typeof(UInt16), typeof(IHomeBallsPokemonSpecies)),
        InlineData(typeof(Byte), typeof(IHomeBallsStat)),
        InlineData(typeof(Byte), typeof(IHomeBallsType)),
    ]
    public Task EnsureLoadedAsync_ShouldDeserializeToDataSet(
        Type key,
        Type record) =>
        (this.GetType()
            .GetMethod(
                nameof(EnsureLoadedAsync_ShouldDeserializeToDataSet_NonTest),
                BindingFlags.Instance | BindingFlags.NonPublic)!
            .MakeGenericMethod(key, record)
            .Invoke(this, new Object?[] { default(Func<Object, Object>) })! as Task)!;

    [Fact]
    public Task EnsureLoadedAsync_ShouldDeserializeToDataSet_WhenRecordIsPokemonForm() =>
        EnsureLoadedAsync_ShouldDeserializeToDataSet_NonTest<(UInt16, Byte), IHomeBallsPokemonForm>(
            form => (form.SpeciesId, form.FormId));
}