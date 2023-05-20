using System.ComponentModel.DataAnnotations;

namespace Talorants.Blog.Models;

public class ForgetpasswordViewModel
{
    [Required]
    public string Email { get; set; }
    
    // public bool EmailSend { get; set; }
}