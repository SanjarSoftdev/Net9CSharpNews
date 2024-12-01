public class Employee
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
        var person = new Employee();
        person.Name = "John Doe";
        person.Age = 30;

        Console.WriteLine($"Name: {person.Name}");
        Console.WriteLine($"Age: {person.Age}");
    }
}