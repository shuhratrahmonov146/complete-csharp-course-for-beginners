# Episode 15 — C# Lists Explained for Beginners — Add, Remove, and Manage Items

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

▶ **Watch the video:** https://www.youtube.com/watch?v=kG0GMseC7I8

[⬅ Episode 14](../Episode-14-Arrays/) · [All episodes](../README.md) · [Episode 16 ➡](../Episode-16-Dictionaries/)

Your shopping list changes every day: you add items, buy them, and cross them off. An array can't do that, because
its length is fixed. In this video you learn **lists**: a collection that grows and shrinks. We build a Shopping List
Manager that adds, shows, and removes items, and handles an empty list and missing items with clear messages
instead of crashing.

## What you will learn

- Arrays vs. lists: a fixed length vs. a collection that grows and shrinks
- Creating a list of strings: `List<string> items = new List<string>();`, or the shorter `new()`
- Adding items with `Add` (always at the end)
- Reading items by index (the first index is 0)
- `Remove`: it returns `true` when the item was removed and `false` when it was not found
- `Remove` vs. `RemoveAt` (by value vs. by index)
- `Count` (lists) vs. `Length` (arrays), and `Contains` (`true` or `false`)
- Looping through every item with `foreach`
- Handling an empty list and a missing item with clear messages

## What is in this folder

| Folder | What it is |
|---|---|
| `ShoppingListManager/` | The menu app built in the video |
| `ShoppingListManager.Tests/` | Automated tests (xUnit): list behaviour, and the real app driven with typed answers |
| `ChallengeSolution/` | The app with option 4: Check an item |

## Step by step

### Step 1 — Create an empty list, and a menu loop

```csharp
List<string> items = new();
bool running = true;

while (running)
{
    Console.WriteLine("===== SHOPPING LIST MANAGER =====");
    Console.WriteLine("1. Show list");
    Console.WriteLine("2. Add an item");
    Console.WriteLine("3. Remove an item");
    Console.WriteLine("4. Exit");
    Console.Write("Choose 1-4: ");
    string choice = (Console.ReadLine() ?? "4").Trim();
```

- `List<string>` is a list of texts. `new()` creates it, empty.
- The menu repeats in a `while (running)` loop, like the Student Menu in Episode 11.
- The menu uses `if`/`else if`. It could also use `switch` (Episode 10); both work.

### Step 2 — Show the list: `Count`, `foreach`, and an index

```csharp
    if (choice == "1")
    {
        if (items.Count == 0)
        {
            Console.WriteLine("Your list is empty.");
        }
        else
        {
            Console.WriteLine($"Items on your list: {items.Count}");
            foreach (string item in items)
            {
                Console.WriteLine($"- {item}");
            }
            int last = items.Count - 1;
            Console.WriteLine($"Last added: {items[last]}");
        }
    }
```

- `items.Count` is the number of items (an array uses `Length` instead).
- An empty list is checked **first**: `items[0]` on an empty list would crash.
- `foreach (string item in items)` visits every item, from first to last.
- The last item has the index `Count - 1`, just like the last element of an array.

### Step 3 — Add an item: `Contains` and `Add`

```csharp
    else if (choice == "2")
    {
        Console.Write("Item to add: ");
        string name = (Console.ReadLine() ?? "").Trim();
        if (name == "")
        {
            Console.WriteLine("Please type an item name.");
        }
        else if (items.Contains(name))
        {
            Console.WriteLine($"{name} is already on the list.");
        }
        else
        {
            items.Add(name);
            Console.WriteLine($"Added {name}.");
        }
    }
```

- Empty names are rejected.
- `items.Contains(name)` is `true` when the item is already on the list, so there are no duplicates.
- `items.Add(name)` puts the item at the end. The list grows by one.

### Step 4 — Remove an item: `Remove` returns `true` or `false`

```csharp
    else if (choice == "3")
    {
        if (items.Count == 0)
        {
            Console.WriteLine("Nothing to remove. The list is empty.");
        }
        else
        {
            Console.Write("Item to remove: ");
            string name = (Console.ReadLine() ?? "").Trim();
            if (items.Remove(name))
            {
                Console.WriteLine($"Removed {name}.");
            }
            else
            {
                Console.WriteLine($"{name} is not on the list.");
            }
        }
    }
```

