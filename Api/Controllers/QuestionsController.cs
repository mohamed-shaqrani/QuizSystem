using Core.Enum;
using Core.ViewModels;
using Core.ViewModels.QuestionViewModels;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/questions/")]
[ApiController]
public class QuestionsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public QuestionsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    [HttpGet("{instructorId}/{courseId}")]
    public async Task<ActionResult> GetAllQuestionsWithChoices(int instructorId, int courseId)
    {
        var res = await _unitOfWork.QuestionService.GetAllQuestions(instructorId, courseId);
        return Ok(res);
    }
    [HttpPost("create")]
    public async Task<ActionResult<ResponseViewModel<int>>> CreateNewCourse([FromBody] CreateQuestionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var resutl = await _unitOfWork.QuestionService.CreateNewQuestion(model);
        return Ok(resutl);
    }
    [HttpPut("update")]
    public async Task<ActionResult<ResponseViewModel<int>>> UpdateQuestion([FromBody] UpdateQuestionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var resutl = await _unitOfWork.QuestionService.UpdateQuestion(model);
        return Ok(resutl);
    }
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<ResponseViewModel<int>>> DeleteQuestion(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _unitOfWork.QuestionService.DeleteQuestion(id);
        if (result.IsSuccess)
        {
            return Ok(result);

        }
        if (result.ErrorCode == ErrorCode.NotFound)
        {
            return NotFound(result);
        }
        else
        {
            return BadRequest(result);
        }
    }
}
