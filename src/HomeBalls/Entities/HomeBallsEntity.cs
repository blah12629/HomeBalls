namespace CEo.Pokemon.HomeBalls.Entities;

[ProtoContract]
[ProtoInclude(1, typeof(HomeBallsByteEntity))]
[ProtoInclude(2, typeof(HomeBallsUInt16Entity))]
[ProtoInclude(3, typeof(HomeBallsPokemonFormEntity))]
[ProtoInclude(4, typeof(HomeBallsEntryEntity))]
public abstract record HomeBallsEntity : IHomeBallsEntity
{
    public HomeBallsEntity() => EntityType = GetType();

    public virtual Type EntityType { get; }
}