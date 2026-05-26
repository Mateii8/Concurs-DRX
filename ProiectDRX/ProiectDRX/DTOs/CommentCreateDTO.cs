namespace ProiectDRX.DTOs;

public class CommentCreateDTO
{
    public int ComplaintId { get; set; }
    public int EmplId { get; set; }
    public string Message { get; set; } = string.Empty;
}