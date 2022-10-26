namespace InternshipTask.Models;

public class ProductHistory
{
    public ulong Id { get; set; }
    public string? Title { get; set; }
    public int Quantiy { get; set; }
    public double Price { get; set; }
    public bool? IsDeleted { get; set; }
    public decimal TotalPrice { get; set; }
    public ulong ProductId { get; set; }
    public Guid UserId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DaletedAt { get; set; }

    // [Obsolete("this constroctor obly be used by Ef Core")]
    // public ProductHistory() { }

    // public ProductHistory(Product entity, DateTime updatedAt, DateTime? deletedAt = null, bool? isDeleted = false)
    // {
    //     Title = entity.Title;
    //     Quantiy = entity.Quantiy;
    //     Price = entity.Price;
    //     TotalPrice = entity.TotalPrice;
    //     ProductId = entity.Id;
    //     UserId = entity.UserId;
    //     CreatedAt = entity.CreatedAt;
    //     UpdatedAt = updatedAt;
    //     DaletedAt = deletedAt;
    //     IsDeleted = isDeleted;
    // }
}