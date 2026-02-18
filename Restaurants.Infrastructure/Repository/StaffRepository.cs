using Microsoft.EntityFrameworkCore;
using Restaurants.Infrastructure.persistence;
using Resturants.Domain.Entities;
using Resturants.Domainntities;

namespace Restaurants.Infrastructure.Repository;

public class StaffRepository(RestaurantDBContext dBContext) : IStaffRepository
{
    public async Task<List<Staff>> GetStaffByRestaurantIdAsync(int restaurantId)
    {
        return await dBContext.Staffs.Include(x => x.User).Where(x => x.RestaurantId == restaurantId).ToListAsync();
    }
    public async Task<string> Create(Staff staff)
    {
        dBContext.Staffs.Add(staff);
        await dBContext.SaveChangesAsync();
        return staff.UserId;
    }
    public async Task Delete(Staff staff)
    {
        dBContext.Staffs.Remove(staff);
        await dBContext.SaveChangesAsync();
    }
    public async Task<Staff?> GetById(int id)
    {
        return await dBContext.Staffs
            .FirstOrDefaultAsync(s => Convert.ToInt16(s.UserId) == id);
    }
    public async Task<IEnumerable<Staff>> GetAllStaffsAsync()
    {
        var Staffs = await dBContext.Staffs.ToListAsync();
        return Staffs;
    }
    public Task SaveChanges()
     => dBContext.SaveChangesAsync();
}

