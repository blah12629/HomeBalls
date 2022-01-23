namespace CEo.Pokemon.HomeBalls.ProtocolBuffers;

public interface IProtobufSerializer
{
    IProtobufGenericSerializer ForGenericTypes { get; }

    IProtobufStaticSerializer ForStaticTypes { get; }
}

public partial class ProtobufSerializer :
    IProtobufSerializer,
    IProtobufGenericSerializer,
    IProtobufStaticSerializer
{
    public ProtobufSerializer(ILogger? logger = default) =>
        Logger = logger;

    public virtual IProtobufGenericSerializer ForGenericTypes => this;

    public virtual IProtobufStaticSerializer ForStaticTypes => this;

    protected internal ILogger? Logger { get; }

    protected internal virtual async Task<Byte[]> ExecuteSerializeAsync(
        Action<Stream> serializeAction,
        CancellationToken cancellationToken = default)
    {
        var bytes = new Memory<Byte> { };
        using (var stream = new MemoryStream())
        {
            serializeAction(stream);
            await stream.ReadAsync(bytes, cancellationToken);
        }
        return bytes.ToArray();
    }
}