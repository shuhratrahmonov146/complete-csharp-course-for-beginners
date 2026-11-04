public class PartTimeEmployee : Employee
{
    public int HoursWorked { get; set; }
    public decimal HourlyRate { get; set; }

    public PartTimeEmployee(string name, int employeeId,
        int hoursWorked, decimal hourlyRate)
        : base(name, employeeId)
    {
        HoursWorked = hoursWorked;
        HourlyRate = hourlyRate;
    }

    public override decimal CalculateSalary()
    {
        return HoursWorked * HourlyRate;
    }
}
