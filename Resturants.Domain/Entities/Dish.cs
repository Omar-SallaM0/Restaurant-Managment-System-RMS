namespace Resturants.Domain.Entities;
public class Dish
{
    public int id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }

    public int RestaurantId { get; set; }
}
