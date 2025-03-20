﻿namespace Movies.Client.Services;

public class HttpClientFactorySamples : IIntegrationService
{
    public async Task RunAsync()
    {
        // await TestDisposeHttpClientAsync();
         await TestReuseHttpClientAsync();
    }

    private async Task TestDisposeHttpClientAsync()
    {
        for (var i = 0; i < 10; i++)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    "https://www.google.com");

                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Request completed with status code " +
                    $"{response.StatusCode}");
            }
        }
    }


    private async Task TestReuseHttpClientAsync()
    {
        var httpClient = new HttpClient();

        for (int i = 0; i < 10; i++)
        {
            var request = new HttpRequestMessage(
            HttpMethod.Get,
            "https://www.google.com");

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Request completed with status code " +
                $"{response.StatusCode}");
        }
    }
}
