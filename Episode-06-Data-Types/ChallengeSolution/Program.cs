Console.WriteLine("================================");
Console.WriteLine("  Welcome to Student Profile App");
Console.WriteLine("================================");
Console.WriteLine();

// Typed by the user
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

// Set by the program
int completedLessons = 4;
double averageScore = 87.5;
decimal courseFee = 249.99m;
string subjectName = "Computer Science";
bool isEnrolled = true;
// Calculated by the program
bool passed = averageScore >= 60;

Console.WriteLine();
Console.WriteLine("========== STUDENT REPORT ==========");
Console.WriteLine($"Name: {studentName}");
Console.WriteLine($"Age: {ageText}");
Console.WriteLine($"Course: {courseName}");
Console.WriteLine($"Average score: {averageScore}");
Console.WriteLine($"Course fee: {courseFee}");
Console.WriteLine($"Passed: {passed}");
Console.WriteLine($"Subject: {subjectName}");
Console.WriteLine($"Enrolled: {isEnrolled}");
Console.WriteLine($"Lessons completed: {completedLessons}");

completedLessons = completedLessons + 1;
Console.WriteLine($"After this lesson: {completedLessons}");
Console.WriteLine();
Console.WriteLine($"Welcome, {studentName}!");
Console.WriteLine("Keep learning and building projects!");
