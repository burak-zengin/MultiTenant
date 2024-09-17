using Identity.Domain.Users;
using Identity.Infrustructure.Security;

namespace Identity.Application.Users.GetToken;

public class Handler(IUserRepository repository, JwtGenerator jwtGenerator)
{
    public string Handle(Query query)
    {
        var user = repository.Get(query.Username, query.Password);
        if (user is null)
        {
            return string.Empty;
        }

        return jwtGenerator.Generate(user);
    }
}
