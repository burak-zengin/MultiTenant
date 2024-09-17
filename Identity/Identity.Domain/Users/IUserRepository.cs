namespace Identity.Domain.Users;

public interface IUserRepository
{
    User Get(string username, string password);
}
