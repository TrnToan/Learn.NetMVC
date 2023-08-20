using LearndotNetMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearndotNetMVC.DataContext;

public class AppDbContext : IdentityDbContext<AppUser>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}


    public DbSet<Race> Races { get; set; }
	public DbSet<Club> Clubs { get; set; }
	public DbSet<Address> Addresss { get; set; }
	public DbSet<City> Cities { get; set; }
}
