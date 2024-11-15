namespace Core.Models;
public class CourseInstructor :BaseModel
{
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public int InstructorId { get; set; }
    public Instructor Instructor { get; set; }

}
