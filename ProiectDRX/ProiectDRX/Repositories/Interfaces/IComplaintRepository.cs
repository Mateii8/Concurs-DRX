using ProiectDRX.Models;

namespace ProiectDRX.Repositories.Interfaces;

public interface IComplaintRepository : IRepository<Complaint>
{
    Task<IEnumerable<Complaint>> GetByEmployeeAsync(int emplId);
    Task<IEnumerable<Complaint>> GetByStatusAsync(string status);
    Task<IEnumerable<Complaint>> GetByDepartmentAsync(int deptId);
    Task<Complaint?> GetWithDetailsAsync(int id);
}
