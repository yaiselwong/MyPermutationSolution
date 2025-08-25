using MyPermutationSolution.Server.Interfaces;
using MyPermutationSolution.Shared.DTO.Request;
using MyPermutationSolution.Shared.DTO.Response;

namespace MyPermutationSolution.Server.Services
{
    public class PermutationService : IPermutationService
    {
        private readonly ILogger<PermutationService> _logger;

        public PermutationService(ILogger<PermutationService> logger)
        {
            _logger = logger;
        }

        public PermutationResponse CalculatePermutation(PermutationRequest request)
        {

            int[] numbersCopy = new int[request.Vector.Length];
            Array.Copy(request.Vector, numbersCopy, request.Vector.Length);

            CalculatePermutation(numbersCopy);

            return new PermutationResponse
            {
                RequestData = request.Vector,
                ResponseData = numbersCopy,
                CalculatedDate = DateTime.UtcNow,
                Message = "The permutation was calculated successfully"
            };
        }

        public Task<PermutationResponse> CalculatePermutationAsync(PermutationRequest request)
        {
            return Task.FromResult(CalculatePermutation(request));
        }

        private void CalculatePermutation(int[] nums)
        {
            if (nums.Length <= 1) return;

            int i = nums.Length - 2;
            while (i >= 0 && nums[i] >= nums[i + 1])
            {
                i--;
            }

            if (i >= 0)
            {
                int j = nums.Length - 1;
                while (j > i && nums[j] <= nums[i])
                {
                    j--;
                }
                (nums[i], nums[j]) = (nums[j], nums[i]);
            }

            int left = i + 1;
            int right = nums.Length - 1;
            while (left < right)
            {
                (nums[left], nums[right]) = (nums[right], nums[left]);
                left++;
                right--;
            }
        }
    }
}
