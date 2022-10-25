using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InternshipTask.Models;
using InternshipTask.Services;

namespace Project.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _service;

    public HomeController(ILogger<HomeController> logger, IProductService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _service.GetAllAsync();
        if (data.Data == null)
            return View("Product NotFound");

        return View(data.Data);
    }

}
