using Core.Models;
using Core.ViewModels;
using Core.ViewModels.ExamViewModels;

namespace Infrastructure.Services;
public interface IExamService<Entity> where Entity : class
{

    Task<ResponseViewModel<Exam>> CreateQuizOrFinal(CreateExamViewModel model);
}
