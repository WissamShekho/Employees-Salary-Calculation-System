using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesSalaryCalculationSystem
{
    internal class Handyman : Employee
    {
        private const decimal Hardship = 75.0m;
        public Handyman(int id, string firstName, string lastName, int loggedHours):
            base(id, 5, loggedHours, firstName, lastName) { }
        
        private static decimal CalculateGrossPay(decimal basicSalary, decimal overtimeSalary, decimal hardship)
        {
            return basicSalary + overtimeSalary + hardship;
        }
        public override string SalaryReport()
        {
            decimal BasicSalary = CalculateBasicSalary(BasicLoggedHours, HourlyRate);
            decimal OvertimeSalary = CalculateOvertimeSalary(OvertimeHours, HourlyRate, PayrollConstants.OvertimeRateMultiplier);
            decimal GrossPay = CalculateGrossPay(BasicSalary, OvertimeSalary, Hardship);
            decimal TaxAmount = CalculateTaxAmount(GrossPay, PayrollConstants.TaxPercentage);
            decimal NetSalary = CalculateNetSalary(GrossPay, TaxAmount);
            string Report = $@"
                
                ID  : {ID}
                Name: {FullName}
            ___________________________________________
                Expected Hours: {PayrollConstants.ExpectedHours}
                Logged Hours: {LoggedHours}
                Basic Hours: {BasicLoggedHours}
                Overtime Hours: {OvertimeHours}
                Hourly Rate: ${HourlyRate}
                Hourly Overtime Percentage: {PayrollConstants.OvertimeRateMultiplier}x
                Basic Salary: {BasicLoggedHours} * ${HourlyRate} = ${BasicSalary}
                Overtime Salary = {OvertimeHours} * ${HourlyRate} * {PayrollConstants.OvertimeRateMultiplier}x = ${OvertimeSalary}
                Hardship: ${Hardship}
                Gross Pay: ${BasicSalary} + ${OvertimeSalary} + ${Hardship} = {GrossPay}
                Tax Percentage: %{PayrollConstants.TaxPercentage:p}
                Tax Amount: ${TaxAmount:f2}
            ___________________________________________
                
                Net Salary: ${NetSalary:f2}
";

            return Report;
        }
    }
}
