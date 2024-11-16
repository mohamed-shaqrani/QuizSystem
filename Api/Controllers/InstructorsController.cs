using Core.Models;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;
[Route("api/instructors")]
[ApiController]
[AllowAnonymous]
public class InstructorsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public InstructorsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    [HttpGet]
    public async Task<List<Instructor>> GetAll()
    {
        return await _unitOfWork.Instructors.GetAllQueryable().ToListAsync();
    }
}
