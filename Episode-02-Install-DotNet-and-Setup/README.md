# Episode 02 — Install .NET and Set Up Your C# Development Environment

**Playlist:** Complete C# Course for Beginners · **Channel:** [ITHubCoding](https://www.youtube.com/@ithubcoding)

⬅ Episode 01 (same Hello World project as this episode) · [All episodes](../README.md) · [Episode 03 ➡](../Episode-03-Understand-Your-First-Project/)

In this video you install the .NET SDK, check that it works, choose an editor, and run your first C# project.
At the end, your own computer prints `Hello, World!`.

## What you will learn

- The difference between C#, the .NET SDK, the .NET runtime, and an editor
- How to install the .NET 10 SDK (LTS) on Windows, macOS, or Linux
- How to check the installation: `dotnet --version`, `dotnet --list-sdks`, `dotnet --list-runtimes`
- Visual Studio vs. Visual Studio Code
- How to create a project with `dotnet new console` and run it with `dotnet run`
- How to fix 5 common problems

## What is in this folder

| Folder | What it is |
|---|---|
| `FirstCSharpProgram/` | The console app created in the video with `dotnet new console` |

## Step by step

### Step 1 — Install the .NET SDK

Download the **.NET 10 SDK** from the official page: https://dotnet.microsoft.com/download

- **Windows:** run the installer, or use `winget install Microsoft.DotNet.SDK.10`
- **Ubuntu:** `sudo apt-get install -y dotnet-sdk-10.0`
- **macOS:** run the installer from the download page

The **SDK** is what you need to *build* programs. It already contains the **runtime**, which *runs* them.

### Step 2 — Check the installation

Open a **new** terminal and type:

```bash
dotnet --version
dotnet --list-sdks
dotnet --list-runtimes
```

- `dotnet --version` prints the SDK version. For this course it should start with `10`.
- `dotnet --list-sdks` lists every installed SDK. If it prints nothing, no SDK is installed.
- `dotnet --list-runtimes` lists the runtimes. `Microsoft.NETCore.App` is the one console apps use.

### Step 3 — Create the project

Go to the folder where you keep your projects, and run:

```bash
dotnet new console -n FirstCSharpProgram
cd FirstCSharpProgram
```

- `dotnet new console` creates a new console app from a template.
- `-n FirstCSharpProgram` gives it a name. The SDK creates a folder with that name.
- `cd FirstCSharpProgram` moves the terminal into the new folder, because `dotnet run` looks for a project file in the current folder.

### Step 4 — Look at the code

This is the whole program, `FirstCSharpProgram/Program.cs`:

```csharp
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
```

- The first line starts with `//`, so it is a **comment**. The computer ignores it.
- `Console.WriteLine("Hello, World!");` prints the text between the quotes, and then a new line.

### Step 5 — Run it

```bash
dotnet run
```

Real output:

```text
Hello, World!
```

`dotnet run` is the **command** (you type it). `Hello, World!` is the **output** (the program prints it).

## Common problems and fixes

| Problem | What you see | Fix |
|---|---|---|
| `dotnet` is not found | `dotnet: command not found` (Windows: "not recognized") | Install the SDK, then close and reopen the terminal |
| Wrong folder | `Couldn't find a project to run.` | `cd` into the folder that contains the `.csproj` file, or use `dotnet run --project FirstCSharpProgram` |
| Project already exists | `Creating this template will make changes to existing files` | Choose a new name or an empty folder. Don't use `--force` unless you really want to overwrite your files |
| SDK too old for the project | `error NETSDK1045: The current .NET SDK does not support targeting .NET …` | Install the SDK version the project needs (here: .NET 10) |
| The editor can't find the SDK | Errors in the editor although `dotnet --version` works | Restart the editor after installing the SDK |

Useful terminal commands: `pwd` (where am I?), `ls` (list files; `dir` in Command Prompt), `cd FolderName` (go into a folder), `cd ..` (go up one level).

## Practice challenge

1. Run `dotnet --version` and `dotnet --list-sdks`.
2. Open your `FirstCSharpProgram` project in your editor.
3. Open a terminal in the project folder and run `dotnet run`.
4. Confirm that the program prints `Hello, World!`.

**Solution:** there is no code to write in this challenge. If you see `Hello, World!` (Step 5), your setup is complete.
If not, compare your error with the table above.

## Video description

The full YouTube description of this video (chapters, official links, notes) is in [video-description.txt](video-description.txt).

---

⬅ Episode 01 (same Hello World project as this episode) · [All episodes](../README.md) · [Episode 03 ➡](../Episode-03-Understand-Your-First-Project/)
