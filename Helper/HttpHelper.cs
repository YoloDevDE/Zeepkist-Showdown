using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Showdown3.Helper;

public class HttpHelper
{
    private readonly string _baseUri = "http://localhost:8080";
    private readonly HttpClient _httpClient;

    public HttpHelper()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(_baseUri) };
    }

    private string CreateFullUri(string url, Dictionary<string, string> queryParams = null)
    {
        var fullUri = new Uri(new Uri(_baseUri), url);
        if (queryParams != null)
        {
            var builder = new UriBuilder(fullUri) { Query = ToQueryString(queryParams) };
            return builder.ToString();
        }

        return fullUri.ToString();
    }

    private static string ToQueryString(Dictionary<string, string> queryParams)
    {
        var array = HttpUtility.ParseQueryString(string.Empty);
        foreach (var key in queryParams.Keys) array.Add(key, queryParams[key]);

        return array.ToString();
    }

    public async Task<string> GetAsync(string url, Dictionary<string, string> queryParams = null)
    {
        url = CreateFullUri(url, queryParams);
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> PostAsync(string url, string jsonData)
    {
        url = CreateFullUri(url);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> PutAsync(string url, string jsonData)
    {
        url = CreateFullUri(url);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(url, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    // Optional: Implementierung weiterer HTTP-Methoden wie DELETE
}