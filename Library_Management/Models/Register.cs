using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    public class Register
    {
        public int Id { get; set; }
        public int Role_id { get; set; }
        [Required(ErrorMessage = "Firstname is required")]
        [RegularExpression(@"^[A-Za-z]{2,50}$", ErrorMessage = "Firstname allows only letters with a length between 2 and 50 characters.")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        [RegularExpression(@"^[A-Za-z]{2,50}$", ErrorMessage = "Lastname allows only letters with a length between 2 and 50 characters.")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password allows letters, digits, and special characters, with a minimum length of 8.")]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^[A-Za-z0-9_-]{3,20}$", ErrorMessage = "Username allows alphanumeric characters, underscores, and hyphens with a length between 3 and 20 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Mobile number must contain only digits with an optional '+' and be between 10 and 15 characters.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "ERP number is required")]
        [RegularExpression(@"^[A-Za-z0-9]{9}$", ErrorMessage = "ERP number allows alphanumeric characters with a length between 1 and 9.")]
        public string ERP_no { get; set; }

        public bool isActived { get; set; }
    }
}
