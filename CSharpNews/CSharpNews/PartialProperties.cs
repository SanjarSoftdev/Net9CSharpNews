
public static class PartialProperties
{
    public static void Demonstrate()
    {
        Library library = new Library();
        library.Name = "City Library";

        library[0] = "The Great Gatsby";
        library[1] = "1984";
        library[2] = "To Kill a Mockingbird";

        Console.WriteLine($"Welcome to {library.Name}!");

        Console.WriteLine($"Book at index 1: {library[1]}");

        library[1] = "Brave New World";
        Console.WriteLine($"Updated book at index 1: {library[1]}");

        library[3] = "Moby Dick";

    }
}

public partial class Library
{
    public partial string Name { get; set; }
    public partial string this[int index] { get; set; }
}

public partial class Library
{
    private string _name;
    public partial string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty or null");
            _name = value;
        }
    }

    private List<string> _books = new List<string>();
    public partial string this[int index]
    {
        get => index >= 0 && index < _books.Count ? _books[index] : "Book not found";
        set
        {
            if (index >= 0 && index < _books.Count)
                _books[index] = value;
            else if (index == _books.Count)
                _books.Add(value);
        }
    }
}