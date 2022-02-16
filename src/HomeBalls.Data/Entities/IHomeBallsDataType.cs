namespace CEo.Pokemon.HomeBalls.Data.Entities;

public interface IHomeBallsDataType
{
    static abstract Type BaseEntityType { get; }

    Object ToBaseType();
}

public interface IHomeBallsDataType<out TBaseType> :
    IHomeBallsDataType
    where TBaseType : class
{
    new TBaseType ToBaseType();

    Object IHomeBallsDataType.ToBaseType() => ToBaseType();
}