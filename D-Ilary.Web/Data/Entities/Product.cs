namespace D_Ilary.Web.Data.Entities;

public class Product
{
    public Guid id { get; set; }
    public string name { get; set; }
    public decimal? priceKg { get; set; }
    public decimal? priceUnit { get; set; }

    public Product(Guid id, string name, decimal? priceKg, decimal? priceUnit)
    {
        this.id = id;
        this.name = name;
        this.priceKg = priceKg;
        this.priceUnit = priceUnit;
    }
}