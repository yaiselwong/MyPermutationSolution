using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPermutationSolution.Shared.DTO.Response
{
    public class PermutationResponse
    {
        public int[] RequestData { get; set; } = Array.Empty<int>();
        public int[] ResponseData { get; set; } = Array.Empty<int>();
        public DateTime CalculatedDate { get; set; }
        public required string Message { get; set; }
    }
}
