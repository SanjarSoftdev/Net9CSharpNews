internal class ParamsExpansionDemo
{
    public static void PrintNumbersCount(params int[] numbers)
        => Console.WriteLine(numbers.Length);

    public static void PrintNumbersCount(params IEnumerable<int> numbers)
        => Console.WriteLine(numbers.Count());

    public static void PrintItemsCount<T>(params ReadOnlySpan<T> items)
        => Console.WriteLine(items.Length);

    public static void PrintItemsCount<T>(params T[] items)
        => Console.WriteLine(items.Length);

    public static void Demonstrate()
    {
        PrintNumbersCount(new int[] { 1, 2, 3, 4, 5 });

        PrintNumbersCount(new int[] { 11, 12, 13 });
    }
}