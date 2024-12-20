﻿public static class FieldKeyword
{
    public static void Demonstrate()
    {
        var person = new Employee();
        person.Name = "John Doe";
        person.Age = 30;

        Console.WriteLine($"Name: {person.Name}");
        Console.WriteLine($"Age: {person.Age}");
    }
}

public class Employee
{
    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty or null");
            _name = value;
        }
    }

    public int Age { get; set; }

    //public int Age
    //{
    //    get => field;
    //    set
    //    {
    //        if (value < 0)
    //            throw new ArgumentOutOfRangeException("The value must not be negative");
    //        field = value;
    //    }
    //}
}