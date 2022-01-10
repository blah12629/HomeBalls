namespace CEo.Pokemon.HomeBalls.Data;

public interface IHomeBallsDataProtobufExporter :
    IFileLoadable<IHomeBallsDataProtobufExporter>
{
    Task<IHomeBallsDataProtobufExporter> ExportDataAsync(
        IHomeBallsDataSource dataSource,
        CancellationToken cancellationToken = default);
}

public class HomeBallsDataProtobufExporter :
    IHomeBallsDataProtobufExporter
{
    public HomeBallsDataProtobufExporter(
        IFileSystem fileSystem,
        IHomeBallsProtobufConverter converter,
        ILogger? logger = default)
    {
        FileSystem = fileSystem;
        Converter = converter;
        Logger = logger;

        RootDirectory = _Values.DefaultProtobufExportRoot;
    }

    protected internal IFileSystem FileSystem { get; }

    protected internal IHomeBallsProtobufConverter Converter { get; }

    protected internal ILogger? Logger { get; }

    protected internal String RootDirectory { get; set; }

    public virtual async Task<HomeBallsDataProtobufExporter> ExportDataAsync(
        IHomeBallsDataSource dataSource,
        CancellationToken cancellationToken = default)
    {
        await Task.WhenAll(dataSource.Entities.Select(entities =>
            ExportDataAsync(entities, cancellationToken)));

        return this;
    }

    protected internal virtual async Task<HomeBallsDataProtobufExporter> ExportDataAsync<T>(
        IHomeBallsReadOnlyCollection<T> entities,
        CancellationToken cancellationToken = default)
        where T : notnull, IHomeBallsEntity
    {
        var path = GetFilePath(entities);
        FileSystem.Directory.CreateDirectory(FileSystem.Path.GetDirectoryName(path));

        var data = await ConvertDataAsync(entities, cancellationToken);
        Int64 length;

        await using (var file = FileSystem.File.OpenWrite(path))
        {
            ProtoBuf.Serializer.Serialize(file, data);
            length = file.Length;
        }

        Logger?.LogDebug($"Successfully written {length} bytes to `{path}`.");
        return this;
    }

    protected internal virtual Task<IEnumerable> ConvertDataAsync<T>(
        IHomeBallsReadOnlyCollection<T> rawData,
        CancellationToken cancellationToken = default) =>
        Task.FromResult<IEnumerable>(rawData.ElementType switch
        {
            var t when t.IsAssignableTo(typeof(IHomeBallsGameVersion)) => Converter.Convert((IEnumerable<IHomeBallsGameVersion>)rawData),
            var t when t.IsAssignableTo(typeof(IHomeBallsGeneration)) => Converter.Convert((IEnumerable<IHomeBallsGeneration>)rawData),
            var t when t.IsAssignableTo(typeof(IHomeBallsItem)) => Converter.Convert((IEnumerable<IHomeBallsItem>)rawData),
            var t when t.IsAssignableTo(typeof(IHomeBallsItemCategory)) => Converter.Convert((IEnumerable<IHomeBallsItemCategory>)rawData),
            var t when t.IsAssignableTo(typeof(IHomeBallsLanguage)) => Converter.Convert((IEnumerable<IHomeBallsLanguage>)rawData),
            var t when t.IsAssignableTo(typeof(IHomeBallsMove)) => Converter.Convert((IEnumerable<IHomeBallsMove>)rawData),
            var t when t.IsAssignableTo(typeof(IHomeBallsMoveDamageCategory)) => Converter.Convert((IEnumerable<IHomeBallsMoveDamageCategory>)rawData),
            var t when t.IsAssignableTo(typeof(IHomeBallsNature)) => Converter.Convert((IEnumerable<IHomeBallsNature>)rawData),
            var t when t.IsAssignableTo(typeof(IHomeBallsPokemonAbility)) => Converter.Convert((IEnumerable<IHomeBallsPokemonAbility>)rawData),
            var t when t.IsAssignableTo(typeof(IHomeBallsPokemonEggGroup)) => Converter.Convert((IEnumerable<IHomeBallsPokemonEggGroup>)rawData),
            var t when t.IsAssignableTo(typeof(IHomeBallsPokemonForm)) => Converter.Convert((IEnumerable<IHomeBallsPokemonForm>)rawData),
            var t when t.IsAssignableTo(typeof(IHomeBallsPokemonSpecies)) => Converter.Convert((IEnumerable<IHomeBallsPokemonSpecies>)rawData),
            var t when t.IsAssignableTo(typeof(IHomeBallsStat)) => Converter.Convert((IEnumerable<IHomeBallsStat>)rawData),
            var t when t.IsAssignableTo(typeof(IHomeBallsType)) => Converter.Convert((IEnumerable<IHomeBallsType>)rawData),
            _ => throw new NotSupportedException()
        });

    protected internal virtual String GetFilePath<T>(IHomeBallsReadOnlyCollection<T> entities) =>
        FileSystem.Path.Join(RootDirectory, $"{entities.ElementType.GetFullNameNonNull()}.bin");

    public virtual HomeBallsDataProtobufExporter InDirectory(String directory)
    {
        RootDirectory = directory;
        return this;
    }

    async Task<IHomeBallsDataProtobufExporter> IHomeBallsDataProtobufExporter
        .ExportDataAsync(
            IHomeBallsDataSource dataSource,
            CancellationToken cancellationToken) =>
        await ExportDataAsync(dataSource, cancellationToken);

    IHomeBallsDataProtobufExporter IFileLoadable<IHomeBallsDataProtobufExporter>
        .InDirectory(String directory) =>
        InDirectory(directory);

    void IFileLoadable.InDirectory(String directory) => InDirectory(directory);
}