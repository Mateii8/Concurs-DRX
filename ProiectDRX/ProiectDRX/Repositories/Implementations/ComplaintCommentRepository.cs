using Microsoft.EntityFrameworkCore;
using ProiectDRX.Data;
using ProiectDRX.Models;
using ProiectDRX.Repositories.Interfaces;

namespace ProiectDRX.Repositories.Implementations;

public class ComplaintCommentRepository : Repository<ComplaintComment>, IComplaintCommentRepository
{
    public ComplaintCommentRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<ComplaintComment>> GetByComplaintAsync(int complaintId) =>
        await _context.ComplaintComments
            .Include(cc => cc.Empl)
            .Where(cc => cc.ComplaintId == complaintId)
            .OrderBy(cc => cc.CreatedAt)
            .ToListAsync();
}