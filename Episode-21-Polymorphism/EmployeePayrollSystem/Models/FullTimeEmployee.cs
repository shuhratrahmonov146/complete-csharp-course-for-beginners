namespace EmployeePayrollSystem.Models;

public class FullTimeEmployee : Employee
{
    public decimal MonthlySalary { get; set; }

    public FullTimeEmployee(int id, string name, decimal salary)
        : base(id, name)
    {
        MonthlySalary = salary;
    }

    public override decimal CalculateSalary()
    {
        return MonthlySalary;
    }
}
