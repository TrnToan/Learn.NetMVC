using Azure.Storage.Blobs;
using LearndotNetMVC.DataContext;
using LearndotNetMVC.Models;
using LearndotNetMVC.Services;
using LearndotNetMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

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
    public IActionResult Create(CreateClubViewModel clubVM)
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

    public IActionResult Edit(int id)
    {
        var club = _context.Clubs.FirstOrDefault(c => c.Id == id);
        if(club is null)
        {
            return View("Error");
        }
        var clubVM = new EditClubViewModel
        {
            Title = club.Title,
            Description = club.Description,
            AddressId = club.AddressId,
            Address = club.Address,
            Url = club.Image,
            ClubCategory = club.ClubCategory
        };
        return View(clubVM);
    }

    [HttpPost]
    public IActionResult Edit(int id, EditClubViewModel editClubViewModel)
    {
        if(!ModelState.IsValid)
        {
            ModelState.AddModelError(" ", "Failed to edit");
            return View("edit", editClubViewModel);
        }

        var userClub = _context.Clubs.FirstOrDefault(c => c.Id == id);
        if (userClub != null)
        {
            try
            {
                _service.DeleteImage(userClub.Image);
            }
            catch(Exception)
            {
                ModelState.AddModelError(" ", "Failed to delete");
                return View(editClubViewModel);
            }
            var photoResult = _service.AddImage(editClubViewModel.Image);

            userClub.Title = editClubViewModel.Title;
            userClub.Description = editClubViewModel.Description;
            userClub.Image = photoResult.Url.ToString();
            userClub.Address = editClubViewModel.Address;
            userClub.AddressId = editClubViewModel.AddressId;
            //Club club = new Club
            //{
            //    Id = id,
            //    Description = editClubViewModel.Description,
            //    Image = photoResult.Url.ToString(),
            //    Address = editClubViewModel.Address
            //};
            //Console.WriteLine(_context.Entry(club).State);
            //Console.WriteLine(_context.Entry(userClub).State);
            //userClub = club;
            //Console.WriteLine(_context.Entry(club).State);
            //Console.WriteLine(_context.Entry(userClub).State);

            _context.Clubs.Update(userClub);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            return View(editClubViewModel);
        }
    }

    public IActionResult Delete(int id)
    {
        var club = _context.Clubs.FirstOrDefault(c => c.Id == id);
        if (club is null)
        {
            return View("Error");
        }
        return View(club);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteClub(int id)
    {
        var clubDetails = _context.Clubs.FirstOrDefault(c => c.Id == id);

        if (clubDetails == null)
        {
            return View("Error");
        }

        if (!string.IsNullOrEmpty(clubDetails.Image))
        {
            _service.DeleteImage(clubDetails.Image);
        }

        _context.Clubs.Remove(clubDetails);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
