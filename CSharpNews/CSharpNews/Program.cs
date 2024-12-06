using System.Reflection;
using System.Runtime.CompilerServices;

class Program
{
    static void Main()
    {
        Printer.Demonstrate();

        //ParamsExpansionDemo.Demonstrate();

        //NewLINQMethods.Demonstrate();

        //FieldKeyword.Demonstrate();

        //NewLockObject.Demonstrate();

        //PartialProperties.Demonstrate();
    }
}


public static class Printer
{
    public static void Print(string text)
        => Console.WriteLine($"Printing string: {text}");

    [OverloadResolutionPriority(1)]
    public static void Print(ReadOnlySpan<char> text)
        => Console.WriteLine($"Printing ReadOnlySpan: {text}");

    public static int Sum(int x, int y)
           => (x + y) * 10;

    [OverloadResolutionPriority(1)]
    public static int Sum(params int[] numbers)
        => numbers.Sum();

    public static void Demonstrate()
    {
        Print("Hello, World!"); // Printing ReadOnlySpan: Hello, World!
        Sum(10, 20);            // 30
    }
}