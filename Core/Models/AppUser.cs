using Microsoft.AspNetCore.Identity;

namespace Core.Models;
public class AppUser :IdentityUser
{
    public Student Student { get; set; }
    public Instructor Instructor { get; set; }

}
