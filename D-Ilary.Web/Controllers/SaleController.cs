using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using D_Ilary.Web.Data;
using D_Ilary.Web.Data.Entities;
using D_Ilary.Web.Interfaces.IServices;

namespace D_Ilary.Web.Controllers;

    public class SaleController : Controller
    {
        private readonly ISaleService _saleService;
        private readonly IReceiptService _receiptService;

        public SaleController(ISaleService saleService, IReceiptService receiptService)
        {
            _saleService = saleService;
            _receiptService = receiptService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Sale());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sale sale)
        {
            if (!ModelState.IsValid)
                return View(sale);

            sale.Id = Guid.NewGuid();
            sale.SaleDate = DateTime.UtcNow;

            // 1️⃣ Guardar la venta
            await _saleService.CreateSaleAsync(sale);

            // 2️⃣ Generar el recibo PDF
            var filePath = await _receiptService.GenerateReceiptAsync(sale);

            // 3️⃣ Devolver el archivo como descarga
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var fileName = Path.GetFileName(filePath);

            return File(fileBytes, "application/pdf", fileName);
        }
    }