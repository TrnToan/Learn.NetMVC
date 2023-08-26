using System.ComponentModel.DataAnnotations;

namespace LearndotNetMVC.ViewModels;

public class LoginViewModel
{
    [Display(Name = "Email Address")]
    [Required]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
