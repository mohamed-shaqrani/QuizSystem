using Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;
public class Question : BaseModel
{
    [Required]

    public string Text { get; set; }
    [Required]

    public DifficultyLevel Level { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }
    public int InstructorId { get; set; }

    public Instructor Instructor { get; set; }
    public int Grade { get; set; }

    public ICollection<ExamQuestion> ExamQuestions { get; set; } = new List<ExamQuestion>();
    public ICollection<Choice> Choices { get; set; } = new List<Choice>();
}
