namespace CEo.Pokemon.HomeBalls.ProtocolBuffers;

public partial interface IProtoBufStaticSerializer
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

    IProtoBufStaticSerializer PrepareSerializer([DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] Type type);

    IProtoBufStaticSerializer Serialize(Stream dest, Object instance);

    IProtoBufStaticSerializer SerializeWithLengthPrefix(Stream destination, Object instance, PrefixStyle style, Int32 fieldNumber);

    Boolean TryDeserializeWithLengthPrefix(Stream source, PrefixStyle style, ProtoBuf.TypeResolver resolver, out Object value);
}

public partial class ProtoBufSerializer :
    IProtoBufSerializer,
    IProtoBufGenericSerializer,
    IProtoBufStaticSerializer
{
    Boolean IProtoBufStaticSerializer.CanSerialize(Type type) =>
        Serializer.NonGeneric.CanSerialize(type);

    Object IProtoBufStaticSerializer.DeepClone(Object instance) =>
        Serializer.NonGeneric.DeepClone(instance);

    Object IProtoBufStaticSerializer.Deserialize(Type type, Stream source) =>
        Serializer.NonGeneric.Deserialize(type, source);

    Object IProtoBufStaticSerializer.Deserialize(Type type, Stream source, Object? instance, Object? userState, Int64 length) =>
        Serializer.NonGeneric.Deserialize(type, source, instance, userState, length);

    Object IProtoBufStaticSerializer.Deserialize(Type type, ReadOnlyMemory<Byte> source, Object? instance, Object? userState) =>
        Serializer.NonGeneric.Deserialize(type, source, instance, userState);

    Object IProtoBufStaticSerializer.Deserialize(Type type, ReadOnlySequence<Byte> source, Object? instance, Object? userState) =>
        Serializer.NonGeneric.Deserialize(type, source, instance, userState);

    Object IProtoBufStaticSerializer.Deserialize(Type type, ReadOnlySpan<Byte> source, Object? instance, Object? userState) =>
        Serializer.NonGeneric.Deserialize(type, source, instance, userState);

    Object IProtoBufStaticSerializer.Merge(Stream source, Object instance) =>
        Serializer.NonGeneric.Merge(source, instance);

    IProtoBufStaticSerializer IProtoBufStaticSerializer.PrepareSerializer(Type type)
    {
        Serializer.NonGeneric.PrepareSerializer(type);
        return this;
    }

    IProtoBufStaticSerializer IProtoBufStaticSerializer.Serialize(Stream dest, Object instance)
    {
        Serializer.NonGeneric.Serialize(dest, instance);
        return this;
    }

    IProtoBufStaticSerializer IProtoBufStaticSerializer.SerializeWithLengthPrefix(Stream destination, Object instance, PrefixStyle style, Int32 fieldNumber)
    {
        Serializer.NonGeneric.SerializeWithLengthPrefix(destination, instance, style, fieldNumber);
        return this;
    }

    Boolean IProtoBufStaticSerializer.TryDeserializeWithLengthPrefix(Stream source, PrefixStyle style, TypeResolver resolver, out Object value) =>
        Serializer.NonGeneric.TryDeserializeWithLengthPrefix(source, style, resolver, out value);
}