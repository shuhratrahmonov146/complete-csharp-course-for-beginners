namespace EmployeePayrollSystem.Models;

public class PartTimeEmployee : Employee
{
    public int HoursWorked { get; set; }
    public decimal HourlyRate { get; set; }

    public PartTimeEmployee(int id, string name, decimal rate)
        : base(id, name)
    {
        HourlyRate = rate;
    }

    public override decimal CalculateSalary()
    {
        return HoursWorked * HourlyRate;
    }
}
