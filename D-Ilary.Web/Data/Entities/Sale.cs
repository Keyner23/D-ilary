using System.ComponentModel.DataAnnotations.Schema;

namespace D_Ilary.Web.Data.Entities;

public class Sale
{
    public Guid Id { get; set; }
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; }

    // 🔹 Relación opcional con Product
    public Guid? ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product? Product { get; set; }

    // 🔹 Relación opcional con Equipment
    public Guid? EquipmentId { get; set; }
    [ForeignKey("EquipmentId")]
    public Equipment? Equipment { get; set; }

    public Sale() { }

    public Sale(Guid id, DateTime saleDate, decimal total, Guid? productId, Guid? equipmentId)
    {
        Id = id;
        SaleDate = saleDate;
        Total = total;
        ProductId = productId;
        EquipmentId = equipmentId;
    }
}