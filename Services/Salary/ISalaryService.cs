using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Services.Salary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services.Salary
{
    public interface ISalaryService
    {
        Task<bool> AddSalaryAsync(AddSalaryRequset requset);
        Task<bool> UpdateSalary(int id, AddSalaryRequset requset);
        Task<bool> DeleteSalaryAsync(int id);
        Task<Entities.Salary> GetSalaryById(int id);
        Task<List<Entities.Salary>> GetSalariesByDateRange(DateTime startDate, DateTime endDate);
    }
}
