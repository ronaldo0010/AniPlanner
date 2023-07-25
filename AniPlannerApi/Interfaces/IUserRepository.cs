using AniPlannerApi.Models;

namespace AniPlannerApi.Interfaces;

public interface IUserRepository
{
    public ICollection<User> GetUsers();
    public User? GetUser(Guid userId);
    bool UserExists(Guid userId);
    bool UserExists(string email);
}