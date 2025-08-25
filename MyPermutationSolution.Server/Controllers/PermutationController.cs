using Microsoft.AspNetCore.Mvc;
using MyPermutationSolution.Server.Interfaces;
using MyPermutationSolution.Shared.DTO.Request;

namespace MyPermutationSolution.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermutationController : ControllerBase
    {
        private readonly IPermutationService _permutationService;
        private readonly ILogger<PermutationController> _logger;

        public PermutationController(
            IPermutationService permutationService,
            ILogger<PermutationController> logger)
        {
            _permutationService = permutationService;
            _logger = logger;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculatePermutation([FromBody] PermutationRequest request)
        {
            try
            {

                var result = await _permutationService.CalculatePermutationAsync(request);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor" });
            }
        }

        [HttpGet("apihealth")]
        public IActionResult HealthCheck()
        {
            return Ok(new { Status = "Healthy", Timestamp = DateTime.UtcNow });
        }
    }
}
