using System.ComponentModel;

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
}