using D_Ilary.Web.Data;
using D_Ilary.Web.Data.Entities;
using D_Ilary.Web.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace D_Ilary.Web.Repository;

public class SaleRepository :ISaleRepository
{
    private readonly ApplicationDbContext _context;

    public SaleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Sale?> GetByIdAsync(Guid id)
    {
        return await _context.Sales
            .Include(s => s.Product)
            .Include(s => s.Equipment)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Sale>> GetAllAsync()
    {
        return await _context.Sales
            .Include(s => s.Product)
            .Include(s => s.Equipment)
            .ToListAsync();
    }

    public async Task AddAsync(Sale sale)
    {
        await _context.Sales.AddAsync(sale);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
