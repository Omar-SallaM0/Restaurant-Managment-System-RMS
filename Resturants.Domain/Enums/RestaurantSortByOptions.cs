using Resturants.Domain.Entities;

namespace Resturants.Domain.Enums;

public static class RestaurantSortByOptions
{
    public const string Name = nameof(Restaurant.Name);
    public const string CreatedBy = nameof(Restaurant.CreatedAt);
}