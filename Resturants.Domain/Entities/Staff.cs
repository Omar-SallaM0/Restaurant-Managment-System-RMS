using System.Text.Json.Serialization;
using Resturants.Domain.Entities;
using Resturants.Domain.Enums;

namespace Resturants.Domainntities;
public class Staff
{
    public string UserId { get; private set; }
    public int RestaurantId { get; private set; }
    public StaffRole Role { get; private set; }
    public User User { get; private set; } = null!;
    [JsonIgnore]
    public Restaurant Restaurant { get; private set; } = null!;



    private Staff() { }

    public Staff(string userId, int restaurantId, StaffRole role)
    {
        UserId = userId;
        RestaurantId = restaurantId;
        Role = role;
    }

    public void UpdateRole(StaffRole role)
    {
        Role = role;
    }
}
