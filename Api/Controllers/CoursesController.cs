using Core.Constants;
using Core.Models;
using Core.ViewModels.CourseViewModel;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Api.Controllers;
[Route("api/course")]
[ApiController]
public class CoursesController : ControllerBase
{
    readonly IUnitOfWork _unitOfWork;

    public CoursesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var list = await _unitOfWork.Courses.GetAllAsync();
        return Ok(list);
    }

    [HttpGet("{courserId}")]
    public async Task<ActionResult> GetById(int courserId)
    {
        var result = await _unitOfWork.Courses.GetById(courserId);
        if (result is null)
            return NotFound();
        return Ok(result);
    }
    [HttpGet("enroll-course/{courseId}"), Authorize(Policy = UserRole.Student)]
    public async Task<ActionResult> EnrolInCourse(int courseId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var getStudentId = await _unitOfWork.Students.AsQuerable()
                                                     .FirstAsync(x => x.IdentityId == userId);
        var getUserId = await _unitOfWork.Students.AsQuerable()
                                                  .Where(a => a.IdentityId == userId)
                                                  .Select(x => x.Id)
                                                  .FirstOrDefaultAsync();

        var studentCourse = await _unitOfWork.CourseStudents.AnyAsync(x => x.CourseId == courseId && x.StudentId == getUserId);

        if (studentCourse)
            return BadRequest("Student Enrolled in this course before");

        var studentCoures = new CourseStudent
        {
            CourseId = courseId,
            StudentId = getStudentId.Id,
            CreatedBy = userId,
        };

        await _unitOfWork.CourseStudents.AddAsync(studentCoures);
        await _unitOfWork.Complete();


        return Ok();
    }
    [HttpPost]
    public async Task<ActionResult> Add(CreateCourseViewModel course)
    {

        await _unitOfWork.Courses.AddAsync(course);
        var result = await _unitOfWork.Complete();

        return result > 0 ? Ok(result) : BadRequest("Something went wrong");
    }
}
