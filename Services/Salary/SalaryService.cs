using Common;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OvertimePolicies;
using Persistence;
using Services.Salary.Model;

namespace Services.Salary
{
    public class SalaryService : ISalaryService
    {  
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SalaryService> _logger;
        private readonly IConfiguration _configuration;
        public SalaryService(ApplicationDbContext context, ILogger<SalaryService> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<bool> AddSalaryAsync(AddSalaryRequset requset)
        {
            _logger.LogInformation($"Adding salary with data: {requset.Data}, overtime calculator: {requset.OverTimeCalculator}");

            var salary = new Entities.Salary
            {
                FirstName = requset.Data.FirstName,
                LastName = requset.Data.LastName,
                BasicSalary = requset.Data.BasicSalary,
                Allowance = requset.Data.Allowance,
                Transportation = requset.Data.Transportation,
                OverTimeRate = OverTimeCalculator.CallCalculator(requset.OverTimeCalculator, requset.Data.BasicSalary + requset.Data.Allowance),
                PayDate = requset.Data.PayDate,
            };
            await _context.Salaries.AddAsync(salary);
            try
            {
                var res = await _context.SaveChangesAsync();
                if (res > 0)
                {
                    _logger.LogInformation($"Salary added successfully");
                    return true;
                }
                _logger.LogError("Error while adding salary");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding salary");
                return false;
            }
        }

        public async Task<bool> DeleteSalaryAsync(int id)
        {
            var salary = await _context.Salaries.FindAsync(id).ConfigureAwait(false);
            if (salary == null)
            {
                _logger.LogInformation($"Salary with id {id} does not exist");
                throw new ArgumentException($"Salary with id {id} does not exist", nameof(id));
            }

            _context.Salaries.Remove(salary);
            var res = await _context.SaveChangesAsync().ConfigureAwait(false);
            if (res > 0)
            {
                _logger.LogInformation($"Salary with id {id} removed successfully");
                return true;
            }

            _logger.LogError($"Error while removing salary with id {id}");
            return false;
        }

        public async Task<bool> UpdateSalary(int id, AddSalaryRequset requset)
        {
            var salary = await _context.Salaries.FindAsync(id).ConfigureAwait(false);
            if (salary == null)
            {
                _logger.LogInformation($"Salary with id {id} does not exist");
                throw new ArgumentException($"Salary with id {id} does not exist", nameof(id));
            }

            _logger.LogInformation($"Updating salary with id {id}");

            salary.FirstName = requset.Data.FirstName;
            salary.LastName = requset.Data.LastName;
            salary.BasicSalary = requset.Data.BasicSalary;
            salary.Allowance = requset.Data.Allowance;
            salary.Transportation = requset.Data.Transportation;
            salary.OverTimeRate = OverTimeCalculator.CallCalculator(requset.OverTimeCalculator, requset.Data.BasicSalary + requset.Data.Allowance);
            salary.PayDate = requset.Data.PayDate;

            _context.Salaries.Update(salary);
            try
            {
                var res = await _context.SaveChangesAsync().ConfigureAwait(false);
                if (res > 0)
                {
                    _logger.LogInformation($"Salary with id {id} updated successfully");
                    return true;
                }

                _logger.LogWarning($"No changes were made to salary with id {id}");
                return false;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Error while updating salary with id {id}");
                throw;
            }
        }

        public async Task<Entities.Salary> GetSalaryById(int id)
        {
            using (var connection = CustomSqlConnection.Create(_configuration))
            {
                connection.Open();
                var salary = await connection.QueryFirstOrDefaultAsync<Entities.Salary>("SELECT * FROM Salaries WHERE Id = @Id", new { Id = id });
                if (salary == null)
                {
                    _logger.LogInformation("salary with given ID doesn't exist");
                    return null;
                }
                else
                {
                    return salary;
                }
            }
            
        }

        public async Task<List<Entities.Salary>> GetSalariesByDateRange(DateTime startDate, DateTime endDate)
        {
            using (var connection = CustomSqlConnection.Create(_configuration))
            {
                connection.Open();
                var salaries = await connection.QueryAsync<Entities.Salary>("SELECT * FROM Salaries WHERE PayDate BETWEEN @StartDate AND @EndDate", new { StartDate = startDate, EndDate = endDate });
                return salaries.AsList();
            }
        }
    }
}
