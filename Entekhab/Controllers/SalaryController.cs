using Azure.Core;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.Salary;
using Services.Salary.Model;
using System.Globalization;
using System.Xml;

namespace Entekhab.Controllers
{
    [ApiController]
    [Route("{dataType}/[controller]/[action]")]
    public class SalaryController : ControllerBase
    {
        private readonly ILogger<SalaryController> _logger;
        private readonly ISalaryService _salaryService;

        public SalaryController(ILogger<SalaryController> logger, ISalaryService salaryService)
        {
            _logger = logger;
            _salaryService = salaryService;
        }

        [HttpPost]
        [Consumes("application/json", "application/xml", "text/custom")]
        public async Task<IActionResult> AddSalary(string dataType, [FromBody] AddSalaryRequset requset)
        {
            try
            {
                _logger.LogInformation($"Received request to add salary data of type '{dataType}' with overtime calculator '{requset.OverTimeCalculator}'");

                // Check if the model state is valid
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Invalid model state: {ModelState}");
                    return BadRequest(ModelState);
                }

                // Call the service to add the salary
                var result = await _salaryService.AddSalaryAsync(requset);
                if (!result)
                {
                    return BadRequest();
                }

                _logger.LogInformation($"Salary added successfully");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding salary");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        [Consumes("application/json", "application/xml", "text/custom")]
        public async Task<IActionResult> UpdateSalary(string dataType,int id, [FromBody] AddSalaryRequset requset)
        {
            _logger.LogInformation($"Received request to update salary with Id: '{id}'");

            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Invalid model state: {ModelState}");
                    return BadRequest(ModelState);
                }
                var result = await _salaryService.UpdateSalary(id, requset);

                if (!result)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (ArgumentException ex)
            {
                _logger.LogInformation(ex, $"Salary with id {id} does not exist");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while updating salary with id {id}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteSalary(string dataType, int id)
        {
            _logger.LogInformation($"Received request to Delete salary with Id: '{id}'");
            try
            {
                var result = await _salaryService.DeleteSalaryAsync(id);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, $"Salary with id {id} not found");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting salary");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetSalaryById(string dataType, int id)
        {
            _logger.LogInformation($"Received request to Get salary with Id: '{id}'");
            try
            {

                var result = await _salaryService.GetSalaryById(id);

                return Ok(result);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while Deleteing salary");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Consumes("application/json")]
        public async Task<IActionResult> GetSalariesByDateRange(string dataType, DateTime startDate, DateTime endDate)
        {
            try
            {

                var result = await _salaryService.GetSalariesByDateRange(startDate, endDate);

                return Ok(result);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while Deleteing salary");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
