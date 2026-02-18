namespace Restaurants.Application.DTO.Category;
public class GetCategoryByIdDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<int> DishIds { get; set; }
}
