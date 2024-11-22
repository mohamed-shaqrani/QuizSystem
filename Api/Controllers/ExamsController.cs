using Core.Constants;
using Core.Models;
using Core.ViewModels;
using Core.ViewModels.ExamViewModels;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers;
[Route("api/exams")]
[Authorize(UserRole.Instructor)]
[ApiController]
public class ExamsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ExamsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost("create")]
    public async Task<ActionResult<ResponseViewModel<Exam>>> Create([FromBody] CreateExamViewModel model)
    {
        var ins = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var insId = _unitOfWork.Instructors.AsQuerable().FirstOrDefault(x => x.IdentityId == ins);
        model.InstructorId = insId.Id;

        var create = await _unitOfWork.ExamService.CreateQuizOrFinal(model);
        if (create.IsSuccess)
            return Ok(create);

        else
            return BadRequest(create);
    }
}
