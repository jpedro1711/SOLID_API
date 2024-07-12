using AutoFixture;
using Moq;
using SOLID.Repositories.Interfaces;
using SOLID.Repositories;
using SOLID.UseCases.Interfaces;
using SOLID.Models;
using SOLID.Controllers.Requests;

namespace SOLID.UseCases.Tests
{
    public class RegisterCheckinOrCheckoutTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepository;
        private readonly Mock<IPayrollRepository> _payrollRepository;
        private readonly Fixture _fixture;
        private readonly IRegisterCheckinOrCheckout _registerCheckinOrCheckout;

        public RegisterCheckinOrCheckoutTests()
        {
            _payrollRepository = new Mock<IPayrollRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();
            _registerCheckinOrCheckout = new RegisterCheckinOrCheckout(_payrollRepository.Object, _employeeRepository.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public void Register_Checkin_Or_Checkout_Should_Register_Checkin_When_There_Is_No_Pending_Checkout()
        {
            var employee = _fixture.Create<Employee>();

            var payrolls = new List<Payroll>
            {
                new Payroll { Checkin = DateTime.Now, Checkout = DateTime.Now.AddHours(8), Employee = employee, Id = Guid.NewGuid() },
                new Payroll { Checkin = DateTime.Now.AddDays(1), Checkout = DateTime.Now.AddDays(1).AddHours(8), Employee = employee, Id = Guid.NewGuid() },
                new Payroll { Checkin = DateTime.Now.AddDays(2), Checkout = DateTime.Now.AddDays(2).AddHours(8), Employee = employee, Id = Guid.NewGuid() }
            };

            _employeeRepository.Setup(x => x.Get(It.IsAny<Func<Employee, bool>>())).Returns(new List<Employee> { employee });
            _payrollRepository.Setup(x => x.Get(It.IsAny<Func<Payroll, bool>>())).Returns(new List<Payroll>());

            var req = new RegisterCheckinOrCheckoutRequest { EmployeeUniqueName = employee.Name.ToLower() };

            _registerCheckinOrCheckout.Execute(req);

            _payrollRepository.Verify(x => x.Save(It.IsAny<Payroll>()), Times.Once);
            _payrollRepository.Verify(x => x.Get(It.IsAny<Func<Payroll, bool>>()), Times.Once);

            VerifyMocks();
        }

        [Fact]
        public void Register_Checkin_Or_Checkout_Should_Register_Checkout_When_There_Is_Pending_Checkout()
        {
            var employee = _fixture.Create<Employee>();

            var payrolls = new List<Payroll>
            {
                new Payroll { Checkin = DateTime.Now, Checkout = DateTime.Now.AddHours(8), Employee = employee, Id = Guid.NewGuid() },
                new Payroll { Checkin = DateTime.Now.AddDays(1), Checkout = DateTime.Now.AddDays(1).AddHours(8), Employee = employee, Id = Guid.NewGuid() },
                new Payroll { Checkin = DateTime.Now.AddDays(2), Checkout = null, Employee = employee, Id = Guid.NewGuid() }
            };

            _employeeRepository.Setup(x => x.Get(It.IsAny<Func<Employee, bool>>())).Returns(new List<Employee> { employee });
            _payrollRepository.Setup(x => x.Get(It.IsAny<Func<Payroll, bool>>())).Returns(payrolls);

            var req = new RegisterCheckinOrCheckoutRequest { EmployeeUniqueName = employee.Name.ToLower() };

            _registerCheckinOrCheckout.Execute(req);

            _payrollRepository.Verify(x => x.Update(It.IsAny<Payroll>()), Times.Once);
            _payrollRepository.Verify(x => x.Get(It.IsAny<Func<Payroll, bool>>()), Times.Once);

            VerifyMocks();
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
