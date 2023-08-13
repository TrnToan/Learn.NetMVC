using Azure.Storage.Blobs;
using LearndotNetMVC.DataContext;
using LearndotNetMVC.Models;
using LearndotNetMVC.Services;
using LearndotNetMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearndotNetMVC.Controllers;
public class ClubController : Controller
{
    private readonly AppDbContext _context;
    private readonly ImageService _service;

    public ClubController(AppDbContext context, ImageService service)
    {
        _context = context;
        _service = service;
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
    public IActionResult Create(ClubViewModel clubVM)
    {
        if (ModelState.IsValid)
        {
            var result = _service.AddImage(clubVM.Image);

            var club = new Club
            {
                Title = clubVM.Title,
                Description = clubVM.Description,
                Image = result.Url.ToString(),
                Address = new Address(clubVM.Address.Street, clubVM.Address.City, clubVM.Address.State)
            };

            _context.Clubs.Add(club);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError(" ", "Image upload failed.");
        }

        return View(clubVM);
    }
}
