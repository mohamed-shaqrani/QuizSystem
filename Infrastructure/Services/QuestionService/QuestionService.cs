using Core.Constants;
using Core.Enum;
using Core.Models;
using Core.ViewModels;
using Core.ViewModels.QuestionViewModels;
using Core.ViewModels.QuestionViewModels.ChoiceViewMode;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.QuestionService;
public class QuestionService<Entity> : IQuestionService<Entity> where Entity : class
{

    private readonly IUnitOfWork _unitOfWork;

    public QuestionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseViewModel<List<QuestioInfoViewModel>>> GetAllQuestions(int instructorId, int courseId)
    {
        var response = new ResponseViewModel<List<QuestioInfoViewModel>>();
        var list = await _unitOfWork.Questions.AsQuerable().
                                            Where(x => x.InstructorId == instructorId && x.CourseId == courseId).
                                            Include(x => x.Choices)
                                            .Select(x => new QuestioInfoViewModel
                                            {
                                                Id = x.Id,
                                                Level = x.Level,
                                                Text = x.Text,
                                                Choices = x.Choices.Select(x => new ChoicesInfoViewModel
                                                {
                                                    IsCorrect = x.IsCorrect,
                                                    Text = x.Text,
                                                }).ToList()
                                            }).ToListAsync();
        response.IsSuccess = true;
        response.Data = list;
        return response;
    }
    public async Task<ResponseViewModel<int>> CreateNewQuestion(CreateQuestionViewModel model)
    {
        var response = new ResponseViewModel<int>();
        if (await DoesQuestionCourseInstructorExist(model, response))
        {
            return response;
        }

        if (!ValidateQuestionChoice(model.ToBaseModel(), response))
        {
            return response;
        }
        var question = new Question
        {
            Text = model.Text,
            Grade = model.Grade,
            InstructorId = model.InstructorId,
            CourseId = model.CourseId,
            CreatedBy = model.UserName,
            CreatedDate = DateTime.UtcNow,

            Choices = model.createChoicesViewModels.Select(choice => new Choice
            {
                IsCorrect = choice.IsCorrect,
                Text = choice.Text,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = model.UserName,

            }).ToList()
        };
        await _unitOfWork.Questions.AddAsync(question);
        var result = await _unitOfWork.Complete() > 0;
        if (result)
        {
            response.IsSuccess = true;
            response.ErrorCode = 0;
            return response;
        }
        return DataBaseError.DataBaseErrorResponse(response);

    }
    private async Task<bool> DoesQuestionCourseInstructorExist(CreateQuestionViewModel model, ResponseViewModel<int> response)
    {
        var checkQuesstionExist = await _unitOfWork.Questions.
                                            AnyAsync(x => x.InstructorId == model.InstructorId
                                                   && x.CourseId == model.CourseId
                                                   && x.Text == model.Text);
        if (checkQuesstionExist)
        {
            response.ErrorCode = ErrorCode.QuestionCourseInstructorAlreadyExist;
            response.IsSuccess = false;
            response.Message = "Question Already Exist in this  course";
            return true;
        }
        else
        {
            response.IsSuccess = true;
            return false;
        }

    }
    private bool ValidateQuestionChoice(BaseUpdateCreateQuestionViewModel model, ResponseViewModel<int> response)
    {
        var totalChoices = model.Choices.Count();
        var correctChoicesCount = model.Choices.Count(x => x.IsCorrect);

        if (totalChoices < 2)
        {
            response.ErrorCode = ErrorCode.InsufficientChoices;
            response.IsSuccess = false;
            response.Message = "The question must have at least two answer choices.";
        }

        if (correctChoicesCount != 1)
        {
            response.ErrorCode = ErrorCode.InvalidCorrectAnswerCount;
            response.IsSuccess = false;
            response.Message = "The question must have exactly one correct answer.";
        }
        response.IsSuccess = true;
        return true;

    }
    public async Task<ResponseViewModel<int>> UpdateQuestion(UpdateQuestionViewModel model)
    {
        var response = new ResponseViewModel<int>();

        if (!ValidateQuestionChoice(model.ToUpdateViewModel(), response))
        {
            return response;
        }
        var qustion = new Question
        {
            Text = model.Text,
            Level = model.Level,
            Id = model.Id,

        };

        _unitOfWork.Questions.SaveInclude(qustion, nameof(Question.Text), nameof(Question.Level));
        var result = await _unitOfWork.Complete() > 0;
        if (result)
        {
            response.IsSuccess = true;
            response.ErrorCode = 0;
            return response;
        }
        return DataBaseError.DataBaseErrorResponse(response);

    }
    public async Task<ResponseViewModel<int>> DeleteQuestion(int id)
    {
        var response = new ResponseViewModel<int>();

        var getQuestion = await _unitOfWork.Questions.GetById(id);
        if (getQuestion is not null && !getQuestion.isDeleted)
        {
            _unitOfWork.Questions.SoftDelete(getQuestion);
            var result = await _unitOfWork.Complete() > 0;
            if (result)
            {
                response.IsSuccess = true;
                response.ErrorCode = 0;
                return response;
            }

            else
            {
                return DataBaseError.DataBaseErrorResponse(response);
            }

        }
        if (getQuestion.isDeleted)
        {
            response.IsSuccess = false;
            response.ErrorCode = ErrorCode.ItemAlreadyDeltedBefore;
            response.Message = "Item Already Delted Before";
            return response;
        }
        response.IsSuccess = false;
        response.ErrorCode = ErrorCode.NotFound;
        return response;
    }

}
