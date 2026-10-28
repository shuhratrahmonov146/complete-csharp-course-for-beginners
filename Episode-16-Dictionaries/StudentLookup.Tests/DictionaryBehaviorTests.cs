// What the lesson says about Dictionary<string, int>, checked by the C# compiler and runtime.
public class DictionaryBehaviorTests
{
    static Dictionary<string, int> ThreeStudents()
    {
        Dictionary<string, int> scores = new();
        scores.Add("Ali", 85);
        scores.Add("Sara", 92);
        scores.Add("Tom", 78);
        return scores;
    }

    [Fact]
    public void NewDictionary_IsEmpty()
    {
        Dictionary<string, int> scores = new();

        Assert.Empty(scores);
    }

    [Fact]
    public void AddAndIndexer_BothAddNewKeys()
    {
        Dictionary<string, int> scores = new();

        scores.Add("Ali", 85);
        scores.Add("Sara", 92);
        scores["Tom"] = 78;

        Assert.Equal(3, scores.Count);
        Assert.Equal(78, scores["Tom"]);
    }

    [Fact]
    public void Indexer_ReadsTheValueOfAKey()
    {
        Dictionary<string, int> scores = ThreeStudents();

        int saraScore = scores["Sara"];

        Assert.Equal(92, saraScore);
    }

    [Fact]
    public void Indexer_WithAMissingKey_Throws()
    {
        Dictionary<string, int> scores = ThreeStudents();

        KeyNotFoundException error = Assert.Throws<KeyNotFoundException>(() => scores["Zara"]);
        Assert.Equal("The given key 'Zara' was not present in the dictionary.", error.Message);
    }

    [Fact]
    public void TryGetValue_ReturnsFalse_ForAMissingKey()
    {
        Dictionary<string, int> scores = ThreeStudents();

        bool foundZara = scores.TryGetValue("Zara", out int zaraScore);
        bool foundSara = scores.TryGetValue("Sara", out int saraScore);

        Assert.False(foundZara);
        Assert.Equal(0, zaraScore);
        Assert.True(foundSara);
        Assert.Equal(92, saraScore);
    }

    [Fact]
    public void Indexer_WithAnExistingKey_UpdatesTheValue_AndKeepsTheKey()
    {
        Dictionary<string, int> scores = ThreeStudents();

        scores["Tom"] = 81;

        Assert.Equal(81, scores["Tom"]);
        Assert.Equal(3, scores.Count);
        Assert.Equal(new[] { "Ali", "Sara", "Tom" }, scores.Keys);
    }

    [Fact]
    public void Add_WithAnExistingKey_Throws_AndKeepsTheOldValue()
    {
        Dictionary<string, int> scores = ThreeStudents();

        Assert.Throws<ArgumentException>(() => scores.Add("Ali", 95));
        Assert.Equal(85, scores["Ali"]);
    }

    [Fact]
    public void ContainsKey_ChecksKeys_NotValues()
    {
        Dictionary<string, int> scores = ThreeStudents();

        Assert.True(scores.ContainsKey("Ali"));
        Assert.False(scores.ContainsKey("Zara"));
        Assert.False(scores.ContainsKey("85")); // a key that looks like a score is still just a key
        Assert.True(scores.ContainsValue(85));
    }

    [Fact]
    public void Keys_CompareTheExactText()
    {
        Dictionary<string, int> scores = ThreeStudents();

        Assert.True(scores.ContainsKey("Sara"));
        Assert.False(scores.ContainsKey("sara"));
    }

    [Fact]
    public void Foreach_VisitsEveryKeyValuePair()
    {
        Dictionary<string, int> scores = ThreeStudents();
        List<string> visited = new();

        foreach (KeyValuePair<string, int> entry in scores)
        {
            visited.Add($"{entry.Key}: {entry.Value}");
        }

        Assert.Equal(new[] { "Ali: 85", "Sara: 92", "Tom: 78" }, visited);
    }

    [Fact]
    public void Dictionary_IsNotSorted_ButSortedDictionaryIs()
    {
        Dictionary<string, int> scores = new();
        scores.Add("Tom", 78);
        scores.Add("Ali", 85);
        scores.Add("Sara", 92);

        SortedDictionary<string, int> sorted = new(scores);

        Assert.NotEqual(new[] { "Ali", "Sara", "Tom" }, scores.Keys);
        Assert.Equal(new[] { "Ali", "Sara", "Tom" }, sorted.Keys);
    }
}
