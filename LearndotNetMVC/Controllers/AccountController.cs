using LearndotNetMVC.DataContext;
using LearndotNetMVC.Models;
using LearndotNetMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearndotNetMVC.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly AppDbContext _context;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    public IActionResult Login()
    {
        var response = new LoginViewModel();
        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginVM)
    {
        if (!ModelState.IsValid) return View(loginVM);

        var user = await _userManager.FindByEmailAsync(loginVM.UserName);
        if (user is not null)
        {
            bool passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
            if (passwordCheck)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["Error"] = "Wrong credentials.";
            return View(loginVM);
        }
        TempData["Error"] = "Wrong credentials. User not found.";
        return View(loginVM);
    }
}
