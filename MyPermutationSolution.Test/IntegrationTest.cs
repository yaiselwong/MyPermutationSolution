using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using MyPermutationSolution.Server.Interfaces;
using MyPermutationSolution.Server.Services;
using MyPermutationSolution.Shared.DTO.Request;
using MyPermutationSolution.Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyPermutationSolution.Test
{
    public class IntegrationTest: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        public IntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient(); 
        }

        [Fact]
        public async Task CalculatePermutation_ReturnsOk_ForValidRequest()
        {
            var request = new PermutationRequest
            {
                Vector = new int[] { 1, 2, 3 }
            };

            var response = await _client.PostAsJsonAsync("/api/permutation/calculate", request);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ServerResponse<PermutationResponse>>();

            Assert.NotNull(result);
            Assert.Equal(new int[] { 1, 3, 2 }, result.Data.ResponseData); // Siguiente permutación
        }
        [Fact]
        public async Task CalculatePermutation_ReturnsBadRequest_ForEmptyVector()
        {
            var request = new PermutationRequest
            {
                Vector = Array.Empty<int>()
            };

            var response = await _client.PostAsJsonAsync("/api/permutation/calculate", request);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData(new[] { 1, 2, 3 }, new[] { 1, 3, 2 })]
        [InlineData(new[] { 3, 2, 1 }, new[] { 1, 2, 3 })]
        [InlineData(new[] { 1, 1, 5 }, new[] { 1, 5, 1 })]
        [InlineData(new[] { 1, 3, 2 }, new[] { 2, 1, 3 })]
        [InlineData(new[] { 2, 3, 1 }, new[] { 3, 1, 2 })]
        public async Task CalculateNextPermutation_ValidInput_ReturnsCorrectResult(int[] input, int[] expected)
        {
            var request = new PermutationRequest { Vector = input };
            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsJsonAsync("/api/Permutation/calculate", request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ServerResponse<PermutationResponse>>();

            Assert.NotNull(result);
            Assert.Equal(input, result.Data.RequestData);
            Assert.Equal(expected, result.Data.ResponseData);
            Assert.Equal("The permutation was calculated successfully", result.Data.Message);
        }
    }


}
