using Identity.Domain.Users;

namespace Identity.Persistence.Users;

public class UserRepository(IdentityDbContext context) : IUserRepository
{
    public User Get(string username, string password)
    {
        return context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
    }
}
