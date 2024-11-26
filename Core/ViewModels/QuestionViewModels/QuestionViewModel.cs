using Core.ViewModels.QuestionViewModels.ChoiceViewMode;
using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.QuestionViewModels;
public class QuestionViewModel
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int QuestionOrder { get; set; }
    [Required]

    public int Marks { get; set; }
    public int QuestionRandomGrade { get; set; }
    [Required]
    public List<ChoicesViewModel> ChoicesViewModel { get; set; } = new List<ChoicesViewModel>();
}
