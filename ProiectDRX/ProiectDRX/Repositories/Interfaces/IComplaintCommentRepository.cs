using ProiectDRX.Models;

namespace ProiectDRX.Repositories.Interfaces;

public interface IComplaintCommentRepository : IRepository<ComplaintComment>
{
    Task<IEnumerable<ComplaintComment>> GetByComplaintAsync(int complaintId);
}
