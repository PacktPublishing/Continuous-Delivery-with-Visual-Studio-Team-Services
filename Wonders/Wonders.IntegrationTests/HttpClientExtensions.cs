using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Wonders.IntegrationTests
{
    public static class HttpClientExtensions
    {
        public static async Task<IActionResult> PostAsJsonAsync<T>(
            this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
                return new OkResult();
            return new NoContentResult();
        }

        public static async Task<IActionResult> PutAsJsonAsync<T>(
            this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await httpClient.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
                return new OkResult();
            return new NoContentResult();
        }

        public static async Task<IActionResult> GetAsync<T>(this HttpClient httpClient, string url)
        {
            var response = await httpClient.GetAsync(url);
            var stringContent = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(stringContent))
                return new NoContentResult();

            var content = JsonConvert.DeserializeObject<T>(stringContent);
            if (content == null)
                return new NoContentResult();
            return new OkObjectResult(content);
        }

        public static async Task<HttpStatusCode> DeleteAsJsonAsync(this HttpClient httpClient, string url)
        {
            var response = await httpClient.DeleteAsync(url);
            return response.StatusCode;
        }
    }
}