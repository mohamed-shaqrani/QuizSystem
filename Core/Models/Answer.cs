namespace Core.Models;
public class Answer:BaseModel
{
    public string AnswerText { get; set; }
    public Student Student { get; set; }

    public Exam Exam { get; set; }
    public Question Question { get; set; }
    public Choice Choice { get; set; }
    public DateTime SubmittedAt { get; set; }
}
