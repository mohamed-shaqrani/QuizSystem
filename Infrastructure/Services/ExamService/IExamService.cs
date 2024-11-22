using Core.ViewModels;
using Core.ViewModels.ExamViewModels;

namespace Infrastructure.Services;
public interface IExamService<Entity> where Entity : class
{

    Task<ResponseViewModel<int>> CreateQuizOrFinal(CreateExamViewModel model);
    Task<ResponseViewModel<int>> AssignStudents(AssignExamToStudentsViewModel model);
}
