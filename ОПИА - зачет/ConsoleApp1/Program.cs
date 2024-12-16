namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {

    }
}
public class Book
{
    public int Id { get; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public string Publisher { get; set; }
    public string ISBN { get; set; }

    public Book(int id, string title, string author, string genre, string publisher, string isbn)
    {
        Id = id;
        Title = title;
        Author = author;
        Genre = genre;
        Publisher = publisher;
        ISBN = isbn;
    }
}

public class Reader
{
    public int Id { get; }
    public string FullName { get; set; }
    public int Age { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public Reader(int id, string fullName, int age, string phoneNumber, string email)
    {
        Id = id;
        FullName = fullName;
        Age = age;
        PhoneNumber = phoneNumber;
        Email = email;
    }
}

public class LoanRecord
{
    public int ReaderId { get; set; }
    public int BookId { get; set; }
    public string ISBN { get; set; }
    public int EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public bool IsReturn { get; set; }

    public LoanRecord(int readerId, int bookId, string isbn, int employeeId, bool isReturn)
    {
        ReaderId = readerId;
        BookId = bookId;
        ISBN = isbn;
        EmployeeId = employeeId;
        IsReturn = isReturn;
        Date = DateTime.Now;
    }
}

public class LiberySystem
{
    private List<Book> books = new List<Book>();
    private List<Reader> readers = new List<Reader>();
    private List<LoanRecord> loanRecords = new List<LoanRecord>();
    public void LoadBooks()
    {
        var lines = File.ReadAllLines("books.txt");
        foreach (var line in lines)
        {
            var parts = line.Split('#');
            books.Add(new Book(int.Parse(parts[0]), parts[1], parts[2], parts[3], parts[4], parts[5]));
        }
    }

    public void SaveBooks()
    {
        var lines = books.Select(b => $"{b.Id}#{b.Title}#{b.Author}#{b.Genre}#{b.Publisher}#{b.ISBN}");
        File.WriteLine(@"mrregari/SystemOfBooks/Books/books.txt", lines);
    }
    
    public void SaveReaders()
    {
        var lines = readers.Select(r => $"{r.Id}#{r.FullName}#{r.Age}#{r.PhoneNumber}#{r.Email}");
        File.WriteLine(@"mrregari/SystemOfBooks/Readers/readers.txt", lines);
    }
    
    public void SaveLoanRecords()
    {
        var lines = LoanRecord.Select(l => $"{l.ReaderId}#{l.BookId}#{l.ISBN}#{l.EmployeeId}#{l.IsReturn}#{l.Date}");
        File.WriteLine(@"mrregari/SystemOfBooks/LoanRecords/loanrecords.txt", lines);
    }
        
    public void AddBook(Book book)
    {
        books.Add(book);
        SaveBooks();
    }
    
    public void AddReader(Reader reader)
    {
        readers.Add(reader);
        SaveReaders();
    }

    public void AddLoanRecord
}