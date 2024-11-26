using Core.ViewModels;
using Core.ViewModels.QuestionViewModels;

namespace Infrastructure.Services.QuestionService;

public interface IQuestionService<Entity> where Entity : class
{
    Task<ResponseViewModel<List<QuestioInfoViewModel>>> GetAllQuestions(int instructorId, int courseId);
    Task<ResponseViewModel<int>> CreateNewQuestion(CreateQuestionViewModel model);
    Task<ResponseViewModel<int>> UpdateQuestion(UpdateQuestionViewModel model);
    Task<ResponseViewModel<int>> DeleteQuestion(int id);

}