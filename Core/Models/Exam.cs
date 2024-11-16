using Core.Enum;

namespace Core.Models;
public class Exam :BaseModel
{
    public string Title { get; set; }
    public int? DurationInMinutes { get; set; }
    public DateTime? StartDateTime { get; set; }
    public bool IsActive { get; set; }
    public string Description { get; set; }

    public ExamType ExamType { get; set; }
    public int NumberOfQuestions { get; set; }
    public bool IsManualAssignment { get; set; }
    public Course Course { get; set; }
    public ICollection<ExamQuestion> ExamQuestions { get; set; } = new List<ExamQuestion>();
    public ICollection<ExamStudent> ExamStudents { get; set; } = new List<ExamStudent>(); 

}
