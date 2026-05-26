using ProiectDRX.Models;

namespace ProiectDRX.Repositories.Interfaces;

public interface IComplaintWorkFlowRepository : IRepository<ComplaintWorkflow>
{
    Task<IEnumerable<ComplaintWorkflow>> GetByComplaintAsync(int complaintId);
    Task<ComplaintWorkflow?> GetLatestByComplaintAsync(int complaintId);
}
