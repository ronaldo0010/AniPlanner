using AniPlannerApi.Data;
using AniPlannerApi.Interfaces;
using AniPlannerApi.Models;

namespace AniPlannerApi.Repository;

public class UserRepository : IUserRepository
{
    private DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public ICollection<User> GetUsers()
    {
        return _context.Users.ToList();
    }

    public User? GetUser(Guid userId)
    {
        return _context.Users.FirstOrDefault(x => x.UserId == userId);
    }

    public bool UserExists(Guid userId)
    {
        return _context.Users.FirstOrDefault(x => x.UserId == userId) != null;
    }

    public bool UserExists(string email)
    {
        return _context.Users.FirstOrDefault(x => x.Email == email) != null;
    }
}