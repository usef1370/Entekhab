using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Salary
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(150)]
        public string LastName { get; set; }
        [Required]
        public decimal BasicSalary { get; set; }
        [Required]
        public decimal Allowance { get; set;}
        [Required]
        public decimal Transportation { get; set;}
        [Required]
        public decimal OverTimeRate { get;set; }
        [Required]
        public DateTime PayDate { get;set; }
    }
}
