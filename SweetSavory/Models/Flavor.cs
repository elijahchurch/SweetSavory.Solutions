using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SweetSavory.Models
{
    public class Flavor
    {
        public int FlavorId { get; set; }
        [Required(ErrorMessage = "A name is required.")]
        public string Name { get; set; }
        public List<TreatFlavor> JoinTable { get; }
    }
}