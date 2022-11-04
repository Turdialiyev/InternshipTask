#pragma warning disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InternshipTask.Models;
using InternshipTask.Services;
using InternshipTask.Repositories;
using InternshipTask.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace InternshipTask.Controllers;

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _service;

    public ProductController(ILogger<ProductController> logger, IProductService service)
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

    [Authorize(Roles = "admin")]
    public IActionResult Create() => View();

    [Authorize(Roles = "admin")]
    [Route("Edit/{id}")]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (id == null)
            return NotFound();

        var product =  _service.GetByIdAsync(id).Data;

        if (product == null)
            return NotFound();

        ViewBag.CategoryId = (ulong)id;
        return View(product);
    }


    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> Create(Product model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (model.Quantiy < 0)
        {
            ViewBag.IsQuantity = false;
            return View(model);
        }
        if (model.Price < 0)
        {
            ViewBag.IsPrice = false;
            return View(model);
        }

        var user = User?.FindFirst(ClaimTypes.NameIdentifier).Value;

        await _service.CreateProduct(user, model);

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "admin")]
    [Route("Edit/{id}")]
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (model.Quantiy < 0)
        {
            ViewBag.IsQuantity = false;
            return View(model);
        }
        if (model.Price < 0)
        {
            ViewBag.IsPrice = false;
            return View(model);
        }
        if (ModelState.IsValid)
        {
            var user = User?.FindFirst(ClaimTypes.NameIdentifier).Value;
             
            _service.UpdateProduct(id, user, model);
           
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id == null)
            return NotFound();

        var product =  _service.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        var user = User?.FindFirst(ClaimTypes.NameIdentifier).Value;    
        _logger.LogInformation($"===============> {user}");
        _service.DeleteProduct(id, user);
    
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> GetProductBydateFormat([FromForm] DateTime? from, [FromForm] DateTime? to)
    {
        var products = _service.GetAduitByDate(from, to);

        if (products == null)
            return Ok("Products are not chaged");

        return Ok(products.Result.Data);
    }

    [Authorize(Roles = "admin")]
    public IActionResult History() => View();
    
}