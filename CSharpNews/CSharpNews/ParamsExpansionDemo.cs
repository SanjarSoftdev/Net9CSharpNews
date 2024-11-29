internal class ParamsExpansionDemo
{
    // Before C# 13, the params keyword allowed only arrays.
    public static void WriteNumbersCount(params int[] numbers)
        => Console.WriteLine(numbers.Length);

    // In C# 13, the params keyword now supports collections implementing IEnumerable<T>.
    // This allows for more flexible method calls with different collection types.
    public static void WriteNumbersCount(params IEnumerable<int> numbers) =>
        Console.WriteLine(numbers.Count());

    public static void WriteItemsCount<T>(params T[] items) =>
        Console.WriteLine(items.Length);

    public static void Demonstrate()
    {
        WriteNumbersCount(1, 2, 3, 4, 5);

        List<int> numbersList = new() { 1, 2, 3, 4, 5 };
        WriteNumbersCount(numbersList);

        string[] items = { "apple", "banana", "cherry" };
        WriteItemsCount(items);
    }
}