`items.Remove(name)` removes the item **and** tells you whether it worked. If the item isn't on the list, it returns
`false` instead of crashing, so we can show a clear message.

`Remove("Milk")` removes by **value**. `RemoveAt(0)` removes by **index** (the first item).

## Common mistakes (shown in the video)

| Mistake | What happens |
|---|---|
| Calling `Add` on an array: `scores.Add(70)` | Build error **CS1061**: arrays have a fixed length and no `Add` |
| An index that doesn't exist, like `items[3]` in a list of 3 | `ArgumentOutOfRangeException` |
| `items[0]` on an empty list | `ArgumentOutOfRangeException`: check `items.Count` first |

## Run it

```bash
cd ShoppingListManager
dotnet run
```

Real run: remove from an empty list, add Milk and Bread, try Milk again, show, remove Pizza (not there), remove Milk,
show, exit:

```text
===== SHOPPING LIST MANAGER =====
1. Show list
2. Add an item
3. Remove an item
4. Exit
Choose 1-4: 3

Nothing to remove. The list is empty.

===== SHOPPING LIST MANAGER =====
1. Show list
2. Add an item
3. Remove an item
4. Exit
Choose 1-4: 2

Item to add: Milk
Added Milk.

===== SHOPPING LIST MANAGER =====
1. Show list
2. Add an item
3. Remove an item
4. Exit
Choose 1-4: 2

Item to add: Bread
Added Bread.

===== SHOPPING LIST MANAGER =====
1. Show list
2. Add an item
3. Remove an item
4. Exit
Choose 1-4: 2

Item to add: Milk
Milk is already on the list.

===== SHOPPING LIST MANAGER =====
1. Show list
2. Add an item
3. Remove an item
4. Exit
Choose 1-4: 1

Items on your list: 2
- Milk
- Bread
Last added: Bread

===== SHOPPING LIST MANAGER =====
1. Show list
2. Add an item
3. Remove an item
4. Exit
Choose 1-4: 3

Item to remove: Pizza
Pizza is not on the list.

===== SHOPPING LIST MANAGER =====
1. Show list
2. Add an item
3. Remove an item
4. Exit
Choose 1-4: 3

Item to remove: Milk
Removed Milk.

===== SHOPPING LIST MANAGER =====
1. Show list
2. Add an item
3. Remove an item
4. Exit
Choose 1-4: 1

Items on your list: 1
- Bread
Last added: Bread

===== SHOPPING LIST MANAGER =====
1. Show list
2. Add an item
3. Remove an item
4. Exit
Choose 1-4: 4

Goodbye!
```

## Run the tests

```bash
cd ShoppingListManager.Tests
dotnet test
```

The tests build the real app, type answers into it, and check its answers. All 18 tests pass.

## Your challenge

Add option 4, **Check an item**, using `Contains`, and move Exit to option 5. Test it with an item that is on the list
and one that is not.

### Solution: `ChallengeSolution/`

```csharp
    else if (choice == "4")
    {
        Console.Write("Item to check: ");
        string name = (Console.ReadLine() ?? "").Trim();
        if (items.Contains(name))
        {
            Console.WriteLine($"Yes, {name} is on the list.");
        }
        else
        {
            Console.WriteLine($"No, {name} is not on the list.");
        }
    }
```

Real run:

```text
===== SHOPPING LIST MANAGER =====
1. Show list
2. Add an item
3. Remove an item
4. Check an item
5. Exit
Choose 1-5: 2

Item to add: Milk
Added Milk.

===== SHOPPING LIST MANAGER =====
1. Show list
2. Add an item
3. Remove an item
4. Check an item
5. Exit
Choose 1-5: 4

Item to check: Milk
Yes, Milk is on the list.

===== SHOPPING LIST MANAGER =====
1. Show list
2. Add an item
3. Remove an item
4. Check an item
5. Exit
Choose 1-5: 4

Item to check: Pizza
No, Pizza is not on the list.

===== SHOPPING LIST MANAGER =====
1. Show list
2. Add an item
3. Remove an item
4. Check an item
5. Exit
Choose 1-5: 5

Goodbye!
```

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

[⬅ Episode 14](../Episode-14-Arrays/) · [All episodes](../README.md) · [Episode 16 ➡](../Episode-16-Dictionaries/)
