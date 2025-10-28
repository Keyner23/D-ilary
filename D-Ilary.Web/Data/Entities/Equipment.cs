namespace D_Ilary.Web.Data.Entities;

public class Equipment
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal priceHour { get; set; }

    public Equipment() {}
    public Equipment(string name, decimal priceHour)
    {
        Id=Guid.NewGuid();
        Name = name;
        this.priceHour = priceHour;
    }
}