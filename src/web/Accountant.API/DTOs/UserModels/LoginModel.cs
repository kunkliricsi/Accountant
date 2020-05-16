using System.ComponentModel.DataAnnotations;

namespace Accountant.API.DTOs.UserModels
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required.")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
