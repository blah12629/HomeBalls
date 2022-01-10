namespace System.Net.Http;

public static class HomeBallsHttpClientExtensions
{
    public static Task<HttpResponseMessage> HeadAsync(
        this HttpClient client,
        CancellationToken cancellationToken = default) =>
        HeadAsync(client, default, cancellationToken);

    public static async Task<HttpResponseMessage> HeadAsync(
        this HttpClient client,
        String? requestUri,
        CancellationToken cancellationToken = default)
    {
        var response = await client.SendAsync(
            new HttpRequestMessage(HttpMethod.Head, requestUri),
            cancellationToken);

        return response;
    }

    public static Task<Boolean> IsSuccessAsync(
        this HttpClient client,
        CancellationToken cancellationToken = default) =>
        IsSuccessAsync(client, default, cancellationToken);

    public static async Task<Boolean> IsSuccessAsync(
        this HttpClient client,
        String? requestUri,
        CancellationToken cancellationToken = default) =>
        (await HeadAsync(client, requestUri, cancellationToken)).IsSuccessStatusCode;

}