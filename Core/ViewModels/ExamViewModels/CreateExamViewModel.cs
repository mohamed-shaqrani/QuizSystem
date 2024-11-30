using Core.Enum;
using Core.ViewModels.QuestionViewModels;
using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.ExamViewModels;
public class CreateExamViewModel
{

    [Range(1, (int)ExamType.Final)]
    public ExamType ExamType { get; set; }
    [Required]
    public required string Description { get; set; }
    [Required]
    public required string Title { get; set; }
    [Required]
    public int CourseId { get; set; }
    public string InstructorUserName { get; set; } = null!;
    public int InstructorId { get; set; }
    public bool IsRandom { get; set; }
    public int NoOfQuestionstions { get; set; }
    public DateTime EnrollemntEndDate { get; set; }
    public DateTime EnrollemntStartDate { get; set; }
    [Required]
    public List<QuestionViewModel> QuestionPools { get; set; } = new List<QuestionViewModel>();


}
