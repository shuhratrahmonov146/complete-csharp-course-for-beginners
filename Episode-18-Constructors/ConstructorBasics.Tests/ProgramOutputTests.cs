using System.Diagnostics;

// Runs the real ConstructorBasics app (its dll is copied next to the tests) and checks what it prints.
public class ProgramOutputTests
{
    [Fact]
    public void TheAppPrintsTheThreeStudentsFromTheVideo()
    {
        string dll = Path.Combine(AppContext.BaseDirectory, "ConstructorBasics.dll");
        ProcessStartInfo start = new ProcessStartInfo("dotnet", $"\"{dll}\"")
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };
        using Process app = Process.Start(start)!;
        string output = app.StandardOutput.ReadToEnd();
        string errors = app.StandardError.ReadToEnd();
        app.WaitForExit();

        string[] expected =
        {
            "Hi, I'm Ali.", "I'm 16 and I'm in grade 11.",
            "Hi, I'm Sara.", "I'm 15 and I'm in grade 10.",
            "Hi, I'm Unknown.", "I'm 0 and I'm in grade 0.",
        };
        Assert.Equal(0, app.ExitCode);
        Assert.Equal("", errors);
        Assert.Equal(expected, output.ReplaceLineEndings("\n").TrimEnd('\n').Split('\n'));
    }
}
