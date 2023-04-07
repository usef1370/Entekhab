using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Persistence;
using Services.Salary.Model;
using Services.Salary;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace Entekhab.Test
{
    public class SalaryServiceTests
    {
        private readonly SalaryService _service;

        public SalaryServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SalaryDatabase")
                .Options;
            var context = new ApplicationDbContext(options);
            var logger = new Mock<ILogger<SalaryService>>();
            var configuration = new Mock<IConfiguration>();
            configuration.Object["ConnectionStrings:DefaultConnection"] = "Data Source=server;Initial Catalog=db;Integrated Security=True;";
            _service = new SalaryService(context, logger.Object, configuration.Object);
        }

        [Fact]
        public async Task AddSalaryAsync_ShouldReturnTrue_WhenSalaryIsAdded()
        {
            // Arrange
            var request = new AddSalaryRequset
            {
                Data = new SalaryData
                {
                    FirstName = "John",
                    LastName = "Doe",
                    BasicSalary = 1000,
                    Allowance = 200,
                    Transportation = 50,
                    PayDate = DateTime.Today
                },
                OverTimeCalculator = "CalculatorA"
            };

            // Act
            var result = await _service.AddSalaryAsync(request);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteSalaryAsync_ShouldReturnTrue_WhenSalaryIsDeleted()
        {
            // Arrange
            var salary = new Salary
            {
                Id= 1,
                FirstName = "John",
                LastName = "Doe",
                BasicSalary = 1000,
                Allowance = 200,
                Transportation = 50,
                OverTimeRate = 0,
                PayDate = DateTime.Today
            };
            await _service.AddSalaryAsync(new AddSalaryRequset
            {
                Data = new SalaryData
                {
                    FirstName = salary.FirstName,
                    LastName = salary.LastName,
                    BasicSalary = salary.BasicSalary,
                    Allowance = salary.Allowance,
                    Transportation = salary.Transportation,
                    PayDate = salary.PayDate
                },
                OverTimeCalculator = "CalculatorB"
            });

            // Act
            var result = await _service.DeleteSalaryAsync(salary.Id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateSalary_ShouldReturnTrue_WhenSalaryIsUpdated()
        {
            // Arrange
            var salary = new Salary
            {
                Id= 2,
                FirstName = "John",
                LastName = "Doe",
                BasicSalary = 1000,
                Allowance = 200,
                Transportation = 50,
                OverTimeRate = 0,
                PayDate = DateTime.Today
            };
            await _service.AddSalaryAsync(new AddSalaryRequset
            {
                Data = new SalaryData
                {
                    FirstName = salary.FirstName,
                    LastName = salary.LastName,
                    BasicSalary = salary.BasicSalary,
                    Allowance = salary.Allowance,
                    Transportation = salary.Transportation,
                    PayDate = salary.PayDate
                },
                OverTimeCalculator = "CalculatorA"
            });
            var request = new AddSalaryRequset
            {
                Data = new SalaryData
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    BasicSalary = 1500,
                    Allowance = 300,
                    Transportation = 100,
                    PayDate = DateTime.Today
                },
                OverTimeCalculator = "CalculatorB"

            };

            // Act
            var result = await _service.UpdateSalary(salary.Id, request);

            // Assert
            Assert.True(result);
        }
    }
}