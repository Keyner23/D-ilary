using D_Ilary.Web.Data.Entities;

namespace D_Ilary.Web.Interfaces.IRepositories;

public interface ISaleRepository
{
    Task<Sale?> GetByIdAsync(Guid id);
    Task<List<Sale>> GetAllAsync();
    Task AddAsync(Sale sale);
    Task SaveChangesAsync();
}