# Episode 16 — C# Dictionaries Explained for Beginners — Key-Value Pairs

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

▶ **Watch the video:** https://www.youtube.com/watch?v=4j3VCF0LEQk

[⬅ Episode 15](../Episode-15-Lists/) · [All episodes](../README.md) · [Episode 17 ➡](../Episode-17-Classes-and-Objects/)

You have a class full of students, and you need one student's score, right now. A list makes you search through the
items. A **dictionary** finds it directly: give it a key, like a name, and it gives you the value, the score. In this
video you learn C# dictionaries step by step, and we build a Student Lookup app that shows, looks up, adds, and updates
scores, and handles unknown students without crashing.

## What you will learn

- Arrays, lists, and dictionaries: finding a value by its **index** vs. by its **key**
- Key-value pairs: a student's name is the key, the score is the value
- Creating a dictionary: `Dictionary<string, int> scores = new();`
- Adding entries with `Add`, and with square brackets
- `Add` vs. square brackets: `Add` throws for a key that already exists, square brackets add **or** update
- Reading a value by its key, and the `KeyNotFoundException` for a missing key
- The safe way: `TryGetValue` (`true` or `false`, never a crash)
- Updating a value: the key stays, only the value changes
- `ContainsKey`: it checks the **keys**, not the values
- Looping through every key-value pair with `foreach` and `KeyValuePair`, and `Count`

## What is in this folder

| Folder | What it is |
|---|---|
| `StudentLookup/` | The menu app built in the video |
| `StudentLookup.Tests/` | Automated tests (xUnit): dictionary behaviour, and the real app driven with typed answers |
| `ChallengeSolution/` | A price lookup: three products and their prices, looked up by name |

## Step by step

### Step 1 — Create the dictionary and add three students

```csharp
Dictionary<string, int> scores = new();
scores.Add("Ali", 85);
scores.Add("Sara", 92);
scores.Add("Tom", 78);
```

- `Dictionary<string, int>`: the **keys** are `string` (names), the **values** are `int` (scores).
- `new()` creates it, empty. `scores.Count` is `0` at this point.
- `Add` puts one key-value pair in. Each key can be in the dictionary only **once**.
- The video also adds with square brackets: `scores["Tom"] = 78;`. Square brackets add a key that is new, and
  update a key that already exists.

### Step 2 — Show every student: `Count` and `foreach`

```csharp
    if (choice == "1")
    {
        Console.WriteLine($"Students: {scores.Count}");
        foreach (KeyValuePair<string, int> entry in scores)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }
```

- `scores.Count` is the number of key-value pairs.
- Each `entry` is a `KeyValuePair<string, int>`: `entry.Key` is the name, `entry.Value` is the score.

### Step 3 — Look up a student safely: `TryGetValue`

```csharp
    else if (choice == "2")
    {
        Console.Write("Name: ");
        string name = (Console.ReadLine() ?? "").Trim();
        if (name == "")
        {
            Console.WriteLine("Please type a name.");
        }
        else if (scores.TryGetValue(name, out int score))
        {
            Console.WriteLine($"{name} scored {score}.");
        }
        else
        {
            Console.WriteLine($"No student named {name}.");
        }
    }
```

- `scores["Zara"]` would **crash** with a `KeyNotFoundException` when there is no key `Zara`.
- `scores.TryGetValue(name, out int score)` returns `true` or `false`. When it is `true`, it also puts the value into
  `score`. When it is `false`, nothing crashes, and we show a clear message.

### Step 4 — Add or update a score: `ContainsKey`

```csharp
    else if (choice == "3")
    {
        Console.Write("Name: ");
        string name = (Console.ReadLine() ?? "").Trim();
        Console.Write("Score: ");
        string scoreText = Console.ReadLine() ?? "";
        bool isNumber = int.TryParse(scoreText, out int score);
        if (name == "" || !isNumber || score < 0 || score > 100)
        {
            Console.WriteLine("Please type a name and a score 0-100.");
        }
        else if (scores.ContainsKey(name))
        {
            int old = scores[name];
            scores[name] = score;
            Console.WriteLine($"Updated {name}: {old} -> {score}");
        }
        else
        {
            scores.Add(name, score);
            Console.WriteLine($"Added {name}: {score}");
        }
    }
```

- `int.TryParse` checks that the score is a whole number. An empty name, or a score that isn't from 0 to 100, changes
  nothing.
- `scores.ContainsKey(name)` is `true` when the **key** is already there. Then it's an update: we keep the old score,
  replace it with square brackets, and show both. The key stays, only the value changes.
