using ProiectDRX.Models;

namespace ProiectDRX.Repositories.Interfaces;

public interface IAssetRepository : IRepository<Asset>
{
    Task<IEnumerable<Asset>> GetByEmployeeAsync(int emplId);
}
