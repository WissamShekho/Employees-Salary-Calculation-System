namespace EmployeesSalaryCalculationSystem
{
    internal class Manager : Employee
    {
        private const decimal Allowance = 100.0m;
        public Manager(int id, string firstName, string lastName, int loggedHours):
            base(id, 10, loggedHours, firstName, lastName){ }
        private static decimal CalculateGrossPay(decimal basicSalary, decimal overtimeSalary, decimal AllowanceRate)
        {
            return basicSalary + overtimeSalary + AllowanceRate;
        }
        private static decimal CalculateAllowance(int loggedHours, decimal allowance)
        {
            return loggedHours >= 40 ? allowance : 0;
        }
        public override string SalaryReport()
        {
            decimal BasicSalary = CalculateBasicSalary(BasicLoggedHours, HourlyRate);
            decimal OvertimeSalary = CalculateOvertimeSalary(OvertimeHours, HourlyRate, PayrollConstants.OvertimeRateMultiplier);
            decimal AllowanceAmount = CalculateAllowance(LoggedHours, Allowance);
            decimal GrossPay = CalculateGrossPay(BasicSalary, OvertimeSalary, AllowanceAmount);
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
                Allowance: ${AllowanceAmount}
                Gross Pay: ${BasicSalary} + ${OvertimeSalary} + ${AllowanceAmount} = {GrossPay}
                Tax Percentage: %{PayrollConstants.TaxPercentage:p}
                Tax Amount: ${TaxAmount:f2}
            ___________________________________________
                
                Net Salary: ${NetSalary:f2}

";

            return Report;
        }
    }
}
