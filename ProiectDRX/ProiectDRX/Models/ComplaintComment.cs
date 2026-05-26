namespace ProiectDRX.Models;

public partial class ComplaintComment
{
    public int CommentId { get; set; }
    public int ComplaintId { get; set; }
    public int EmplId { get; set; }
    public string Message { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }

    public virtual Complaint Complaint { get; set; } = null!;
    public virtual Employee Empl { get; set; } = null!;
}
