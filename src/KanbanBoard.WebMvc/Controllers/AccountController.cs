using AspNetCoreHero.ToastNotification.Abstractions;
using KanbanBoard.Application.Dtos.IdentityDtos;
using KanbanBoard.Application.Services.Manager;
using KanbanBoard.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.IO;
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
        if (string.IsNullOrWhiteSpace(login.Password))
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


    public async Task<IActionResult> Register()
    {
        var titles = await _manager.UserTitle.GetTitlesAsync();
        ViewBag.titles = new SelectList(titles, "Id", "Title");
       
        ViewBag.pageName = "Account / Sign up";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto register)
    {
        var errors = new List<string>();

        if(string.IsNullOrWhiteSpace(register.FirstName))
        {
            errors.Add("First name cannot be empty!");
        }
        if (string.IsNullOrWhiteSpace(register.LastName))
        {
            errors.Add("Last name cannot be empty!");
        }
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
            FirstName = register.FirstName,
            LastName = register.LastName,
            Email = register.Email,
            UserName = register.Username,
            TitleId = register.TitleId
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

    public async Task<IActionResult> Profile()
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

    [HttpPost]
    public async Task<IActionResult> Update(string username, string email, string firstName, string lastName)
    {
        var user = await _userManager.FindByNameAsync(User.Identity!.Name!);

        user.FirstName = firstName;
        user.LastName = lastName;
        user.UserName = username;
        user.Email = email;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            _notyfService.Success("Profile has been updated successful.");
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        foreach (var error in result.Errors)
        {
            _notyfService.Error(error.Description);
        }
        return RedirectToAction(nameof(Profile));


    }

    [HttpPost]
    public async Task<IActionResult> UpdatePicture(IFormFile file)
    {
        var user = await _userManager.FindByNameAsync(User.Identity!.Name!);


        // User's Old picture is deleting from the directory
        System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", user!.ProfilePicture!));


        // User's new picture is adding to the directory
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", file.FileName);
        using var stream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream);
        user.ProfilePicture = file.FileName;


        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            _notyfService.Success("Profile picture has been updated successful.");
            return RedirectToAction(nameof(Profile));
        }

        foreach (var error in result.Errors)
        {
            _notyfService.Error(error.Description);
        }
        return RedirectToAction(nameof(Profile));


    }

    [HttpPost]
    public async Task<IActionResult> UploadPicture(IFormFile file)
    {
        var user = await _userManager.FindByNameAsync(User.Identity!.Name!);

        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", file.FileName);

        if (Directory.Exists(path))
        {
            _notyfService.Success("Picture is already exists!");
            return RedirectToAction(nameof(Profile));
        }

        using var stream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream);
        user!.ProfilePicture = file.FileName;


        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            _notyfService.Success("Profile picture has been updated successful.");
            return RedirectToAction(nameof(Profile));
        }

        foreach (var error in result.Errors)
        {
            _notyfService.Error(error.Description);
        }
        return RedirectToAction(nameof(Profile));


    }

}
