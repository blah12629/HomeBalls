namespace CEo.Pokemon.HomeBalls.ProtocolBuffers;

public partial interface IProtobufGenericSerializer
{
    TTo ChangeType<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] TFrom, [DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] TTo>(TFrom instance);

    IFormatter CreateFormatter<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>();

    T DeepClone<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T instance, SerializationContext context);

    T DeepClone<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T instance, Object? userState = default);

    T Deserialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(
        ReadOnlySpan<Byte> source,
        #nullable disable
        T value = default(T),
        #nullable enable
        Object? userState = default);

    T Deserialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(
        ReadOnlySequence<Byte> source,
        #nullable disable
        T value = default(T),
        #nullable enable
        Object? userState = default);

    T Deserialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(
        ReadOnlyMemory<Byte> source,
        #nullable disable
        T value = default(T),
        #nullable enable
        Object? userState = default);

    Object Deserialize([DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] Type type, Stream source);

    T Deserialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(
        Stream source,
        #nullable disable
        T value = default(T),
        #nullable enable
        Object? userState = default,
        Int64 length = -1);

    T Deserialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, T value, SerializationContext context, Int64 length = -1);

    T Deserialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source);

    IEnumerable<T> DeserializeItems<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, PrefixStyle style, Int32 fieldNumber);

    T DeserializeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, PrefixStyle style);

    T DeserializeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, PrefixStyle style, Int32 fieldNumber);

    String GetProto<T>();

    String GetProto<T>(ProtoSyntax syntax);

    String GetProto(SchemaGenerationOptions options);

    MeasureState<T> Measure<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T value, Object? userState = default, Int64 abortAfter = -1);

    T Merge<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, T instance);

    IProtobufGenericSerializer Merge<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(XmlReader reader, T instance) where T : IXmlSerializable;

    IProtobufGenericSerializer Merge<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, StreamingContext context, T instance) where T : class, ISerializable;

    IProtobufGenericSerializer Merge<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, T instance) where T : class, ISerializable;

    T MergeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, T instance, PrefixStyle style);

    IProtobufGenericSerializer PrepareSerializer<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>();

    IProtobufGenericSerializer Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, StreamingContext context, T instance) where T : class, ISerializable;

    IProtobufGenericSerializer Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream destination, T instance);

    IProtobufGenericSerializer Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream destination, T instance, Object userState);

    IProtobufGenericSerializer Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(IBufferWriter<Byte> destination, T instance, Object? userState = default);

    IProtobufGenericSerializer Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, T instance) where T : class, ISerializable;

    IProtobufGenericSerializer Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(XmlWriter writer, T instance) where T : IXmlSerializable;

    IProtobufGenericSerializer SerializeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream destination, T instance, PrefixStyle style);

    IProtobufGenericSerializer SerializeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream destination, T instance, PrefixStyle style, Int32 fieldNumber);

    Boolean TryReadLengthPrefix(Byte[] buffer, Int32 index, Int32 count, PrefixStyle style, out Int32 length);

    Boolean TryReadLengthPrefix(Stream source, PrefixStyle style, out Int32 length);
}

