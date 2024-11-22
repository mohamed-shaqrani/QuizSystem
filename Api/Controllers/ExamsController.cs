using AutoMapper;
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
    private readonly IMapper _mapper;


    public ExamsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    [HttpGet]
    public async Task<ActionResult<Exam>> GetById(int id)
    {
        var exam = await _unitOfWork.Exams.GetById(id);
        return Ok(exam);
    }
    [HttpPost("create")]
    public async Task<ActionResult<ResponseViewModel<int>>> Create([FromBody] CreateExamViewModel model)
    {
        var ins = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var insId = _unitOfWork.Instructors.AsQuerable().FirstOrDefault(x => x.IdentityId == ins);
        model.InstructorId = insId.Id;
        model.InstructorUserName = User.FindFirst(ClaimTypes.GivenName).Value;
        var result = await _unitOfWork.ExamService.CreateQuizOrFinal(model);
        if (result.IsSuccess)
            return Ok(result.Data);

        else
            return BadRequest(result);
    }
    [HttpPost("assgin-students")]

    public async Task<ActionResult<ResponseViewModel<int>>> AssignStudents([FromBody] AssignExamToStudentsViewModel model)
    {
        model.InstructorUserName = User.FindFirst(ClaimTypes.GivenName).Value;

        var result = await _unitOfWork.ExamService.AssignStudents(model);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }


}
