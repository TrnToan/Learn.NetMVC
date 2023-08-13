using LearndotNetMVC.Models;
using LearndotNetMVC.Models.Enum;

namespace LearndotNetMVC.ViewModels;

public class ClubViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Address Address { get; set; }
    public IFormFile Image { get; set; }
    public EClubCategory ClubCategory { get; set; }
}
