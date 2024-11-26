using Core.ViewModels;
using Core.ViewModels.ExamViewModels;
using Core.ViewModels.ExamViewModels.Quiz;

namespace Infrastructure.Services;
public interface IExamService<Entity> where Entity : class
{

    Task<ResponseViewModel<int>> CreateQuizOrFinal(CreateExamViewModel model);
    Task<ResponseViewModel<int>> AssignStudents(AssignExamToStudentsViewModel model);
    Task<ResponseViewModel<int>> CreateRandomExam(CreateRandomExam model);
    Task<ResponseViewModel<int>> TakeQuiz(TakeQuizViewModel model);

}
