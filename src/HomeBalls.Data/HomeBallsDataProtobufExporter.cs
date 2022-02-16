// namespace CEo.Pokemon.HomeBalls.Data;

// public interface IHomeBallsDataProtobufExporter :
//     IFileLoadable<IHomeBallsDataProtobufExporter>
// {
//     Task<IHomeBallsDataProtobufExporter> ExportDataAsync(
//         IHomeBallsLoadableDataSource dataSource,
//         CancellationToken cancellationToken = default);
// }

// public class HomeBallsDataProtobufExporter :
//     IHomeBallsDataProtobufExporter
// {
//     public HomeBallsDataProtobufExporter(
//         IFileSystem fileSystem,
//         IHomeBallsProtobufConverter converter,
//         IProtoBufSerializer serializer,
//         ILogger? logger = default)
//     {
//         FileSystem = fileSystem;
//         (Converter, Serializer) = (converter, serializer);
//         Logger = logger;

//         RootDirectory = _Values.DefaultProtobufExportRoot;
//     }

//     protected internal IFileSystem FileSystem { get; }

//     protected internal IHomeBallsProtobufConverter Converter { get; }

//     protected internal IProtoBufSerializer Serializer { get; }

//     protected internal ILogger? Logger { get; }

//     protected internal String RootDirectory { get; set; }

//     public virtual async Task<HomeBallsDataProtobufExporter> ExportDataAsync(
//         IHomeBallsLoadableDataSource dataSource,
//         CancellationToken cancellationToken = default)
//     {
//         await Task.WhenAll(new[]
//         {
//             ExportDataAsync(dataSource.GameVersions, cancellationToken),
//             ExportDataAsync(dataSource.Generations, cancellationToken),
//             ExportDataAsync(dataSource.Items, cancellationToken),
//             ExportDataAsync(dataSource.ItemCategories, cancellationToken),
//             ExportDataAsync(dataSource.Languages, cancellationToken),
//             ExportDataAsync(dataSource.Legalities, cancellationToken),
//             ExportDataAsync(dataSource.Moves, cancellationToken),
//             ExportDataAsync(dataSource.MoveDamageCategories, cancellationToken),
//             ExportDataAsync(dataSource.Natures, cancellationToken),
//             ExportDataAsync(dataSource.PokemonAbilities, cancellationToken),
//             ExportDataAsync(dataSource.PokemonEggGroups, cancellationToken),
//             ExportDataAsync(dataSource.PokemonForms, cancellationToken),
//             ExportDataAsync(dataSource.PokemonSpecies, cancellationToken),
//             ExportDataAsync(dataSource.Stats, cancellationToken),
//             ExportDataAsync(dataSource.Types, cancellationToken)
//         });

//         return this;
//     }

//     protected internal virtual async Task<HomeBallsDataProtobufExporter> ExportDataAsync<TKey, TRecord>(
//         IHomeBallsLoadableDataSet<TKey, TRecord> entities,
//         CancellationToken cancellationToken = default)
//         where TKey : notnull, IEquatable<TKey>
//         where TRecord : notnull, IHomeBallsEntity, IKeyed<TKey>, IIdentifiable =>
//         await ExportDataAsync(
//             (IHomeBallsReadOnlyCollection<TRecord>)(await entities.EnsureLoadedAsync(cancellationToken)).Values,
//             cancellationToken);

//     protected internal virtual async Task<HomeBallsDataProtobufExporter> ExportDataAsync<T>(
//         IHomeBallsReadOnlyCollection<T> entities,
//         CancellationToken cancellationToken = default)
//         where T : notnull, IHomeBallsEntity
//     {
//         var path = GetFilePath(entities);
//         FileSystem.Directory.CreateDirectory(FileSystem.Path.GetDirectoryName(path));
//         Logger?.LogDebug($"Exporting data to `{path}`.");

//         var data = await ConvertDataAsync(entities, out var elementType, cancellationToken);
//         var dataSorted = data.Cast<Object>()
//             .OrderBy(element =>
//                 element is IKeyed<Byte> byteKeyed ? byteKeyed.Id :
//                 element is IKeyed<UInt16> shortKeyed ? shortKeyed.Id :
//                 element is IHomeBallsPokemonForm form ? (form.SpeciesId, form.FormId) :
//                 element is IHomeBallsEntryLegality legality ? (legality.SpeciesId, legality.FormId, legality.BallId) :
//                 element)
//             .ToList().AsReadOnly();
//         var dataType = typeof(IEnumerable<>).MakeGenericType(elementType);
//         var dataCast = getEnumerableMethod(nameof(Enumerable.Cast))
//             .Invoke(default, new Object?[] { dataSorted });
//         var dataSave = getEnumerableMethod(nameof(Enumerable.ToList))
//             .Invoke(default, new Object?[] { dataCast });

//         // var serializeMethod = typeof(ProtoBuf.Serializer)
//         //     .GetMethods(BindingFlags.Static | BindingFlags.Public)
//         //     .Single(method =>
//         //     {
//         //         var parameters = method.GetParameters();
//         //         return method.Name == nameof(ProtoBuf.Serializer.Serialize) &&
//         //             method.IsGenericMethodDefinition &&
//         //             parameters.Length == 2 &&
//         //             parameters[0].ParameterType == typeof(Stream) &&
//         //             parameters[1].ParameterType == method.GetGenericArguments()[0];
//         //     });

