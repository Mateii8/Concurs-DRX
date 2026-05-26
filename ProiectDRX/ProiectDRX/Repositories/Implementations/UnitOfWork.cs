using ProiectDRX.Data;
using ProiectDRX.Repositories.Interfaces;

namespace ProiectDRX.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IEmployeeRepository Employees { get; }
    public IDepartmentRepository Departments { get; }
    public IAssetRepository Assets { get; }
    public IComplaintRepository Complaints { get; }
    public IComplaintCommentRepository Comments { get; }
    public IComplaintWorkFlowRepository Workflows { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Employees = new EmployeeRepository(context);
        Departments = new DepartmentRepository(context);
        Assets = new AssetRepository(context);
        Complaints = new ComplaintRepository(context);
        Comments = new ComplaintCommentRepository(context);
        Workflows = new ComplaintWorkFlowRepository(context);
    }

    public async Task<int> SaveAsync() =>
        await _context.SaveChangesAsync();

    public void Dispose() =>
        _context.Dispose();
}