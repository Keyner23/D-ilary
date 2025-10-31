using D_Ilary.Web.Data;
using D_Ilary.Web.Data.Entities;
using D_Ilary.Web.Interfaces.IRepositories;

namespace D_Ilary.Web.Repository;

public class EquipmentRepository: IEquipmentRepository
{
    
    private readonly ApplicationDbContext _context;
//carga de constructores con la bd
    public EquipmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public void Delete(Guid id)
    {
        var equipment = _context.Equipments.Find(id);
        if (equipment != null)
        {
            _context.Equipments.Remove(equipment);
            _context.SaveChanges();
        }
    }
    
    public List<Equipment> GetAll()
    {
        return _context.Equipments.ToList();
    }

    
    public void Add(Equipment equipment)
    {
        _context.Equipments.Add(equipment);
        _context.SaveChanges();
    }

    public Equipment? GetById(int id)
    {
        throw new NotImplementedException();
    }
}