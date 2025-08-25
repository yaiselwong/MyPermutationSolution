using MyPermutationSolution.Shared.DTO.Request;
using MyPermutationSolution.Shared.DTO.Response;

namespace MyPermutationSolution.Server.Interfaces
{
    public interface IPermutationService
    {
        PermutationResponse CalculatePermutation(PermutationRequest request);
        Task<PermutationResponse> CalculatePermutationAsync(PermutationRequest request);
    }
}
