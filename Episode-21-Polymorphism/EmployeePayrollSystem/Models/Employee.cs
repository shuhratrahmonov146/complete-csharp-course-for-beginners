namespace EmployeePayrollSystem.Models;

public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; } = "";

    public Employee(int id, string name)
    {
        EmployeeId = id;
        Name = name;
    }

    public virtual decimal CalculateSalary()
    {
        return 0;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"{Name} (ID {EmployeeId})");
    }
}
