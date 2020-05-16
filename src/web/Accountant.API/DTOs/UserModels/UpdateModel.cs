using System.ComponentModel.DataAnnotations;

namespace Accountant.API.DTOs.UserModels
{
    public class UpdateModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required.")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
