using System.ComponentModel.DataAnnotations;

namespace Services.Salary.Model
{
    public class AddSalaryRequset
    {
        public SalaryData Data { get; set; }
        [Required(ErrorMessage = "Please enter the OverTimeCalculator")]
        public string OverTimeCalculator { get; set; }
    }

}
