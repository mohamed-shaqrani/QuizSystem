using Core.Enum;
using Core.ViewModels.QuestionViewModels.ChoiceViewMode;

namespace Core.ViewModels.QuestionViewModels;
public class BaseUpdateCreateQuestionViewModel
{
    public string Text { get; set; }
    public DifficultyLevel Level { get; set; }

    public int Grade { get; set; }
    public int InstructorId { get; set; }
    public int CourseId { get; set; }
    public string UserName { get; set; }
    public List<CreateChoicesViewModel> Choices { get; set; }

}
public static class BaseModelExetenstion
{
    public static BaseUpdateCreateQuestionViewModel ToBaseModel(this CreateQuestionViewModel viewModel)
    {
        var baseModel = new BaseUpdateCreateQuestionViewModel
        {
            CourseId = viewModel.CourseId,
            Grade = viewModel.Grade,
            Level = viewModel.Level,
            UserName = viewModel.UserName,
            InstructorId = viewModel.InstructorId,
            Text = viewModel.Text,
            Choices = viewModel.createChoicesViewModels.Select(x => new CreateChoicesViewModel
            {
                IsCorrect = x.IsCorrect,
                Text = x.Text,
            }).ToList()
        };
        return baseModel;
    }
    public static BaseUpdateCreateQuestionViewModel ToUpdateViewModel(this UpdateQuestionViewModel viewModel)
    {
        var baseModel = new BaseUpdateCreateQuestionViewModel
        {
            CourseId = viewModel.CourseId,
            Grade = viewModel.Grade,
            Level = viewModel.Level,
            UserName = viewModel.UserName,
            InstructorId = viewModel.InstructorId,
            Text = viewModel.Text,
            Choices = viewModel.updateChoicesViewModels.Select(x => new CreateChoicesViewModel
            {
                IsCorrect = x.IsCorrect,
                Text = x.Text,
            }).ToList()
        };
        return baseModel;
    }
}