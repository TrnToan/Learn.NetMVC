using LearndotNetMVC.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearndotNetMVC.Models;

public class Club
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Image { get; set; } = string.Empty;

    [ForeignKey("Address")]
    public int AddressId { get; set; }
    public Address Address { get; set; }
    public EClubCategory ClubCategory { get; set; }

    [ForeignKey("AppUser")]
    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

}
