using Core.Enum;
using Core.ViewModels.QuestionViewModels.ChoiceViewMode;

namespace Core.ViewModels.QuestionViewModels;
public class QuestioInfoViewModel
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DifficultyLevel Level { get; set; }
    public List<ChoicesInfoViewModel> Choices { get; set; } = new List<ChoicesInfoViewModel>();
}
