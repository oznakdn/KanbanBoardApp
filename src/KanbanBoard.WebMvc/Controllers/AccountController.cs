using AspNetCoreHero.ToastNotification.Abstractions;
using KanbanBoard.Application.Dtos.IdentityDtos;
using KanbanBoard.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBoard.WebMvc.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly INotyfService _notyfService;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, INotyfService notyfService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _notyfService = notyfService;
    }


    public IActionResult Login()
    {
        ViewBag.pageName = "Account / Sign in";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto login)
    {
       
        if(string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
        {
            _notyfService.Warning("Username and/or Password cannot be empty!");
            return View();
        }

        var user = await _userManager.FindByNameAsync(login.Username);

        if (user is null)
        {
            _notyfService.Warning("Username and/or Password is wrong!");
            return View();
        }

        var result = await _signInManager.PasswordSignInAsync(user!, login.Password, isPersistent: login.Remember ? true : false, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            _notyfService.Success("Login successful.");
            return RedirectToAction("Index", "Home");
        }

        _notyfService.Warning("Username and/or Password is wrong!");
        return View(login);
    }


    public IActionResult Register()
    {
        ViewBag.pageName = "Account / Sign up";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto register)
    {
        var existUser = await _userManager.FindByNameAsync(register.Username);

        if (existUser is not null)
        {
            _notyfService.Warning("You cannot use this username!");
        }

        var result = await _userManager.CreateAsync(new User
        {
            Email = register.Email,
            UserName = register.Username
        }, register.Password);


        if (result.Succeeded)
        {
            _notyfService.Success("Login successful.");
            return RedirectToAction("Login", "Account",new {username=register.Username});
        }

        _notyfService.Warning("Username, Email or/and Password invalid!");
        return View(register);
    }

}
