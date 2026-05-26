namespace ProiectDRX.DTOs;

public class EmployeeCreateDTO
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public int DeptId { get; set; }
}
