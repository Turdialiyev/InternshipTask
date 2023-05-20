namespace Talorants.Models;

public class CreateOrUpdatePostViewModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public IFormFile Image { get; set; }
}