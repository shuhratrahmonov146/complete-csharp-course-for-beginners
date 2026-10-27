public class Student
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public int Grade { get; set; }

    public Student(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public void Introduce()
    {
        Console.WriteLine($"Hi, I'm {Name}.");
        Console.WriteLine($"I'm {Age} and I'm in grade {Grade}.");
    }
}
