using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MultipleDatabase.Ordering.Domain.Orders;
using MultipleDatabase.Ordering.Persistence;
using MultipleDatabase.Ordering.Persistence.Orders;
using Shared;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(co =>
{
    co.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    co.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bAafd@A7d9#@F4*V!LHZs#ebKQrkE6pad2f3kj34c3dXy@")),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<OrderingDbContext>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<MultipleDatabase.Ordering.Application.Orders.Post.Handler>();
builder.Services.AddScoped<MultipleDatabase.Ordering.Application.Orders.Get.Handler>();
builder.Services.AddScoped<TenantProvider>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", (MultipleDatabase.Ordering.Application.Orders.Get.Handler handler) =>
{
    return handler.Handle();
}).RequireAuthorization();

app.MapPost("/", (MultipleDatabase.Ordering.Application.Orders.Post.Handler handler, MultipleDatabase.Ordering.Application.Orders.Post.Command command) =>
{
    return handler.Handle(command);
}).RequireAuthorization();

app.Run();