namespace ProiectDRX.Models;

public partial class Complaint
{
    public int ComplaintId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Status { get; set; } = null!;
    public int AssetId { get; set; }
    public int EmplId { get; set; }

    public virtual Asset Asset { get; set; } = null!;
    public virtual Employee Empl { get; set; } = null!;
    public virtual ICollection<ComplaintComment> ComplaintComments { get; set; } = [];
    public virtual ICollection<ComplaintWorkflow> ComplaintWorkflows { get; set; } = [];
}
