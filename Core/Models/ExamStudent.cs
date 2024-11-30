namespace Core.Models;
public class ExamStudent : BaseModel
{
    public int ExamId { get; set; }
    public Exam Exam { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; }

    public DateTime DateTaken { get; set; }
    public int? Score { get; set; }
    public bool IsEnrolled { get; set; }
    public bool IsSubmitted { get; set; }
    public DateTime? EnrollDate { get; set; }
    public DateTime? SubmissionDate { get; set; }
}
