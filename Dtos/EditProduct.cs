namespace InternshipTask.Dtos;

public class EditProduct
{  
    public string? Title { get; set; }
    public int Quantiy { get; set; }
    public double Price { get; set; }
    public  Guid UserId { get; set; }
}