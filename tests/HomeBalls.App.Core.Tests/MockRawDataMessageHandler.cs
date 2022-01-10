namespace CEo.Pokemon.HomeBalls.App.Core.DataAccess.Tests;

public class MockRawDataMessageHandler : HttpMessageHandler
{
    public MockRawDataMessageHandler(
        HttpContent responseContent) =>
        ResponseContent = responseContent;

    public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.OK;

    public HttpContent ResponseContent { get; init; }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken) =>
        Task.FromResult(new HttpResponseMessage
        {
            Content = ResponseContent,
            StatusCode = StatusCode
        });
}