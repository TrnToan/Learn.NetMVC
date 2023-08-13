using LearndotNetMVC.DataContext;
using LearndotNetMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearndotNetMVC.Controllers;
public class ClubController : Controller
{
    private readonly AppDbContext _context;

    public ClubController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        List<Club> clubs = _context.Clubs.ToList();
        return View(clubs);
    }
}
