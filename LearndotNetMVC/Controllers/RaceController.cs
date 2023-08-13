using LearndotNetMVC.DataContext;
using LearndotNetMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public IActionResult Detail(int id)
    {
        Race? race = _context.Races
            .Include(r => r.Address)
            .FirstOrDefault(r => r.Id == id);

        return View(race);
    }

    public IActionResult Create(Race race)
    {
        if (!ModelState.IsValid)
        {
            return View(race);
        }
        _context.Races.Add(race);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
