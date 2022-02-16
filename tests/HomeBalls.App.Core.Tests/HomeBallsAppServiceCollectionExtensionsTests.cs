// namespace CEo.Pokemon.HomeBalls.App.Core;

// public class HomeBallsAppServiceCollectionExtensionsTests
// {
//     public HomeBallsAppServiceCollectionExtensionsTests()
//     {
//         BaseAddress = "https://localhost:5001/";
//         Sut = new ServiceCollection();
//     }

//     protected String BaseAddress { get; }

//     protected IServiceCollection Sut { get; }

//     [Fact]
//     public void AddHttpClients_ShouldConfigureDataClient()
//     {
//         var services = Sut
//             .AddHttpClients(BaseAddress)
//             .BuildServiceProvider();

//         var client = services
//             .GetRequiredService<IHttpClientFactory>()
//             .CreateClient(DataClientKey);

//         var address = client.BaseAddress?.ToString();
//         address.Should().Be($"{BaseAddress}data/");
//     }
// }