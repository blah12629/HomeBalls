namespace CEo.Pokemon.HomeBalls.ProtocolBuffers;

[ProtoContract]
[ProtoInclude(1, typeof(ProtobufEntity))]
[ProtoInclude(2, typeof(ProtobufEntityComponent))]
[ProtoInclude(3, typeof(ProtobufEntry))]
public abstract record ProtobufRecord { }