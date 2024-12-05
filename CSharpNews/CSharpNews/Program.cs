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
    [OverloadResolutionPriority(1)]
    public static void Print(string text)
        => Console.WriteLine($"Printing string: {text}");

    [OverloadResolutionPriority(2)]
    public static void Print(ReadOnlySpan<char> text)
        => Console.WriteLine($"Printing ReadOnlySpan: {text}");

    [OverloadResolutionPriority(1)]
    public static void PrintNumbersCount(params int[] numbers)
        => Console.WriteLine(numbers.Length);

    [OverloadResolutionPriority(2)]
    public static void PrintNumbersCount(params List<int> numbers)
        => Console.WriteLine(numbers.Count());

    public static void Demonstrate()
    {
        Print("Hello, World!");

        PrintNumbersCount(1, 2, 3, 4);
    }
}
