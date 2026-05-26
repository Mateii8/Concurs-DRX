using ProiectDRX.Models;

namespace ProiectDRX.Repositories.Interfaces;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<Department?> GetWithEmployeesAsync(int deptId);
}
