using Resturants.Domainntities;

namespace Resturants.Domain.Entities;
public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool HasDelivery { get; set; }

    public string? ConatactPhone { get; set; }
    public string? ConatactEmail { get; set; }
    public DateTime CreatedAt { get; set; }
    public Address? address { get; set; } = new();
    public List<Dish> Dishes { get; set; } = new();
    public User Owner { get; set; } =default!;
    public string OwnerId { get; set; } = default!;
    public virtual List<Category> Categories { get; set; } = new List<Category>();
    public List<Staff> StaffMembers { get; private set; } = new();

}

