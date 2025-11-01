using D_Ilary.Web.Data.Entities;

namespace D_Ilary.Web.Interfaces.IServices;

public interface ISaleService
{
    Task<Sale> CreateSaleAsync(Sale sale);
}