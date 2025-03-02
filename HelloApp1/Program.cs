using Microsoft.EntityFrameworkCore;
using BusinessLayer.Service;
using RepositoryLayer;
using RepositoryLayer.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register SQL Server DbContext
builder.Services.AddDbContext<UserAuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<RegisterHelloBL>(); // adding dependency injection so objects can be created
// registering this as a service
builder.Services.AddScoped<RegisterHelloRL>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
