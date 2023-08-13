using Azure.Storage.Blobs;
using LearndotNetMVC.DataContext;
using LearndotNetMVC.Models;
using LearndotNetMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearndotNetMVC.Controllers;
public class ClubController : Controller
{
    private const string blobConnectionString = "";
    private const string containerName = "";
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
    public IActionResult Create(ClubViewModel clubVM)
    {
        if (!ModelState.IsValid)
        {
            return View(clubVM);
        }
        var imageFileUrl = UploadFile(clubVM.Image);
        var club = new Club
        {
            Title = clubVM.Title,
            Description = clubVM.Description,
            Image = imageFileUrl,
            Address = new Address(clubVM.Address.Street, clubVM.Address.City, clubVM.Address.State)
        };
        _context.Clubs.Add(club);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    private static string GenerateFileName(string fileName, string customerName)
    {
        try
        {
            string strFileName = string.Empty;
            string[] strName = fileName.Split(',');
            strFileName = customerName + DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmsfff") + "." + strName[strName.Length - 1];
            return strFileName;
        }
        catch (Exception)
        {
            return fileName;
        }
    }

    private string UploadFile(IFormFile file)
    {
        try
        {
            var filename = GenerateFileName(file.FileName, file.FileName);
            var fileUrl = "";
            BlobContainerClient container = new BlobContainerClient(blobConnectionString, containerName);
            BlobClient blob = container.GetBlobClient(filename);
            using (Stream stream = file.OpenReadStream())
            {
                blob.Upload(stream);
            }
            fileUrl = blob.Uri.AbsoluteUri;
            return fileUrl;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
