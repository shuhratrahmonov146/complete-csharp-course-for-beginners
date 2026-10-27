Console.WriteLine("===== STUDENT RECORDS =====");
Console.WriteLine();

Student student1 = new Student("Aisha", 15);
student1.Grade = 10;

Student student2 = new Student("Daniel", 16);
student2.Grade = 11;

Student student3 = new Student("Maria", 14);
student3.Grade = 9;

student1.Introduce();
Console.WriteLine();
student2.Introduce();
Console.WriteLine();
student3.Introduce();
