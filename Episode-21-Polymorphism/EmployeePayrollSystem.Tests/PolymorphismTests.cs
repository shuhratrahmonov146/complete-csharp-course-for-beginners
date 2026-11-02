using System.Reflection;
using EmployeePayrollSystem.Models;

// The tests replace Console.Out, so they must not run in parallel.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace EmployeePayrollSystem.Tests;

/// <summary>The class design taught in Episode 21: one base class with a virtual method, two overrides.</summary>
public class ClassDesignTests
{
    [Fact]
    public void BothEmployeeTypesDeriveFromEmployee()
    {
        Assert.Equal(typeof(Employee), typeof(FullTimeEmployee).BaseType);
        Assert.Equal(typeof(Employee), typeof(PartTimeEmployee).BaseType);
    }

    [Fact]
    public void CalculateSalaryIsVirtualInTheBaseClass()
    {
        MethodInfo method = typeof(Employee).GetMethod("CalculateSalary")!;
        Assert.True(method.IsVirtual);
        Assert.False(method.IsFinal);
        Assert.Equal(typeof(decimal), method.ReturnType);
        Assert.Empty(method.GetParameters());
    }

    [Theory]
    [InlineData(typeof(FullTimeEmployee))]
    [InlineData(typeof(PartTimeEmployee))]
    public void EachDerivedClassOverridesCalculateSalary(Type type)
    {
        // declared in the derived class itself, and it overrides Employee's virtual method (it does not hide it:
        // mistake 2 of the episode, a method without "override", would have its own base definition)
        MethodInfo method = type.GetMethod("CalculateSalary")!;
        Assert.Equal(type, method.DeclaringType);
        Assert.Equal(typeof(Employee), method.GetBaseDefinition().DeclaringType);
    }

    [Fact]
    public void ShowInfoIsWrittenOnceAndIsNotVirtual()
    {
        MethodInfo method = typeof(Employee).GetMethod("ShowInfo")!;
        Assert.False(method.IsVirtual);
        Assert.Equal(typeof(Employee), typeof(FullTimeEmployee).GetMethod("ShowInfo")!.DeclaringType);
        Assert.Equal(typeof(Employee), typeof(PartTimeEmployee).GetMethod("ShowInfo")!.DeclaringType);
    }

    [Fact]
    public void TheClassesLiveInTheModelsNamespace()
    {
        Assert.Equal("EmployeePayrollSystem.Models", typeof(Employee).Namespace);
        Assert.Equal("EmployeePayrollSystem.Models", typeof(FullTimeEmployee).Namespace);
        Assert.Equal("EmployeePayrollSystem.Models", typeof(PartTimeEmployee).Namespace);
    }
}

/// <summary>The salary rules: full-time = the monthly salary; part-time = hours worked x hourly rate.</summary>
public class SalaryTests
{
    [Theory]
    [InlineData("3000")]
    [InlineData("4500.50")]
    [InlineData("0")]
    public void FullTimeSalaryIsTheMonthlySalary(string monthly)
    {
        decimal salary = decimal.Parse(monthly, System.Globalization.CultureInfo.InvariantCulture);
        var ali = new FullTimeEmployee(101, "Ali", salary);
        Assert.Equal(salary, ali.CalculateSalary());
    }

    [Theory]
    [InlineData(80, "15", "1200")]
    [InlineData(37, "12.50", "462.50")]
    [InlineData(0, "15", "0")]
    public void PartTimeSalaryIsHoursTimesRate(int hours, string rate, string expected)
    {
        var inv = System.Globalization.CultureInfo.InvariantCulture;
        var sara = new PartTimeEmployee(102, "Sara", decimal.Parse(rate, inv));
        sara.HoursWorked = hours;
        Assert.Equal(decimal.Parse(expected, inv), sara.CalculateSalary());
    }

    [Fact]
    public void APlainEmployeeHasNoSalaryRule()
    {
        Assert.Equal(0m, new Employee(100, "Test").CalculateSalary());
    }

    [Fact]
    public void ConstructorsSetTheInheritedAndTheOwnProperties()
    {
        var ali = new FullTimeEmployee(101, "Ali", 3000m);
        var sara = new PartTimeEmployee(102, "Sara", 15m);
        Assert.Equal((101, "Ali", 3000m), (ali.EmployeeId, ali.Name, ali.MonthlySalary));
        Assert.Equal((102, "Sara", 15m, 0), (sara.EmployeeId, sara.Name, sara.HourlyRate, sara.HoursWorked));
    }
}

/// <summary>Polymorphism: the same call through an Employee reference runs the real object's version.</summary>
public class PolymorphismTests
{
    [Fact]
    public void ABaseClassVariableRunsTheDerivedOverride()
    {
        var sara = new PartTimeEmployee(102, "Sara", 15m) { HoursWorked = 80 };
        Employee employee = sara;
        Assert.Equal(1200m, employee.CalculateSalary());
    }

    [Fact]
    public void OneListOfEmployeeHoldsBothTypesAndEachRunsItsOwnVersion()
    {
        var sara = new PartTimeEmployee(102, "Sara", 15m) { HoursWorked = 80 };
        List<Employee> employees = new() { new FullTimeEmployee(101, "Ali", 3000m), sara };
        decimal[] salaries = employees.Select(e => e.CalculateSalary()).ToArray();
        Assert.Equal(new[] { 3000m, 1200m }, salaries);
        Assert.Equal(4200m, salaries.Sum());
    }
}

/// <summary>What the app prints (Console output captured).</summary>
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
    public void ShowInfoPrintsTheNameAndTheId()
    {
        var ali = new FullTimeEmployee(101, "Ali", 3000m);
        Assert.Equal("Ali (ID 101)\n", Capture(ali.ShowInfo));
    }

    [Fact]
    public void TheFinishedAppPrintsThePayroll()
    {
        MethodInfo main = typeof(Employee).Assembly.EntryPoint!;
        object?[] args = main.GetParameters().Length == 0 ? Array.Empty<object?>() : new object?[] { Array.Empty<string>() };
        string output = Capture(() => main.Invoke(null, args));
        Assert.Equal("Ali (ID 101)\nSalary: 3000\n\nSara (ID 102)\nSalary: 1200\n\nTotal payroll: 4200\n",
                     output.Replace("\r\n", "\n"));
    }
}
