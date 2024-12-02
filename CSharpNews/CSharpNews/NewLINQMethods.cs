﻿internal class NewLINQMethods
{
    static (string fullName, string department, int vacationDaysLeft)[] employees =
    [
        ("John Doe", "IT", 12),
        ("Jane Peterson", "Marketing", 18),
        ("John Smith", "IT", 28),
        ("Mary Johnson", "HR", 17),
        ("Nick Carson", "Marketing", 5),
        ("Mary Morgan", "HR", 9)
    ];

    public static void CountBy()
    {
        var departmentCounts = employees
            .GroupBy(p => p.Item2)
            .ToDictionary(group => group.Key, group => group.Count())
            .AsEnumerable();

        Console.WriteLine();
        foreach (var entry in departmentCounts)
            Console.WriteLine($"Department {entry.Key} appears {entry.Value} times");

        departmentCounts = employees.CountBy(p => p.Item2);

        Console.WriteLine();
        foreach (var entry in departmentCounts)
            Console.WriteLine($"Department {entry.Key} appears {entry.Value} times");
    }

    public static void AggregateBy()
    {
        // Before
        var departmentVacationDaysLeft = employees
            .GroupBy(emp => emp.Item2)
            .ToDictionary(group => group.Key, group => group.Sum(emp => emp.Item3))
            .AsEnumerable();

        Console.WriteLine();
        foreach (var entry in departmentVacationDaysLeft)
            Console.WriteLine($"Department {entry.Key} has a total of {entry.Value} vacation days left");

        // In C# 13
        departmentVacationDaysLeft = employees.AggregateBy(emp => emp.Item2, 0, (acc, emp) => acc + emp.Item3);
        Console.WriteLine();
        foreach (var entry in departmentVacationDaysLeft)
            Console.WriteLine($"Department {entry.Key} has a total of {entry.Value} vacation days left");
    }

    public static void Index()
    {
        // Before
        var indexedEmployees = employees.Select((m, i) => (i, m));
        Console.WriteLine();
        foreach (var (index, employee) in indexedEmployees)
            Console.WriteLine($"Employee {index}: {employee}");

        // In C# 13
        indexedEmployees = employees.Index();
        Console.WriteLine();
        foreach (var (index, employee) in indexedEmployees)
            Console.WriteLine($"Employee {index}: {employee}");
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