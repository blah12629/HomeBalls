namespace CEo.Pokemon.HomeBalls.ProtocolBuffers;

public interface IProtoBufSerializer
{
    IProtoBufGenericSerializer ForGenericTypes { get; }

    IProtoBufStaticSerializer ForStaticTypes { get; }
}

public partial class ProtoBufSerializer :
    IProtoBufSerializer,
    IProtoBufGenericSerializer,
    IProtoBufStaticSerializer
{
    public ProtoBufSerializer(ILogger? logger = default) =>
        Logger = logger;

    public virtual IProtoBufGenericSerializer ForGenericTypes => this;

    public virtual IProtoBufStaticSerializer ForStaticTypes => this;

    protected internal ILogger? Logger { get; }

    protected internal virtual Byte[] ExecuteSerialize(
        Action<Stream> serializeAction)
    {
        using var stream = new MemoryStream();
        serializeAction(stream);
        return stream.ToArray();;
    }

    protected internal virtual async Task<Byte[]> ExecuteSerializeAsync(
        Action<Stream> serializeAction,
        CancellationToken cancellationToken = default)
    {
        var bytes = new Memory<Byte> { };
        await using (var stream = new MemoryStream())
        {
            serializeAction(stream);
            await stream.ReadAsync(bytes, cancellationToken);
        }
        return bytes.ToArray();
    }
}