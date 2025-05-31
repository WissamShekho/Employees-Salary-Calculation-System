using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesSalaryCalculationSystem
{
    internal class SoftwareEngineer : Employee
    {
        private const decimal TrainingAllowance = 50.0m;
        private const decimal Bonus = 40.0m;
        private const int TasksTarget = 8;

        private readonly int TasksCompleted;

        public SoftwareEngineer(int id, string firstName, string lastName, int loggedHours, int tasksCompleted) :
            base(id, 10, loggedHours, firstName, lastName)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(TasksCompleted);
            TasksCompleted = tasksCompleted;
        }
        private static decimal CalculateGrossPay(decimal basicSalary, decimal overtimeSalary, decimal trainingAllowance, decimal TasksCompleted)
        {
            return basicSalary + overtimeSalary + trainingAllowance + (TasksCompleted >= TasksTarget ? Bonus : 0);
        }
        private static decimal CalculateBonus(int tasksCompleted, int tasksTarget, decimal Bonus)
        {
            return tasksCompleted >= tasksTarget ? Bonus : 0;
        }
        public override string SalaryReport()
        {
            decimal BasicSalary = CalculateBasicSalary(BasicLoggedHours, HourlyRate);
            decimal OvertimeSalary = CalculateOvertimeSalary(OvertimeHours, HourlyRate, PayrollConstants.OvertimeRateMultiplier);
            decimal BonusAmount = CalculateBonus(TasksCompleted, TasksTarget, Bonus);
            decimal GrossPay = CalculateGrossPay(BasicSalary, OvertimeSalary, TrainingAllowance, TasksCompleted);
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
                Tasks Traget: {TasksTarget} 
                Bonus (if Target Achieved): ${Bonus}
                Tasks Completed: {TasksCompleted}
                Training Allowance: {TrainingAllowance}
                Gross Pay: ${BasicSalary} + ${OvertimeSalary} + ${TrainingAllowance} + ${BonusAmount} = {GrossPay}
                Tax Percentage: %{PayrollConstants.TaxPercentage:p}
                Tax Amount: ${TaxAmount:f2}
            ___________________________________________
                
                Net Salary: ${NetSalary:f2}
";

            return Report;
        }
    }
}
