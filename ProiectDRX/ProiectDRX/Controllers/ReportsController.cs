using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectDRX.Data;

namespace ProiectDRX.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReportsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("status")]
    public async Task<IActionResult> GetByStatus()
    {
        var result = await _context.Complaints
            .GroupBy(c => c.Status)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToListAsync();

        return Ok(result);
    }

    [HttpGet("department")]
    public async Task<IActionResult> GetByDepartment()
    {
        var result = await _context.Complaints
            .GroupBy(c => c.Empl.Dept.Name)
            .Select(g => new { Department = g.Key, Count = g.Count() })
            .ToListAsync();

        return Ok(result);
    }
}
