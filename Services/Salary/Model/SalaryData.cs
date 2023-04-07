using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Salary.Model
{

    public class SalaryData
    {
        [Required(ErrorMessage = "Please enter the FirstName")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter the LastName")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter the BasicSalary")]
        public decimal BasicSalary { get; set; }
        [Required(ErrorMessage = "Please enter the Allowance")]
        public decimal Allowance { get; set; }
        [Required(ErrorMessage = "Please enter the Transportation")]
        public decimal Transportation { get; set; }
        [Required(ErrorMessage = "Please enter the Date")]
        public DateTime PayDate { get; set; }
    }
}
