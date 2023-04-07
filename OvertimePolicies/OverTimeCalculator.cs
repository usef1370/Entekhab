using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OvertimePolicies
{
    public static class OverTimeCalculator
    {
        /// <summary>
        /// Calls the specified calculator method with the given monthly salary.
        /// </summary>
        /// <param name="calculatorName">Name of the calculator method to call</param>
        /// <param name="monthlySalary">Monthly salary of the employee</param>
        /// <returns>The calculated overtime pay</returns>
        /// <exception cref="ArgumentException">Thrown when the specified calculator method is not found.</exception>
        public static decimal CallCalculator(string calculatorName, decimal monthlySalary)
        {
            Type type = typeof(OverTimeCalculator);
            MethodInfo method = type.GetMethod(calculatorName);
            if (method != null)
            {
                return (decimal)method.Invoke(null, new object[] { monthlySalary });
            }
            else
            {
                throw new ArgumentException("Calculator method not found.", "calculatorName");
            }
        }

        public static decimal CalculatorC(decimal monthlySalary)
        {
            double overtimeHours = 50.0; // The number of overtime hours at speed C.
            decimal overtimeRate = 1.5m * HourlyRate(monthlySalary); // The overtime rate at speed C.
            return (decimal)(overtimeHours * (double)overtimeRate);
        }

        public static decimal CalculatorB(decimal monthlySalary)
        {
            double overtimeHours = 40.0; // The number of overtime hours at speed B.
            decimal overtimeRate = 1.25m * HourlyRate(monthlySalary); // The overtime rate at speed B.
            return (decimal)(overtimeHours * (double)overtimeRate);
        }

        public static decimal CalculatorA(decimal monthlySalary)
        {
            double overtimeHours = 30.0; // The number of overtime hours at speed A.
            decimal overtimeRate = HourlyRate(monthlySalary); // The overtime rate at speed A.
            return (decimal)(overtimeHours * (double)overtimeRate);
        }

        private static decimal HourlyRate(decimal monthlySalary)
        {
            double workingHours = 240.0; // The number of working hours in a month.
            return Math.Round(monthlySalary / (decimal)workingHours, 2); // Dividing monthly salary by working hours in a month.
        }
    }

}
