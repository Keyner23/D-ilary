using ClosedXML.Excel;
using D_Ilary.Web.Data.Entities;
using D_Ilary.Web.Interfaces.IRepositories;
using D_Ilary.Web.Interfaces.IServices;

namespace D_Ilary.Web.Service;

public class EquipmentService:IEquipmentService
{
    private readonly IEquipmentRepository _repository;

    public EquipmentService(IEquipmentRepository repository)
    {
        _repository = repository;
    }
    
    
    public List<Equipment> GetEquipment()
    {
        return _repository.GetAll();
    }

    
    public void CreateEquipment(Equipment equipment)
    {
        _repository.Add(equipment);
    }

    public Equipment? GetById(int id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<int> ProcessExcelAsync(IFormFile file)
{
    int registrosImportados = 0;

    using (var stream = new MemoryStream())
    {
        await file.CopyToAsync(stream);
        using (var workbook = new XLWorkbook(stream))
        {
            var worksheet = workbook.Worksheet(1);

            if (worksheet == null)
                throw new Exception("El archivo Excel no contiene ninguna hoja válida.");

            var range = worksheet.RangeUsed();
            if (range == null)
                throw new Exception("El archivo Excel está vacío.");

            var headerRow = range.FirstRowUsed();

            // Validar nombres de columnas
            string col1 = headerRow.Cell(1).GetValue<string>().Trim().ToLower();
            string col2 = headerRow.Cell(2).GetValue<string>().Trim().ToLower();

            if (col1 != "name" || col2 != "pricehour")
            {
                throw new Exception("El archivo Excel debe tener las columnas: 'Name' y 'PriceHour' en la primera fila.");
            }

            // Procesar filas
            foreach (var row in range.RowsUsed().Skip(1))
            {
                try
                {
                    string name = row.Cell(1).GetValue<string>().Trim();
                    decimal priceHour = row.Cell(2).GetValue<decimal>();

                    if (string.IsNullOrEmpty(name))
                        continue;

                    var equipment = new Equipment
                    {
                        Id = Guid.NewGuid(),
                        Name = name,
                        priceHour = priceHour
                    };

                    _repository.Add(equipment);
                    registrosImportados++;
                }
                catch (Exception ex)
                {
                    // Si hay error en una fila, continuar con las demás
                    Console.WriteLine($"Error al procesar fila {row.RowNumber()}: {ex.Message}");
                    continue;
                }
            }
        }
    }
    return registrosImportados;
}
}