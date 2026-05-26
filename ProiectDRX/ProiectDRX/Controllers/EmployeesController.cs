using Microsoft.AspNetCore.Mvc;
using ProiectDRX.DTOs;
using ProiectDRX.Models;
using ProiectDRX.Repositories.Interfaces;

namespace ProiectDRX.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IUnitOfWork _uow;

    public EmployeesController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _uow.Employees.GetAllAsync();
        var result = employees.Select(e => new EmployeeResponseDTO
        {
            EmplId = e.EmplId,
            Name = e.Name,
            Email = e.Email,
            Role = e.Role,
            DepartmentName = e.Dept?.Name ?? ""
        });
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var employee = await _uow.Employees.GetByIdAsync(id);
        if (employee == null) return NotFound();

        var result = new EmployeeResponseDTO
        {
            EmplId = employee.EmplId,
            Name = employee.Name,
            Email = employee.Email,
            Role = employee.Role,
            DepartmentName = employee.Dept?.Name ?? ""
        };
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeCreateDTO dto)
    {
        var employee = new Employee
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = dto.Password,
            Role = dto.Role,
            DeptId = dto.DeptId,
            IsActive = true,
            CreatedAt = DateTime.Now
        };
        await _uow.Employees.AddAsync(employee);
        await _uow.SaveAsync();
        return CreatedAtAction(nameof(GetById), new { id = employee.EmplId }, new EmployeeResponseDTO
        {
            EmplId = employee.EmplId,
            Name = employee.Name,
            Email = employee.Email,
            Role = employee.Role
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, EmployeeCreateDTO dto)
    {
        var employee = await _uow.Employees.GetByIdAsync(id);
        if (employee == null) return NotFound();

        employee.Name = dto.Name;
        employee.Email = dto.Email;
        employee.PasswordHash = dto.Password;
        employee.Role = dto.Role;
        employee.DeptId = dto.DeptId;

        _uow.Employees.Update(employee);
        await _uow.SaveAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _uow.Employees.GetByIdAsync(id);
        if (employee == null) return NotFound();
        _uow.Employees.Delete(employee);
        await _uow.SaveAsync();
        return NoContent();
    }
}