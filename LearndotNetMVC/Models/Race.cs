﻿using LearndotNetMVC.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearndotNetMVC.Models;

public class Race
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Image { get; set; } = string.Empty;

    [ForeignKey("Address")]
    public int AddressId { get; set; }
    public Address Address { get; set; }
    public ERaceCategory RaceCategory { get; set; }

    [ForeignKey("AppUser")]
    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}
