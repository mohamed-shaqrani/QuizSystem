using Core.Enum;
using Core.ViewModels.QuestionViewModels;
using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.ExamViewModels;
public class CreateExamViewModel
{
    [Range(1, (int)ExamType.Final)]
    public ExamType ExamType { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]

    public string Title { get; set; }
    [Required]

    public List<QuestionViewModel> QuestionPools { get; set; } = new List<QuestionViewModel>();
    [Required]
    public int CourseId { get; set; }
    public string InstructorIdentityId { get; set; }


}
