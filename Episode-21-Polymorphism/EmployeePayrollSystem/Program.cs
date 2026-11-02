using EmployeePayrollSystem.Models;

FullTimeEmployee ali = new FullTimeEmployee(101, "Ali", 3000m);
PartTimeEmployee sara = new PartTimeEmployee(102, "Sara", 15m);
sara.HoursWorked = 80;

List<Employee> employees = new();
employees.Add(ali);
employees.Add(sara);

decimal total = 0;
foreach (Employee employee in employees)
{
    employee.ShowInfo();
    decimal salary = employee.CalculateSalary();
    Console.WriteLine($"Salary: {salary}");
    Console.WriteLine();
    total += salary;
}
Console.WriteLine($"Total payroll: {total}");
