#pragma warning disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InternshipTask.Dtos;
using InternshipTask.Models;
using InternshipTask.Services;

namespace Project.Controllers;

[Authorize]
public class ProductsController : Controller
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductService _service;

    public ProductsController(ILogger<ProductsController> logger, IProductService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _service.GetAllAsync();
        foreach (var c in User.Claims) Console.WriteLine($"{c.Type}, {c.Value}");
        if (data.Data == null)
            return View("Product NotFound");

        return View(data.Data);
    }

    public IActionResult Create()
    {
        ViewBag.userId = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProduct dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }
        await _service.CreateProduct(ToModel(dto));

        return RedirectToAction(nameof(Index));
    }

    [Route("Edit/{id}")]
    [HttpGet]
    public  IActionResult Edit(ulong id)
    {
        var ProductDetails =  _service.GetByIdAsync(id);

        if (ProductDetails.Data == null)
            return Redirect("/");

        ViewBag.CategoryId = (ulong)id;
        ViewBag.UserId = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;

        return View(ToDto(ProductDetails.Data));
    }

    private InternshipTask.Dtos.CreateProduct ToDto(Product? model)
    => new()
    {
        Title = model.Title,
        Quantiy = model.Quantiy,
        Price = model.Price,
        UserId = model.UserId.ToString(),
    };

    [Route("Edit/{id}")]
    [HttpPost]
    public async Task<IActionResult> Edit(ulong id, CreateProduct dto)
    {
        if (!ModelState.IsValid)
            return View(dto);
        
        var existProduct = _service.GetByIdAsync(id);

        if (existProduct.Data == null)
           return Redirect("Create");

        await _service.UpdateProduct(id, ToModel(dto));

        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(ulong id)
    {
        var ProductDetails = _service.GetByIdAsync(id);

        if (ProductDetails.Data == null)
            return Redirect("/");

        await _service.DeleteProduct(id);
        return RedirectToAction(nameof(Index));
    }

    private InternshipTask.Models.Product ToModel(CreateProduct dto)
    => new()
    {
        Title = dto.Title,
        Quantiy = dto.Quantiy,
        Price = dto.Price,
        UserId = new Guid(dto.UserId!)
    };
}