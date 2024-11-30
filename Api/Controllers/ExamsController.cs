using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Enum;
using Core.Models;
using Core.ViewModels;
using Core.ViewModels.ExamViewModels;
using Core.ViewModels.ExamViewModels.Quiz;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Api.Controllers;
[Route("api/exams")]
//[Authorize(UserRole.Instructor)]
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
    [HttpGet("details")]
    public async Task<ActionResult<Exam>> GetById(int id, int instructorId)
    {
        var exam = await _unitOfWork.Exams.AsQuerable()
                                            .Where(x => x.Id == id && x.InstructorId == instructorId)
                                           .Include(e => e.ExamQuestions)
                                           .ThenInclude(eq => eq.Question)
                                           .ThenInclude(eq => eq.Choices)
                                           .ProjectTo<ExamDetailsViewModel>(_mapper.ConfigurationProvider)
                                           .FirstOrDefaultAsync();

        return Ok(exam);
    }
    [HttpGet("instructor")]
    public async Task<ActionResult<Exam>> GetAllInstuctorExams(int instructorId)
    {
        var exam = await _unitOfWork.Exams.AsQuerable()
                                            .Where(x => x.InstructorId == instructorId)
                                           .Include(e => e.ExamQuestions)
                                           .ThenInclude(eq => eq.Question)
                                           .ThenInclude(eq => eq.Choices)
                                           .ProjectTo<ExamDetailsViewModel>(_mapper.ConfigurationProvider)
                                           .ToListAsync();

        return Ok(exam);
    }
    [HttpGet]
    public async Task<ActionResult<Exam>> GetSingleExamFullDetails(int id)
    {
        var exam = await _unitOfWork.Exams.GetById(id);
        return Ok(exam);
    }
    [HttpPost("create")]
    public async Task<ActionResult<ResponseViewModel<int>>> Create([FromBody] CreateExamViewModel model)
    {
        var ins = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //var insId = _unitOfWork.Instructors.AsQuerable().FirstOrDefault(x => x.IdentityId == ins);
        model.InstructorId = 1;// insId.Id;
        model.InstructorUserName = User.FindFirst(ClaimTypes.GivenName).Value;
        var result = await _unitOfWork.ExamService.CreateQuizOrFinal(model);
        if (result.IsSuccess)
            return Ok(result.Data);

        else
            return BadRequest(result);
    }
    [HttpPost("create-random")]
    public async Task<ActionResult<ResponseViewModel<int>>> CreateRandom([FromBody] CreateRandomExam model)
    {
        var instructorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        model.InstructorUserName = User.FindFirst(ClaimTypes.GivenName).Value;
        model.InstructorId = int.Parse(instructorId);
        var result = await _unitOfWork.ExamService.CreateRandomExam(model);
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
    [HttpPut("take-quiz")]
    public async Task<ActionResult<ResponseViewModel<int>>> TakeQuiz([FromBody] TakeQuizViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _unitOfWork.ExamService.TakeQuiz(model);
        if (result.IsSuccess)
        {
            return Ok(result);

        }
        if (result.ErrorCode != ErrorCode.DatabaseSaveError)
        {
            return StatusCode(((int)result.ErrorCode), result);
        }
        return BadRequest(result);
    }

}
