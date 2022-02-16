namespace CEo.Pokemon.HomeBalls;

public interface IHomeBallsIdentifierService
{
    String GenerateIdentifier<TEntity>() where TEntity : notnull;

    String GenerateIdentifier(Type entityType);
}