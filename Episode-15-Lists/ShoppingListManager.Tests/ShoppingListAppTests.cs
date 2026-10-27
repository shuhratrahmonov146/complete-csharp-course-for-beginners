using System.Diagnostics;

// Runs the real ShoppingListManager, types answers into it, and checks what it prints.
public class ShoppingListAppTests
{
    static string AppPath()
    {
        // ShoppingListManager.Tests/bin/Debug/net10.0 -> ShoppingListManager/bin/Debug/net10.0
        string root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        return Path.Combine(root, "ShoppingListManager", "bin", "Debug", "net10.0",
            "ShoppingListManager.dll");
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
    public void ShowList_OnAnEmptyList_SaysItIsEmpty()
    {
        string output = Run("1", "4");

        Assert.Contains("Your list is empty.", output);
    }

    [Fact]
    public void Remove_OnAnEmptyList_SaysThereIsNothingToRemove()
    {
        string output = Run("3", "4");

        Assert.Contains("Nothing to remove. The list is empty.", output);
        Assert.DoesNotContain("Item to remove:", output);
    }

    [Fact]
    public void AddedItems_AreShownInOrder_WithCountAndLastAdded()
    {
        string output = Run("2", "Milk", "2", "Bread", "2", "Eggs", "1", "4");

        Assert.Contains("Items on your list: 3\n- Milk\n- Bread\n- Eggs\nLast added: Eggs", output);
    }

    [Fact]
    public void EmptyName_IsNotAdded()
    {
        string output = Run("2", "", "2", "   ", "1", "4");

        Assert.Contains("Please type an item name.", output);
        Assert.Contains("Your list is empty.", output);
    }

    [Fact]
    public void Duplicate_IsNotAddedTwice()
    {
        string output = Run("2", "Milk", "2", "Milk", "1", "4");

        Assert.Contains("Milk is already on the list.", output);
        Assert.Contains("Items on your list: 1\n- Milk\n", output);
    }

    [Fact]
    public void Remove_AnItemOnTheList_RemovesIt()
    {
        string output = Run("2", "Milk", "2", "Bread", "3", "Milk", "1", "4");

        Assert.Contains("Removed Milk.", output);
        Assert.Contains("Items on your list: 1\n- Bread\nLast added: Bread", output);
    }

    [Fact]
    public void Remove_AMissingItem_SaysSo_AndKeepsTheList()
    {
        string output = Run("2", "Milk", "3", "Pizza", "1", "4");

        Assert.Contains("Pizza is not on the list.", output);
        Assert.Contains("Items on your list: 1\n- Milk\n", output);
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
        string output = Run("2", "Milk"); // then the input ends

        Assert.Contains("Added Milk.", output);
        Assert.Contains("Goodbye!", output);
    }
}
