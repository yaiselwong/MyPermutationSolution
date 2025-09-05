using MyPermutationSolution.Shared.DTO.Request;
using MyPermutationSolution.Shared.DTO.Response;

namespace MyPermutationSolution.Client.Shared.Interfaces
{
    public interface IPermutationService
    {
        Task<PermutationResponse> GetNextPermutation(PermutationRequest dataRequest);
    }
}
