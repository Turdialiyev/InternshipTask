namespace Talorants.Models;

public class PostViewModel
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Image { get; set; }
    public string AppUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsEdited { get; set; }
}