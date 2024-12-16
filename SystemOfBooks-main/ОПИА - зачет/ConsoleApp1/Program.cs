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
            Console.WriteLine("3. Выдать книгу");
            Console.WriteLine("4. Сохранить списки в файлы");
            Console.WriteLine("5. Выход");

            var choice = Console.ReadLine();

            switch (choice)
            {
            case "1":
                librarySystem.ViewBooks();
                break;
            case "2":
                Console.Write("Введите ID книги: ");
                int id = Convert.ToInt32(Console.ReadLine());
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
                librarySystem.AddLoanRecord();
                break;
            case "4":
                librarySystem.AddTextFiles();
                break;
            case "5":
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
    public int Id { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public Reader(int id, string fullName, string phoneNumber, string email)
    {
        Id = id;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Email = email;
    }
    public override string ToString()
    {
        return $"{Id}#{FullName}#{PhoneNumber}#{Email}";
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

public class Employee
{
    public int Id { get; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Position { get; set; }

    public Employee(int id, string fullName, string phoneNumber, string email, string position)
    {
        Id = id;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Email = email;
        Position = position;
    }

    public override string ToString()
    {
        return $"{Id}#{FullName}#{PhoneNumber}#{Email}#{Position}";
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
                        int id = Convert.ToInt16(parts[0]);
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

        
    public void AddBook(Book book)
    {
        books.Add(book);
    }
    
    public void AddReader(Reader reader)
    {
        readers.Add(reader);
    }

    public void AddLoanRecord()
    {
        Console.Write("Введите ID читателя: ");
        var readerId = Convert.ToInt32(Console.ReadLine());
        if (readers.Contains(new Reader {Id = readerId, FullName = "", PhoneNumber = "", Email = ""}))
        Console.Write("Введите название книги: ");
        int bookId = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите автора книги: ");
        string employeeId = Console.ReadLine();
        loanRecords.Add();
    }



    public void AddTextFiles(){
        var booklines = books.Select(b => $"{b.Id}#{b.Title}#{b.Author}#{b.Genre}#{b.Publisher}#{b.ISBN}").ToList();
        using(StreamWriter bookwriter = new StreamWriter(@"..\Books\books.txt")){
            foreach (var line in booklines)
            {
                bookwriter.WriteLine(line);        
            }
        }
        var readerlines = loanRecords.Select(l => $"{l.ReaderId}#{l.BookId}#{l.ISBN}#{l.EmployeeId}#{l.IsReturn}#{l.Date}");
        using(StreamWriter bookwriter = new StreamWriter(@"..\LoanRecords\loanrecords.txt")){
            foreach (var line in readerlines)
            {
                bookwriter.WriteLine(line);        
            }
        }
                var lines = readers.Select(r => $"{r.Id}#{r.FullName}#{r.PhoneNumber}#{r.Email}");
        using(StreamWriter bookwriter = new StreamWriter(@"..\Readers\readers.txt")){
            foreach (var line in lines)
            {
                bookwriter.WriteLine(line);        
            }
        }
    } 
}
public class AccountManager
{
    private List<Reader> readers = new List<Reader>();
    private List<Employee> employees = new List<Employee>();

    public void LoadReaders(string filePath)
    {
        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split('#');
            readers.Add(new Reader(Convert.ToInt16(parts[0]), parts[1], parts[2], parts[3]));
        }
    }

    public void LoadEmployees(string filePath)
    {
        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split('#');
            employees.Add(new Employee(Convert.ToInt16(parts[0]), parts[1], parts[2], parts[3], parts[4]));
        }
    }

    public void SaveReaders(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            foreach (var reader in readers)
            {
                writer.WriteLine(reader);
            }
        }
    }

    public void SaveEmployees(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            foreach (var employee in employees)
            {
                writer.WriteLine(employee);
            }
        }
    }

    public void AddReader(Reader reader)
    {
        readers.Add(reader);
        SaveReaders(@"..\Readers\readers.txt");
    }

    public void AddEmployee(Employee employee)
    {
        employees.Add(employee);
        SaveEmployees(@"..\Employees\employees.txt");
    }

    public void DeleteReader(int id)
    {
        var reader = readers.FirstOrDefault(r => r.Id == id);
        if (reader != null)
        {
            readers.Remove(reader);
            SaveReaders(@"..\Readers\readers.txt");
        }
    }

    public void DeleteEmployee(int id)
    {
        var employee = employees.FirstOrDefault(e => e.Id == id);
        if (employee != null)
        {
            employees.Remove(employee);
            SaveEmployees(@"..\Employees\employees.txt");
        }
    }


}
