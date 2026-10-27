SayHello();
GreetPlayer("Sam");
GreetPlayer("Lena");

int result = Square(4);
Console.WriteLine($"Square(4) returned {result}");

static void SayHello()
{
    Console.WriteLine("Hello from a method!");
}

static void GreetPlayer(string name)
{
    Console.WriteLine($"Welcome to the game, {name}!");
}

static int Square(int number)
{
    return number * number;
}
