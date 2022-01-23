namespace CEo.Pokemon.HomeBalls.ProtocolBuffers;

public partial interface IProtobufGenericSerializer
{
    // Task<Byte[]> SerializeAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, StreamingContext context, T instance, CancellationToken cancellationToken = default) where T : class, ISerializable;

    Task<Byte[]> SerializeAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T instance, CancellationToken cancellationToken = default);

    Task<Byte[]> SerializeAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T instance, Object userState, CancellationToken cancellationToken = default);

    // Task<Byte[]> SerializeAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(IBufferWriter<Byte> destination, T instance, Object? userState = default, CancellationToken cancellationToken = default);

    // Task<Byte[]> SerializeAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, T instance, CancellationToken cancellationToken = default) where T : class, ISerializable;

    // Task<Byte[]> SerializeAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(XmlWriter writer, T instance, CancellationToken cancellationToken = default) where T : IXmlSerializable;

    Task<Byte[]> SerializeWithLengthPrefixAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T instance, PrefixStyle style, CancellationToken cancellationToken = default);

    Task<Byte[]> SerializeWithLengthPrefixAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T instance, PrefixStyle style, Int32 fieldNumber, CancellationToken cancellationToken = default);
}

public partial class ProtobufSerializer :
    IProtobufSerializer,
    IProtobufGenericSerializer,
    IProtobufStaticSerializer
{
    Task<Byte[]> IProtobufGenericSerializer.SerializeAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(
        T instance,
        CancellationToken cancellationToken) =>
        ExecuteSerializeAsync(
            stream => ForGenericTypes.Serialize(stream, instance),
            cancellationToken);

    Task<Byte[]> IProtobufGenericSerializer.SerializeAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(
        T instance,
        Object userState,
        CancellationToken cancellationToken) =>
        ExecuteSerializeAsync(
            stream => ForGenericTypes.Serialize(stream, instance, userState),
            cancellationToken);

    Task<Byte[]> IProtobufGenericSerializer.SerializeWithLengthPrefixAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(
        T instance,
        PrefixStyle style,
        CancellationToken cancellationToken) =>
        ExecuteSerializeAsync(
            stream => ForGenericTypes.SerializeWithLengthPrefix(stream, instance, style),
            cancellationToken);

    Task<Byte[]> IProtobufGenericSerializer.SerializeWithLengthPrefixAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(
        T instance,
        PrefixStyle style,
        Int32 fieldNumber,
        CancellationToken cancellationToken) =>
        ExecuteSerializeAsync(
            stream => ForGenericTypes.SerializeWithLengthPrefix(stream, instance, style, fieldNumber),
            cancellationToken);
}