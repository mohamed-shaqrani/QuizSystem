using Core.Models;
using Core.ViewModels;
using Core.ViewModels.ExamViewModels;
using Core.ViewModels.QuestionViewModels;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;
public class ExamServices
{
    private readonly IUnitOfWork _unitOfWork;

    public ExamServices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseViewModel<Exam>> CreateQuizOrFinal(CreateExamViewModel model, int instructorId)
    {
        var response = new ResponseViewModel<Exam>();

        var checkInstructorCourse = await _unitOfWork.CourseInstructors
                                                      .AnyAsync(x => x.InstructorId == instructorId && x.CourseId == model.CourseId);
        if (!checkInstructorCourse)
        {
            response.IsSuccess = false;
            response.Message = "The instructor is not assigned to this course.";
            return response;
        }
      
        var listOfQuestionIds = model.QuestionPools.Select(x => x.QuestionId).ToList();
        var questionPools = await _unitOfWork.Questions.AsQuerable()
                                              .Select(x => new QuestionViewModel
                                              {
                                                  QuestionId = x.Id,

                                              }).Where(q => listOfQuestionIds
                                              .Any(a => a == q.QuestionId))
                                              .ToListAsync();
        var exam = new Exam
        {
            ExamType = model.ExamType,
            Description = model.Description,
            Title = model.Title,
            CourseId = model.CourseId,
            InstructorId = instructorId,
        };
        if (questionPools.Any())
        {
            foreach (var question in questionPools)
            {

                var examQuestion = new ExamQuestion
                {
                    QuestionId = question.QuestionId,
                    QuestionOrder = question.QuestionOrder,
                    CreatedBy = model.InstructorIdentityId,
                    CreatedDate = DateTime.UtcNow,
                    CourseId = model.CourseId,
                    Exam = exam,
                };
                exam.ExamQuestions.Add(examQuestion);
            }
        }

        await _unitOfWork.Exams.AddAsync(exam);
        await _unitOfWork.Complete();
        response.IsSuccess = true;
        return response;
    }
}
