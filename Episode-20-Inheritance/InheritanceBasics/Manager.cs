public class Manager : Employee
{
    public int TeamSize { get; set; }

    public Manager(string name, int employeeId, int teamSize)
        : base(name, employeeId)
    {
        TeamSize = teamSize;
    }

    public void LeadTeam()
    {
        Console.WriteLine($"{Name} leads a team of {TeamSize}.");
    }
}
