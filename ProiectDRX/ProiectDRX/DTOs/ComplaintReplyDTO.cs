namespace ProiectDRX.DTOs;

public class ComplaintReplyDTO
{
    public int ComplaintId { get; set; }
    public int EmplId { get; set; }
    public string Message { get; set; } = string.Empty;
}