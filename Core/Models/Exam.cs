using Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;
public class Exam : BaseModel
{
    [Required, StringLength(250)]

    public string Title { get; set; }
    public int? DurationInMinutes { get; set; }
    public DateTime? StartDateTime { get; set; }
    public bool IsActive { get; set; }
    public bool IsEnrolled { get; set; }
    public bool IsSubmitted { get; set; }
    public string Description { get; set; }
    public int MaxScore { get; set; }
    public DateTime? EnrollmentEndDate { get; set; }
    public ExamType ExamType { get; set; }
    public int NumberOfQuestions { get; set; }
    public int CourseId { get; set; }

    public Course Course { get; set; }

    public int InstructorId { get; set; }
    public bool IsRandom { get; set; }

    public Instructor Instructor { get; set; }
    public ICollection<ExamQuestion> ExamQuestions { get; set; } = new List<ExamQuestion>();
    public ICollection<ExamStudent> ExamStudents { get; set; } = new List<ExamStudent>();

}
