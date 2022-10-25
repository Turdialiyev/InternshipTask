using InternshipTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using InternshipTask.Dtos;

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
    public IActionResult Register(string returnUrl) => View(new UserRegister() { ReturnUrl = returnUrl });

    [HttpPost]
    public async Task<IActionResult> Register(UserRegister dto)
    {

        if (!ModelState.IsValid)
            return View(dto);

        var existUser = await _userManager.FindByNameAsync(dto.UserName);

        if (existUser is not null)
        {
            ViewBag.UserExist = false;
            return View(dto);
        }

        var user = new IdentityUser(dto.UserName);
        var result = await _userManager.CreateAsync(user, dto.Password);

        return LocalRedirect($"/Account/Login?returnUrl={dto.ReturnUrl}");
    }

    [HttpGet]
    public IActionResult Login(string returnUrl) => View(new UserLogin() { ReturnUrl = returnUrl });


    [HttpPost]
    public async Task<IActionResult> Login(UserLogin dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var user = await _userManager.FindByNameAsync(dto.UserName);

        if (user is null)
        {
            ViewBag.UserNotFount = false;
            _logger.LogInformation($"User is not found {dto.UserName}");
            return View(dto);
        }

        var result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);

        if (!result.Succeeded)
        {
            ViewBag.PasswordError = false;
            _logger.LogInformation($"Signing is not successful {result.IsNotAllowed}");

            return View(dto);
        }
        _logger.LogInformation($"Signing is successful {result.IsNotAllowed}");

        return LocalRedirect($"{dto.ReturnUrl ?? "/"}");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

}