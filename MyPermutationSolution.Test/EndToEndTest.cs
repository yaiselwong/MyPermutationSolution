using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using MyPermutationSolution.Shared.DTO.Request;
using MyPermutationSolution.Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyPermutationSolution.Test
{
    public class EndToEndTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public EndToEndTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CalculatePermutation_ReturnsNextPermutation()
        {
            var request = new PermutationRequest { Vector = new int[] { 1, 2, 3 } };
            var response = await _client.PostAsJsonAsync("/api/permutation/calculate", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServerResponse<PermutationResponse>>();
            result.Should().NotBeNull();
            result.Data.ResponseData.Should().Equal(new int[] { 1, 3, 2 });
            result.Data.Message.Should().Be("The permutation was calculated successfully");
        }

        [Fact]
        public async Task CalculatePermutation_ReturnsBadRequest_WhenVectorIsEmpty()
        {
            var request = new PermutationRequest { Vector = Array.Empty<int>() };

            var response = await _client.PostAsJsonAsync("/api/permutation/calculate", request);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            error.Should().ContainKey("error");
            error["error"].Should().Be("The data cannot be empty");
        }

        [Fact]
        public async Task CalculatePermutation_UsesCache_WhenRequestRepeated()
        {
            var request = new PermutationRequest { Vector = new int[] { 1, 2, 3 } };

            var first = await _client.PostAsJsonAsync("/api/permutation/calculate", request);
            first.EnsureSuccessStatusCode();
            var firstResult = await first.Content.ReadFromJsonAsync<ServerResponse<PermutationResponse>>();

            var second = await _client.PostAsJsonAsync("/api/permutation/calculate", request);
            second.EnsureSuccessStatusCode();
            var secondResult = await second.Content.ReadFromJsonAsync<ServerResponse<PermutationResponse>>();

            secondResult.Data.Cache.Should().Be("Result in Cache");
        }
    }
}
