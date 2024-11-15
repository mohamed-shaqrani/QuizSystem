
namespace Core.Models;
public class Instructor :BaseModel
{
    public string IdentityId { get; set; } // Primary Key, same as AppUser's Id

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string Mobile { get; set; }
    public DateTime DateOfBirth { get; set; }
    public AppUser AppUser { get; set; }

    public ICollection<CourseInstructor> CourseInstructors { get; set; }


}
