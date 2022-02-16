namespace CEo.Pokemon.HomeBalls.ProtocolBuffers;

public partial interface IProtoBufGenericSerializer
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

    IProtoBufGenericSerializer Merge<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(XmlReader reader, T instance) where T : IXmlSerializable;

    IProtoBufGenericSerializer Merge<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, StreamingContext context, T instance) where T : class, ISerializable;

    IProtoBufGenericSerializer Merge<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, T instance) where T : class, ISerializable;

    T MergeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, T instance, PrefixStyle style);

    IProtoBufGenericSerializer PrepareSerializer<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>();

    IProtoBufGenericSerializer Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, StreamingContext context, T instance) where T : class, ISerializable;

    IProtoBufGenericSerializer Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream destination, T instance);

    IProtoBufGenericSerializer Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream destination, T instance, Object userState);

    IProtoBufGenericSerializer Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(IBufferWriter<Byte> destination, T instance, Object? userState = default);

    IProtoBufGenericSerializer Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, T instance) where T : class, ISerializable;

    IProtoBufGenericSerializer Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(XmlWriter writer, T instance) where T : IXmlSerializable;

    IProtoBufGenericSerializer SerializeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream destination, T instance, PrefixStyle style);

    IProtoBufGenericSerializer SerializeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream destination, T instance, PrefixStyle style, Int32 fieldNumber);

    Boolean TryReadLengthPrefix(Byte[] buffer, Int32 index, Int32 count, PrefixStyle style, out Int32 length);

    Boolean TryReadLengthPrefix(Stream source, PrefixStyle style, out Int32 length);
}

