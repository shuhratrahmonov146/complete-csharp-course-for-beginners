using System.Reflection;

// The tests replace Console.Out, so they must not run in parallel.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace EmployeeManagementSystem.Tests;

internal static class ConsoleCapture
{
    public static string Run(Action action)
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
        return writer.ToString().Replace("\r\n", "\n");
    }
}

/// <summary>Part A: method overloading - one name, different parameter lists, chosen by the arguments.</summary>
public class OverloadingTests
{
    private static MethodInfo[] Overloads() =>
        typeof(Employee).GetMethods().Where(m => m.Name == "DisplayInformation").ToArray();

    [Fact]
    public void EmployeeHasFourDisplayInformationOverloads()
    {
        // three from Part A, one without parameters from Part C
        int[] counts = Overloads().Select(m => m.GetParameters().Length).OrderBy(n => n).ToArray();
        Assert.Equal(new[] { 0, 1, 2, 3 }, counts);
    }

    [Fact]
    public void TheOverloadsDifferOnlyInTheirParameterLists()
    {
        // same name, same return type: the parameter list is what makes each one different
        Assert.All(Overloads(), m => Assert.Equal(typeof(void), m.ReturnType));
        string[] lists = Overloads()
            .Select(m => string.Join(",", m.GetParameters().Select(p => p.ParameterType.Name)))
            .ToArray();
        Assert.Equal(lists.Length, lists.Distinct().Count());
        Assert.Contains("String,Int32,String", lists);
    }

    [Fact]
    public void OneArgumentSelectsTheNameOnlyOverload()
    {
        var employee = new Employee("Unused", 0);
        Assert.Equal("Name: Ali\n", ConsoleCapture.Run(() => employee.DisplayInformation("Ali")));
    }

    [Fact]
    public void TwoArgumentsSelectTheNameAndIdOverload()
    {
        var employee = new Employee("Unused", 0);
        Assert.Equal("Name: Ali, ID: 101\n", ConsoleCapture.Run(() => employee.DisplayInformation("Ali", 101)));
    }

    [Fact]
    public void ThreeArgumentsSelectTheOverloadWithTheDepartment()
    {
        var employee = new Employee("Unused", 0);
        Assert.Equal("Name: Ali, ID: 101\nDepartment: Finance\n",
                     ConsoleCapture.Run(() => employee.DisplayInformation("Ali", 101, "Finance")));
    }

    [Fact]
    public void NoArgumentsDisplaysTheObjectsOwnData()
    {
        Employee employee = new PartTimeEmployee("Maria", 102, 80, 22m);
        Assert.Equal("Name: Maria, ID: 102\n", ConsoleCapture.Run(() => employee.DisplayInformation()));
    }

    [Fact]
    public void DisplayInformationIsInheritedNotOverridden()
    {
        // inheritance alone overrides nothing: the derived classes use Employee's overloads as they are
        foreach (Type derived in new[] { typeof(FullTimeEmployee), typeof(PartTimeEmployee) })
        {
            MethodInfo method = derived.GetMethod("DisplayInformation", Type.EmptyTypes)!;
            Assert.Equal(typeof(Employee), method.DeclaringType);
            Assert.False(method.IsVirtual);
        }
    }
}

/// <summary>Part B: method overriding - virtual in the base class, override in the derived classes.</summary>
public class OverridingTests
{
    [Fact]
    public void BothEmployeeTypesInheritFromEmployee()
    {
        Assert.Equal(typeof(Employee), typeof(FullTimeEmployee).BaseType);
        Assert.Equal(typeof(Employee), typeof(PartTimeEmployee).BaseType);
    }

    [Fact]
    public void CalculateSalaryIsVirtualInEmployeeAndReturnsDecimal()
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
    public void EachDerivedClassOverridesCalculateSalary(Type derived)
    {
        // override (not a new method that hides it): the base definition is Employee's virtual method
        MethodInfo method = derived.GetMethod("CalculateSalary", Type.EmptyTypes)!;
        Assert.Equal(derived, method.DeclaringType);
        Assert.Equal(typeof(Employee), method.GetBaseDefinition().DeclaringType);
    }

    [Fact]
    public void TheBaseVersionReturnsZero()
    {
        Assert.Equal(0m, new Employee("Base", 1).CalculateSalary());
    }

    [Theory]
    [InlineData(4500, 4500)]
    [InlineData(3800, 3800)]
    public void AFullTimeSalaryIsTheMonthlySalary(int monthly, int expected)
    {
        Assert.Equal((decimal)expected, new FullTimeEmployee("Ali", 101, monthly).CalculateSalary());
    }

    [Theory]
    [InlineData(80, "22", "1760")]
    [InlineData(37, "18.50", "684.50")]
    [InlineData(0, "25", "0")]
    public void APartTimeSalaryIsHoursTimesTheHourlyRate(int hours, string rate, string expected)
    {
        var employee = new PartTimeEmployee("Maria", 102, hours, decimal.Parse(rate, System.Globalization.CultureInfo.InvariantCulture));
        Assert.Equal(decimal.Parse(expected, System.Globalization.CultureInfo.InvariantCulture), employee.CalculateSalary());
    }

    [Fact]
    public void AnEmployeeReferenceRunsTheVersionOfTheRealObject()
    {
        Employee employee = new FullTimeEmployee("Ali", 101, 4500m);
        Assert.Equal(4500m, employee.CalculateSalary());

        employee = new PartTimeEmployee("Maria", 102, 80, 22m);
        Assert.Equal(1760m, employee.CalculateSalary());
    }

    [Fact]
    public void AListOfEmployeesUsesEachObjectsOwnOverride()
    {
        List<Employee> employees = new()
        {
            new FullTimeEmployee("Ali", 101, 4500m),
            new PartTimeEmployee("Maria", 102, 80, 22m),
            new FullTimeEmployee("Daniel", 103, 3800m),
        };
        Assert.Equal(new[] { 4500m, 1760m, 3800m }, employees.Select(e => e.CalculateSalary()).ToArray());
    }
}

/// <summary>Part C: the finished app, both concepts in one loop.</summary>
public class ProgramTests
{
    [Fact]
    public void TheFinishedAppPrintsEveryEmployeeWithTheRightSalary()
    {
        MethodInfo main = typeof(Employee).Assembly.EntryPoint!;
        object?[] args = main.GetParameters().Length == 0 ? Array.Empty<object?>() : new object?[] { Array.Empty<string>() };
        string output = ConsoleCapture.Run(() => main.Invoke(null, args));
        Assert.Equal("Name: Ali, ID: 101\nSalary: 4500\n\n" +
                     "Name: Maria, ID: 102\nSalary: 1760\n\n" +
                     "Name: Daniel, ID: 103\nSalary: 3800\n\n", output);
    }
}
