namespace CEo.Pokemon.HomeBalls.ProtocolBuffers;

public partial interface IProtobufStaticSerializer
{
    Boolean CanSerialize(Type type);

    Object DeepClone(Object instance);

    Object Deserialize([DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] Type type, Stream source);

    Object Deserialize([DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] Type type, Stream source, Object? instance = default, Object? userState = default, Int64 length = -1);

    Object Deserialize([DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] Type type, ReadOnlyMemory<Byte> source, Object? instance = default, Object? userState = default);

    Object Deserialize([DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] Type type, ReadOnlySequence<Byte> source, Object? instance = default, Object? userState = default);

    Object Deserialize([DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] Type type, ReadOnlySpan<Byte> source, Object? instance = default, Object? userState = default);

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    Object Merge(Stream source, Object instance);

    IProtobufStaticSerializer PrepareSerializer([DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] Type type);

    IProtobufStaticSerializer Serialize(Stream dest, Object instance);

    IProtobufStaticSerializer SerializeWithLengthPrefix(Stream destination, Object instance, PrefixStyle style, Int32 fieldNumber);

    Boolean TryDeserializeWithLengthPrefix(Stream source, PrefixStyle style, ProtoBuf.TypeResolver resolver, out Object value);
}

public partial class ProtobufSerializer :
    IProtobufSerializer,
    IProtobufGenericSerializer,
    IProtobufStaticSerializer
{
    Boolean IProtobufStaticSerializer.CanSerialize(Type type) =>
        Serializer.NonGeneric.CanSerialize(type);

    Object IProtobufStaticSerializer.DeepClone(Object instance) =>
        Serializer.NonGeneric.DeepClone(instance);

    Object IProtobufStaticSerializer.Deserialize(Type type, Stream source) =>
        Serializer.NonGeneric.Deserialize(type, source);

    Object IProtobufStaticSerializer.Deserialize(Type type, Stream source, Object? instance, Object? userState, Int64 length) =>
        Serializer.NonGeneric.Deserialize(type, source, instance, userState, length);

    Object IProtobufStaticSerializer.Deserialize(Type type, ReadOnlyMemory<Byte> source, Object? instance, Object? userState) =>
        Serializer.NonGeneric.Deserialize(type, source, instance, userState);

    Object IProtobufStaticSerializer.Deserialize(Type type, ReadOnlySequence<Byte> source, Object? instance, Object? userState) =>
        Serializer.NonGeneric.Deserialize(type, source, instance, userState);

    Object IProtobufStaticSerializer.Deserialize(Type type, ReadOnlySpan<Byte> source, Object? instance, Object? userState) =>
        Serializer.NonGeneric.Deserialize(type, source, instance, userState);

    Object IProtobufStaticSerializer.Merge(Stream source, Object instance) =>
        Serializer.NonGeneric.Merge(source, instance);

    IProtobufStaticSerializer IProtobufStaticSerializer.PrepareSerializer(Type type)
    {
        Serializer.NonGeneric.PrepareSerializer(type);
        return this;
    }

    IProtobufStaticSerializer IProtobufStaticSerializer.Serialize(Stream dest, Object instance)
    {
        Serializer.NonGeneric.Serialize(dest, instance);
        return this;
    }

    IProtobufStaticSerializer IProtobufStaticSerializer.SerializeWithLengthPrefix(Stream destination, Object instance, PrefixStyle style, Int32 fieldNumber)
    {
        Serializer.NonGeneric.SerializeWithLengthPrefix(destination, instance, style, fieldNumber);
        return this;
    }

    Boolean IProtobufStaticSerializer.TryDeserializeWithLengthPrefix(Stream source, PrefixStyle style, TypeResolver resolver, out Object value) =>
        Serializer.NonGeneric.TryDeserializeWithLengthPrefix(source, style, resolver, out value);
}