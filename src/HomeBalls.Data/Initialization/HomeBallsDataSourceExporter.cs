using CEo.Pokemon.HomeBalls.Entities;

namespace CEo.Pokemon.HomeBalls.Data.Initialization;

// STOPPED HERE: 
// - something wrong with exporting
// - `Items` and `PokeBalls` have the same identifiers

public interface IHomeBallsDataSourceExporter
{
    Task<IHomeBallsDataSourceExporter> ExportAsync(
        IHomeBallsDataDataSource data,
        CancellationToken cancellationToken = default);

    Task<IHomeBallsDataSourceExporter> ExportAsync<TKey, TEntity>(
        IHomeBallsDataSourceReadOnlyProperty<TKey, TEntity> data,
        CancellationToken cancellationToken = default)
        where TKey : notnull, IEquatable<TKey>
        where TEntity :
            notnull,
            HomeBalls.Entities.IKeyed<TKey>,
            HomeBalls.Entities.IIdentifiable,
            IHomeBallsDataType;

    Task<IHomeBallsDataSourceExporter> ExportAsync<TKey, TEntity>(
        IHomeBallsDataSourceReadOnlyProperty<TKey, TEntity> data,
        String identifier,
        CancellationToken cancellationToken = default)
        where TKey : notnull, IEquatable<TKey>
        where TEntity :
            notnull,
            HomeBalls.Entities.IKeyed<TKey>,
            HomeBalls.Entities.IIdentifiable,
            IHomeBallsDataType;

    Task<IHomeBallsDataSourceExporter> ExportAsync<TKey, TEntity>(
        IReadOnlyCollection<TEntity> data,
        String identifier,
        CancellationToken cancellationToken = default)
        where TKey : notnull, IEquatable<TKey>
        where TEntity :
            notnull,
            HomeBalls.Entities.IKeyed<TKey>,
            HomeBalls.Entities.IIdentifiable,
            IHomeBallsDataType;
}

