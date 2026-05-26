using Microsoft.AspNetCore.Mvc;
using ProiectDRX.DTOs;
using ProiectDRX.Models;
using ProiectDRX.Repositories.Interfaces;

namespace ProiectDRX.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IUnitOfWork _uow;

    public DepartmentsController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var departments = await _uow.Departments.GetAllAsync();
        var result = departments.Select(d => new
        {
            d.DeptId,
            d.Name,
            d.ResponsibleEmplId
        });
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var department = await _uow.Departments.GetWithEmployeesAsync(id);
        if (department == null) return NotFound();
        return Ok(new
        {
            department.DeptId,
            department.Name,
            department.ResponsibleEmplId,
            Employees = department.Employees.Select(e => new
            {
                e.EmplId,
                e.Name,
                e.Email,
                e.Role
            })
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(DepartmentCreateDTO dto)
    {
        var department = new Department
        {
            Name = dto.Name,
            ResponsibleEmplId = dto.ResponsibleEmplId
        };
        await _uow.Departments.AddAsync(department);
        await _uow.SaveAsync();
        return CreatedAtAction(nameof(GetById), new { id = department.DeptId }, new
        {
            department.DeptId,
            department.Name,
            department.ResponsibleEmplId
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, DepartmentCreateDTO dto)
    {
        var department = await _uow.Departments.GetByIdAsync(id);
        if (department == null) return NotFound();

        department.Name = dto.Name;
        department.ResponsibleEmplId = dto.ResponsibleEmplId;

        _uow.Departments.Update(department);
        await _uow.SaveAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var department = await _uow.Departments.GetByIdAsync(id);
        if (department == null) return NotFound();
        _uow.Departments.Delete(department);
        await _uow.SaveAsync();
        return NoContent();
    }
}
