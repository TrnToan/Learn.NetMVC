using System.ComponentModel.DataAnnotations;

namespace LearndotNetMVC.Models;

public class Address
{
    public Address(string street, string city, string state)
    {
        Street = street;
        City = city;
        State = state;
    }

    [Key]
    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; } 
    public string State { get; set; } 
    public int ZipCode { get; set; }
}
