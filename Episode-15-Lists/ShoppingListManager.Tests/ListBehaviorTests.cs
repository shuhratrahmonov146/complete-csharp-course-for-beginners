// What the lesson says about List<string>, checked by the C# compiler and runtime.
public class ListBehaviorTests
{
    [Fact]
    public void NewList_IsEmpty()
    {
        List<string> items = new();

        int count = items.Count;

        Assert.Equal(0, count);
    }

    [Fact]
    public void Add_PutsTheItemAtTheEnd()
    {
        List<string> items = new();

        items.Add("Milk");
        items.Add("Bread");
        items.Add("Eggs");

        Assert.Equal(new[] { "Milk", "Bread", "Eggs" }, items);
        Assert.Equal(3, items.Count);
    }

    [Fact]
    public void Index_ReadsAndUpdatesAnItem()
    {
        List<string> items = new() { "Milk", "Bread", "Eggs" };

        Assert.Equal("Milk", items[0]);
        items[1] = "Brown bread";

        Assert.Equal(new[] { "Milk", "Brown bread", "Eggs" }, items);
        Assert.Equal(3, items.Count);
    }

    [Fact]
    public void Index_OutsideTheList_Throws()
    {
        List<string> items = new() { "Milk", "Brown bread", "Eggs" };

        Assert.Throws<ArgumentOutOfRangeException>(() => items[3]);
    }

    [Fact]
    public void Remove_ReturnsTrue_AndTheLaterItemsMoveLeft()
    {
        List<string> items = new() { "Milk", "Brown bread", "Eggs" };

        bool removed = items.Remove("Milk");

        Assert.True(removed);
        Assert.Equal(new[] { "Brown bread", "Eggs" }, items);
        Assert.Equal("Brown bread", items[0]);
    }

    [Fact]
    public void Remove_ReturnsFalse_ForAMissingItem()
    {
        List<string> items = new() { "Brown bread", "Eggs" };

        bool removed = items.Remove("Pizza");

        Assert.False(removed);
        Assert.Equal(new[] { "Brown bread", "Eggs" }, items);
    }

    [Fact]
    public void RemoveAt_RemovesByIndex_AndNeedsAValidIndex()
    {
        List<string> items = new() { "Milk", "Brown bread", "Eggs" };

        items.RemoveAt(0);

        Assert.Equal(new[] { "Brown bread", "Eggs" }, items);
        Assert.Throws<ArgumentOutOfRangeException>(() => items.RemoveAt(5));
    }

    [Fact]
    public void Contains_ComparesTheExactText()
    {
        List<string> items = new() { "Milk", "Brown bread", "Eggs" };

        bool hasEggs = items.Contains("Eggs");
        bool hasLowercaseEggs = items.Contains("eggs");
        bool hasPizza = items.Contains("Pizza");

        Assert.True(hasEggs);
        Assert.False(hasLowercaseEggs);
        Assert.False(hasPizza);
    }

    [Fact]
    public void Foreach_VisitsEveryItemInOrder()
    {
        List<string> items = new() { "Brown bread", "Eggs", "Apples" };
        List<string> visited = new();

        foreach (string item in items)
        {
            visited.Add(item);
        }

        Assert.Equal(items, visited);
    }
}
