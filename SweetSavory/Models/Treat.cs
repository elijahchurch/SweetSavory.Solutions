using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SweetSavory.Models
{
    public class Treat
    {
        public int TreatId { get; set; }
        [Required(ErrorMessage = "A name is required.")]
        public string Name { get; set;}
        [Required(ErrorMessage = "A price must be set.")]
        public int Price { get; set;}
        [Required(ErrorMessage = "A description is required.")]
        public string Description { get; set;}
        public List<TreatFlavor> JoinTable { get;}
    }
}