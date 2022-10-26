namespace InternshipTask.Models;

public class Product
{
    public ulong Id { get; set; }
    public string? Title { get; set; }
    public int Quantiy { get; set; }
    public double Price { get; set; }
    public decimal TotalPrice { get; set; }

    public Guid UserId { get; set; }
}