using Microsoft.Extensions.Logging;
using Moq;
using MyPermutationSolution.Server.Services;
using MyPermutationSolution.Shared.DTO.Request;

namespace MyPermutationSolution.Test
{
    public class PermutationServiceTests
    {
        private readonly Mock<ILogger<PermutationService>> _loggerMock;
        private readonly PermutationService _service;

        public PermutationServiceTests()
        {
            _loggerMock = new Mock<ILogger<PermutationService>>();
            _service = new PermutationService(_loggerMock.Object);
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
            // Arrange
            int[] invalidArray = new int[101]; // Demasiado largo
            var request = new PermutationRequest { Vector = invalidArray };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _service.CalculatePermutation(request));
        }

    }
}
