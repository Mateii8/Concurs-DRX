namespace ProiectDRX.Models;

public partial class Asset
{
    public int AssetId { get; set; }
    public string Name { get; set; } = null!;
    public string SerialNumber { get; set; } = null!;
    public int EmplId { get; set; }

    public virtual ICollection<Complaint> Complaints { get; set; } = [];
    public virtual Employee Empl { get; set; } = null!;
}
