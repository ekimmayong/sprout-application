using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Factories;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Business.Repositories;
using Sprout.Exam.Business.Services;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.UnitTest.Services
{
    public class EmployeeServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IBaseRepository<Employee>> _mockRepository;
        private readonly EmployeeService _employeeService;
        private readonly Mock<IEmployeeFactory> _mockEmployeeFactory;
        private readonly Mock<ISalaryCalculator> _mockSalaryCalculator;

        public EmployeeServiceTest()
        {
            _fixture = new Fixture();
            _mockRepository = _fixture.Freeze<Mock<IBaseRepository<Employee>>>();
            _mockEmployeeFactory = _fixture.Freeze<Mock<IEmployeeFactory>>();
            _mockSalaryCalculator = new Mock<ISalaryCalculator>();
            _employeeService = new EmployeeService(_mockRepository.Object, _mockEmployeeFactory.Object);


        }

        [Fact]
        public async void CreateNewEmployee_ShouldBEAbleToCreate_ReturnResultIfSuccessfull()
        {
            //Arrange
            var mockEmployee = _fixture.Create<Employee>();
            _mockRepository.Setup(x => x.AddAsync(mockEmployee)).ReturnsAsync(mockEmployee);

            //Act
            var result = await _employeeService.CreateNewEmployee(mockEmployee);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeSameAs(mockEmployee);
            result.Should().BeAssignableTo<Employee>();
            _mockRepository.Verify(x => x.AddAsync(mockEmployee), Times.Once());
        }

        [Fact]
        public async void CalculateRegularSalary_ShouldReturnCorrectSalary()
        {
            //Arrange
            var id = 1;
            IQueryable<Employee> employeeList = new List<Employee>()
            {
                new ()
                {
                    Id = id,
                    EmployeeTypeId = 1,
                    FullName = "Test",
                    EmployeeType = new EmployeeType() { Id = 1, TypeName = "Regular" }
                }
            }.AsQueryable();

            var data = new WorkAndAbsentDaysDto();

            _mockSalaryCalculator.Setup(x => x.CalculateSalary(data)).ReturnsAsync("16,590.91");

            _mockEmployeeFactory.Setup(x => x.CreateSalaryCalculator("Regular")).Returns(_mockSalaryCalculator.Object);

            _mockRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Employee, bool>>>(), It.IsAny<string>()))
               .Returns(employeeList);

            ////Act
            var response = await _employeeService.CalculateSalary(id, data);

            ////Assert
            response.Should().NotBeNull();
            Assert.Equal("16,590.91", response);
        }

        [Fact]
        public async void CalculateContractualSalary_ShouldReturnCorrectSalary()
        {
            //Arrange
            var id = 1;
            IQueryable<Employee> employeeList = new List<Employee>()
            {
                new ()
                {
                    Id = id,
                    EmployeeTypeId = 1,
                    FullName = "Test",
                    EmployeeType = new EmployeeType() { Id = 2, TypeName = "Contractual" }
                }
            }.AsQueryable();

            var data = new WorkAndAbsentDaysDto();

            _mockSalaryCalculator.Setup(x => x.CalculateSalary(data)).ReturnsAsync("7,750.00");

            _mockEmployeeFactory.Setup(x => x.CreateSalaryCalculator("Contractual")).Returns(_mockSalaryCalculator.Object);

            _mockRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Employee, bool>>>(), It.IsAny<string>()))
               .Returns(employeeList);

            ////Act
            var response = await _employeeService.CalculateSalary(id, data);

            ////Assert
            response.Should().NotBeNull();
            Assert.Equal("7,750.00", response);
        }
    }
}
