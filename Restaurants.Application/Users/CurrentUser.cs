namespace Restaurants.Application.Users
{
    public record CurrentUser(string Id, string Email, List<string> Roles, string? Nationality,
    DateOnly? DateOfBirth)
    {
        public bool IsinRole(string role) => Roles.Contains(role);
    }
}
