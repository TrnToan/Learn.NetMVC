using System.ComponentModel.DataAnnotations;

namespace LearndotNetMVC.Models;

public class AppUser
{
    [Key]
    public string Id { get; set; }
    public int? Pace { get; set; }
    public int? Milage { get; set; }

    public Address? Address { get; set; }
    public IList<Club> Clubs { get; set; }
    public IList<Race> Races { get; set; }
}
