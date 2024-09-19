using Identity.Application.Users.GetToken;
using Identity.Domain.Users;
using Identity.Infrustructure.Security;
using Identity.Persistence;
using Identity.Persistence.Users;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<IdentityDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<Handler>();
builder.Services.AddScoped<JwtGenerator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.CustomSchemaIds(ss => ss.FullName);
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/Token", Results<Ok<string>, BadRequest> (
    Handler handler,
    Command command) =>
{
    var token = handler.Handle(command);

    if (string.IsNullOrEmpty(token))
    {
        return TypedResults.BadRequest();
    }

    return TypedResults.Ok(token);
});
app.Run();