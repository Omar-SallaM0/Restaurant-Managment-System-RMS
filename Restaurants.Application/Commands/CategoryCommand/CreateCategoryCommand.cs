using System.Text.Json.Serialization;
using MediatR;

namespace Restaurants.Application.Commands.CategoryCommand;
public class CreateCategoryCommand : IRequest<int>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    [JsonIgnore]
    public int RestaurantId { get; set; }
}