- Otherwise it's a new student, so `Add` is safe: we already checked the key.

## Common mistakes (shown in the video)

| Mistake | What happens |
|---|---|
| Reading a key that isn't there: `scores["Zara"]` | `KeyNotFoundException`: The given key 'Zara' was not present in the dictionary. Use `TryGetValue` |
| `Add` with a key that already exists: `scores.Add("Ali", 70)` | `ArgumentException`: An item with the same key has already been added. Use square brackets to update |
| Looking for a value with `ContainsKey`: `scores.ContainsKey(92)` | Build error **CS1503**: cannot convert from 'int' to 'string'. `ContainsKey` checks keys, not values |
| Expecting the dictionary to be sorted | It isn't: `Tom, Ali, Sara` stays in that order. A `SortedDictionary` gives `Ali, Sara, Tom` |

## Run it

```bash
cd StudentLookup
dotnet run
```

Real run: show all students, look up Sara, look up Zara (not there), exit:

```text
===== STUDENT LOOKUP =====
1. Show all students
2. Look up a student
3. Add or update a score
4. Exit
Choose 1-4: 1

Students: 3
Ali: 85
Sara: 92
Tom: 78

===== STUDENT LOOKUP =====
1. Show all students
2. Look up a student
3. Add or update a score
4. Exit
Choose 1-4: 2

Name: Sara
Sara scored 92.

===== STUDENT LOOKUP =====
1. Show all students
2. Look up a student
3. Add or update a score
4. Exit
Choose 1-4: 2

Name: Zara
No student named Zara.

===== STUDENT LOOKUP =====
1. Show all students
2. Look up a student
3. Add or update a score
4. Exit
Choose 1-4: 4

Goodbye!
```

Real run: add Mia, update Tom from 78 to 81, try a score that isn't a number, show all students, look up Mia, exit:

```text
===== STUDENT LOOKUP =====
1. Show all students
2. Look up a student
3. Add or update a score
4. Exit
Choose 1-4: 3

Name: Mia
Score: 88
Added Mia: 88

===== STUDENT LOOKUP =====
1. Show all students
2. Look up a student
3. Add or update a score
4. Exit
Choose 1-4: 3

Name: Tom
Score: 81
Updated Tom: 78 -> 81

===== STUDENT LOOKUP =====
1. Show all students
2. Look up a student
3. Add or update a score
4. Exit
Choose 1-4: 3

Name: Ben
Score: abc
Please type a name and a score 0-100.

===== STUDENT LOOKUP =====
1. Show all students
2. Look up a student
3. Add or update a score
4. Exit
Choose 1-4: 1

Students: 4
Ali: 85
Sara: 92
Tom: 81
Mia: 88

===== STUDENT LOOKUP =====
1. Show all students
2. Look up a student
3. Add or update a score
4. Exit
Choose 1-4: 2

Name: Mia
Mia scored 88.

===== STUDENT LOOKUP =====
1. Show all students
2. Look up a student
3. Add or update a score
4. Exit
Choose 1-4: 4

Goodbye!
```

## Run the tests

```bash
cd StudentLookup.Tests
dotnet test
```

The tests check how a dictionary behaves (including the two exceptions above), then build the real app, type answers
into it, and check its answers. All 25 tests pass.

## Your challenge

Create a dictionary of three products and their prices, then look up one product by name with `TryGetValue`. Test a
product that exists, and one that does not.

### Solution: `ChallengeSolution/`

```csharp
Dictionary<string, decimal> prices = new();
prices.Add("Bread", 2.50m);
prices.Add("Milk", 1.20m);
prices.Add("Apples", 3.10m);

Console.Write("Product: ");
string name = (Console.ReadLine() ?? "").Trim();
if (prices.TryGetValue(name, out decimal price))
{
    Console.WriteLine($"{name} costs {price:0.00}.");
}
else
{
    Console.WriteLine($"No product named {name}.");
}
```

- The prices are `decimal`, so the dictionary is `Dictionary<string, decimal>`, and each price ends with `m`.
- `{price:0.00}` always shows two decimal places: `1.20`, not `1.2`.

Real runs, a product that exists and one that does not:

```text
Product: Milk
Milk costs 1.20.
```

```text
Product: Pizza
No product named Pizza.
```

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 15](../Episode-15-Lists/) · [All episodes](../README.md) · [Episode 17 ➡](../Episode-17-Classes-and-Objects/)
