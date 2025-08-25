using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPermutationSolution.Shared.DTO.Request
{
    public class PermutationRequest
    {
        public required int[] Vector { get; set; }
        public string? ClientId { get; set; }
    }
}
