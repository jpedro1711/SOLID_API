using Moq;
using SOLID.Models;
using SOLID.Repositories;
using SOLID.Repositories.Interfaces;
using SOLID.UseCases.Interfaces;
using SOLID.UseCases.Strategies.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SOLID.UseCases.Tests
{
    public class CalculatePayrollTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepository;
        private readonly Mock<IPayrollRepository> _payrollRepository;
        private readonly ICalculatePayroll _calculatePayroll;
        private readonly ICalculateSalaryFactoryMethod calculateSalaryFactoryMethod;

        public CalculatePayrollTests()
        {
            _payrollRepository = new Mock<IPayrollRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();
            calculateSalaryFactoryMethod = new CalculateSalaryFactory();
            _calculatePayroll = new CalculatePayroll(_employeeRepository.Object, _payrollRepository.Object, calculateSalaryFactoryMethod);
        }

        [Fact]
        public void CalculatePayroll_ShouldReturnSalary_WhenEmployeeCategoryIsHourly()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            int year = 2024;
            int month = 7;

            var employee = new Employee
            {
                Id = employeeId,
                Name = "Test",
                HourlyRate = 10,
                Category = "hourly"
            };

            var payrolls = new List<Payroll>
            {
                new Payroll { Checkin = DateTime.Now, Checkout = DateTime.Now.AddHours(8), Employee = employee, Id = Guid.NewGuid() },
                new Payroll { Checkin = DateTime.Now.AddDays(1), Checkout = DateTime.Now.AddDays(1).AddHours(8), Employee = employee, Id = Guid.NewGuid() },
                new Payroll { Checkin = DateTime.Now.AddDays(2), Checkout = DateTime.Now.AddDays(2).AddHours(8), Employee = employee, Id = Guid.NewGuid() }
            };
            // Act

            _employeeRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns(employee);
            _payrollRepository.Setup(x => x.Get(It.IsAny<Func<Payroll, bool>>())).Returns(payrolls);

            var expectedResultSalary = 240;

            var resultSalary = _calculatePayroll.Execute(employeeId, year, month);

            // Assert

            _employeeRepository.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
            _payrollRepository.Verify(x => x.Get(It.IsAny<Func<Payroll, bool>>()), Times.Once);

            Assert.Equal(expectedResultSalary, resultSalary);

            VerifyMocks();
        }

        [Fact]
        public void CalculatePayroll_ShouldReturnSalary_WhenEmployeeCategoryIsMonthly()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            int year = 2024;
            int month = 7;

            var employee = new Employee
            {
                Id = employeeId,
                Name = "Test",
                HourlyRate = 100,
                Category = "monthly"
            };

            var payrolls = new List<Payroll>
            {
                new Payroll { Checkin = DateTime.Now, Checkout = DateTime.Now.AddHours(8), Employee = employee, Id = Guid.NewGuid() },
                new Payroll { Checkin = DateTime.Now.AddDays(1), Checkout = DateTime.Now.AddDays(1).AddHours(8), Employee = employee, Id = Guid.NewGuid() },
                new Payroll { Checkin = DateTime.Now.AddDays(2), Checkout = DateTime.Now.AddDays(2).AddHours(8), Employee = employee, Id = Guid.NewGuid() }
            };
            // Act

            _employeeRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns(employee);
            _payrollRepository.Setup(x => x.Get(It.IsAny<Func<Payroll, bool>>())).Returns(payrolls);

            var expectedResultSalary = 2400;

            var resultSalary = _calculatePayroll.Execute(employeeId, year, month);

            // Assert

            _employeeRepository.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
            _payrollRepository.Verify(x => x.Get(It.IsAny<Func<Payroll, bool>>()), Times.Once);

            Assert.Equal(expectedResultSalary, resultSalary);

            VerifyMocks();
        }

        [Fact]
        public void CalculatePayroll_ShouldReturnError_WhenEmployeeCategoryIsVolunteer()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            int year = 2024;
            int month = 7;

            var employee = new Employee
            {
                Id = employeeId,
                Name = "Test",
                HourlyRate = 10,
                Category = "volunteer"
            };

            var payrolls = new List<Payroll>
            {
                new Payroll { Checkin = DateTime.Now, Checkout = DateTime.Now.AddHours(8), Employee = employee, Id = Guid.NewGuid() },
                new Payroll { Checkin = DateTime.Now.AddDays(1), Checkout = DateTime.Now.AddDays(1).AddHours(8), Employee = employee, Id = Guid.NewGuid() },
                new Payroll { Checkin = DateTime.Now.AddDays(2), Checkout = DateTime.Now.AddDays(2).AddHours(8), Employee = employee, Id = Guid.NewGuid() }
            };
            // Act

            _employeeRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns(employee);
            _payrollRepository.Setup(x => x.Get(It.IsAny<Func<Payroll, bool>>())).Returns(payrolls);

            // Assert

            Assert.Throws<ArgumentException>(() => _calculatePayroll.Execute(employeeId, year, month));

            _employeeRepository.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
            _payrollRepository.Verify(x => x.Get(It.IsAny<Func<Payroll, bool>>()), Times.Once);

            VerifyMocks();
        }

        [Fact]
        public void CalculatePayroll_WhenEmployeeDoesNotExists_ShouldReturnError()
        {
            var employeeId = Guid.NewGuid();
            int year = 2024;
            int month = 7;

            _employeeRepository.Setup(x => x.Get(It.IsAny<Guid>())).Throws(new ArgumentException());

            Assert.Throws<ArgumentException>(() => _calculatePayroll.Execute(employeeId, year, month));
        }

        private void VerifyMocks()
        {
            _employeeRepository.VerifyAll();
            _employeeRepository.VerifyNoOtherCalls();

            _payrollRepository.VerifyNoOtherCalls();
            _payrollRepository.VerifyAll();
        }
    }
}
