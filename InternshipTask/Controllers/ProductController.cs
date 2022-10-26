#pragma warning disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InternshipTask.Models;
using InternshipTask.Services;

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

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Product model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.UserId = new Guid();

        await _service.CreateProduct(model);

        return RedirectToAction(nameof(Index));
    }

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

    [Route("Edit/{id}")]
    [HttpPost]
    public async Task<IActionResult> Edit(ulong id, Product model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var existProduct = _service.GetByIdAsync(id);

        if (existProduct.Data == null)
            return Redirect("Create");

        model.UserId = new Guid();
        // User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)!.Value

        await _service.UpdateProduct(id, model);

        return RedirectToAction(nameof(Index));
    }
    
    // [Route("Delete/{id}")]
    // [HttpGet]
    public async Task<IActionResult> Delete(ulong id)
    {
        var ProductDetails = _service.GetByIdAsync(id);

        if (ProductDetails.Data == null)
            return Redirect("/");

        await _service.DeleteProduct(id, new Guid().ToString());
        return RedirectToAction(nameof(Index));
    }
}