using Microsoft.EntityFrameworkCore;
using SOLID.Controllers;
using SOLID.Data;
using SOLID.Middlewares;
using SOLID.Repositories;
using SOLID.Repositories.Base;
using SOLID.Repositories.Interfaces;
using SOLID.UseCases;
using SOLID.UseCases.Interfaces;
using SOLID.UseCases.Strategies.Factories;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<SalaryAppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAppDbContext, SalaryAppDbContext>();
builder.Services.AddScoped<IPayrollRepository, PayrollRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICalculatePayroll, CalculatePayroll>();
builder.Services.AddScoped<ICalculateSalaryFactoryMethod, CalculateSalaryFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ErrorMiddleware>();
app.UseAuthorization();

app.MapControllers();

try
{
    InitDB.InitDb(app);
}
catch (Exception e)
{
    Console.WriteLine("Error seeding DB");
}

ApplyMigration();
app.Run();

void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<SalaryAppDbContext>();

        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}