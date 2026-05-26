using Microsoft.EntityFrameworkCore;
using ProiectDRX.Data;
using ProiectDRX.Models;
using ProiectDRX.Repositories.Interfaces;

namespace ProiectDRX.Repositories.Implementations;

public class ComplaintRepository : Repository<Complaint>, IComplaintRepository
{
    public ComplaintRepository(AppDbContext context) : base(context) { }

    private IQueryable<Complaint> BaseQuery() =>
        _context.Complaints
            .Include(c => c.Asset)
            .Include(c => c.Empl)
                .ThenInclude(e => e.Dept);

    public override async Task<IEnumerable<Complaint>> GetAllAsync() =>
        await BaseQuery().ToListAsync();

    public async Task<IEnumerable<Complaint>> GetByEmployeeAsync(int emplId) =>
        await BaseQuery()
            .Where(c => c.EmplId == emplId)
            .ToListAsync();

    public async Task<IEnumerable<Complaint>> GetByStatusAsync(string status) =>
        await BaseQuery()
            .Where(c => c.Status == status)
            .ToListAsync();

    public async Task<IEnumerable<Complaint>> GetByDepartmentAsync(int deptId) =>
        await BaseQuery()
            .Where(c => c.Empl.DeptId == deptId)
            .ToListAsync();

    public async Task<Complaint?> GetWithDetailsAsync(int id) =>
        await BaseQuery()
            .Include(c => c.ComplaintComments)
                .ThenInclude(cc => cc.Empl)
            .Include(c => c.ComplaintWorkflows)
            .FirstOrDefaultAsync(c => c.ComplaintId == id);
}
