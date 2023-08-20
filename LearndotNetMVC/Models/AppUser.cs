using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearndotNetMVC.Models;

public class AppUser : IdentityUser
{
    public int? Pace { get; set; }
    public int? Milage { get; set; }

    [ForeignKey("Address")]
    public int AddressId { get; set; }
    public Address? Address { get; set; }
    public IList<Club> Clubs { get; set; }
    public IList<Race> Races { get; set; }
}
