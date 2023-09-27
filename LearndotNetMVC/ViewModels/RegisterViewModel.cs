using System.ComponentModel.DataAnnotations;

namespace LearndotNetMVC.ViewModels;

public class RegisterViewModel
{
    [Display(Name = "Email Address")]
    [Required(ErrorMessage = "Email Address is required.")]
    public string Email { get; set; } = string.Empty;
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    [Display(Name = "Confirm your password")]
    [Required(ErrorMessage = "You must confirm your password.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password does not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
