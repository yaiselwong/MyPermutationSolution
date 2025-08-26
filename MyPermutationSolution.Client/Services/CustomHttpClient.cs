using Microsoft.AspNetCore.Components;
using MyPermutationSolution.Client.Interfaces;
using MyPermutationSolution.Shared.DTO.Response;
using Newtonsoft.Json;
using System.Text;

namespace MyPermutationSolution.Client.Services
{
    public class CustomHttpClient : ICustomHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CustomHttpClient> _logger;
        private readonly NavigationManager _navigation;

        public CustomHttpClient(HttpClient httpClient,
                           ILogger<CustomHttpClient> logger,
                           NavigationManager navigation)
        {
            _httpClient = httpClient;
            _logger = logger;
            _navigation = navigation;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            _logger.LogInformation("GetAsync start");
            var response = await _httpClient.GetAsync("api/" + url);
            _logger.LogInformation("GetAsync end" + " api/" + url);
            return await CheckResponse<T>(response);
        }


        public async Task<T> PostAsync<T>(string url, object requestObject)
        {
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(requestObject),
                Encoding.UTF8,
                "application/json-patch+json");

            var response = await _httpClient.PostAsync("api/" + url, requestContent);
            return await CheckResponse<T>(response);
        }

        public async Task<T> DeleteAsync<T>(string url)
        {
            var response = await _httpClient.DeleteAsync("api/" + url);
            return await CheckResponse<T>(response);
        }

        private async Task<T> CheckResponse<T>(HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();
            try
            {
                var _result = JsonConvert.DeserializeObject<ServerResponse<T>>(content);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Error ocurred. Error message: {0}.\r\n Error Description: {1}", _result.Message, _result.Description);

                    _navigation.NavigateTo("/errorpage");
                }
                return _result.Data;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("CustomHttpClient catch expception");
                _logger.LogInformation("Error ocurred. Error message: {0}.\r\n Error Description: {1}", ex.Message, ex.ToString());
                _navigation.NavigateTo("/errorpage");

            }

            return new ServerResponse<T>().Data;
        }
    }
}
