using System.Net.Http.Headers;
using Newtonsoft.Json;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace WebApplication1.Utils.Api;

public static class NotionApiCaller
{
    private static HttpClient ApiClient { get; set; }

    public static void InitializeClient()
    {
        ApiClient = new HttpClient();
        ApiClient.DefaultRequestHeaders.Clear();
        ApiClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public static async Task<T> GetFromDatabase<T>(string databaseId)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri =
                new Uri(
                    $"https://api.notion.com/v1/databases/{databaseId}/query"),
            Headers =
            {
                { "accept", "application/json" },
                { "Notion-Version", "2022-06-28" },
                {
                    "Authorization",
                    "secret_LMtX4OH1SM1K9omxermg5gx7Rb7XPaXrM4Wgjd7LQAs"
                }
            },
            Content = new StringContent("{\"page_size\":100}")
            {
                Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
            },
        };
        using var response = await ApiClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        // Console.WriteLine(body);
        var jsonBody = JsonConvert.DeserializeObject<T>(body) ??
                       throw new NullReferenceException();
        return jsonBody;
    }
}