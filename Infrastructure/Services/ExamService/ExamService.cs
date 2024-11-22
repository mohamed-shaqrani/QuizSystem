using Core.Models;
using Core.ViewModels;
using Core.ViewModels.ExamViewModels;
using Core.ViewModels.QuestionViewModels;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ExamService;
public class ExamService<Entity> : IExamService<Entity> where Entity : class
{
    //private readonly IUnitOfWork _unitOfWork;
    private readonly AppDbContext _context;

    public ExamService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<ResponseViewModel<Exam>> CreateQuizOrFinal(CreateExamViewModel model)
    {
        var response = new ResponseViewModel<Exam>();
        var isValid = await ValidateInstructorCourse(model, response);

        if (!isValid.IsSuccess)
            return response;


        var listOfQuestionIds = model.QuestionPools.Select(x => x.QuestionId);
        var listOfChicesId = model.QuestionPools.Select(x => x.ChoicesViewModel).ToList();

        var questionPools = await _context.Questions
                                              .Select(x => new QuestionViewModel
                                              {
                                                  QuestionId = x.Id,

                                              }).Where(q => listOfQuestionIds
                                              .Any(a => a == q.QuestionId))
                                              .ToListAsync();

        if (questionPools.Any())
            return await ExamCreation(model, response, questionPools);


        response.IsSuccess = false;
        response.Message = "Something went wrong";
        return response;
    }

    private async Task<ResponseViewModel<Exam>> ExamCreation(CreateExamViewModel model, ResponseViewModel<Exam> response, List<QuestionViewModel> questionPools)
    {
        var exam = new Exam
        {
            ExamType = model.ExamType,
            Description = model.Description,
            Title = model.Title,
            CourseId = model.CourseId,
            InstructorId = model.InstructorId,
            CreatedBy = model.InstructorUserName,
            ExamQuestions = questionPools.Select(eq => new ExamQuestion
            {
                QuestionId = eq.QuestionId,
                QuestionOrder = eq.QuestionOrder,
                CreatedBy = model.InstructorUserName,
                CreatedDate = DateTime.UtcNow,
                CourseId = model.CourseId,
            }).ToList(),

        };
        await _context.Exams.AddAsync(exam);
        var result = await _context.SaveChangesAsync() > 0;
        if (result)
        {
            response.IsSuccess = true;
            response.Data = exam;
            return response;
        }
        response.IsSuccess = false;
        response.Message = "Something went wrong";
        return response;
    }

    private async Task<ResponseViewModel<Exam>> ValidateInstructorCourse(CreateExamViewModel model, ResponseViewModel<Exam> response)
    {
        var checkInstructorCourse = await _context.CourseInstructors
                                                                     .AnyAsync(x => x.InstructorId == model.InstructorId
                                                                                   && x.CourseId == model.CourseId);
        if (!checkInstructorCourse)
        {
            response.IsSuccess = false;
            response.Message = "The instructor is not assigned to this course.";
            return response;
        }
        response.IsSuccess = true;
        return response;
    }
}
