﻿using Movies.Client.Helpers;
using Movies.Client.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Movies.Client.Services;

public class CompressionSamples : IIntegrationService
{

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptionsWrapper _jsonSerializerOptionsWrapper;

    public CompressionSamples(IHttpClientFactory httpClientFactory,
             JsonSerializerOptionsWrapper jsonSerializerOptionsWrapper)
    {
        _jsonSerializerOptionsWrapper = jsonSerializerOptionsWrapper ??
            throw new ArgumentNullException(nameof(jsonSerializerOptionsWrapper));
        _httpClientFactory = httpClientFactory ??
            throw new ArgumentNullException(nameof(httpClientFactory));
    }
    public async Task RunAsync()
    {
        await GetPosterWithGZipCompressionAsync();
    }

    private async Task GetPosterWithGZipCompressionAsync()
    {
        var httpClient = _httpClientFactory.CreateClient("MoviesAPIClient");

        var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"api/movies/d8663e5e-7494-4f81-8739-6e0de1bea7ee/posters/{Guid.NewGuid()}");
        request.Headers.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        request.Headers.AcceptEncoding.Add(
            new StringWithQualityHeaderValue("gzip"));

        using (var response = await httpClient.SendAsync(request,
           HttpCompletionOption.ResponseHeadersRead))
        {
            var stream = await response.Content.ReadAsStreamAsync();

            response.EnsureSuccessStatusCode();

            var poster = await JsonSerializer.DeserializeAsync<Poster>(
                stream,
                _jsonSerializerOptionsWrapper.Options);
        }
    }
}

