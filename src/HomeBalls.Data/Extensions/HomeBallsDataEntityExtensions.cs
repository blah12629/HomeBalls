namespace CEo.Pokemon.HomeBalls.Data;

public static class HomeBallsDataEntityExtensions
{
    public static TEntity AdaptNamed<TEntity>(this IHomeBallsDataType data)
    {
        var entity = data.Adapt<TEntity>() ?? throw new InvalidCastException();
        return data is HomeBalls.Entities.INamed ?
            AdaptNames(entity as dynamic) :
            entity;
    }

    public static TEntity AdaptNames<TEntity>(this TEntity entity)
        where TEntity :
            notnull,
            HomeBalls.Entities.HomeBallsEntity,
            HomeBalls.Entities.INamed
    {
        IEnumerable<HomeBalls.Entities.HomeBallsString> adaptedNames =
            entity.Names
                .Select(name => (HomeBalls.Entities.HomeBallsString)(
                    name.GetType() == typeof(HomeBalls.Entities.HomeBallsString) ? name :
                    name.GetType() == typeof(HomeBallsString) ? ((HomeBallsString)name).ToBaseType() :
                    throw new NotSupportedException()))
                .ToList().AsReadOnly();

        var setter = typeof(TEntity)
            .GetProperty(nameof(entity.Names), BindingFlags.Instance | BindingFlags.Public)?
            .GetSetMethod() ?? throw new ArgumentNullException();

        setter.Invoke(entity, new Object?[] { adaptedNames });
        return entity;
    }
}