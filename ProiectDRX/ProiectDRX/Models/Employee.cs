namespace ProiectDRX.Models;

public partial class Employee
{
    public int EmplId { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Role { get; set; } = null!;
    public int DeptId { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? LastLogin { get; set; }
    public DateTime? CreatedAt { get; set; }

    public virtual Department Dept { get; set; } = null!;
    public virtual ICollection<Asset> Assets { get; set; } = [];
    public virtual ICollection<Complaint> Complaints { get; set; } = [];
    public virtual ICollection<ComplaintComment> ComplaintComments { get; set; } = [];
    public virtual ICollection<ComplaintWorkflow> ComplaintWorkflows { get; set; } = [];
    public virtual ICollection<Department> Departments { get; set; } = [];
}
