using LearndotNetMVC.Models.Enum;
using LearndotNetMVC.Models;

namespace LearndotNetMVC.ViewModels;

public class CreateRaceViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public Address Address { get; set; }
    public IFormFile Image { get; set; }
    public ERaceCategory RaceCategory { get; set; }
}
