using System.Globalization;

public class Book
{
    public int Id { get; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public string Publisher { get; set; }
    public string ISBN { get; set; }
    public int Gived { get; set; }

    public Book(int id, string title, string author, string genre, string publisher, string isbn, int gived)
    {
        Id = id;
        Title = title;
        Author = author;
        Genre = genre;
        Publisher = publisher;
        ISBN = isbn;
        Gived = gived;
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

    public LoanRecord(int readerId, int bookId, string isbn,bool isReturn, int employeeid)
    {
        ReaderId = readerId;
        BookId = bookId;
        ISBN = isbn;
        EmployeeId = employeeid;
        IsReturn = isReturn;
        Date = DateTime.Now;
    }

    public override string ToString()
    {
        return $"ReaderID:{ReaderId}, BookID: {BookId}, ISBN: {ISBN}, IsReturn: {IsReturn}, Date {Date}";
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
                    if (parts.Length == 7)
                    {
                        int id = Convert.ToInt16(parts[0]);
                        string title = parts[1];
                        string author = parts[2];
                        string genre = parts[3];
                        string publisher = parts[4];
                        string isbn = parts[5];
                        int gived = Convert.ToInt16(parts[6]);

                        books.Add(new Book(id, title, author, genre, publisher, isbn, gived));
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
        LibMenu();
    }



    public void ViewStats()
    {
        System.Console.WriteLine("Генерация отчетов: ");
        System.Console.WriteLine("1. Самая популярная / непопулярная книга");
        System.Console.WriteLine("2. Рейтинг сотрудников");
        System.Console.WriteLine("Для выхода напишите любое другое значение");
        int choise = Convert.ToInt16(Console.ReadLine());
        switch(choise)
        {
            case 1:
                break;
            case 2:
                break;
            default:
                break;
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
        LibMenu();
    }

        
    public void AddBook(Book book)
    {
        books.Add(book);
    }
    
    public void AddReader(Reader reader)
    {
        readers.Add(reader);
    }

    public void AddLoanRecord(LoanRecord loanRecord)
    {
        books[7].Gived++;
        loanRecords.Add(loanRecord);
    }

    
    public void LibMenu(){
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
                ViewBooks();
                break;
            case "2":
                Console.Write("Введите ID книги: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите название книги: ");
                string title = FirstLetter(Console.ReadLine());
                Console.Write("Введите автора книги: ");
                string author = FirstLetter(Console.ReadLine());
                Console.Write("Введите жанр книги: ");
                string genre = Console.ReadLine();
                Console.Write("Введите издателя книги: ");
                string publisher = Console.ReadLine();
                Console.Write("Введите ISBN книги: ");
                string isbn = Console.ReadLine();
                int gived = 0;
                AddBook(new Book(id, title, author, genre, publisher, isbn, gived));
                Console.WriteLine("Книга добавлена.");
                LoadBooks();
                break;
            case "3":
                Console.Write("Введите ID читателя: ");
                var readerId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите ID книги: ");
                int bookId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите ISBN книги: ");
                string bookIsbn = Console.ReadLine();
                Console.WriteLine("Введите ID сотрудника: ");
                int employeeId = Convert.ToInt16(Console.ReadLine());
                bool isReturn = false;
                AddLoanRecord(new LoanRecord(readerId, bookId, bookIsbn, isReturn, employeeId));
                break;
            case "4":
                AddTextFiles();
                break;
            case "5":
                return;
            default:
                Console.WriteLine("Неверный выбор. Попробуйте снова.");
                break;
            }
        }
    }



    public void AddTextFiles(){
        var booklines = books.Select(b => $"{b.Id}#{b.Title}#{b.Author}#{b.Genre}#{b.Publisher}#{b.ISBN}").ToList();
        using(StreamWriter bookwriter = new StreamWriter(@"..\Books\books.txt")){
            foreach (var line in booklines)
            {
                bookwriter.WriteLine(line);        
            }
        }
        var readerlines = loanRecords.Select(l => $"{l.ReaderId}#{l.BookId}#{l.ISBN}#{l.IsReturn}#{l.Date}");
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
    LibMenu();
    }
    public string FirstLetter(string author){
        if (string.IsNullOrEmpty(author)){
            return author;
        }
        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(author.ToLower());
    }

}