public class HomeBallsDataSourceExporter :
    IHomeBallsDataSourceExporter,
    IFileLoadable<HomeBallsDataSourceExporter>
{
    public HomeBallsDataSourceExporter(
        IFileSystem fileSystem,
        IProtoBufSerializer serializer,
        String dataRootDirectory = DefaultProtobufExportRoot,
        ILogger? logger = default)
    {
        DataRoot = dataRootDirectory;
        (FileSystem, Serializer) = (fileSystem, serializer);
        Logger = logger;
    }

    protected internal String DataRoot { get; set; }

    protected internal IFileSystem FileSystem { get; }

    protected internal IProtoBufSerializer Serializer { get; }

    protected internal ILogger? Logger { get; }

    public virtual async Task<HomeBallsDataSourceExporter> ExportAsync(
        IHomeBallsDataDataSource data,
        CancellationToken cancellationToken = default)
    {
        var tasks = new Task<HomeBallsDataSourceExporter>[]
        {
            ExportAsync(data.GameVersions, cancellationToken),
            ExportAsync(data.Generations, cancellationToken),
            ExportAsync(data.Items, cancellationToken),
            ExportAsync(data.ItemCategories, cancellationToken),
            ExportAsync(data.Languages, cancellationToken),
            ExportAsync(data.Legalities, cancellationToken),
            ExportAsync(data.Moves, cancellationToken),
            ExportAsync(data.MoveDamageCategories, cancellationToken),
            ExportAsync(data.Natures, cancellationToken),
            ExportAsync(data.PokemonAbilities, cancellationToken),
            ExportAsync(data.PokemonEggGroups, cancellationToken),
            ExportAsync(data.PokemonForms, cancellationToken),
            ExportAsync(data.PokemonSpecies, cancellationToken),
            ExportAsync(data.Stats, cancellationToken),
            ExportAsync(data.Types, cancellationToken),
            ExportAsync(data.BreedablePokemonForms, cancellationToken),
            ExportAsync(data.BreedablePokemonSpecies, cancellationToken),
            ExportAsync(data.Pokeballs, cancellationToken),
        };

        // await Task.WhenAll(tasks);
        foreach (var task in tasks) await task;
        return this;
    }

    public virtual Task<HomeBallsDataSourceExporter> ExportAsync<TKey, TEntity>(
        IHomeBallsDataSourceReadOnlyProperty<TKey, TEntity> data,
        CancellationToken cancellationToken = default)
        where TKey : notnull, IEquatable<TKey>
        where TEntity :
            notnull,
            HomeBalls.Entities.IKeyed<TKey>,
            HomeBalls.Entities.IIdentifiable,
            IHomeBallsDataType =>
        ExportAsync<TKey, TEntity>(data, data.PropertyName, cancellationToken);

    public virtual Task<HomeBallsDataSourceExporter> ExportAsync<TKey, TEntity>(
        IHomeBallsDataSourceReadOnlyProperty<TKey, TEntity> data,
        String identifier,
        CancellationToken cancellationToken = default)
        where TKey : notnull, IEquatable<TKey>
        where TEntity :
            notnull,
            HomeBalls.Entities.IKeyed<TKey>,
            HomeBalls.Entities.IIdentifiable,
            IHomeBallsDataType =>
        ExportAsync<TKey, TEntity>(data.Values, identifier, cancellationToken);

    public virtual Task<HomeBallsDataSourceExporter> ExportAsync<TKey, TEntity>(
        IReadOnlyCollection<TEntity> data,
        String identifier,
        CancellationToken cancellationToken = default)
        where TKey : notnull, IEquatable<TKey>
        where TEntity :
            notnull,
            HomeBalls.Entities.IKeyed<TKey>,
            HomeBalls.Entities.IIdentifiable,
            IHomeBallsDataType
    {
        var baseType = TEntity.BaseEntityType;
        var fileName = identifier.AddFileExtension(DefaultProtoBufExtension);
        var filePath = FileSystem.Path.Join(DataRoot, fileName);
        var serializationType = typeof(ICollection<>).MakeGenericType(baseType);
        var dataCast = data
            .OrderBy(value => value.Id)
            .Select(value => value.ToBaseType())
            .Cast(baseType)
            .ToList(baseType);

        return ExportAsync(dataCast, serializationType, serializationType, filePath, cancellationToken);
    }

    protected internal virtual async Task<HomeBallsDataSourceExporter> ExportAsync(
        IEnumerable<Object> data,
        Type serializationType,
        Type deserializationType,
        String filePath,
        CancellationToken cancellationToken = default)
    {
        var memory = new MemoryStream();
        Serializer.ForStaticTypes.Serialize(memory, data, serializationType);
        var bytes = memory.ToArray();
        await memory.DisposeAsync();

        var bytesString = bytes.ToBase64String();
        var bytesMemory = Convert.FromBase64String(bytesString).AsMemory();
        var deserialized = (IEnumerable<Object>)Serializer.ForStaticTypes.Deserialize(deserializationType, bytesMemory);
        var (dataCount, deserializedCount) = (data.Count(), deserialized.Count());

        if (dataCount != deserializedCount)
            Logger?.LogWarning($"Data deserialization count mismatch occurred.");

        FileSystem.Directory.CreateDirectory(FileSystem.Path.GetDirectoryName(filePath));
        await FileSystem.File.WriteAllBytesAsync(filePath, bytes, cancellationToken);
        return this;
    }

    public virtual HomeBallsDataSourceExporter InDirectory(String directory)
    {
        DataRoot = directory;
        return this;
    }

    async Task<IHomeBallsDataSourceExporter> IHomeBallsDataSourceExporter
        .ExportAsync(
            IHomeBallsDataDataSource data,
            CancellationToken cancellationToken) =>
        await ExportAsync(data, cancellationToken);

    async Task<IHomeBallsDataSourceExporter> IHomeBallsDataSourceExporter
        .ExportAsync<TKey, TEntity>(
            IHomeBallsDataSourceReadOnlyProperty<TKey, TEntity> data,
            CancellationToken cancellationToken) =>
        await ExportAsync(data, cancellationToken);

    async Task<IHomeBallsDataSourceExporter> IHomeBallsDataSourceExporter
        .ExportAsync<TKey, TEntity>(
            IHomeBallsDataSourceReadOnlyProperty<TKey, TEntity> data,
            String identifier,
            CancellationToken cancellationToken) =>
        await ExportAsync(data, identifier, cancellationToken);

    async Task<IHomeBallsDataSourceExporter> IHomeBallsDataSourceExporter
        .ExportAsync<TKey, TEntity>(
            IReadOnlyCollection<TEntity> data,
            String identifier,
            CancellationToken cancellationToken) =>
        await ExportAsync<TKey, TEntity>(data, identifier, cancellationToken);

    void IFileLoadable.InDirectory(String directory) => InDirectory(directory);
}