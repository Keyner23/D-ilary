using D_Ilary.Web.Data.Entities;

namespace D_Ilary.Web.Interfaces.IRepositories;

public interface IEquipmentRepository
{
    List<Equipment> GetAll();
    void Add(Equipment equipment);
    Equipment? GetById(int id);
    void Delete(Guid id);
}