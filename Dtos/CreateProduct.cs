using System.ComponentModel.DataAnnotations;

namespace InternshipTask.Dtos;

public class CreateProduct
{
    [Display(Name = "Title ")]
    [Required(ErrorMessage = "Title is required ")]
   
    public string? Title { get; set; }

    [Display(Name = "Quantiy ")]
    [Required(ErrorMessage = "Quantiy is required ")]
    public int Quantiy { get; set; }

    [Display(Name = "Price ")]
    [Required(ErrorMessage = "Price is required ")]
    public double Price { get; set; }

    public string? UserId { get; set; }
}