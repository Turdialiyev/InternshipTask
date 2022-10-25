using InternshipTask.Models;

namespace InternshipTask.Services;

public partial class ProductService
{
    private Product ToModel(Entities.Product entity)
   => new()
   {
       Id = entity.Id,
       Title = entity.Title,
       Quantiy = entity.Quantiy,
       Price = entity.Price,
       UserId = entity.UserId,
       TotalPrice = entity.TotalPrice
   };
}