using System.Diagnostics;

// Console output is redirected in some tests, so these tests run one after another.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

public class StudentTests
{
    static string[] IntroduceLines(Student student)
    {
        TextWriter original = Console.Out;
        StringWriter writer = new StringWriter();
        Console.SetOut(writer);
        try
        {
            student.Introduce();
        }
        finally
        {
            Console.SetOut(original);
        }
        return writer.ToString().TrimEnd().Split(Environment.NewLine);
    }

    [Fact]
    public void Constructor_SetsNameAndAge_GradeStartsAtZero()
    {
        Student student = new Student("Aisha", 15);

        Assert.Equal("Aisha", student.Name);
        Assert.Equal(15, student.Age);
        Assert.Equal(0, student.Grade);
    }

    [Fact]
    public void Properties_CanBeReadAndChanged()
    {
        Student student = new Student("Aisha", 15);

        student.Grade = 10;
        student.Age = 16;
        student.Name = "Aisha K.";

        Assert.Equal(10, student.Grade);
        Assert.Equal(16, student.Age);
        Assert.Equal("Aisha K.", student.Name);
    }

    [Fact]
    public void TwoObjects_KeepTheirOwnData()
    {
        Student student1 = new Student("Aisha", 15);
        Student student2 = new Student("Daniel", 16);

        student2.Name = "Dan";

        Assert.Equal("Aisha", student1.Name);
        Assert.Equal("Dan", student2.Name);
        Assert.NotSame(student1, student2);
    }

    [Fact]
    public void TwoVariables_CanReferToTheSameObject()
    {
        Student student1 = new Student("Aisha", 15);
        Student sameStudent = student1;

        sameStudent.Grade = 10;

        Assert.Same(student1, sameStudent);
        Assert.Equal(10, student1.Grade);
    }

    [Theory]
    [InlineData("Aisha", 15, 10)]
    [InlineData("Daniel", 16, 11)]
    [InlineData("Maria", 14, 9)]
    public void Introduce_PrintsTheObjectsOwnData(string name, int age, int grade)
    {
        Student student = new Student(name, age);
        student.Grade = grade;

        string[] lines = IntroduceLines(student);

        Assert.Equal(new[] { $"Hi, I'm {name}.", $"I'm {age} and I'm in grade {grade}." }, lines);
    }

    [Fact]
    public void Introduce_UsesTheCurrentValues()
    {
        Student student = new Student("Daniel", 16);
        student.Grade = 11;
        student.Name = "Dan";

        Assert.Equal("Hi, I'm Dan.", IntroduceLines(student)[0]);
    }

    [Fact]
    public void TheProgram_PrintsAllThreeStudents()
    {
        string dll = Path.Combine(AppContext.BaseDirectory, "StudentManagementBasics.dll");
        ProcessStartInfo info = new ProcessStartInfo("dotnet", $"\"{dll}\"")
        {
            RedirectStandardOutput = true,
            UseShellExecute = false
        };
        using Process process = Process.Start(info)!;
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        string[] expected =
        {
            "===== STUDENT RECORDS =====", "",
            "Hi, I'm Aisha.", "I'm 15 and I'm in grade 10.", "",
            "Hi, I'm Daniel.", "I'm 16 and I'm in grade 11.", "",
            "Hi, I'm Maria.", "I'm 14 and I'm in grade 9."
        };
        Assert.Equal(0, process.ExitCode);
        Assert.Equal(expected, output.TrimEnd().Split(Environment.NewLine));
    }
}
