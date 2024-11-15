using Core.Enum;

namespace Core.Models;
public class Question :BaseModel
{
    public string Text { get; set; } 
    public DifficultyLevel Level { get; set; } 
    public ICollection<ExamQuestion> ExamQuestions { get; set; } = new List<ExamQuestion>();
    public ICollection<Choice> Choices { get; set; } = new List<Choice>();
}
