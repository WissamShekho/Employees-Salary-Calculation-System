namespace EmployeesSalaryCalculationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Manager manager = new Manager(101, "Muhammed", "Khalid", 50);
            // Console.WriteLine(manager.SalaryReport());

            SalesAgent salesAgent = new SalesAgent(102, "Fadi", "Abdullah", 20, 10_000);
            // Console.WriteLine(salesAgent.SalaryReport());

            Handyman handyman = new Handyman(1, "Khalid", "Sami", 51);
            // Console.WriteLine(handyman.SalaryReport());

            SoftwareEngineer softwareEngineer = new SoftwareEngineer(1, "Ahmed", "Sabri", 42, 10);
            // Console.WriteLine(softwareEngineer.SalaryReport());
            
            List<Employee> Employees = [manager, salesAgent, handyman, softwareEngineer]; 

            Employees.ForEach(employee => Console.WriteLine(employee.SalaryReport())); // polymorphically

            Console.ReadKey();
        }
    }
}
