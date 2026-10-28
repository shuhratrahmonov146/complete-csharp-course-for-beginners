using System.Reflection;

// What the lesson says about the Student class from ConstructorBasics, checked on the real class.
public class StudentConstructorTests
{
    [Fact]
    public void ParameterlessConstructor_SetsNameToUnknown_AgeAndGradeStayZero()
    {
        Student student = new Student();

        Assert.Equal("Unknown", student.Name);
        Assert.Equal(0, student.Age);
        Assert.Equal(0, student.Grade);
    }

    [Fact]
    public void NameAndAgeConstructor_SetsNameAndAge_GradeKeepsItsDefault()
    {
        Student student = new Student("Ali", 16);

        Assert.Equal("Ali", student.Name);
        Assert.Equal(16, student.Age);
        Assert.Equal(0, student.Grade);
    }

    [Fact]
    public void NameAgeAndGradeConstructor_SetsAllThreeProperties()
    {
        Student student = new Student("Ali", 16, 11);

        Assert.Equal("Ali", student.Name);
        Assert.Equal(16, student.Age);
        Assert.Equal(11, student.Grade);
    }

    [Fact]
    public void ArgumentsGoToParametersInOrder()
    {
        Student student = new Student("Sara", 15);
        student.Grade = 10;

        Assert.Equal("Sara", student.Name);
        Assert.Equal(15, student.Age);
        Assert.Equal(10, student.Grade);
    }

    [Fact]
    public void TwoObjectsFromTheSameConstructor_KeepTheirOwnValues()
    {
        Student student1 = new Student("Ali", 16);
        Student student2 = new Student("Sara", 15);

        student2.Name = "Changed";

        Assert.Equal("Ali", student1.Name);
        Assert.Equal(16, student1.Age);
        Assert.Equal("Changed", student2.Name);
        Assert.NotSame(student1, student2);
    }

    [Fact]
    public void TheClassHasExactlyTheThreeConstructorsFromTheVideo()
    {
        string[] signatures = typeof(Student)
            .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
            .Select(c => string.Join(", ", c.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}")))
            .OrderBy(s => s.Length)
            .ToArray();

        Assert.Equal(new[] { "", "String name, Int32 age", "String name, Int32 age, Int32 grade" }, signatures);
    }

    [Fact]
    public void Introduce_PrintsTheNameThenTheAgeAndGrade()
    {
        Student student = new Student("Ali", 16, 11);
        StringWriter output = new StringWriter();
        TextWriter original = Console.Out;
        Console.SetOut(output);
        try
        {
            student.Introduce();
        }
        finally
        {
            Console.SetOut(original);
        }

        Assert.Equal($"Hi, I'm Ali.{Environment.NewLine}I'm 16 and I'm in grade 11.{Environment.NewLine}",
                     output.ToString());
    }
}
