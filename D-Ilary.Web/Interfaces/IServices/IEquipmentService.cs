using D_Ilary.Web.Data.Entities;

namespace D_Ilary.Web.Interfaces.IServices;

public interface IEquipmentService
{
    List<Equipment> GetEquipment();
    void CreateEquipment(Equipment equipment);
    Equipment? GetById(int id);
    Task<int> ProcessExcelAsync(IFormFile file);
}