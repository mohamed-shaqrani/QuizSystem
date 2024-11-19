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
        if (!await _unitOfWork.Courses.AnyAsync(x => x.Id == courseId))
            return BadRequest("course was not found");


        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var getStudentId = await _unitOfWork.Students.AsQuerable()
                                                     .FirstAsync(x => x.IdentityId == userId);
        var getUserId = await _unitOfWork.Students.AsQuerable()
                                                  .Where(a => a.IdentityId == userId)
                                                  .Select(x => x.Id)
                                                  .FirstOrDefaultAsync();

        var studentCourseExist = await _unitOfWork.CourseStudents.AnyAsync(x => x.CourseId == courseId && x.StudentId == getUserId);

        if (studentCourseExist)
            return BadRequest("Student Enrolled in this course before");

        var studentCoures = new CourseStudent
        {
            CourseId = courseId,
            StudentId = getStudentId.Id,
            CreatedBy = userId!,
        };

        await _unitOfWork.CourseStudents.AddAsync(studentCoures);
        await _unitOfWork.Complete();


        return Ok();
    }

    [HttpPost("create"), Authorize(Policy = UserRole.Instructor)]
    public async Task<ActionResult> Create([FromBody] CreateCourseViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(model);

        var course = new Course
        {
            Description = model.Description,
            Name = model.Name,
            CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
            CreatedDate = DateTime.UtcNow,
        };
        await _unitOfWork.Courses.AddAsync(course);
        var result = await _unitOfWork.Complete();

        return result > 0 ? Ok(result) : BadRequest("Something went wrong");
    }
    [HttpPut("update"), Authorize(Policy = UserRole.Instructor)]
    public async Task<ActionResult> Update([FromBody] UpdateCourseViewModel model)
    {
        if (!ModelState.IsValid)

            return BadRequest(model);

        var course = new Course
        {
            UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
            Description = model.Description,
            Name = model.Name,
            Id = model.Id
        };
        _unitOfWork.Courses.SaveInclude(course, nameof(Course.UpdatedBy), nameof(Course.Name), nameof(Course.Description));
        var result = await _unitOfWork.Complete();

        return result > 0 ? Ok(result) : BadRequest("Something went wrong");
    }
    [HttpDelete("delete/{courseId}"), Authorize(Policy = UserRole.Instructor)]
    public async Task<ActionResult> Delete(int courseId)
    {
        var course = await _unitOfWork.Courses.GetById(courseId);
        _unitOfWork.Courses.SoftDelete(course);
        await _unitOfWork.Complete();

        return Ok();

    }
}
