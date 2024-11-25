using Core.Constants;
using Core.Models;
using Core.ViewModels.CourseViewModel;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult> EnrollInCourse(int courseId, int studentId)
    {
        var userName = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var getResponse = await _unitOfWork.CourseService.EnrollStudentInCourse(courseId, studentId, userName);

        return Ok(getResponse);
    }

    [HttpPost("create"), Authorize(Policy = UserRole.Instructor)]
    public async Task<ActionResult> Create([FromBody] CreateCourseViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(model);

        var response = await _unitOfWork.CourseService.CreateNewCourse(model);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    [HttpPut("update"), Authorize(Policy = UserRole.Instructor)]
    public async Task<ActionResult> Update([FromBody] UpdateCourseViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(model);
        var response = await _unitOfWork.CourseService.UpdateCourse(model);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);

    }
    [HttpDelete("delete/{courseId}"), Authorize(Policy = UserRole.Instructor)]
    public async Task<ActionResult> Delete(int courseId)
    {
        var response = await _unitOfWork.CourseService.DeleteCourse(courseId);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);

    }
}
