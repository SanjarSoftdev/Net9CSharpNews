internal class NewLINQMethods
{


    public static void LinkMethods()
    {
        (string Name, string Dept, int Salary)[] employees = [
            ("Mary", "Accounting", 80000 ),
            ("Jean", "Accounting", 140000 ),
            ("Bill", "Accounting", 90000 ),
            ("Suzy", "IT", 125000 ),
            ("Mike", "IT", 160000 )
        ];

        var departmentCounts = employees
            .GroupBy(p => p.Dept)
            .ToDictionary(group => group.Key, group => group.Count())
            .AsEnumerable();

        Console.WriteLine();
        foreach (var group in departmentCounts)
            Console.WriteLine($"Department {group.Key} appears {group.Value} times");

        departmentCounts = employees.CountBy(p => p.Dept);

        Console.WriteLine();
        foreach (var group in departmentCounts)
            Console.WriteLine($"Department {group.Key} appears {group.Value} times");

        // Before
        var totalSalaryByDept = employees
            .GroupBy(emp => emp.Dept)
            .ToDictionary(group => group.Key, group => group.Sum(emp => emp.Salary))
            .AsEnumerable();

        Console.WriteLine();
        foreach (var group in totalSalaryByDept)
            Console.WriteLine($"Department: {group.Key}, Total Salary: {group.Value}");

        // In C# 13
        var totalSalaryByDept1 = employees.AggregateBy(emp => emp.Dept, 0, (total, emp) => total + emp.Salary);
        Console.WriteLine();
        foreach (var group in totalSalaryByDept1)
            Console.WriteLine($"Department: {group.Key}, Total Salary: {group.Value}");

        // Before
        var employeesWithIndex = employees.Select((emp, i) => (i, emp));
        Console.WriteLine();
        foreach (var (index, employee) in employeesWithIndex)
            Console.WriteLine($"Employee {index}: {employee}");

        // In C# 13
        employeesWithIndex = employees.Index();
        Console.WriteLine();
        foreach (var (index, employee) in employeesWithIndex)
            Console.WriteLine($"Employee {index}: {employee}");
    }
}