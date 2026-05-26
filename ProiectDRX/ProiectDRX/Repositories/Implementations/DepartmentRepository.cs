using Microsoft.EntityFrameworkCore;
using ProiectDRX.Data;
using ProiectDRX.Models;
using ProiectDRX.Repositories.Interfaces;

namespace ProiectDRX.Repositories.Implementations;

public class DepartmentRepository : Repository<Department>, IDepartmentRepository
{
    public DepartmentRepository(AppDbContext context) : base(context) { }

    public async Task<Department?> GetWithEmployeesAsync(int deptId) =>
        await _context.Departments
            .Include(d => d.Employees)
            .FirstOrDefaultAsync(d => d.DeptId == deptId);
}