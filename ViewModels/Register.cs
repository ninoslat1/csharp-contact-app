using System.ComponentModel.DataAnnotations;

namespace LoginApp.ViewModels
{
    public class Register
    {
        [Required]
        [DataType(DataType.Text)]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage ="Password and Confirm Password doesn't match")]
        public string? ConfirmPassword { get; set; }
    }
}
