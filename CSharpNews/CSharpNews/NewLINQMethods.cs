internal class NewLINQMethods
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

    public static void Demonstrate()
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