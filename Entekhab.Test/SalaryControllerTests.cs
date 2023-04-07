using Entekhab.Controllers;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Services.Salary;
using Services.Salary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Entekhab.Test
{
    public class SalaryControllerTests
    {
        private readonly Mock<ILogger<SalaryController>> _loggerMock;
        private readonly Mock<ISalaryService> _salaryServiceMock;
        private readonly SalaryController _salaryController;

        public SalaryControllerTests()
        {
            _loggerMock = new Mock<ILogger<SalaryController>>();
            _salaryServiceMock = new Mock<ISalaryService>();
            _salaryController = new SalaryController(_loggerMock.Object, _salaryServiceMock.Object);
        }

        [Fact]
        public async Task AddSalary_ValidRequest_ReturnsOkResult()
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
                    Transportation = 100,
                    PayDate = DateTime.Now
                },
                OverTimeCalculator = "CalculatorA"
            };
            _salaryServiceMock.Setup(x => x.AddSalaryAsync(request)).ReturnsAsync(true);

            // Act
            var result = await _salaryController.AddSalary("json", request);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task AddSalary_InvalidRequest_ReturnsBadRequestResult()
        {
            // Arrange
            var request = new AddSalaryRequset();
            _salaryController.ModelState.AddModelError("Data.FirstName", "The FirstName field is required.");

            // Act
            var result = await _salaryController.AddSalary("json", request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsAssignableFrom<SerializableError>(badRequestResult.Value);
            var errors = (SerializableError)badRequestResult.Value;
            Assert.True(errors.ContainsKey("Data.FirstName"));
        }

        [Fact]
        public async Task AddSalary_ServiceReturnsFalse_ReturnsBadRequestResult()
        {
            // Arrange
            var request = new AddSalaryRequset();
            _salaryServiceMock.Setup(x => x.AddSalaryAsync(request)).ReturnsAsync(false);

            // Act
            var result = await _salaryController.AddSalary("json", request);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task AddSalary_ServiceThrowsException_ReturnsInternalServerErrorResult()
        {
            // Arrange
            var request = new AddSalaryRequset();
            _salaryServiceMock.Setup(x => x.AddSalaryAsync(request)).ThrowsAsync(new Exception());

            // Act
            var result = await _salaryController.AddSalary("json", request);

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            var statusCodeResult = (StatusCodeResult)result;
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetSalaryById_ValidRequest_ReturnsSalary()
        {
            // Arrange
            var salary = new Salary
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                BasicSalary = 1000,
                Allowance = 200,
                Transportation = 100,
                PayDate = DateTime.Now
            };
            _salaryServiceMock.Setup(x => x.GetSalaryById(salary.Id)).ReturnsAsync(salary);

            // Act
            var result = await _salaryController.GetSalaryById("Json",salary.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.Equal(salary, okObjectResult.Value);
        }

        [Fact]
        public async Task UpdateSalary_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var salary = new Salary
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                BasicSalary = 1000,
                Allowance = 200,
                Transportation = 100,
                PayDate = DateTime.Now
            };
            var request = new AddSalaryRequset
            {
                Data = new SalaryData
                {
                    FirstName = "Johnny",
                    LastName = "Doe",
                    BasicSalary = 1100,
                    Allowance = 250,
                    Transportation = 125,
                    PayDate = salary.PayDate
                }
            };
            _salaryServiceMock.Setup(x => x.UpdateSalary(salary.Id, request)).ReturnsAsync(true);

            // Act
            var result = await _salaryController.UpdateSalary("json",salary.Id,request);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateSalary_InvalidRequest_ReturnsBadRequestResult()
        {
            // Arrange
            var request = new AddSalaryRequset();
            _salaryController.ModelState.AddModelError("Data.FirstName", "The FirstName field is required.");

            // Act
            var result = await _salaryController.UpdateSalary("json", 1, request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteSalary_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var id = 1;
            _salaryServiceMock.Setup(x => x.DeleteSalaryAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _salaryController.DeleteSalary("json",id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteSalary_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var id = -1;
            _salaryServiceMock.Setup(x => x.DeleteSalaryAsync(id)).ReturnsAsync(false);

            // Act
            var result = await _salaryController.DeleteSalary("json", id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
