using AspNetCoreHero.ToastNotification.Abstractions;
using KanbanBoard.Application.Dtos.IdentityDtos;
using KanbanBoard.Application.Services.Manager;
using KanbanBoard.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KanbanBoard.WebMvc.Controllers;

public class AccountController : Controller
{
    private readonly IServiceManager _manager;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly INotyfService _notyfService;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, INotyfService notyfService, IServiceManager manager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _notyfService = notyfService;
        _manager = manager;
    }


    public IActionResult Login()
    {
        ViewBag.pageName = "Account / Sign in";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto login)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(login.Username))
        {
            errors.Add("Username cannot be empty!");
        }
        if(string.IsNullOrWhiteSpace(login.Password))
        {
            errors.Add("Password cannot be empty!");
        }

        if(errors.Count>0)
        {
            errors.ForEach(error =>
            {
                _notyfService.Warning(error);
            });
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
            return RedirectToAction("Index", "Board");
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
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(register.Username))
        {
            errors.Add("Username cannot be empty!");
        }
        if (string.IsNullOrWhiteSpace(register.Email))
        {
            errors.Add("Email cannot be empty!");
        }
        if (string.IsNullOrWhiteSpace(register.Password))
        {
            errors.Add("Password cannot be empty!");
        }

        if (errors.Count > 0)
        {
            errors.ForEach(error =>
            {
                _notyfService.Warning(error);

            });

            return View();
        }


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
            _notyfService.Success("Your login was successful.");
            return RedirectToAction("Login", "Account", new { username = register.Username });
        }

        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        _notyfService.Success("You have successfully logged out of the system.");
        return RedirectToAction(nameof(Login));
    }

    public async Task<IActionResult>Profile()
    {
        ViewBag.pageName = "Account / Profile";
        var username = User.Identity!.Name;
        var user = await _userManager.FindByNameAsync(username!);

        var userTitle = await _manager.UserTitle.GetTitleByIdAsync(user!.TitleId);

        var profileDto = new GetProfileDto
        {
            Username = username!,
            Email = user!.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            FullName = user.Fullname,
            Picture = user.ProfilePicture!,
            Title = userTitle.Title
        };

        return View(profileDto);
    }

}
