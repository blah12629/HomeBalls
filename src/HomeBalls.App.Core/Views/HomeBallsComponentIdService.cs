namespace CEo.Pokemon.HomeBalls.App.Views;

public interface IHomeBallsComponentIdService
{
    Int32 CreateNew();
}

public class HomeBallsComponentIdService :
    IHomeBallsComponentIdService
{
    public HomeBallsComponentIdService(
        ILogger? logger = default) =>
        Logger = logger;

    protected internal ILogger? Logger { get; }

    protected internal Object IdLock { get; } = new Object();

    protected internal Int32 CurrentId { get; set; } = 0;

    public virtual Int32 CreateNew()
    {
        Int32 id;
        lock(IdLock)
        {
            CurrentId = CurrentId + 1;
            id = CurrentId;
        }

        return id;
    }
}