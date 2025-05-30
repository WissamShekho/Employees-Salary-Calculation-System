using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesSalaryCalculationSystem
{
    internal class Handyman
    {
        private const decimal HourlyRate = 5.0m;
        private const decimal Hardship = 75.0m;
        private const decimal TaxPercentage = 0.1m;
        private const decimal HourlyOvertimePercentage = 1.5m;
        private const int ExpectedHours = 40;

        public int ID { get; }
        public int LoggedHours { get; }
        public string FirstName { set; get; } = string.Empty;  // Nullable referance type is enabled
        public string LastName { set; get; } = string.Empty;
        public string FullName { get; } = string.Empty;

        private int BasicHours { get; set; }
        private int OvertimeHours { get; set; }
        
        public Handyman(int id, string firstName, string lastName, int loggedHours)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(loggedHours);

            ID = id;
            FirstName = firstName;
            LastName = lastName;
            LoggedHours = loggedHours;

            FullName = firstName + " " + LastName;

            if (loggedHours >= ExpectedHours)
            {
                BasicHours = ExpectedHours;
                OvertimeHours = LoggedHours - ExpectedHours;
            }
            else
            {
                BasicHours = LoggedHours;
                OvertimeHours = 0;
            }
        }

        private decimal _calculateNetSalary(decimal grossPay, decimal taxAmount)
        {
            return grossPay - taxAmount;
        }
        private decimal _calculateTaxAmount(decimal grossPay, decimal taxPercentage)
        {
            return grossPay * (decimal)taxPercentage;
        }

        private decimal _calculateGrossPay(decimal basicSalary, decimal overtimeSalary, decimal hardship)
        {
            return basicSalary + overtimeSalary + hardship;
        }

        private decimal _calculateOvertimeSalary(int loggedHours, int basicHours, decimal hourlyRate, decimal hourlyOvertimePercentage)
        {
            return (loggedHours - basicHours) * (decimal)hourlyOvertimePercentage * hourlyRate;
        }

        private decimal _calculateBasicSalary(int expectedHours, decimal hourlyRate)
        {
            return expectedHours * hourlyRate;
        }

        public string SalaryReport()
        {
            decimal BasicSalary = _calculateBasicSalary(BasicHours, HourlyRate);
            decimal OvertimeSalary = _calculateOvertimeSalary(LoggedHours, BasicHours, HourlyRate, HourlyOvertimePercentage);
            decimal GrossPay = _calculateGrossPay(BasicSalary, OvertimeSalary, Hardship);
            decimal TaxAmount = _calculateTaxAmount(GrossPay, TaxPercentage);
            decimal NetSalary = _calculateNetSalary(GrossPay, TaxAmount);
            string Report = $@"
                
                ID  : {ID}
                Name: {FullName}
            ___________________________________________
                Expected Hours: {ExpectedHours}
                Logged Hours: {LoggedHours}
                Basic Hours: {BasicHours}
                Overtime Hours: {OvertimeHours}
                Hourly Rate: ${HourlyRate}
                Hourly Overtime Percentage: {HourlyOvertimePercentage}x
                Basic Salary: {BasicHours} * ${HourlyRate} = ${BasicSalary}
                Overtime Salary = {OvertimeHours} * ${HourlyRate} * {HourlyOvertimePercentage}x = ${OvertimeSalary}
                Hardship: ${Hardship}
                Gross Pay: ${BasicSalary} + ${OvertimeSalary} + ${Hardship} = {GrossPay}
                Tax Percentage: %{TaxPercentage:p}
                Tax Amount: ${TaxAmount:f2}
            ___________________________________________
                
                Net Salary: ${NetSalary:f2}
";

            return Report;
        }
    }
}
