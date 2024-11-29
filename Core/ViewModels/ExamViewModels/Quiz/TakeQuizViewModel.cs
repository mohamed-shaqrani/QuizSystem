using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.ExamViewModels.Quiz;
public class TakeQuizViewModel
{
    [Required, Range(1, int.MaxValue, ErrorMessage = "QuizId is invalid ")]
    public int QuizId { get; set; }
    [Required, Range(1, int.MaxValue, ErrorMessage = "StudentId is invalid ")]

    public int StudentId { get; set; }
    [Required, Range(1, int.MaxValue, ErrorMessage = "CourseId is invalid ")]

    public int CourseId { get; set; }

}
