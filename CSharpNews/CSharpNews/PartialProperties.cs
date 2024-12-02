
public static class PartialProperties
{
    public static void Demonstrate()
    {
        Library library = new Library();
        library.LibraryName = "City Library";

        library.AddBook("The Great Gatsby");
        library.AddBook("1984");
        library.AddBook("To Kill a Mockingbird");

        Console.WriteLine($"Welcome to {library.LibraryName}!");
        library.ListBooks();

        Console.WriteLine($"Book at index 1: {library[1]}");

        library[1] = "Brave New World";
        Console.WriteLine($"Updated book at index 1: {library[1]}");

        library[3] = "Moby Dick";
        library.ListBooks();
    }
}

public partial class Library
{
    public partial string LibraryName { get; set; }
    public partial string this[int index] { get; set; }
}

public partial class Library
{
    public partial string LibraryName
    {
        get => field;
        set => field = value;
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

    public void AddBook(string book) => _books.Add(book);

    public void ListBooks()
    {
        for (int i = 0; i < _books.Count; i++)
            Console.WriteLine($"{i}: {_books[i]}");
    }
}