using System.ComponentModel.DataAnnotations;

namespace Core.Models;
public class Choice : BaseModel
{
    [Required]
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
    [Required]
    public int QuestionId { get; set; }
    public Question Question { get; set; }

}
