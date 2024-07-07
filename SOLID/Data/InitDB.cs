using Microsoft.EntityFrameworkCore;
using SOLID.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SOLID.Data
{
    public class InitDB
    {
        public static void InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            SeedData(scope.ServiceProvider.GetService<SalaryAppDbContext>());
        }

        private static void SeedData(SalaryAppDbContext context)
        {
            context.Database.Migrate();

            if (context.Employee.Any() || context.Payroll.Any())
            {
                return;
            }

            var employees = new List<Employee>
            {
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = "John Doe",
                    HourlyRate = 100.00,
                    Category = "Hourly"
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = "Jane Smith",
                    HourlyRate = 200.00,
                    Category = "Hourly"
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = "Alice Johnson",
                    HourlyRate = 300.00,
                    Category = "Monthly"
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = "Bob Brown",
                    HourlyRate = 0,
                    Category = "Volunteer"
                }
            };

            context.AddRange(employees);
            context.SaveChanges();

            var payrolls = new List<Payroll>
            {
                new Payroll
                {
                    Id = Guid.NewGuid(),
                    Checkin = DateTime.Now.AddHours(-9),
                    Checkout = DateTime.Now,
                    EmployeeId = employees[0].Id,
                    Employee = employees[0]
                },
                new Payroll
                {
                    Id = Guid.NewGuid(),
                    Checkin = DateTime.Now.AddHours(-8),
                    Checkout = DateTime.Now,
                    EmployeeId = employees[1].Id,
                    Employee = employees[1]
                },
                new Payroll
                {
                    Id = Guid.NewGuid(),
                    Checkin = DateTime.Now.AddHours(-7),
                    Checkout = DateTime.Now,
                    EmployeeId = employees[2].Id,
                    Employee = employees[2]
                },
                new Payroll
                {
                    Id = Guid.NewGuid(),
                    Checkin = DateTime.Now.AddHours(-6),
                    Checkout = DateTime.Now,
                    EmployeeId = employees[3].Id,
                    Employee = employees[3]
                }
            };

            context.AddRange(payrolls);
            context.SaveChanges();
        }
    }
}
