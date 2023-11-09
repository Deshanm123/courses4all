using System.ComponentModel.DataAnnotations;

namespace Courses4All.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(60,MinimumLength =5)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        [Display(Name="Postal Code")]
        [RegularExpression("^[A-Za-z]{1,2}[0-9A-Za-z]{1,2}[ ]?[0-9]{0,1}[A-Za-z]{2}$", ErrorMessage = "Please provide valid UK Postal Code")]
        public string PostCode { get; set; }

        [Required]
        [Display(Name ="Phone Number")]
        [RegularExpression("^\\s*\\(?(020[7,8]{1}\\)?[ ]?[1-9]{1}[0-9{2}[ ]?[0-9]{4})|(0[1-8]{1}[0-9]{3}\\)?[ ]?[1-9]{1}[0-9]{2}[ ]?[0-9]{3})\\s*$", ErrorMessage="Please Provide a Valid UK Phone number")]
        public string PhoneNumber { get; set; }

        public bool AcceptUserAgreement { get;set; }
        public string RegistrationInValid {  get; set; }


    }
}
