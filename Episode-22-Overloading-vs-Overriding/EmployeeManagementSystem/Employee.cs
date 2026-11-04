public class Employee
{
    public string Name { get; set; } = "";
    public int EmployeeId { get; set; }

    public Employee(string name, int employeeId)
    {
        Name = name;
        EmployeeId = employeeId;
    }

    public void DisplayInformation()
    {
        DisplayInformation(Name, EmployeeId);
    }

    public void DisplayInformation(string name)
    {
        Console.WriteLine($"Name: {name}");
    }

    public void DisplayInformation(string name, int employeeId)
    {
        Console.WriteLine($"Name: {name}, ID: {employeeId}");
    }

    public void DisplayInformation(string name, int employeeId,
        string department)
    {
        Console.WriteLine($"Name: {name}, ID: {employeeId}");
        Console.WriteLine($"Department: {department}");
    }

    public virtual decimal CalculateSalary()
    {
        return 0;
    }
}
