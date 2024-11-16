using System.ComponentModel.DataAnnotations;

namespace Infrastructure.AuthService;

public class TokenRequestModel
{
    [Required]
    public string Email {  get; set; }
    [Required]

    public string Password { get; set; }
}