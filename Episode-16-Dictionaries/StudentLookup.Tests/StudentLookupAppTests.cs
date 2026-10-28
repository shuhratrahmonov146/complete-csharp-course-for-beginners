using System.Diagnostics;

// Runs the real StudentLookup, types answers into it, and checks what it prints.
public class StudentLookupAppTests
{
    static string AppPath()
    {
        // StudentLookup.Tests/bin/Debug/net10.0 -> StudentLookup/bin/Debug/net10.0
        string root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        return Path.Combine(root, "StudentLookup", "bin", "Debug", "net10.0",
            "StudentLookup.dll");
    }

    static string Run(params string[] answers)
    {
        ProcessStartInfo info = new("dotnet", $"\"{AppPath()}\"")
        {
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
        };
        using Process app = Process.Start(info)!;
        foreach (string answer in answers)
        {
            app.StandardInput.WriteLine(answer);
        }
        app.StandardInput.Close(); // no more input: ReadLine returns null
        string output = app.StandardOutput.ReadToEnd();
        Assert.True(app.WaitForExit(10_000), "the app did not stop");
        Assert.Equal(0, app.ExitCode);
        return output.Replace("\r\n", "\n");
    }

    [Fact]
    public void ShowAll_ListsTheThreeStudents_WithCount()
    {
        string output = Run("1", "4");

        Assert.Contains("Students: 3\nAli: 85\nSara: 92\nTom: 78\n", output);
    }

    [Fact]
    public void LookUp_AnExistingStudent_ShowsTheScore()
    {
        string output = Run("2", "Sara", "4");

        Assert.Contains("Sara scored 92.", output);
    }

    [Fact]
    public void LookUp_AnUnknownStudent_DoesNotCrash()
    {
        string output = Run("2", "Zara", "2", "sara", "4");

        Assert.Contains("No student named Zara.", output);
        Assert.Contains("No student named sara.", output);
        Assert.Contains("Goodbye!", output);
    }

    [Fact]
    public void LookUp_WithoutAName_AsksForOne()
    {
        string output = Run("2", "", "4");

        Assert.Contains("Please type a name.", output);
    }

    [Fact]
    public void AddOrUpdate_ANewStudent_AddsItAtTheEnd()
    {
        string output = Run("3", "Mia", "88", "1", "4");

        Assert.Contains("Added Mia: 88", output);
        Assert.Contains("Students: 4\nAli: 85\nSara: 92\nTom: 78\nMia: 88\n", output);
    }

    [Fact]
    public void AddOrUpdate_AnExistingStudent_ChangesOnlyTheValue()
    {
        string output = Run("3", "Tom", "81", "1", "4");

        Assert.Contains("Updated Tom: 78 -> 81", output);
        Assert.Contains("Students: 3\nAli: 85\nSara: 92\nTom: 81\n", output);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("8.5")]
    [InlineData("101")]
    [InlineData("-1")]
    [InlineData("")]
    public void AddOrUpdate_WithAnInvalidScore_ChangesNothing(string score)
    {
        string output = Run("3", "Ben", score, "1", "4");

        Assert.Contains("Please type a name and a score 0-100.", output);
        Assert.Contains("Students: 3\n", output);
    }

    [Fact]
    public void AddOrUpdate_WithoutAName_ChangesNothing()
    {
        string output = Run("3", "", "50", "1", "4");

        Assert.Contains("Please type a name and a score 0-100.", output);
        Assert.Contains("Students: 3\n", output);
    }

    [Fact]
    public void InvalidChoice_GetsAHelpfulMessage()
    {
        string output = Run("9", "abc", "4");

        Assert.Contains("Please choose 1, 2, 3, or 4.", output);
    }

    [Fact]
    public void ClosedInput_EndsTheProgram()
    {
        string output = Run("3", "Mia"); // then the input ends: no score, then no menu choice

        Assert.Contains("Please type a name and a score 0-100.", output);
        Assert.Contains("Goodbye!", output);
    }
}
