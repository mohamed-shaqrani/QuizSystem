using Core.ViewModels.ChoiceViewModels;
using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.QuestionViewModels;
public class QuestionDetailsViewModel
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int QuestionOrder { get; set; }
    [Required]

    public int Marks { get; set; }
    public int QuestionRandomGrade { get; set; }
    [Required]
    public List<ChoiceDetailsViewModel> ChoicesViewModel { get; set; } = new List<ChoiceDetailsViewModel>();
}
