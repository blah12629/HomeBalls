namespace CEo.Pokemon.HomeBalls.Data;

public interface IHomeBallsEntriesProtobufExporter :
    IFileLoadable<IHomeBallsEntriesProtobufExporter>
{
    Task<IHomeBallsEntriesProtobufExporter> ExportEntriesAsync(
        IEnumerable<IHomeBallsEntry> entries,
        CancellationToken cancellationToken = default);
}

public class HomeBallsEntriesProtobufExporter :
    IHomeBallsEntriesProtobufExporter
{
    public HomeBallsEntriesProtobufExporter(
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

    protected internal virtual String FilePath => FileSystem.Path.Join(
        RootDirectory,
        $"{typeof(IHomeBallsEntry).GetFullNameNonNull()}.bin");

    public virtual async Task<HomeBallsEntriesProtobufExporter> ExportEntriesAsync(
        IEnumerable<IHomeBallsEntry> entries,
        CancellationToken cancellationToken = default)
    {
        var path = FilePath;
        var converted = Converter.Convert(entries);
        Logger?.LogDebug(
            $"Exporting {converted.Count} `{nameof(IHomeBallsEntry)}` " +
            $"to `{path}`.");

        Int64 length;
        await using (var file = FileSystem.File.OpenWrite(path))
        {
            ProtoBuf.Serializer.Serialize<IEnumerable<ProtobufEntry>>(file, converted);
            length = file.Length;
        }

        Logger?.LogDebug($"Successfully written {length} bytes to `{path}`.");
        return this;
    }

    public virtual HomeBallsEntriesProtobufExporter InDirectory(String directory)
    {
        RootDirectory = directory;
        return this;
    }

    async Task<IHomeBallsEntriesProtobufExporter> IHomeBallsEntriesProtobufExporter
        .ExportEntriesAsync(
            IEnumerable<IHomeBallsEntry> entries,
            CancellationToken cancellationToken) =>
        await ExportEntriesAsync(entries, cancellationToken);

    IHomeBallsEntriesProtobufExporter IFileLoadable<IHomeBallsEntriesProtobufExporter>
        .InDirectory(String directory) =>
        InDirectory(directory);

    void IFileLoadable.InDirectory(String directory) => InDirectory(directory);
}