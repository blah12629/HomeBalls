namespace CEo.Pokemon.HomeBalls.Data;

public interface IFileLoadable
{
    void InDirectory(String directory);
}

public interface IFileLoadable<T> : IFileLoadable
{
    new T InDirectory(String directory);
}