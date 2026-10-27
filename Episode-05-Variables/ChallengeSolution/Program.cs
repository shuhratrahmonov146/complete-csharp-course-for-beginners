Console.WriteLine("================================");
Console.WriteLine("  Welcome to Student Profile App");
Console.WriteLine("================================");
Console.WriteLine();

Console.Write("What is your name? ");
string studentName = Console.ReadLine() ?? "";
studentName = studentName.Trim();
if (studentName == "")
{
    studentName = "Student";
}

Console.Write("How old are you? ");
string ageInput = Console.ReadLine() ?? "";
string ageText = "not provided";
if (int.TryParse(ageInput, out int studentAge))
{
    ageText = $"{studentAge}";
}

Console.Write("Which course are you learning? ");
string courseName = Console.ReadLine() ?? "";
courseName = courseName.Trim();
if (courseName == "")
{
    courseName = "not chosen yet";
}

Console.Write("Favorite programming language? ");
string favoriteLanguage = Console.ReadLine() ?? "";

int completedLessons = 4;

Console.WriteLine();
Console.WriteLine("-------- STUDENT PROFILE --------");
Console.WriteLine($"Name: {studentName}");
Console.WriteLine($"Age: {ageText}");
Console.WriteLine($"Course: {courseName}");
Console.WriteLine($"Favorite language: {favoriteLanguage}");
Console.WriteLine($"Lessons completed: {completedLessons}");

completedLessons = completedLessons + 1;
Console.WriteLine($"After this lesson: {completedLessons}");
completedLessons = completedLessons + 1;
Console.WriteLine($"After the challenge: {completedLessons}");
Console.WriteLine();
Console.WriteLine($"Welcome, {studentName}!");
Console.WriteLine("Keep learning and building projects!");
