using Core.Models;
using Core.ViewModels.ExamViewModels;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;
[Route("api/students")]
//[Authorize(Policy = UserRole.Student)]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public StudentsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    [HttpGet]
    public async Task<ActionResult<Student>> GetStudents()
    {
        var result = await _unitOfWork.Students.AsQuerable().ToListAsync();
        return Ok(result);
    }
    [HttpGet("ans")]
    public async Task<ActionResult> GetStudentsss()
    {
        var result = await _unitOfWork.Courses.AsQuerable().Select(x => x.Name).ToListAsync();
        return Ok(result);
    }
    [HttpGet("upcoming-exams/{studentId}")]
    public async Task<ActionResult<ExamViewModel>> GetStudentUpcomingExams(int studentId)
    {
        var result = await _unitOfWork.ExamService.GetStudentUpcomingExams(studentId);

        return Ok(result);
    }
}
