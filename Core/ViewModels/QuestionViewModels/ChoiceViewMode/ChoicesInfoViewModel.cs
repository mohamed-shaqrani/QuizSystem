using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.QuestionViewModels.ChoiceViewMode;
public class ChoicesInfoViewModel
{
    [Required]
    public string Text { get; set; }
    [Required]

    public bool IsCorrect { get; set; }
}
