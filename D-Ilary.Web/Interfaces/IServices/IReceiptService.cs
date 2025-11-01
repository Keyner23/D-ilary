using D_Ilary.Web.Data.Entities;

namespace D_Ilary.Web.Interfaces.IServices;

public interface IReceiptService
{
    Task<string> GenerateReceiptAsync(Sale sale);
}