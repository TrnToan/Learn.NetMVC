using LearndotNetMVC.DataContext;
using LearndotNetMVC.Models;
using LearndotNetMVC.Services;
using LearndotNetMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearndotNetMVC.Controllers;
public class RaceController : Controller
{
    private readonly AppDbContext _context;
    private readonly ImageService _service;

    public RaceController(AppDbContext context, ImageService service)
    {
        _context = context;
        _service = service;
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

    public IActionResult Create(RaceViewModel raceVM)
    {
        if (ModelState.IsValid)
        {
            var result = _service.AddImage(raceVM.Image);

            var race = new Race
            {
                Title = raceVM.Title,
                Description = raceVM.Description,
                Image = result.Url.ToString(),
                Address = new Address(raceVM.Address.Street, raceVM.Address.City, raceVM.Address.State)
            };

            _context.Races.Add(race);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError(" ", "Image upload failed.");
        }

        return View(raceVM);
    }
}
