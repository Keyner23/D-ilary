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
}