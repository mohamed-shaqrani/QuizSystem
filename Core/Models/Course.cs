using System.ComponentModel.DataAnnotations;

namespace Core.Models;
public class Course : BaseModel
{

    [Required,StringLength(250)]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    public ICollection<CourseInstructor> CourseInstructors { get; set; } = new List<CourseInstructor>();
    public ICollection<Exam> Exams { get; set; } = new List<Exam>();
    public ICollection<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();


}
