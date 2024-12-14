using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding DBContext Service (in memory)
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope()) 
{ 
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    SeedData(context); 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void SeedData(ApplicationDbContext context)
{
    context.Database.EnsureCreated();
    if (!context.Employees.Any())
    {
        context.Employees.AddRange(
                new Employee { Id = Guid.NewGuid(), Name = "Joe Doe", Email = "asdas@asdas.com", Phone = "+19282727876", Salary = 4000 },
                new Employee { Id = Guid.NewGuid(), Name = "Jane Doe", Email = "asddeeas@asdas.com", Phone = "+19283327876", Salary = 4000 },
                new Employee { Id = Guid.NewGuid(), Name = "Carlos Doe", Email = "asdasssss@asdas.com", Phone = "+19222727876", Salary = 5000 }
        );
        context.SaveChanges();
    }
}