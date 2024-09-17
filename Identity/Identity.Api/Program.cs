using Identity.Application.Users.GetToken;
using Identity.Domain.Users;
using Identity.Infrustructure.Security;
using Identity.Persistence;
using Identity.Persistence.Users;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<IdentityDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<Handler>();
builder.Services.AddScoped<JwtGenerator>();

var app = builder.Build();
app.UseHttpsRedirection();

app.MapPost("/Token", (Handler handler, Query query) =>
{
    return handler.Handle(query);
});

app.Run();