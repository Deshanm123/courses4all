using System.ComponentModel.DataAnnotations;

namespace Courses4All.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(60,MinimumLength =5)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(60,MinimumLength =5,ErrorMessage = "Password should be longer than 5 Characters.")]
        public string Password { get; set; }


        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        
        public string LoginInvalid {get;set;}
    }
}
