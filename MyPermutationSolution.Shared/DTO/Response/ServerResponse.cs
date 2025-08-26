using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPermutationSolution.Shared.DTO.Response
{
    public class ServerResponse<T>
    {
        public string? Message { get; set; }
        public string? Description { get; set; }
        public T? Data { get; set; }

    }
}