public partial class ProtobufSerializer :
    IProtobufSerializer,
    IProtobufGenericSerializer,
    IProtobufStaticSerializer
{
    TTo IProtobufGenericSerializer.ChangeType<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] TFrom, [DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] TTo>(TFrom instance) =>
        ProtoBuf.Serializer.ChangeType<TFrom, TTo>(instance);

    IFormatter IProtobufGenericSerializer.CreateFormatter<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>() =>
        ProtoBuf.Serializer.CreateFormatter<T>();

    T IProtobufGenericSerializer.DeepClone<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T instance, SerializationContext context) =>
        ProtoBuf.Serializer.DeepClone(instance, context);

    T IProtobufGenericSerializer.DeepClone<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T instance, Object? userState) =>
        ProtoBuf.Serializer.DeepClone(instance, userState);

    T IProtobufGenericSerializer.Deserialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(ReadOnlySpan<Byte> source, T value, Object? userState) =>
        ProtoBuf.Serializer.Deserialize(source, value, userState);

    T IProtobufGenericSerializer.Deserialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(ReadOnlySequence<Byte> source, T value, Object? userState) =>
        ProtoBuf.Serializer.Deserialize(source, value, userState);

    T IProtobufGenericSerializer.Deserialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(ReadOnlyMemory<Byte> source, T value, Object? userState) =>
        ProtoBuf.Serializer.Deserialize(source, value, userState);

    Object IProtobufGenericSerializer.Deserialize(Type type, Stream source) =>
        ProtoBuf.Serializer.Deserialize(type, source);

    T IProtobufGenericSerializer.Deserialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, T value, Object? userState, Int64 length) =>
        ProtoBuf.Serializer.Deserialize(source, value, userState, length);

    T IProtobufGenericSerializer.Deserialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, T value, SerializationContext context, Int64 length) =>
        ProtoBuf.Serializer.Deserialize(source, value, context, length);

    T IProtobufGenericSerializer.Deserialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source) =>
        ProtoBuf.Serializer.Deserialize<T>(source);

    IEnumerable<T> IProtobufGenericSerializer.DeserializeItems<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, PrefixStyle style, Int32 fieldNumber) =>
        ProtoBuf.Serializer.DeserializeItems<T>(source, style, fieldNumber);

    T IProtobufGenericSerializer.DeserializeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, PrefixStyle style) =>
        ProtoBuf.Serializer.DeserializeWithLengthPrefix<T>(source, style);

    T IProtobufGenericSerializer.DeserializeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, PrefixStyle style, Int32 fieldNumber) =>
        ProtoBuf.Serializer.DeserializeWithLengthPrefix<T>(source, style, fieldNumber);

    String IProtobufGenericSerializer.GetProto<T>() =>
        ProtoBuf.Serializer.GetProto<T>();

    String IProtobufGenericSerializer.GetProto<T>(ProtoSyntax syntax) =>
        ProtoBuf.Serializer.GetProto<T>(syntax);

    String IProtobufGenericSerializer.GetProto(SchemaGenerationOptions options) =>
        ProtoBuf.Serializer.GetProto(options);

    MeasureState<T> IProtobufGenericSerializer.Measure<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T value, Object? userState, Int64 abortAfter) =>
        ProtoBuf.Serializer.Measure(value, userState, abortAfter);

    T IProtobufGenericSerializer.Merge<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, T instance) =>
        ProtoBuf.Serializer.Merge(source, instance);

    IProtobufGenericSerializer IProtobufGenericSerializer.Merge<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(XmlReader reader, T instance)
    {
        ProtoBuf.Serializer.Merge(reader, instance);
        return this;
    }

    IProtobufGenericSerializer IProtobufGenericSerializer.Merge<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, StreamingContext context, T instance)
    {
        ProtoBuf.Serializer.Merge(info, context, instance);
        return this;
    }

    IProtobufGenericSerializer IProtobufGenericSerializer.Merge<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, T instance)
    {
        ProtoBuf.Serializer.Merge(info, instance);
        return this;
    }

    T IProtobufGenericSerializer.MergeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, T instance, PrefixStyle style) =>
        ProtoBuf.Serializer.MergeWithLengthPrefix(source, instance, style);

    IProtobufGenericSerializer IProtobufGenericSerializer.PrepareSerializer<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>()
    {
        ProtoBuf.Serializer.PrepareSerializer<T>();
        return this;
    }

    IProtobufGenericSerializer IProtobufGenericSerializer.Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, StreamingContext context, T instance)
    {
        ProtoBuf.Serializer.Serialize(info, context, instance);
        return this;
    }

    IProtobufGenericSerializer IProtobufGenericSerializer.Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream destination, T instance)
    {
        ProtoBuf.Serializer.Serialize(destination, instance);
        return this;
    }

    IProtobufGenericSerializer IProtobufGenericSerializer.Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream destination, T instance, Object userState)
    {
        ProtoBuf.Serializer.Serialize(destination, instance, userState);
        return this;
    }

    IProtobufGenericSerializer IProtobufGenericSerializer.Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(IBufferWriter<Byte> destination, T instance, Object? userState)
    {
        ProtoBuf.Serializer.Serialize(destination, instance, userState);
        return this;
    }

    IProtobufGenericSerializer IProtobufGenericSerializer.Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, T instance)
    {
        ProtoBuf.Serializer.Serialize(info, instance);
        return this;
    }

    IProtobufGenericSerializer IProtobufGenericSerializer.Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(XmlWriter writer, T instance)
    {
        ProtoBuf.Serializer.Serialize(writer, instance);
        return this;
    }

    IProtobufGenericSerializer IProtobufGenericSerializer.SerializeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream destination, T instance, PrefixStyle style)
    {
        ProtoBuf.Serializer.SerializeWithLengthPrefix(destination, instance, style);
        return this;
    }

    IProtobufGenericSerializer IProtobufGenericSerializer.SerializeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream destination, T instance, PrefixStyle style, Int32 fieldNumber)
    {
        ProtoBuf.Serializer.SerializeWithLengthPrefix(destination, instance, style, fieldNumber);
        return this;
    }

    Boolean IProtobufGenericSerializer.TryReadLengthPrefix(Byte[] buffer, Int32 index, Int32 count, PrefixStyle style, out Int32 length) =>
        ProtoBuf.Serializer.TryReadLengthPrefix(buffer, index, count, style, out length);

    Boolean IProtobufGenericSerializer.TryReadLengthPrefix(Stream source, PrefixStyle style, out Int32 length) =>
        ProtoBuf.Serializer.TryReadLengthPrefix(source, style, out length);
}