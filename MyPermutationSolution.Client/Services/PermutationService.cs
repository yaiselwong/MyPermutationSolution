using MyPermutationSolution.Client.Interfaces;
using MyPermutationSolution.Shared.DTO.Request;
using MyPermutationSolution.Shared.DTO.Response;
using System.Net.Http;

namespace MyPermutationSolution.Client.Services
{
    public class PermutationService: IPermutationService
    {
        private readonly ICustomHttpClient _httpClient;
        public PermutationService(
         ICustomHttpClient httpClient)
        {
           _httpClient = httpClient;
        }
        public async Task<PermutationResponse> GetNextPermutation(PermutationRequest dataRequest)
        {
            return await _httpClient.PostAsync<PermutationResponse>("Permutation/calculate", dataRequest);
        }
    }
}
