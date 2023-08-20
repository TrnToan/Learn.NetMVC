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

    public IActionResult Create(CreateRaceViewModel raceVM)
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

    public IActionResult Edit(int id)
    {
        var race = _context.Races.FirstOrDefault(c => c.Id == id);
        if (race is null)
        {
            return View("Error");
        }
        var clubVM = new EditRaceViewModel
        {
            Title = race.Title,
            Description = race.Description,
            AddressId = race.AddressId,
            Address = race.Address,
            Url = race.Image,
            RaceCategory = race.RaceCategory
        };
        return View(clubVM);
    }

    [HttpPost]
    public IActionResult Edit(int id, EditRaceViewModel editRaceViewModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(" ", "Failed to edit");
            return View("edit", editRaceViewModel);
        }

        var userRace = _context.Races.FirstOrDefault(c => c.Id == id);
        if (userRace != null)
        {
            try
            {
                _service.DeleteImage(userRace.Image);
            }
            catch (Exception)
            {
                ModelState.AddModelError(" ", "Failed to delete");
                return View(editRaceViewModel);
            }
            var photoResult = _service.AddImage(editRaceViewModel.Image);

            userRace.Title = editRaceViewModel.Title;
            userRace.Description = editRaceViewModel?.Description;
            userRace.Image = photoResult.Url.ToString();
            userRace.Address = editRaceViewModel.Address;
            userRace.AddressId = editRaceViewModel.AddressId;
            
            _context.Races.Update(userRace);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            return View(editRaceViewModel);
        }
    }
}
