using Microsoft.EntityFrameworkCore;
using ProiectDRX.Data;
using ProiectDRX.Models;
using ProiectDRX.Repositories.Interfaces;

namespace ProiectDRX.Repositories.Implementations;

public class AssetRepository : Repository<Asset>, IAssetRepository
{
    public AssetRepository(AppDbContext context) : base(context) { }

    private IQueryable<Asset> BaseQuery() =>
        _context.Assets
            .Include(a => a.Empl);

    public override async Task<IEnumerable<Asset>> GetAllAsync() =>
        await BaseQuery().ToListAsync();

    public async Task<IEnumerable<Asset>> GetByEmployeeAsync(int emplId) =>
        await BaseQuery()
            .Where(a => a.EmplId == emplId)
            .ToListAsync();
}