using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPermutationSolution.Shared.Models
{
    public class NumberModel
    {
        [Required(ErrorMessage = "A number is mandatory")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Only numbers are avaliable")]
        [Range(1, 100, ErrorMessage = "The number must be beetween 1 y 100")]
        public string NumberAdded { get; set; } = string.Empty;
    }
}
