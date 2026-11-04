public class FullTimeEmployee : Employee
{
    public decimal MonthlySalary { get; set; }

    public FullTimeEmployee(string name, int employeeId,
        decimal monthlySalary)
        : base(name, employeeId)
    {
        MonthlySalary = monthlySalary;
    }

    public override decimal CalculateSalary()
    {
        return MonthlySalary;
    }
}
