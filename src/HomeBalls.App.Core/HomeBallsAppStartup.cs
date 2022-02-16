namespace CEo.Pokemon.HomeBalls.App;

public interface IHomeBallsAppStartup
{
    Task StartupAsync(CancellationToken cancellationToken = default);
}

// public class HomeBallsAppStartup :
//     IHomeBallsAppStartup
// {
// }