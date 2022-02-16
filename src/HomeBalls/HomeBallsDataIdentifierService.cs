// namespace CEo.Pokemon.HomeBalls;

// public interface IHomeBallsDataIdentifierService : IHomeBallsIdentifierService
// {
//     String GenerateIdentifier(IProperty property);
// }

// public class HomeBallsDataIdentifierService : IHomeBallsDataIdentifierService
// {
//     public HomeBallsDataIdentifierService(
//         IProtoBufSerializer serializer,
//         ILogger? logger = default) =>
//         (Serializer, Logger) = (serializer, logger);

//     protected internal IProtoBufSerializer Serializer { get; }

//     protected internal ILogger? Logger { get; }

//     public virtual String GenerateIdentifier<TEntity>()
//         where TEntity : notnull =>
//         GenerateIdentifier(typeof(TEntity));

//     public virtual String GenerateIdentifier(Type entityType)
//     {
//         String name = entityType.Name, nameLower = name.ToLower();
//         foreach (var @string in new[] { "i", "homeballs" }) trimStart(@string);
//         return Serializer.ForGenericTypes.Serialize(name).ToBase64String(true);

//         void trimStart(String @string)
//         {
//             if (!nameLower.StartsWith(@string)) return;
//             var length = @string.Length;
//             (name, nameLower) = (name[length ..], nameLower[length ..]);
//         }
//     }
// }