using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesSalaryCalculationSystem
{
    internal class SalesAgent : Employee
    {
       
        private const decimal CommissionPercentage = 0.005m;

        public readonly int TotalSales;
        public SalesAgent(int id, string firstName, string lastName, int loggedHours, int totalSales) :
            base(id, 10, loggedHours, firstName, lastName)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(totalSales);
            TotalSales = totalSales;
        }

        private static decimal CalculateGrossPay(decimal basicSalary, decimal overtimeSalary, decimal comissionAmount)
        {
            return basicSalary + overtimeSalary + comissionAmount;
        }

        private static decimal CalculateCommissionAmount(int totalSales, decimal comissionPercentage)
        {
            return totalSales * comissionPercentage;
        }
        public override string SalaryReport()
        {
            decimal BasicSalary = CalculateBasicSalary(BasicLoggedHours, HourlyRate);
            decimal OvertimeSalary = CalculateOvertimeSalary(OvertimeHours, HourlyRate, PayrollConstants.OvertimeRateMultiplier);
            decimal ComissionAmount = CalculateCommissionAmount(TotalSales, CommissionPercentage);
            decimal GrossPay = CalculateGrossPay(BasicSalary, OvertimeSalary, ComissionAmount);
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
                Overtime Salary: {OvertimeHours} * ${HourlyRate} * {PayrollConstants.OvertimeRateMultiplier}x = ${OvertimeSalary}
                Total Sales: {TotalSales}
                Sales Comission Amount: {TotalSales} * %{CommissionPercentage:P} = ${ComissionAmount:f2} 
                Gross Pay: ${BasicSalary} + ${OvertimeSalary} + ${ComissionAmount} = {GrossPay}
                Tax Percentage: %{PayrollConstants.TaxPercentage:p}
                Tax Amount: ${TaxAmount:f2}
            ___________________________________________
                
                Net Salary: ${NetSalary:f2}
";

            return Report;
        }
    }
}
