namespace ProiectDRX.Models;

public partial class ComplaintWorkflow
{
    public int WorkflowId { get; set; }
    public int ComplaintId { get; set; }
    public int EmplId { get; set; }
    public string? OldStatus { get; set; }
    public string CurrentStatus { get; set; } = null!;
    public DateTime? ChangedAt { get; set; }

    public virtual Complaint Complaint { get; set; } = null!;
    public virtual Employee Empl { get; set; } = null!;
}
