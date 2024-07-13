using Microsoft.EntityFrameworkCore;
using SOLID.Data;
using SOLID.Middlewares;
using SOLID.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

DependencyInjectionExtension.ConfigureAuth(builder);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwagger();
builder.Services.AddApplicationServices();

builder.Services.AddDbContext<SalaryAppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ErrorMiddleware>();
app.UseCors("AllowLocalhost5173");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

DependencyInjectionExtension.ApplyMigration(app);
app.Run();

