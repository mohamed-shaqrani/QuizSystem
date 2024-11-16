using Core.Constants;
using Core.Models;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;
[Route("api/students")]
//[Authorize(Policy= UserRole.Student)]   
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public StudentsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task EnrolInCourse(int courseId)
    {
        var user = User.IsInRole(UserRole.Student);
        if (user)
        {
            var getCourse = await _unitOfWork.Courses.GetById(courseId);
        }

    }
    [HttpGet]
    public async Task<ActionResult<Student>> GetStudents()
    {
        var result = await _unitOfWork.Students.GetAllQueryable().ToListAsync();
        return Ok(result);
    }
}
