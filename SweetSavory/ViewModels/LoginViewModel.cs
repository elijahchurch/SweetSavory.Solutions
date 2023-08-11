using System.ComponentModel.DataAnnotations;

namespace SweetSavory.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Email Address")]
        public string Email {get; set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}