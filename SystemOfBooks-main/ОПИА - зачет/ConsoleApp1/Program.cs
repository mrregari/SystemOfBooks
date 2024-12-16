namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        LibrarySystem librarySystem = new LibrarySystem();
        librarySystem.LoadBooks();

        while (true)
        {
            Console.WriteLine("1. Просмотр книг");
            Console.WriteLine("2. Добавить книгу");
            Console.WriteLine("3. Генерация отчета");
            Console.WriteLine("4. Выход");

            var choice = Console.ReadLine();

            switch (choice)
            {
            case "1":
                librarySystem.ViewBooks();
                break;
            case "2":
                    Console.Write("Введите ID книги: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Введите название книги: ");
                    string title = Console.ReadLine();
                    Console.Write("Введите автора книги: ");
                    string author = Console.ReadLine();
                    Console.Write("Введите жанр книги: ");
                    string genre = Console.ReadLine();
                    Console.Write("Введите издателя книги: ");
                    string publisher = Console.ReadLine();
                    Console.Write("Введите ISBN книги: ");
                    string isbn = Console.ReadLine();

                    librarySystem.AddBook(new Book(id, title, author, genre, publisher, isbn));
                    Console.WriteLine("Книга добавлена.");
                    librarySystem.LoadBooks();
                    break;
            case "3":
                // Код для генерации отчета
                break;
            case "4":
                return;
            default:
                Console.WriteLine("Неверный выбор. Попробуйте снова.");
                break;
            }
        }
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
    public override string ToString()
    {
        return $"ID: {Id}, Title: {Title}, Author: {Author}, Genre: {Genre}, Publisher: {Publisher}, ISBN: {ISBN}";
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

public class LibrarySystem
{
    private List<Book> books = new List<Book>();
    private List<Reader> readers = new List<Reader>();
    private List<LoanRecord> loanRecords = new List<LoanRecord>();
    public void LoadBooks()
    {
        if (File.Exists(@"..\Books\books.txt"))
        {
        try
        {
            using (StreamReader bookReader = new StreamReader(@"..\Books\books.txt"))
            {
                string line;
                while ((line = bookReader.ReadLine()) != null)
                {
                    var parts = line.Split('#');
                    if (parts.Length == 6) // Убедитесь, что у нас есть все части
                    {
                        int id = int.Parse(parts[0]);
                        string title = parts[1];
                        string author = parts[2];
                        string genre = parts[3];
                        string publisher = parts[4];
                        string isbn = parts[5];

                        books.Add(new Book(id, title, author, genre, publisher, isbn));
                    }
                }
            }

            // Выводим все книги
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        }
    }

    public void ViewBooks()
    {
        FileInfo filebook = new FileInfo(@"..\Books\books.txt");
        Console.WriteLine("Список книг: \n");
        if (filebook.Exists)
        {
            using(StreamReader bookreader = new StreamReader(filebook.FullName))
            {
                Console.WriteLine(bookreader.ReadToEnd());
            }
        }
    }

    public void SaveBooks()
    {
        var lines = ($"{books.Id}#{b.Title}#{b.Author}#{b.Genre}#{b.Publisher}#{b.ISBN}");
        using(StreamWriter bookwriter = new StreamWriter(@"..\Books\books.txt")){
            bookwriter.WriteLine(lines);
        }
    }
    
    public void SaveReaders()
    {
        var lines = readers.Select(r => $"{r.Id}#{r.FullName}#{r.Age}#{r.PhoneNumber}#{r.Email}");
        File.WriteAllLines(@"Readers\readers.txt", lines);
    }
   
    public void SaveLoanRecords()
    {
        var lines = loanRecords.Select(l => $"{l.ReaderId}#{l.BookId}#{l.ISBN}#{l.EmployeeId}#{l.IsReturn}#{l.Date}");
        File.WriteAllLines(@"mrregari/SystemOfBooks/LoanRecords/loanrecords.txt", lines);
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

    public void AddLoanRecord(LoanRecord loanRecord)
    {
        loanRecords.Add(loanRecord);
        SaveLoanRecords();
    }

}