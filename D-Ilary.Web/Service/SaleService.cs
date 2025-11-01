using D_Ilary.Web.Data.Entities;
using D_Ilary.Web.Interfaces.IRepositories;
using D_Ilary.Web.Interfaces.IServices;

namespace D_Ilary.Web.Service;

public class SaleService :ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IReceiptService _receiptService;

    public SaleService(ISaleRepository saleRepository, IReceiptService receiptService)
    {
        _saleRepository = saleRepository;
        _receiptService = receiptService;
    }

    public async Task<Sale> CreateSaleAsync(Sale sale)
    {
        await _saleRepository.AddAsync(sale);

        // Generar recibo PDF automáticamente
        await _receiptService.GenerateReceiptAsync(sale);

        return sale;
    }
}