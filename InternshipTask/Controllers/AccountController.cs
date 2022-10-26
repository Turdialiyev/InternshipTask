using InternshipTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using InternshipTask.Models;

namespace Project.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(
    ILogger<AccountController> logger,
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Register(string returnUrl) => View();

    [HttpPost]
    public async Task<IActionResult> Register(User model)
    {

        if (!ModelState.IsValid)
            return View(model);

        var existUser = await _userManager.FindByNameAsync(model.UserName);

        if (existUser is not null)
        {
            ViewBag.UserExist = false;
            return View(model);
        }

        var user = new IdentityUser(model.UserName);
        var result = await _userManager.CreateAsync(user, model.Password);

        return LocalRedirect($"/Account/Login");
    }

    [HttpGet]
    public IActionResult Login(string returnUrl) => View();


    [HttpPost]
    public async Task<IActionResult> Login(User model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByNameAsync(model.UserName);

        if (user is null)
        {
            ViewBag.UserNotFount = false;
            _logger.LogInformation($"User is not found {model.UserName}");
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

        if (!result.Succeeded)
        {
            ViewBag.PasswordError = false;
            _logger.LogInformation($"Signing is not successful {result.IsNotAllowed}");

            return View(model);
        }
        _logger.LogInformation($"Signing is successful {result.IsNotAllowed}");

        return LocalRedirect($"/Product/Index");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

}