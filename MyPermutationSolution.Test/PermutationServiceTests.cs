using Microsoft.Extensions.Logging;
using Moq;
using MyPermutationSolution.Server.Interfaces;
using MyPermutationSolution.Server.Services;
using MyPermutationSolution.Shared.DTO.Request;

namespace MyPermutationSolution.Test
{
    public class PermutationServiceTests
    {
        private readonly Mock<ILogger<PermutationService>> _loggerMock;
        private readonly PermutationService _service;
        private readonly Mock<IMemoryCacheService> _cacheServiceMock;

        public PermutationServiceTests()
        {
            _loggerMock = new Mock<ILogger<PermutationService>>();
            _service = new PermutationService(_loggerMock.Object, _cacheServiceMock.Object);
        }

        [Fact]
        public void CalculatePermutation_WithSingleElement_ReturnsSameArray()
        {
            var request = new PermutationRequest { Vector = new[] { 1, 2, 3 } };

            var response = _service.CalculatePermutation(request);

            Assert.Equal(new[] { 1, 2, 3 }, response.RequestData);
            Assert.Equal(new[] { 1, 3, 2 }, response.ResponseData);
            Assert.Equal("The permutation was calculated successfully", response.Message);
        }
        [Fact]
        public void CalculateNextPermutation_WithInvalidArray_ThrowsArgumentException()
        {
            int[] invalidArray = new int[101];
            var request = new PermutationRequest { Vector = invalidArray };

            Assert.Throws<ArgumentException>(() => _service.CalculatePermutation(request));
        }

        [Theory]
        [InlineData(new[] { 1, 2, 3 }, new[] { 1, 3, 2 })]
        [InlineData(new[] { 3, 2, 1 }, new[] { 1, 2, 3 })]
        [InlineData(new[] { 1, 1, 5 }, new[] { 1, 5, 1 })]
        public void CalculateNextPermutation_Multiple_Result(int[] input, int[] expected)
        {
            var request = new PermutationRequest { Vector = input };
            var response = _service.CalculatePermutation(request);
            int[] result2 = response.ResponseData;

            // Assert - Ambos métodos deben dar el mismo resultado
            Assert.Equal(expected, result2);
        }

    }
}
