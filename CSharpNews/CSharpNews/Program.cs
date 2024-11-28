class Program
{
    static void Main()
    {
        //ParamsExpansionDemo.DemonstrateParams();

        //NewLINQMethods.DemonstrateNewLINQMethods();

        Person.Demonstrate();
    }
}

class ParamsExpansionDemo
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

    public static void DemonstrateParams()
    {
        WriteNumbersCount(1, 2, 3, 4, 5);

        List<int> numbersList = new() { 1, 2, 3, 4, 5 };
        WriteNumbersCount(numbersList);

        string[] items = { "apple", "banana", "cherry" };
        WriteItemsCount(items);
    }
}

class NewLINQMethods
{
    public static void CountBy()
    {
        (string firstName, string lastName)[] people = new[]
        {
            ("John", "Doe"),
            ("Jane", "Peterson"),
            ("John", "Smith"),
            ("Mary", "Johnson"),
            ("Nick", "Carson"),
            ("Mary", "Morgan")
        };

        // Before
        var firstNameCounts = people
            .GroupBy(p => p.Item1)
            .ToDictionary(group => group.Key, group => group.Count())
            .AsEnumerable();

        Console.WriteLine();
        foreach (var entry in firstNameCounts)
            Console.WriteLine($"First Name {entry.Key} appears {entry.Value} times");

        // In C# 13
        firstNameCounts = people
            .CountBy(p => p.Item1);

        Console.WriteLine();
        foreach (var entry in firstNameCounts)
            Console.WriteLine($"First Name {entry.Key} appears {entry.Value} times");
    }

    public static void AggregateBy()
    {
        (string name, string department, int vacationDaysLeft)[] employees = new[]
       {
            ("John Doe", "IT", 12),
            ("Jane Peterson", "Marketing", 18),
            ("John Smith", "IT", 28),
            ("Mary Johnson", "HR", 17),
            ("Nick Carson", "Marketing", 5),
            ("Mary Morgan", "HR", 9)
        };

        // Before
        var departmentVacationDaysLeft = employees
            .GroupBy(emp => emp.Item2)
            .ToDictionary(group => group.Key, group => group.Sum(emp => emp.Item3))
            .AsEnumerable();

        Console.WriteLine();
        foreach (var entry in departmentVacationDaysLeft)
            Console.WriteLine($"Department {entry.Key} has a total of {entry.Value} vacation days left");

        // In C# 13
        departmentVacationDaysLeft = employees
            .AggregateBy(emp => emp.Item2, 0, (acc, emp) => acc + emp.Item3);

        Console.WriteLine();
        foreach (var entry in departmentVacationDaysLeft)
            Console.WriteLine($"Department {entry.Key} has a total of {entry.Value} vacation days left");
    }

    public static void Index()
    {
        var managers = new[]
        {
            "John Doe",
            "Jane Peterson",
            "John Smith"
        };

        // Before
        Console.WriteLine();
        foreach (var (index, manager) in managers.Select((m, i) => (i, m)))
            Console.WriteLine($"Manager {index}: {manager}");

        // In C# 13
        Console.WriteLine();
        foreach (var (index, manager) in managers.Index())
            Console.WriteLine($"Manager {index}: {manager}");
    }

    public static void DemonstrateNewLINQMethods()
    {
        Console.WriteLine("CountBy Method:");
        CountBy();
        Console.WriteLine();

        Console.WriteLine("AggregateBy Method:");
        AggregateBy();
        Console.WriteLine();

        Console.WriteLine("Index Method:");
        Index();
        Console.WriteLine();
    }
}

public class Person
{
    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty or null", nameof(value));
            _name = value;
        }
    }

    public int Age
    {
        get => field;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "The value must not be negative");
            field = value;
        }
    }

    public static void Demonstrate()
    {
        var person = new Person();
        person.Name = "John Doe";
        person.Age = 30;

        Console.WriteLine($"Name: {person.Name}");
        Console.WriteLine($"Age: {person.Age}");
    }
}