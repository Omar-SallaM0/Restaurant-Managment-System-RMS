using System.Text.Json.Serialization;

namespace Resturants.Domain.Entities;
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    // Foreign Keys
    public int RestaurantId { get; set; }

    // Navigations
    [JsonIgnore]
    public Restaurant Restaurant { get; private set; } = null!;
    public virtual List<Dish> Dishes { get; set; } = new List<Dish>();
}
