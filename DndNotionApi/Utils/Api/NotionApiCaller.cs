using System.Net.Http.Headers;
using Newtonsoft.Json;
using WebApplication1.DTO;

namespace WebApplication1.Utils.Api;

/// <summary>
///     Static class to make calls to Notion API databases.
/// </summary>
public static class NotionApiCaller
{
    private static HttpClient ApiClient { get; set; }

    /// <summary>
    ///     Make a get requests towards the Notion API for the database with id
    ///     <paramref name="databaseId" />.
    /// </summary>
    /// <param name="databaseId">Id of database to call</param>
    /// <typeparam name="T">The DTO expected to be received from API call</typeparam>
    /// <typeparam name="TU">
    ///     The Model type that the DTO is expected to be converted to
    ///     when <typeparamref name="T" /><c>.Process()</c> is called
    /// </typeparam>
    /// <remarks>
    ///     If you wish to make a call to a database and expect <see cref="GodsJson" />
    ///     (value of <c>T</c>),
    ///     You must pass the type in the <see cref="IProcessableJson{T}" />, which is
    ///     <c>
    ///         List<![CDATA[<]]></c>
    ///     <see cref="God" /><c><![CDATA[>]]></c>
    /// </remarks>
    /// >
    /// <returns>the json body of type T that the API request returned</returns>
    /// <exception cref="NullReferenceException">
    ///     Thrown when the JSON conversion
    ///     returned an empty object.
    /// </exception>
    /// <exception cref="HttpRequestException">
    ///     Thrown when http response does not have
    ///     2xx code.
    /// </exception>
    public static async Task<T> GetFromDatabase<T, TU>(string databaseId)
        where T : IProcessableJson<TU>
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
            }
        };
        using var response = await ApiClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        var jsonBody = JsonConvert.DeserializeObject<T>(body) ??
                       throw new NullReferenceException();
        return jsonBody;
    }

    /// <summary>
    ///     Create and add default configuration
    /// </summary>
    public static void InitializeClient()
    {
        ApiClient = new HttpClient();
        ApiClient.DefaultRequestHeaders.Clear();
        ApiClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }
}