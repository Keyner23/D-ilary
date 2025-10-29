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

namespace D_Ilary.Web.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly IEquipmentService _service;

        public EquipmentController(IEquipmentService service)
        {
            _service = service;
        }

        // GET: Equipment/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Index()
        {
            var equipments = _service.GetEquipment(); // Método que retorne una lista
            return View(equipments);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEquipment(Equipment equipment)
        {
            _service.CreateEquipment(equipment);
            return View("Create");
        }

        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Message = "Por favor selecciona un archivo Excel válido.";
                ViewBag.AlertType = "danger";
                return View("Upload");
            }

            try
            {
                var total = await _service.ProcessExcelAsync(file);
                ViewBag.Message = $"✅ Se importaron {total} equipos correctamente.";
                ViewBag.AlertType = "success";
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"❌ Error: {ex.Message}";
                ViewBag.AlertType = "danger";
            }

            return View("Upload");
        }

    }
}
