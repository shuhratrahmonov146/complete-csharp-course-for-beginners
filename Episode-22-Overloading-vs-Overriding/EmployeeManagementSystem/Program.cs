List<Employee> employees = new();
employees.Add(new FullTimeEmployee("Ali", 101, 4500m));
employees.Add(new PartTimeEmployee("Maria", 102, 80, 22m));
employees.Add(new FullTimeEmployee("Daniel", 103, 3800m));

foreach (Employee employee in employees)
{
    employee.DisplayInformation();
    Console.WriteLine($"Salary: {employee.CalculateSalary()}");
    Console.WriteLine();
}
