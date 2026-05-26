using Microsoft.AspNetCore.Mvc;
using ProiectDRX.DTOs;
using ProiectDRX.Models;
using ProiectDRX.Repositories.Interfaces;

namespace ProiectDRX.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUnitOfWork _uow;

    public AuthController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDTO dto)
    {
        var validationError = ValidatePassword(dto.Password);
        if (validationError is not null)
            return BadRequest(validationError);

        var existing = await _uow.Employees.GetByEmailAsync(dto.Email);
        if (existing != null)
            return BadRequest("Email-ul este deja folosit.");

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

        return Ok(new EmployeeResponseDTO
        {
            EmplId = employee.EmplId,
            Name = employee.Name,
            Email = employee.Email,
            Role = employee.Role
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO dto)
    {
        var employee = await _uow.Employees.GetByEmailAsync(dto.Email);

        if (employee == null || dto.Password != employee.PasswordHash)
            return Unauthorized("Email sau parolă greșită.");

        return Ok(new EmployeeResponseDTO
        {
            EmplId = employee.EmplId,
            Name = employee.Name,
            Email = employee.Email,
            Role = employee.Role,
            DepartmentName = employee.Dept?.Name ?? ""
        });
    }

    private static string? ValidatePassword(string password)
    {
        if (password.Length < 8)
            return "Parola trebuie să aibă minim 8 caractere.";
        if (!password.Any(char.IsUpper))
            return "Parola trebuie să conțină o literă mare.";
        if (!password.Any(char.IsLower))
            return "Parola trebuie să conțină o literă mică.";
        if (!password.Any(char.IsDigit))
            return "Parola trebuie să conțină o cifră.";
        if (!password.Any(ch => "!@#$%^&*".Contains(ch)))
            return "Parola trebuie să conțină un caracter special (!@#$%^&*).";
        return null;
    }
}
