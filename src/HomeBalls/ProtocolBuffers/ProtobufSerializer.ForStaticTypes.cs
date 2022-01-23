namespace CEo.Pokemon.HomeBalls.ProtocolBuffers;

public partial interface IProtobufStaticSerializer
{
    Task<Byte[]> SerializeAsync(Object instance, CancellationToken cancellationToken = default);

    Task<Byte[]> SerializeWithLengthPrefixAsync(Object instance, PrefixStyle style, Int32 fieldNumber, CancellationToken cancellationToken = default);
}

public partial class ProtobufSerializer :
    IProtobufSerializer,
    IProtobufGenericSerializer,
    IProtobufStaticSerializer
{
    Task<Byte[]> IProtobufStaticSerializer.SerializeAsync(
        Object instance,
        CancellationToken cancellationToken) =>
        ExecuteSerializeAsync(
            stream => ForStaticTypes.Serialize(stream, instance),
            cancellationToken);

    Task<Byte[]> IProtobufStaticSerializer.SerializeWithLengthPrefixAsync(
        Object instance,
        PrefixStyle style,
        Int32 fieldNumber,
        CancellationToken cancellationToken) =>
        ExecuteSerializeAsync(
            stream => ForStaticTypes.SerializeWithLengthPrefix(stream, instance, style, fieldNumber),
            cancellationToken);
}