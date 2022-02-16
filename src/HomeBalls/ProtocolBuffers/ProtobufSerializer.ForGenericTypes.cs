namespace CEo.Pokemon.HomeBalls.ProtocolBuffers;

public partial interface IProtoBufGenericSerializer
{
    // Task<Byte[]> SerializeAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, StreamingContext context, T instance, CancellationToken cancellationToken = default) where T : class, ISerializable;

    Task<Byte[]> SerializeAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T instance, CancellationToken cancellationToken = default);

    Task<Byte[]> SerializeAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T instance, Object userState, CancellationToken cancellationToken = default);

    // Task<Byte[]> SerializeAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(IBufferWriter<Byte> destination, T instance, Object? userState = default, CancellationToken cancellationToken = default);

    // Task<Byte[]> SerializeAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, T instance, CancellationToken cancellationToken = default) where T : class, ISerializable;

    // Task<Byte[]> SerializeAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(XmlWriter writer, T instance, CancellationToken cancellationToken = default) where T : IXmlSerializable;

    Task<Byte[]> SerializeWithLengthPrefixAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T instance, PrefixStyle style, CancellationToken cancellationToken = default);

    Task<Byte[]> SerializeWithLengthPrefixAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T instance, PrefixStyle style, Int32 fieldNumber, CancellationToken cancellationToken = default);

    Byte[] Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T instance);

    Byte[] Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T instance, Object userState);
    Byte[] SerializeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T instance, PrefixStyle style);

    Byte[] SerializeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T instance, PrefixStyle style, Int32 fieldNumber);
}

public partial class ProtoBufSerializer :
    IProtoBufSerializer,
    IProtoBufGenericSerializer,
    IProtoBufStaticSerializer
{
    Task<Byte[]> IProtoBufGenericSerializer.SerializeAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(
        T instance,
        CancellationToken cancellationToken) =>
        ExecuteSerializeAsync(
            stream => ForGenericTypes.Serialize(stream, instance),
            cancellationToken);

    Task<Byte[]> IProtoBufGenericSerializer.SerializeAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(
        T instance,
        Object userState,
        CancellationToken cancellationToken) =>
        ExecuteSerializeAsync(
            stream => ForGenericTypes.Serialize(stream, instance, userState),
            cancellationToken);

    Task<Byte[]> IProtoBufGenericSerializer.SerializeWithLengthPrefixAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(
        T instance,
        PrefixStyle style,
        CancellationToken cancellationToken) =>
        ExecuteSerializeAsync(
            stream => ForGenericTypes.SerializeWithLengthPrefix(stream, instance, style),
            cancellationToken);

    Task<Byte[]> IProtoBufGenericSerializer.SerializeWithLengthPrefixAsync<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(
        T instance,
        PrefixStyle style,
        Int32 fieldNumber,
        CancellationToken cancellationToken) =>
        ExecuteSerializeAsync(
            stream => ForGenericTypes.SerializeWithLengthPrefix(stream, instance, style, fieldNumber),
            cancellationToken);


    Byte[] IProtoBufGenericSerializer.Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(
        T instance) =>
        ExecuteSerialize(stream => ForGenericTypes.Serialize(stream, instance));

    Byte[] IProtoBufGenericSerializer.Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(
        T instance,
        Object userState) =>
        ExecuteSerialize(stream => ForGenericTypes.Serialize(stream, instance, userState));

    Byte[] IProtoBufGenericSerializer.SerializeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(
        T instance,
        PrefixStyle style) =>
        ExecuteSerialize(stream => ForGenericTypes.SerializeWithLengthPrefix(stream, instance, style));

    Byte[] IProtoBufGenericSerializer.SerializeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(
        T instance,
        PrefixStyle style,
        Int32 fieldNumber)  =>
        ExecuteSerialize(stream => ForGenericTypes.SerializeWithLengthPrefix(stream, instance, style, fieldNumber));
}