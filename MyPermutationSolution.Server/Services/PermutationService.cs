using MyPermutationSolution.Server.Interfaces;
using MyPermutationSolution.Shared.DTO.Request;
using MyPermutationSolution.Shared.DTO.Response;

namespace MyPermutationSolution.Server.Services
{
    public class PermutationService : IPermutationService
    {
        private readonly ILogger<PermutationService> _logger;
        public readonly IMemoryCacheService _cacheService;

        public PermutationService(ILogger<PermutationService> logger,
            IMemoryCacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }

        public PermutationResponse CalculatePermutation(PermutationRequest request)
        {
            var cacheKey = GenerateCacheKey(request.Vector);
            if (_cacheService.Exists(cacheKey))
            {
                var response= _cacheService.Get<PermutationResponse>(cacheKey);
                response.Cache = "Result in Cache";
                return response;
            }
            ValidateRequest(request);

            int[] numbersCopy = new int[request.Vector.Length];
            Array.Copy(request.Vector, numbersCopy, request.Vector.Length);

            CalculatePermutation(numbersCopy);

            var responsedata= new PermutationResponse
            {
                RequestData = request.Vector,
                ResponseData = numbersCopy,
                CalculatedDate = DateTime.UtcNow,
                Message = "The permutation was calculated successfully"
            };
            _cacheService.Set(cacheKey, responsedata, TimeSpan.FromMinutes(60));
            return responsedata;
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
        private void ValidateRequest(PermutationRequest request)
        {
            if (request.Vector == null || request.Vector.Length == 0)
            {
                throw new ArgumentException("The data cannot be empty");
            }

            if (request.Vector.Length > 100)
            {
                throw new ArgumentException("The length of the data cannot exceed 100");
            }

            foreach (var number in request.Vector)
            {
                if (number < 0 || number > 100)
                {
                    throw new ArgumentException("The data must be Numbers between 0 and 100");
                }
            }
        }

        private static string GenerateCacheKey(int[] numbers)
        {
            return $"permutation:v1:{string.Join(":", numbers)}";
        }
    }
}