//         Int64 length;
//         await using (var file = FileSystem.File.OpenWrite(path))
//         {
//             // serializeMethod
//             //     .MakeGenericMethod(dataType)
//             //     .Invoke(default, new Object?[] { file, dataSave });

//             Serializer.ForStaticTypes.Serialize(file, dataSave!);
//             length = file.Length;
//         }

//         Logger?.LogDebug($"Successfully written {length} bytes to `{path}`.");
//         return this;

//         MethodInfo getEnumerableMethod(String methodName) =>
//             typeof(Enumerable).GetMethod(methodName)!.MakeGenericMethod(elementType);
//     }

//     protected internal virtual Task<IEnumerable> ConvertDataAsync<T>(
//         IHomeBallsReadOnlyCollection<T> rawData,
//         out Type elementType,
//         CancellationToken cancellationToken = default)
//     {
//         IEnumerable converted;
//         (elementType, converted) = ((Type, IEnumerable))(rawData.ElementType switch
//         {
//             var t when t.IsAssignableTo(typeof(IHomeBallsEntryLegality)) => (typeof(ProtobufEntryLegality), Converter.Convert((IEnumerable<IHomeBallsEntryLegality>)rawData)),
//             var t when t.IsAssignableTo(typeof(IHomeBallsGameVersion)) => (typeof(ProtobufGameVersion), Converter.Convert((IEnumerable<IHomeBallsGameVersion>)rawData)),
//             var t when t.IsAssignableTo(typeof(IHomeBallsGeneration)) => (typeof(ProtobufGeneration), Converter.Convert((IEnumerable<IHomeBallsGeneration>)rawData)),
//             var t when t.IsAssignableTo(typeof(IHomeBallsItem)) => (typeof(ProtobufItem), Converter.Convert((IEnumerable<IHomeBallsItem>)rawData)),
//             var t when t.IsAssignableTo(typeof(IHomeBallsItemCategory)) => (typeof(ProtobufItemCategory), Converter.Convert((IEnumerable<IHomeBallsItemCategory>)rawData)),
//             var t when t.IsAssignableTo(typeof(IHomeBallsLanguage)) => (typeof(ProtobufLanguage), Converter.Convert((IEnumerable<IHomeBallsLanguage>)rawData)),
//             var t when t.IsAssignableTo(typeof(IHomeBallsMove)) => (typeof(ProtobufMove), Converter.Convert((IEnumerable<IHomeBallsMove>)rawData)),
//             var t when t.IsAssignableTo(typeof(IHomeBallsMoveDamageCategory)) => (typeof(ProtobufMoveDamageCategory), Converter.Convert((IEnumerable<IHomeBallsMoveDamageCategory>)rawData)),
//             var t when t.IsAssignableTo(typeof(IHomeBallsNature)) => (typeof(ProtobufNature), Converter.Convert((IEnumerable<IHomeBallsNature>)rawData)),
//             var t when t.IsAssignableTo(typeof(IHomeBallsPokemonAbility)) => (typeof(ProtobufPokemonAbility), Converter.Convert((IEnumerable<IHomeBallsPokemonAbility>)rawData)),
//             var t when t.IsAssignableTo(typeof(IHomeBallsPokemonEggGroup)) => (typeof(ProtobufPokemonEggGroup), Converter.Convert((IEnumerable<IHomeBallsPokemonEggGroup>)rawData)),
//             var t when t.IsAssignableTo(typeof(IHomeBallsPokemonForm)) => (typeof(ProtobufPokemonForm), Converter.Convert((IEnumerable<IHomeBallsPokemonForm>)rawData)),
//             var t when t.IsAssignableTo(typeof(IHomeBallsPokemonSpecies)) => (typeof(ProtobufPokemonSpecies), Converter.Convert((IEnumerable<IHomeBallsPokemonSpecies>)rawData)),
//             var t when t.IsAssignableTo(typeof(IHomeBallsStat)) => (typeof(ProtobufStat), Converter.Convert((IEnumerable<IHomeBallsStat>)rawData)),
//             var t when t.IsAssignableTo(typeof(IHomeBallsType)) => (typeof(ProtobufType), Converter.Convert((IEnumerable<IHomeBallsType>)rawData)),
//             _ => throw new NotSupportedException()
//         });

//         return Task.FromResult(converted);
//     }

//     protected internal virtual String GetFilePath<T>(IHomeBallsReadOnlyCollection<T> entities) =>
//         FileSystem.Path.Join(RootDirectory, $"{entities.ElementType.GetFullNameNonNull()}.bin");

//     public virtual HomeBallsDataProtobufExporter InDirectory(String directory)
//     {
//         RootDirectory = directory;
//         return this;
//     }

//     async Task<IHomeBallsDataProtobufExporter> IHomeBallsDataProtobufExporter
//         .ExportDataAsync(
//             IHomeBallsLoadableDataSource dataSource,
//             CancellationToken cancellationToken) =>
//         await ExportDataAsync(dataSource, cancellationToken);

//     IHomeBallsDataProtobufExporter IFileLoadable<IHomeBallsDataProtobufExporter>
//         .InDirectory(String directory) =>
//         InDirectory(directory);

//     void IFileLoadable.InDirectory(String directory) => InDirectory(directory);
// }