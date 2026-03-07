using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^[A-Za-z0-9_-]{3,20}$", ErrorMessage = "allows alphanumeric characters, underscores, and hyphens with a length between 3 and 20 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Allows letters, digits, and special characters, with a minimum length of 8.")]
        public string Password { get; set; }
    }
}
