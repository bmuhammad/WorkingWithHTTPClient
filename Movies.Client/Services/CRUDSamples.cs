using Movies.Client.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Xml.Serialization;

namespace Movies.Client.Services;

public class CRUDSamples : IIntegrationService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CRUDSamples(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ??
              throw new ArgumentNullException(nameof(httpClientFactory));
    }
    public async Task RunAsync()
    {
        // await GetResourceAsync();
        await GetResourceThroughHttpRequestMessageAsync();
    }

    public async Task GetResourceAsync()
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri("http://localhost:5001");
        httpClient.Timeout = new TimeSpan(0, 0, 30);

        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        httpClient.DefaultRequestHeaders.Accept.Add(
           new MediaTypeWithQualityHeaderValue("application/xml",
           0.9));


        var response = await httpClient.GetAsync("api/movies");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(); ;

        var movies = new List<Movie>();


        if (response.Content.Headers.ContentType?.MediaType
           == "application/json")
        {
            movies = JsonSerializer.Deserialize<List<Movie>>(
                content,
                new JsonSerializerOptions
                { 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
        }
        else if (response.Content.Headers.ContentType?.MediaType
            == "application/xml")
        {
            var serializer = new XmlSerializer(typeof(List<Movie>));
            movies = serializer.Deserialize(
                new StringReader(content)) as List<Movie>;
        }

    }

    public async Task GetResourceThroughHttpRequestMessageAsync()
    {
        var httpClient = _httpClientFactory.CreateClient("MoviesAPIClient");
        httpClient.BaseAddress = new Uri("http://localhost:5001");
        httpClient.Timeout = new TimeSpan(0, 0, 30);

        var request = new HttpRequestMessage(
            HttpMethod.Get,
            "api/movies");
        request.Headers.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        var movies = JsonSerializer.Deserialize<IEnumerable<Movie>>(
            content,
             new JsonSerializerOptions
             {
                 PropertyNamingPolicy = JsonNamingPolicy.CamelCase
             });
    }

}