public partial class ProtoBufSerializer :
    IProtoBufSerializer,
    IProtoBufGenericSerializer,
    IProtoBufStaticSerializer
{
    TTo IProtoBufGenericSerializer.ChangeType<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] TFrom, [DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] TTo>(TFrom instance) =>
        ProtoBuf.Serializer.ChangeType<TFrom, TTo>(instance);

    IFormatter IProtoBufGenericSerializer.CreateFormatter<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>() =>
        ProtoBuf.Serializer.CreateFormatter<T>();

    T IProtoBufGenericSerializer.DeepClone<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T instance, SerializationContext context) =>
        ProtoBuf.Serializer.DeepClone(instance, context);

    T IProtoBufGenericSerializer.DeepClone<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T instance, Object? userState) =>
        ProtoBuf.Serializer.DeepClone(instance, userState);

    T IProtoBufGenericSerializer.Deserialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(ReadOnlySpan<Byte> source, T value, Object? userState) =>
        ProtoBuf.Serializer.Deserialize(source, value, userState);

    T IProtoBufGenericSerializer.Deserialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(ReadOnlySequence<Byte> source, T value, Object? userState) =>
        ProtoBuf.Serializer.Deserialize(source, value, userState);

    T IProtoBufGenericSerializer.Deserialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(ReadOnlyMemory<Byte> source, T value, Object? userState) =>
        ProtoBuf.Serializer.Deserialize(source, value, userState);

    Object IProtoBufGenericSerializer.Deserialize(Type type, Stream source) =>
        ProtoBuf.Serializer.Deserialize(type, source);

    T IProtoBufGenericSerializer.Deserialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, T value, Object? userState, Int64 length) =>
        ProtoBuf.Serializer.Deserialize(source, value, userState, length);

    T IProtoBufGenericSerializer.Deserialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, T value, SerializationContext context, Int64 length) =>
        ProtoBuf.Serializer.Deserialize(source, value, context, length);

    T IProtoBufGenericSerializer.Deserialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source) =>
        ProtoBuf.Serializer.Deserialize<T>(source);

    IEnumerable<T> IProtoBufGenericSerializer.DeserializeItems<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, PrefixStyle style, Int32 fieldNumber) =>
        ProtoBuf.Serializer.DeserializeItems<T>(source, style, fieldNumber);

    T IProtoBufGenericSerializer.DeserializeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, PrefixStyle style) =>
        ProtoBuf.Serializer.DeserializeWithLengthPrefix<T>(source, style);

    T IProtoBufGenericSerializer.DeserializeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, PrefixStyle style, Int32 fieldNumber) =>
        ProtoBuf.Serializer.DeserializeWithLengthPrefix<T>(source, style, fieldNumber);

    String IProtoBufGenericSerializer.GetProto<T>() =>
        ProtoBuf.Serializer.GetProto<T>();

    String IProtoBufGenericSerializer.GetProto<T>(ProtoSyntax syntax) =>
        ProtoBuf.Serializer.GetProto<T>(syntax);

    String IProtoBufGenericSerializer.GetProto(SchemaGenerationOptions options) =>
        ProtoBuf.Serializer.GetProto(options);

    MeasureState<T> IProtoBufGenericSerializer.Measure<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(T value, Object? userState, Int64 abortAfter) =>
        ProtoBuf.Serializer.Measure(value, userState, abortAfter);

    T IProtoBufGenericSerializer.Merge<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, T instance) =>
        ProtoBuf.Serializer.Merge(source, instance);

    IProtoBufGenericSerializer IProtoBufGenericSerializer.Merge<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(XmlReader reader, T instance)
    {
        ProtoBuf.Serializer.Merge(reader, instance);
        return this;
    }

    IProtoBufGenericSerializer IProtoBufGenericSerializer.Merge<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, StreamingContext context, T instance)
    {
        ProtoBuf.Serializer.Merge(info, context, instance);
        return this;
    }

    IProtoBufGenericSerializer IProtoBufGenericSerializer.Merge<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, T instance)
    {
        ProtoBuf.Serializer.Merge(info, instance);
        return this;
    }

    T IProtoBufGenericSerializer.MergeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream source, T instance, PrefixStyle style) =>
        ProtoBuf.Serializer.MergeWithLengthPrefix(source, instance, style);

    IProtoBufGenericSerializer IProtoBufGenericSerializer.PrepareSerializer<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>()
    {
        ProtoBuf.Serializer.PrepareSerializer<T>();
        return this;
    }

    IProtoBufGenericSerializer IProtoBufGenericSerializer.Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, StreamingContext context, T instance)
    {
        ProtoBuf.Serializer.Serialize(info, context, instance);
        return this;
    }

    IProtoBufGenericSerializer IProtoBufGenericSerializer.Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream destination, T instance)
    {
        ProtoBuf.Serializer.Serialize(destination, instance);
        return this;
    }

    IProtoBufGenericSerializer IProtoBufGenericSerializer.Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream destination, T instance, Object userState)
    {
        ProtoBuf.Serializer.Serialize(destination, instance, userState);
        return this;
    }

    IProtoBufGenericSerializer IProtoBufGenericSerializer.Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(IBufferWriter<Byte> destination, T instance, Object? userState)
    {
        ProtoBuf.Serializer.Serialize(destination, instance, userState);
        return this;
    }

    IProtoBufGenericSerializer IProtoBufGenericSerializer.Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(SerializationInfo info, T instance)
    {
        ProtoBuf.Serializer.Serialize(info, instance);
        return this;
    }

    IProtoBufGenericSerializer IProtoBufGenericSerializer.Serialize<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(XmlWriter writer, T instance)
    {
        ProtoBuf.Serializer.Serialize(writer, instance);
        return this;
    }

    IProtoBufGenericSerializer IProtoBufGenericSerializer.SerializeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream destination, T instance, PrefixStyle style)
    {
        ProtoBuf.Serializer.SerializeWithLengthPrefix(destination, instance, style);
        return this;
    }

    IProtoBufGenericSerializer IProtoBufGenericSerializer.SerializeWithLengthPrefix<[DynamicallyAccessedMembers(DefaultDynamicallyAccessedMemberTypes)] T>(Stream destination, T instance, PrefixStyle style, Int32 fieldNumber)
    {
        ProtoBuf.Serializer.SerializeWithLengthPrefix(destination, instance, style, fieldNumber);
        return this;
    }

    Boolean IProtoBufGenericSerializer.TryReadLengthPrefix(Byte[] buffer, Int32 index, Int32 count, PrefixStyle style, out Int32 length) =>
        ProtoBuf.Serializer.TryReadLengthPrefix(buffer, index, count, style, out length);

    Boolean IProtoBufGenericSerializer.TryReadLengthPrefix(Stream source, PrefixStyle style, out Int32 length) =>
        ProtoBuf.Serializer.TryReadLengthPrefix(source, style, out length);
}