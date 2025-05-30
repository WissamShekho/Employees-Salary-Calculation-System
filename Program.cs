namespace EmployeesSalaryCalculationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Manager m1 = new Manager(1, "Wissam", "Shekho", 50);
            //Console.WriteLine(m1.SalaryReport());

            //SalesAgent salesAgent = new SalesAgent(1, "Wissam", "Shekho", 20, 10_000);
            //Console.WriteLine(salesAgent.SalaryReport());

            //Handyman handyman = new Handyman(1, "Wissam", "Shekho", 100);
            //Console.WriteLine(handyman.SalaryReport());

            SoftwareEngineer softwareEngineer = new SoftwareEngineer(1, "Wissam", "Shekho", 50, 10);
            Console.WriteLine(softwareEngineer.SalaryReport());

            Console.ReadKey();
        }
    }
}
