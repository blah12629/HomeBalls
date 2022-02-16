namespace CEo.Pokemon.HomeBalls.ProtocolBuffers;

public partial interface IProtoBufStaticSerializer
{
    IProtoBufStaticSerializer Serialize(Stream stream, Object instance, Type type);

    // Task<Byte[]> SerializeAsync(Object instance, Type type, CancellationToken cancellationToken = default;

    // Byte[] Serialize(Object instance, Type type);

    Task<Byte[]> SerializeAsync(Object instance, CancellationToken cancellationToken = default);

    Task<Byte[]> SerializeWithLengthPrefixAsync(Object instance, PrefixStyle style, Int32 fieldNumber, CancellationToken cancellationToken = default);

    Byte[] Serialize(Object instance);

    Byte[] SerializeWithLengthPrefix(Object instance, PrefixStyle style, Int32 fieldNumber);
}

public partial class ProtoBufSerializer :
    IProtoBufSerializer,
    IProtoBufGenericSerializer,
    IProtoBufStaticSerializer
{
    protected internal virtual MethodInfo GetSerializeMethod(Type instanceType)
    {
        return typeof(IProtoBufGenericSerializer)
            .GetMethods(BindingFlags.Instance | BindingFlags.Public)
            .Single(method =>
                method.Name == nameof(IProtoBufGenericSerializer.Serialize) &&
                method.IsGenericMethod &&
                method.GetGenericArguments().Length == 1 &&
                method.GetParameters().Length == 2 &&
                method.GetParameters()[0].ParameterType == typeof(Stream));
    }


    IProtoBufStaticSerializer IProtoBufStaticSerializer
        .Serialize(Stream stream, Object instance, Type type)
    {
        GetSerializeMethod(type)
            .MakeGenericMethod(type)
            .Invoke(this, new Object?[] { stream, instance });

        return this;
    }

    Task<Byte[]> IProtoBufStaticSerializer.SerializeAsync(
        Object instance,
        CancellationToken cancellationToken) =>
        ExecuteSerializeAsync(
            stream => ForStaticTypes.Serialize(stream, instance),
            cancellationToken);

    Task<Byte[]> IProtoBufStaticSerializer.SerializeWithLengthPrefixAsync(
        Object instance,
        PrefixStyle style,
        Int32 fieldNumber,
        CancellationToken cancellationToken) =>
        ExecuteSerializeAsync(
            stream => ForStaticTypes.SerializeWithLengthPrefix(stream, instance, style, fieldNumber),
            cancellationToken);

    Byte[] IProtoBufStaticSerializer.Serialize(Object instance) =>
        ExecuteSerialize(stream => ForStaticTypes.Serialize(stream, instance));

    Byte[] IProtoBufStaticSerializer.SerializeWithLengthPrefix(
        Object instance,
        PrefixStyle style,
        Int32 fieldNumber) =>
        ExecuteSerialize(stream => ForStaticTypes
            .SerializeWithLengthPrefix(stream, instance, style, fieldNumber));
}