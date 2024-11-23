using Core.Enum;
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

    public async Task<ResponseViewModel<int>> AssignStudents(AssignExamToStudentsViewModel model)
    {
        var response = new ResponseViewModel<int>();
        var getCouseIdOfExam = await _context.Exams.Where(x => x.Id == model.ExamId)
                                                   .Select(x => x.CourseId)
                                                   .FirstOrDefaultAsync();

        var studentIds = await _context.CourseStudents.Where(x => x.CourseId == getCouseIdOfExam
                                                         && model.StudentIds.All(a => a == x.StudentId))
                                                        .Select(x => x.StudentId)
                                                      .ToListAsync();
        if (studentIds.Any())
        {
            return await AssignStudentsToExamAsync(model, response, studentIds);
        }

        else
        {
            response.IsSuccess = false;
            response.ErrorCode = ErrorCode.StudentsNotEnrolledInCourse;

            response.Message = "No valid students found for the specified course and exam.";
            return response;
        }
    }

    private async Task<ResponseViewModel<int>> AssignStudentsToExamAsync(AssignExamToStudentsViewModel model, ResponseViewModel<int> response, List<int> studentIds)
    {
        var ExamStduentList = new List<ExamStudent>();

        var studentsToAssign = studentIds.Select(studentId => new ExamStudent
        {
            ExamId = model.ExamId,
            StudentId = studentId,
            CreatedBy = model.InstructorUserName,
        }).ToList();

        await _context.AddRangeAsync(ExamStduentList);
        var result = await _context.SaveChangesAsync() > 0;
        if (result)
        {
            response.IsSuccess = true;
            response.Data = 1;
            return response;

        }
        response.IsSuccess = false;
        response.ErrorCode = ErrorCode.DatabaseSaveError;
        response.Message = "Failed to save exam-student assignments.";
        return response;
    }
    public async Task<ResponseViewModel<int>> CreateRandomExam(CreateRandomExam model)
    {
        var result = await _context.Questions.Where(x => x.InstructorId == model.InstructorId
                                                         && x.CourseId == model.CourseId)
                                            .Include(x => x.Choices)
                                            .Select(x => new RandomQuestionViewModel
                                            {

                                                QuestionId = x.Id,
                                                DifficultyLevel = x.Level,
                                                ChoicesViewModel = x.Choices.Select(choice => new ChoicesViewModel
                                                {
                                                    Id = choice.Id,

                                                }).ToList(),
                                            })
                                            .ToListAsync();
        var response = await ValidateRandomExam(result, model);
        if (!response.IsSuccess)
            return response;


        var quetionIds = GetRandomQuestionIds(result);
        var questions = _context.Questions.Where(x => quetionIds.Contains(x.Id))
                                          .Include(x => x.Choices).ToList();

        var createExamViewModel = new CreateExamViewModel
        {
            CourseId = model.CourseId,
            InstructorId = (int)model.InstructorId,
            Description = "deeeee",
            ExamType = ExamType.Final,
            IsRandom = true,
            Title = "",
            InstructorUserName = model.InstructorUserName,
            QuestionPools = questions.Select(x => new QuestionViewModel
            {
                Marks = x.Grade,
                QuestionId = x.Id,


            }).ToList(),
        };

        var created = await ExamCreation(createExamViewModel, response, createExamViewModel.QuestionPools);
        return created;
    }

    private static List<int> GetRandomQuestionIds(List<RandomQuestionViewModel> result)
    {
        var groupQuestionsByDifficultyLevel = result.GroupBy(x => x.DifficultyLevel)
                                                    .ToDictionary(g => g.Key, g =>
                                                    g.Select(x => x.QuestionId)
                                                    .OrderBy(x => Guid.NewGuid())
                                                    .ToList());
        var newBalancedRandomQuestionIds = new List<int>();
        var questionExamCount = result.Count();
        var maxDiffcultyLevel = (int)DifficultyLevel.Hard;
        while (questionExamCount != 0)
        {
            for (var i = 1; i <= maxDiffcultyLevel; i++)
            {
                var availableQuestions = groupQuestionsByDifficultyLevel[(DifficultyLevel)i]
                                                       .Where(q => !newBalancedRandomQuestionIds.Contains(q))
                                                       .ToList();

                if (availableQuestions.Any())
                {
                    var randomQuestionId = availableQuestions.FirstOrDefault();
                    newBalancedRandomQuestionIds.Add(randomQuestionId);
                    questionExamCount--;
                }
            }
        }
        return newBalancedRandomQuestionIds;
    }

    public async Task<ResponseViewModel<int>> ValidateRandomExam(List<RandomQuestionViewModel> result, CreateRandomExam createRandom)
    {
        var response = new ResponseViewModel<int>();
        if (!result.Any() || result.Count < 3)
        {
            response.IsSuccess = false;
            response.ErrorCode = ErrorCode.InsufficientQuestion;
            response.Message = "The question pool does not have the minimum required number of questions (3)";
            return response;
        }
        var sum = result.Sum(x => (int)x.DifficultyLevel);
        var averageDifficulty = (double)sum / result.Count;

        if (averageDifficulty < 1 || averageDifficulty > 3)
        {
            response.IsSuccess = false;
            response.ErrorCode = ErrorCode.UnbalancedDifficulty;
            response.Message = "The selected questions have an imbalanced overall difficulty level. Consider adding questions with varying difficulty to achieve a more balanced exam.";
            return response;
        }

        response.IsSuccess = true;
        return response;
    }
    public async Task<ResponseViewModel<int>> CreateQuizOrFinal(CreateExamViewModel model)
    {
        var response = new ResponseViewModel<int>();
        var isValid = await ValidateInstructorCourse(model, response);

        if (!isValid.IsSuccess)
        {
            response.Message = "Instructor is not associated with the course or course does not exist.";
            response.ErrorCode = ErrorCode.InstructorNotAssignedToCourse;
            return response;
        }


        var listOfQuestionIds = model.QuestionPools.Select(x => x.QuestionId);
        var listOfChicesId = model.QuestionPools.Select(x => x.ChoicesViewModel).ToList();

        var questionPools = await _context.Questions
                                              .Select(x => new QuestionViewModel
                                              {
                                                  QuestionId = x.Id,

                                              }).Where(q => listOfQuestionIds
                                              .Any(a => a == q.QuestionId))
                                              .ToListAsync();

        if (!questionPools.Any())
        {
            response.IsSuccess = false;
            response.Message = "None of the provided question IDs match existing questions.";
            response.ErrorCode = ErrorCode.InvalidQuestionIds;
            return response;
        }

        return await ExamCreation(model, response, questionPools);
    }

    private async Task<ResponseViewModel<int>> ExamCreation(CreateExamViewModel model, ResponseViewModel<int> response, List<QuestionViewModel> questionPools)
    {

        var exam = new Exam
        {
            ExamType = model.ExamType,
            Description = model.Description,
            Title = model.Title,
            CourseId = model.CourseId,
            InstructorId = model.InstructorId,
            CreatedBy = model.InstructorUserName,
            MaxScore = model.QuestionPools.Sum(s => s.Marks),
            IsRandom = model.IsRandom,
            ExamQuestions = questionPools.Select(eq => new ExamQuestion
            {
                QuestionId = eq.QuestionId,
                QuestionOrder = eq.QuestionOrder,
                CreatedBy = model.InstructorUserName,
                CreatedDate = DateTime.UtcNow,
                CourseId = model.CourseId,
                Marks = eq.Marks,
            }).ToList(),

        };
        await _context.Exams.AddAsync(exam);
        var result = await _context.SaveChangesAsync() > 0;
        if (result)
        {

            response.IsSuccess = true;
            response.Data = exam.Id;
            return response;
        }
        response.IsSuccess = false;
        response.Message = "Something went wrong";
        return response;
    }

    private async Task<ResponseViewModel<int>> ValidateInstructorCourse(CreateExamViewModel model, ResponseViewModel<int> response)
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
