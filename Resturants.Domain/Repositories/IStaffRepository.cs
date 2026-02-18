using Resturants.Domainntities;

namespace Restaurants.Infrastructure.Repository
{
    public interface IStaffRepository
    {
        Task<List<Staff>> GetStaffByRestaurantIdAsync(int restaurantId);
        Task<Staff?> GetById(int Staffid);
        Task<string> Create(Staff staff);
        Task Delete(Staff staff);
        Task<IEnumerable<Staff>> GetAllStaffsAsync();
        Task SaveChanges();
    }
}