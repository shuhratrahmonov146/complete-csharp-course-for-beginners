using System.Diagnostics;

// Runs the real EncapsulationBasics app and checks everything it prints.
public class EncapsulationAppTests
{
    [Fact]
    public void TheFinalProgram_PrintsEveryAcceptedAndRejectedRequest()
    {
        string app = Path.Combine(AppContext.BaseDirectory, "EncapsulationBasics.dll");
        ProcessStartInfo info = new("dotnet", $"\"{app}\"")
        {
            RedirectStandardOutput = true,
            UseShellExecute = false,
        };
        using Process process = Process.Start(info)!;
        string output = process.StandardOutput.ReadToEnd().Replace("\r\n", "\n");
        process.WaitForExit();

        string expected = """
            Balance: 0
            Deposited 100. Balance: 100
            Deposit rejected: -20
            Deposit rejected: 0
            Balance: 100
            Withdrew 30. Balance: 70
            Not enough money for 500.
            Withdrawal rejected: -5
            Final balance: 70

            """;
        Assert.Equal(0, process.ExitCode);
        Assert.Equal(expected.Replace("\r\n", "\n"), output);
    }
}
