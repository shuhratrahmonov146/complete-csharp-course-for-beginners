using System.Reflection;

// The tests replace Console.Out, so they must not run in parallel.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace InheritanceBasics.Tests;

/// <summary>The class design taught in Episode 20: one base class, two derived classes.</summary>
public class ClassDesignTests
{
    [Fact]
    public void DeveloperAndManagerInheritFromEmployee()
    {
        Assert.Equal(typeof(Employee), typeof(Developer).BaseType);
        Assert.Equal(typeof(Employee), typeof(Manager).BaseType);
        Assert.Equal(typeof(object), typeof(Employee).BaseType);
    }

    [Fact]
    public void ADeveloperIsAnEmployee()
    {
        Employee employee = new Developer("Aisha", 101, "C#");
        Assert.IsAssignableFrom<Employee>(employee);
    }

    [Theory]
    [InlineData("Name")]
    [InlineData("EmployeeId")]
    public void SharedPropertiesAreDeclaredOnlyInEmployee(string property)
    {
        // mistake 2 of the episode: a second Name in Developer would hide the inherited one
        Assert.Equal(typeof(Employee), typeof(Developer).GetProperty(property)!.DeclaringType);
        Assert.Equal(typeof(Employee), typeof(Manager).GetProperty(property)!.DeclaringType);
    }

    [Fact]
    public void ShowDetailsIsWrittenOnceInEmployee()
    {
        Assert.Equal(typeof(Employee), typeof(Developer).GetMethod("ShowDetails")!.DeclaringType);
        Assert.Equal(typeof(Employee), typeof(Manager).GetMethod("ShowDetails")!.DeclaringType);
    }

    [Fact]
    public void SpecializedMembersBelongOnlyToTheirOwnClass()
    {
        Assert.NotNull(typeof(Developer).GetProperty("ProgrammingLanguage"));
        Assert.NotNull(typeof(Manager).GetProperty("TeamSize"));
        Assert.Null(typeof(Developer).GetProperty("TeamSize"));
        Assert.Null(typeof(Manager).GetProperty("ProgrammingLanguage"));
        Assert.Null(typeof(Employee).GetProperty("ProgrammingLanguage"));
        Assert.Null(typeof(Employee).GetProperty("TeamSize"));
    }

    [Fact]
    public void AnEmployeeCannotBeCreatedWithoutANameAndAnId()
    {
        Assert.Null(typeof(Employee).GetConstructor(Type.EmptyTypes));
        Assert.NotNull(typeof(Employee).GetConstructor(new[] { typeof(string), typeof(int) }));
    }
}

/// <summary>Constructors: base(...) passes the shared values to the Employee constructor.</summary>
public class ConstructorTests
{
    [Fact]
    public void DeveloperConstructorSetsInheritedAndOwnProperties()
    {
        var dev = new Developer("Aisha", 101, "C#");
        Assert.Equal("Aisha", dev.Name);
        Assert.Equal(101, dev.EmployeeId);
        Assert.Equal("C#", dev.ProgrammingLanguage);
    }

    [Fact]
    public void ManagerConstructorSetsInheritedAndOwnProperties()
    {
        var manager = new Manager("Daniel", 102, 5);
        Assert.Equal("Daniel", manager.Name);
        Assert.Equal(102, manager.EmployeeId);
        Assert.Equal(5, manager.TeamSize);
    }
}

/// <summary>What the methods print (Console output captured).</summary>
public class OutputTests
{
    private static string Capture(Action action)
    {
        TextWriter original = Console.Out;
        var writer = new StringWriter { NewLine = "\n" };
        Console.SetOut(writer);
        try
        {
            action();
        }
        finally
        {
            Console.SetOut(original);
        }
        return writer.ToString();
    }

    [Fact]
    public void InheritedShowDetailsPrintsTheDevelopersSharedInformation()
    {
        var dev = new Developer("Aisha", 101, "C#");
        Assert.Equal("Name: Aisha\nEmployee ID: 101\n", Capture(dev.ShowDetails));
    }

    [Fact]
    public void InheritedShowDetailsPrintsTheManagersSharedInformation()
    {
        var manager = new Manager("Daniel", 102, 5);
        Assert.Equal("Name: Daniel\nEmployee ID: 102\n", Capture(manager.ShowDetails));
    }

    [Fact]
    public void WriteCodeUsesTheInheritedNameAndTheOwnLanguage()
    {
        var dev = new Developer("Aisha", 101, "C#");
        Assert.Equal("Aisha codes in C#.\n", Capture(dev.WriteCode));
    }

    [Fact]
    public void LeadTeamUsesTheInheritedNameAndTheOwnTeamSize()
    {
        var manager = new Manager("Daniel", 102, 5);
        Assert.Equal("Daniel leads a team of 5.\n", Capture(manager.LeadTeam));
    }

    [Fact]
    public void TheFinishedAppPrintsBothEmployees()
    {
        MethodInfo main = typeof(Employee).Assembly.EntryPoint!;
        object?[] args = main.GetParameters().Length == 0 ? Array.Empty<object?>() : new object?[] { Array.Empty<string>() };
        string output = Capture(() => main.Invoke(null, args));
        Assert.Equal("Name: Aisha\nEmployee ID: 101\nAisha codes in C#.\n\n" +
                     "Name: Daniel\nEmployee ID: 102\nDaniel leads a team of 5.\n", output.Replace("\r\n", "\n"));
    }
}
