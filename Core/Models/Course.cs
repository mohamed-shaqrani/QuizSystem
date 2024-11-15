namespace Core.Models;
public class Course :BaseModel
{

    public string Name {  get; set; }
    public string Description { get; set; }
    public ICollection<CourseInstructor> CourseInstructors { get; set; }
}
