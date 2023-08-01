using AniPlannerApi.Data;
using Contracts;

namespace Repository;

public class UserRepository : IUserRepository
{
    private DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

}