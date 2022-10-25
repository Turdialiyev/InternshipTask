#pragma warning disable
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InternshipTask.Repositories;

namespace Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductHistoryController : ControllerBase
{
    private IUnitOfWork _unitOfWork;

    public ProductHistoryController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
    [Authorize]
    public async Task<IActionResult> GetProductBydateFormat([FromForm]DateTime from, [FromQuery]DateTime to)
    {
        DateTime startTime = Convert.ToDateTime(from);
        DateTime endTime = Convert.ToDateTime(from);

        var UpdateProducts = _unitOfWork.ProductRepositories.GetAll().Where(p => p.UpdatedAt != null);
        var col = UpdateProducts.Where(p => Convert.ToDateTime(p.UpdatedAt) > startTime && Convert.ToDateTime(p.UpdatedAt) < endTime);
        return Ok(col);
    }
}

