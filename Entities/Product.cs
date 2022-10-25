#pragma warning disable
namespace InternshipTask.Entities;

public class Product
{
    public ulong Id { get; set; }
    public string? Title { get; set; }
    public int Quantiy { get; set; }
    public double Price { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid UserId { get; set; }
    public string? CreatedAt { get; set; }

    [Obsolete("this constroctor obly be used by Ef Core")]
    public Product() { }

    public Product(Models.Product model, string? createdAt, decimal totalPrice)
    {
        Title = model.Title;
        Quantiy = model.Quantiy;
        Price = model.Price;
        UserId = model.UserId;
        CreatedAt = createdAt;
        TotalPrice = totalPrice;
    }
}