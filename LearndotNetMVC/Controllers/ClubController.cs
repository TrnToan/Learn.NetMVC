using LearndotNetMVC.DataContext;
using LearndotNetMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    public IActionResult Detail(int id)
    {
        Club? club = _context.Clubs
            .Include(c => c.Address)
            .SingleOrDefault(c => c.Id == id);
        return View(club);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Club club)
    {
        if (!ModelState.IsValid)
        {
            return View(club);
        }
        _context.Clubs.Add(club);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
