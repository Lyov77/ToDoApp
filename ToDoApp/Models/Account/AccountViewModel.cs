using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models.Account
{
    public class HomeViewModel
    {
        public LoginViewModel LoginViewModel { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Required field")]
        public string Login { get; set; }
        
        [Required(ErrorMessage = "Required field")]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Required field")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string RepeatPassword { get; set; }
}
}
