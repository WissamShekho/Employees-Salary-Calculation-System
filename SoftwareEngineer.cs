using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesSalaryCalculationSystem
{
    internal class SoftwareEngineer
    {
        private const decimal HourlyRate = 10.0m;
        private const decimal TrainingAllowance = 50.0m;
        private const decimal TaxPercentage = 0.1m;
        private const decimal HourlyOvertimePercentage = 1.5m;
        private const decimal Bonus = 40.0m;
        private const int ExpectedHours = 40;
        private const int StoryPointTarget = 8;

        public int ID { get; }
        public int LoggedHours { get; }
        public string FirstName { set; get; } = string.Empty;  // Nullable referance type is enabled
        public string LastName { set; get; } = string.Empty;
        public string FullName { get; } = string.Empty;

        private decimal BonusAmount { get; set; }
        private int StoryPoint { get; set; }
        private int BasicHours { get; set; }
        private int OvertimeHours { get; set; }

        public SoftwareEngineer(int id, string firstName, string lastName, int loggedHours, int storyPoint)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(loggedHours);
            ArgumentOutOfRangeException.ThrowIfNegative(storyPoint);

            ID = id;
            FirstName = firstName;
            LastName = lastName;
            LoggedHours = loggedHours;
            StoryPoint = storyPoint;

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

            BonusAmount = storyPoint >= StoryPointTarget ? Bonus : 0;
        }

        private decimal _calculateNetSalary(decimal grossPay, decimal taxAmount)
        {
            return grossPay - taxAmount;
        }
        private decimal _calculateTaxAmount(decimal grossPay, decimal taxPercentage)
        {
            return grossPay * (decimal)taxPercentage;
        }

        private decimal _calculateGrossPay(decimal basicSalary, decimal overtimeSalary, decimal trainingAllowance, decimal storyPoint)
        {
            return basicSalary + overtimeSalary + trainingAllowance + (storyPoint >= StoryPointTarget ? Bonus : 0);
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
            decimal GrossPay = _calculateGrossPay(BasicSalary, OvertimeSalary, TrainingAllowance, StoryPoint);
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
                Overtime Salary: {OvertimeHours} * ${HourlyRate} * {HourlyOvertimePercentage}x = ${OvertimeSalary}
                Story Point Traget: {StoryPointTarget} 
                Bonus (if Target Achieved): ${Bonus}
                Story Point: {StoryPoint}
                Training Allowance: {TrainingAllowance}
                Gross Pay: ${BasicSalary} + ${OvertimeSalary} + ${TrainingAllowance} + ${BonusAmount} = {GrossPay}
                Tax Percentage: %{TaxPercentage:p}
                Tax Amount: ${TaxAmount:f2}
            ___________________________________________
                
                Net Salary: ${NetSalary:f2}
";

            return Report;
        }
    }

}
