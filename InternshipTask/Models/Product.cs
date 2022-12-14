using System.ComponentModel.DataAnnotations;

namespace InternshipTask.Models;

public class Product
{
    public int Id { get; set; }
    [Required]
    public string? Title { get; set; }

    [Required(ErrorMessage = ("The Quantiy field is required"))]
    public int Quantiy { get; set; }
    [Required(ErrorMessage = ("The Price field is required"))]
    public double Price { get; set; }
    public decimal TotalPrice { get; set; }
}