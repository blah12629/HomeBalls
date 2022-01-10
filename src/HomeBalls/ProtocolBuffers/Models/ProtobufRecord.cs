namespace CEo.Pokemon.HomeBalls.ProtocolBuffers;

[ProtoContract]
[ProtoInclude(1, typeof(ProtobufEntity))]
[ProtoInclude(2, typeof(ProtobufEntityComponent))]
public abstract record ProtobufRecord { }