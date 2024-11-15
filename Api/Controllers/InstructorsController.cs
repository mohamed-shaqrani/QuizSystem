using Core.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class InstructorsController : ControllerBase
{
    private readonly AppDbContext _context;
    public InstructorsController(AppDbContext dbContext)
    {
        _context = dbContext;
    }
    [HttpGet]
    public List<Instructor> GetAll()
    {
        return _context.Instructors.ToList();
    }
}
