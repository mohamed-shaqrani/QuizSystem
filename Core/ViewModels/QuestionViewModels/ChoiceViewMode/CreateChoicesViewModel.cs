using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.QuestionViewModels.ChoiceViewMode;
public class CreateChoicesViewModel
{
    [Required]
    public string Text { get; set; }
    [Required]

    public bool IsCorrect { get; set; }
}
