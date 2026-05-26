using Microsoft.EntityFrameworkCore;
using ProiectDRX.Data;
using ProiectDRX.Models;
using ProiectDRX.Repositories.Interfaces;

namespace ProiectDRX.Repositories.Implementations;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext context) : base(context) { }

    public override async Task<IEnumerable<Employee>> GetAllAsync() =>
        await _context.Employees
            .Include(e => e.Dept)
            .ToListAsync();

    public async Task<Employee?> GetByEmailAsync(string email) =>
        await _context.Employees
            .FirstOrDefaultAsync(e => e.Email == email);

    public async Task<IEnumerable<Employee>> GetByDepartmentAsync(int deptId) =>
        await _context.Employees
            .Include(e => e.Dept)
            .Where(e => e.DeptId == deptId)
            .ToListAsync();
}
