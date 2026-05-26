namespace ProiectDRX.DTOs;

public class EmployeeResponseDTO
{
    public int EmplId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
}