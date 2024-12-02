using System.Runtime.CompilerServices;

internal class ParamsExpansionDemo
{
    // Before C# 13
    public static void WriteNumbersCount(params int[] numbers)
        => Console.WriteLine(numbers.Length);

    // In C# 13
    [OverloadResolutionPriority(1)]
    public static void WriteNumbersCount(params IEnumerable<int> numbers) =>
        Console.WriteLine(numbers.Count());

    [OverloadResolutionPriority(2)]
    public static void WriteNumbersCount(params List<int> numbers) =>
        Console.WriteLine(numbers.Count());

    public static void WriteItemsCount<T>(params T[] items) =>
        Console.WriteLine(items.Length);

    public static void Demonstrate()
    {
        List<int> numbersList = new() { 1, 2, 3, 4, 5 };
        WriteNumbersCount(numbersList);

        string[] items = { "apple", "banana", "cherry" };
        WriteItemsCount(items);
    }
}