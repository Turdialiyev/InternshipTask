namespace Talorants.Models;

public class SignUpViewModel
{
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    // public string RePassword { get; set; }
    public IFormFile Avatar { get; set; }
    public string ReturnUrl { get; set; }
}