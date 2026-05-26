namespace ProiectDRX.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IEmployeeRepository Employees { get; }
    IDepartmentRepository Departments { get; }
    IAssetRepository Assets { get; }
    IComplaintRepository Complaints { get; }
    IComplaintCommentRepository Comments { get; }
    IComplaintWorkFlowRepository Workflows { get; }
    Task<int> SaveAsync();
}
