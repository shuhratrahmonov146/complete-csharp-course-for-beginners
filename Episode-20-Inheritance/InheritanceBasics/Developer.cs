public class Developer : Employee
{
    public string ProgrammingLanguage { get; set; } = "";

    public Developer(string name, int employeeId, string language)
        : base(name, employeeId)
    {
        ProgrammingLanguage = language;
    }

    public void WriteCode()
    {
        Console.WriteLine($"{Name} codes in {ProgrammingLanguage}.");
    }
}
