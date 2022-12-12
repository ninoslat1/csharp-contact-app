using System.ComponentModel.DataAnnotations;

namespace LoginApp.ViewModels
{
    public class Login
    {
        [Required]
        [DataType(DataType.Text)]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
