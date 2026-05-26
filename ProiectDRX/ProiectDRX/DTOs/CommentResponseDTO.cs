namespace ProiectDRX.DTOs;

public class CommentResponseDTO
{
    public int CommentId { get; set; }
    public string Message { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public string EmployeeRole { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
}