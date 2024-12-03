using System.Runtime.CompilerServices;

internal class ParamsExpansionDemo
{
    [OverloadResolutionPriority(1)]
    public static void PrintNumbersCount(params int[] numbers)
        => Console.WriteLine(numbers.Length);

    public static void PrintNumbersCount(params IEnumerable<int> numbers)
        => Console.WriteLine(numbers.Count());

    [OverloadResolutionPriority(2)]
    public static void PrintNumbersCount(params List<int> numbers)
        => Console.WriteLine(numbers.Count());

    public static void PrintItemsCount<T>(params T[] items)
        => Console.WriteLine(items.Length);

    public static void Demonstrate()
    {
        PrintNumbersCount(new int[] { 1, 2, 3, 4, 5 });

        PrintNumbersCount(1, 2, 3, 4, 5);

        PrintItemsCount(["apple", "banana", "cherry"]);
    }
}