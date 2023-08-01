using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class UserRepository :GenericRepo<User>, IUserRepository
{
    public UserRepository(DbContext context) : base(context) { }
}