using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SOLID.Controllers;
using SOLID.Data;
using SOLID.Extensions;
using SOLID.Middlewares;
using SOLID.Repositories;
using SOLID.Repositories.Base;
using SOLID.Repositories.Interfaces;
using SOLID.Services;
using SOLID.Services.interfaces;
using SOLID.Transactions;
using SOLID.UseCases;
using SOLID.UseCases.Interfaces;
using SOLID.UseCases.Strategies.Factories;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Text;

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

DependencyInjectionExtension.InitMigration(app);

DependencyInjectionExtension.ApplyMigration(app);
app.Run();

