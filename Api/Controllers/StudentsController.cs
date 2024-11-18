using Core.Constants;
using Core.Models;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Api.Controllers;
[Route("api/students")]
[Authorize( Policy = UserRole.Student)]
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
}
