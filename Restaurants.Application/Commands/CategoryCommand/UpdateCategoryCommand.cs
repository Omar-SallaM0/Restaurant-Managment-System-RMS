using System.Text.Json.Serialization;
using MediatR;

namespace Restaurants.Application.Commands.CategoryCommand;
public class UpdateCategoryCommand :IRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    [JsonIgnore]
    public int RestaurantId { get; set; }
}
