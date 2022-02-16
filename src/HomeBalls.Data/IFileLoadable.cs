namespace CEo.Pokemon.HomeBalls.Data;

public interface IFileLoadable
{
    void InDirectory(String directory);
}

public interface IFileLoadable<out T> : IFileLoadable
{
    new T InDirectory(String directory);
}