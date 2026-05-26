using Microsoft.EntityFrameworkCore;
using ProiectDRX.Data;
using ProiectDRX.Models;
using ProiectDRX.Repositories.Interfaces;

namespace ProiectDRX.Repositories.Implementations;

public class ComplaintWorkFlowRepository : Repository<ComplaintWorkflow>, IComplaintWorkFlowRepository
{
    public ComplaintWorkFlowRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<ComplaintWorkflow>> GetByComplaintAsync(int complaintId) =>
        await _context.ComplaintWorkflows
            .Include(cw => cw.Empl)
            .Where(cw => cw.ComplaintId == complaintId)
            .OrderBy(cw => cw.ChangedAt)
            .ToListAsync();

    public async Task<ComplaintWorkflow?> GetLatestByComplaintAsync(int complaintId) =>
        await _context.ComplaintWorkflows
            .Where(cw => cw.ComplaintId == complaintId)
            .OrderByDescending(cw => cw.ChangedAt)
            .FirstOrDefaultAsync();
}