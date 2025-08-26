using MyPermutationSolution.Shared.DTO.Request;
using MyPermutationSolution.Shared.DTO.Response;

namespace MyPermutationSolution.Client.Interfaces
{
    public interface IPermutationService
    {
        Task<PermutationResponse> GetNextPermutation(PermutationRequest dataRequest);
    }
}
