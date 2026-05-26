namespace ProiectDRX.Models;

public partial class Department
{
    public int DeptId { get; set; }
    public string Name { get; set; } = null!;
    public int? ResponsibleEmplId { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = [];
    public virtual Employee? ResponsibleEmpl { get; set; }
}
