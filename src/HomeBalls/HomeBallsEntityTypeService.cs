// namespace CEo.Pokemon.HomeBalls;

// public interface IHomeBallsEntityTypeService
// {
//     ICollection<Type> SupportedTypes { get; }

//     String GetIdentifier(Type type);

//     String GetFileName(Type type, String? extension = default);
// }

// public class HomeBallsEntityTypeService :
//     IHomeBallsEntityTypeService
// {
//     protected internal IPluralize Pluralizer { get; }

//     protected internal IValidatingList<Type> SupportedTypes { get; }

//     protected internal IDictionary<Type, String> IdentifierLookup { get; }

//     ICollection<Type> IHomeBallsEntityTypeService.SupportedTypes => SupportedTypes;

//     public virtual String GetIdentifier(Type type)
//     {
//         ThrowIfTypeNotSupported(type);

//         if (!IdentifierLookup.ContainsKey(type))
//             IdentifierLookup.Add(type, CreateIdentifier(type));

//         return IdentifierLookup[type];
//     }

//     protected internal virtual String CreateIdentifier(Type type)
//     {
//         var segments = TypeNameWithoutPrefix(type.Name).ToSnakeCase().Split('_');
//         return String.Join('_', segments[.. ^ 1]
//             .Append(Pluralizer.Pluralize(segments[^ 1])));
//     }

//     public virtual String GetFileName(Type type, String? extension = default)
//     {
//         var name = GetIdentifier(type);
//         extension = (extension?.StartsWith('.') ?? false) ? extension[1 ..] : extension;

//         return String.IsNullOrWhiteSpace(extension) ? name : $"{name}.{extension}";
//     }

//     protected internal virtual HomeBallsEntityTypeService InitializeSupportedTypes()
//     {
//         foreach (var type in GetType().Assembly.GetTypes())
//             if (type.IsAssignableTo(typeof(IHomeBallsEntity)))
//                 SupportedTypes.Add(type);

//         return this;
//     }

//     protected internal virtual HomeBallsEntityTypeService ThrowIfTypeNotSupported(
//         Type type) =>
//         SupportedTypes.Contains(type) ? this : throw new NotSupportedException();

//     protected internal virtual String TypeNameWithoutPrefix(String typeName)
//     {
//         return tryTrimStart("IHomeBalls", out var homeBalls) ? homeBalls :
//             tryTrimStart("Protobuf", out var protobuf) ? protobuf :
//             typeName;

//         Boolean tryTrimStart(String prefix, out String trimmed)
//         {
//             var startsWith = typeName.StartsWith(prefix);
//             trimmed = startsWith ? typeName[prefix.Length ..] : typeName;
//             return startsWith;
//         }
//     }
// }