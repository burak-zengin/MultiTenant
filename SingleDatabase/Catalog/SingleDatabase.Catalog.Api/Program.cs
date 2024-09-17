using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared;
using SingleDatabase.Catalog.Domain.Products;
using SingleDatabase.Catalog.Persistence;
using SingleDatabase.Catalog.Persistence.Products;
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
builder.Services.AddDbContext<CatalogDbContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<SingleDatabase.Catalog.Application.Products.Post.Handler>();
builder.Services.AddScoped<SingleDatabase.Catalog.Application.Products.Get.Handler>();
builder.Services.AddScoped<TenantProvider>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/", (SingleDatabase.Catalog.Application.Products.Post.Handler handler, SingleDatabase.Catalog.Application.Products.Post.Command command) =>
{
    return handler.Handle(command);
}).RequireAuthorization();

app.MapGet("/", (SingleDatabase.Catalog.Application.Products.Get.Handler handler) =>
{
    return handler.Handle();
}).RequireAuthorization();

app.Run();