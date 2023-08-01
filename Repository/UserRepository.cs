using AniPlannerApi.Data;
using Contracts;
using Entities.Models;

namespace Repository;

public class UserRepository :GenericRepo<User>, IUserRepository
{
    public UserRepository(DataContext context) : base(context) { }
}