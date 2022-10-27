#pragma warning disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InternshipTask.Models;
using InternshipTask.Services;
using InternshipTask.Repositories;

namespace InternshipTask.Controllers;

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _service;
    private readonly IProductHistoryRepository _repo;

    public ProductController(ILogger<ProductController> logger, IProductService service, IProductHistoryRepository repository)
    {
        _logger = logger;
        _service = service;
        _repo = repository;
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
    public IActionResult Edit(ulong id)
    {
        var ProductDetails = _service.GetByIdAsync(id);

        if (ProductDetails.Data == null)
            return Redirect("/");

        ViewBag.CategoryId = (ulong)id;

        return View(ProductDetails.Data);
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

        model.UserId = new Guid(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

        await _service.CreateProduct(model);

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "admin")]
    [Route("Edit/{id}")]
    [HttpPost]
    public async Task<IActionResult> Edit(ulong id, Product model)
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

        var existProduct = _service.GetByIdAsync(id);

        if (existProduct.Data == null)
            return Redirect("Create");

        model.UserId = new Guid(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

        await _service.UpdateProduct(id, model);

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(ulong id)
    {
        var ProductDetails = _service.GetByIdAsync(id);

        if (ProductDetails.Data == null)
            return Redirect("/");

        await _service.DeleteProduct(id, User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> GetProductBydateFormat([FromForm] DateTime? from, [FromForm] DateTime? to)
    {
        var products = _service.GetProductHistoryAsync(from, to);

        if (products.Result.Data != null)
           return Ok("Products are not chaged");
       
        return Ok(products.Result.Data);
    }

    [Authorize(Roles = "admin")]
    public IActionResult History() => View();
}