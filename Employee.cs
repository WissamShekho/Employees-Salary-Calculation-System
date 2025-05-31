using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesSalaryCalculationSystem
{
    abstract class Employee
    {
        protected Employee(int iD, decimal hourlyRate, int loggedHours, string firstName, string lastName)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(loggedHours);

            ID = iD;
            HourlyRate = hourlyRate;
            LoggedHours = loggedHours;
            FirstName = firstName;
            LastName = lastName;

            BasicLoggedHours = Math.Min(loggedHours, PayrollConstants.ExpectedHours);
            OvertimeHours = Math.Max(0, loggedHours - PayrollConstants.ExpectedHours);
        }

        public int ID { get; }
        protected decimal HourlyRate { get; }
        protected int LoggedHours { get; }
        protected int BasicLoggedHours { get; }
        protected int OvertimeHours { get; }
        public string FirstName  { set; get; } = string.Empty;// Nullable referance type is enabled
        public string LastName { set; get; } = string.Empty; // Nullable referance type is enabled
        public string FullName { get => FirstName + " " + LastName; }
      

        protected static decimal CalculateNetSalary(decimal grossPay, decimal taxAmount)
        {
            return grossPay - taxAmount;
        }

        protected static decimal CalculateTaxAmount(decimal grossPay, decimal taxPercentage)
        {
            return grossPay * (decimal)taxPercentage;
        }

        protected static decimal CalculateOvertimeSalary(int overtimeHours, decimal hourlyRate, decimal OvertimeRateMultiplier)
        {
            return overtimeHours * OvertimeRateMultiplier * hourlyRate;
        }

        protected static decimal CalculateBasicSalary(int basicLoggedHours, decimal hourlyRate)
        {
            return PayrollConstants.ExpectedHours * hourlyRate;
        }

        abstract public string SalaryReport();
    }
}
