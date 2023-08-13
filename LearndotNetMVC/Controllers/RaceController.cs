using LearndotNetMVC.DataContext;
using LearndotNetMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace LearndotNetMVC.Controllers;
public class RaceController : Controller
{
    private readonly AppDbContext _context;

    public RaceController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        List<Race> races = _context.Races.ToList();
        return View(races);
    }
}
