using System.Globalization;
using System.Security.Cryptography.X509Certificates;

public class LibrarySystem
{

    AccountManager accountManager = new AccountManager();
    Lists lists = new Lists();

    public void GeneringReport()
    {
        Book maxItem = lists.books.OrderByDescending(item => item.Gived).FirstOrDefault();
        if (maxItem != null)
        {
            Console.WriteLine($"\nСамая популярная книга: [{maxItem.Title}] с {maxItem.Gived} выдачами");
        }
        else
        {
            Console.WriteLine("Список пуст.");
        }
    }

    public void LoadBooks() // Подгрузка в списки всего и вся из файлов
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

                        lists.books.Add(new Book(id, title, author, genre, publisher, isbn, gived));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        }
        if (File.Exists(@"..\LoanRecords\loanrecords.txt")){
            {
                using(StreamReader loanReader = new StreamReader(@"..\LoanRecords\loanrecords.txt")){
                    string line;
                    while ((line = loanReader.ReadLine()) != null)
                    {
                        var parts = line.Split('#');
                        if (parts.Length == 5)
                        {
                            int readerId = Convert.ToInt16(parts[0]);
                            int bookId = Convert.ToInt16(parts[1]);
                            int employeeId = Convert.ToInt16(parts[2]);
                            bool isReturn = Convert.ToBoolean(parts[3]);
                            DateTime Date = Convert.ToDateTime(parts[4]);

                            lists.loanRecords.Add(new LoanRecord(readerId, bookId, isReturn, employeeId, Date));
                        }
                    }
                }
            }

        }
        if (File.Exists(@"..\Readers\readers.txt")){
        {
            using(StreamReader readReader = new StreamReader(@"..\Readers\readers.txt")){
                string line;
                while ((line = readReader.ReadLine()) != null)
                {
                    var parts = line.Split('#');
                    if (parts.Length == 4)
                    {
                        int id = Convert.ToInt16(parts[0]);
                        string fullName = parts[1];
                        string phoneNumber = parts[2];
                        string email = parts[3];

                        lists.readers.Add(new Reader(id, fullName, phoneNumber, email));
                    }
                }
            }
        }

    }
}



    public void ViewStats() // меню отчетов
    {
        System.Console.WriteLine("Генерация отчетов: ");
        System.Console.WriteLine("1. Самая популярная книга");
        System.Console.WriteLine("2. Рейтинг сотрудников");
        System.Console.WriteLine("Для выхода напишите любое другое значение");
        int choise = Convert.ToInt16(Console.ReadLine());
        switch(choise)
        {
            case 1:
                GeneringReport();
                break;
            case 2:
                GeneringReportEmpl();
                break;
            default:
                break;
        }
    }

    public void GeneringReportEmpl(){
        Employee maxItem = lists.employees.OrderByDescending(item => item.GivedBooks).FirstOrDefault();
        if (maxItem != null)
        {
            Console.WriteLine($"\nСамый работающий сотрудник: [{maxItem.FullName}] с {maxItem.GivedBooks} выдачами");
        }
        else
        {
            Console.WriteLine("Список пуст.");
        }
    }

    public void ViewBooks() // просмотр книг
    {
        FileInfo filebook = new FileInfo(@"..\Books\books.txt");
        Console.WriteLine("Список книг: \n");
        foreach(var book in lists.books){
            Console.WriteLine(book); 
        }

        LibMenu();
    }

        // нада
    public void AddBook(Book book)
    {
        lists.books.Add(book);
        AddTextFilesBooks();
    }
    
    public void AddReader(Reader reader)
    {
        lists.readers.Add(reader);
        AddTextFilesReader();
    }

    public void AddLoanRecord(LoanRecord loanRecord)
    {
        lists.loanRecords.Add(loanRecord);
        AddTextFilesLoan();
    }

    
    public void LibMenu() // меню справочника
{
    while (true)
    {
        Console.WriteLine("1. Просмотр книг");
        Console.WriteLine("2. Добавить книгу");
        Console.WriteLine("3. Выдать книгу");
        Console.WriteLine("4. Вернуть книгу");
        Console.WriteLine("5. Выход (Два раза для подтверждения)");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ViewBooks();
                break;

            case "2":

                if (!File.Exists(@"..\Books\books.txt"))
                {
                    Console.WriteLine("Файл с книгами не найден.");
                    break;
                }


                int id = lists.books?.Any() == true ? lists.books.Max(b => b.Id) + 1 : 1;

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
                if (lists.books.Any(b => b.ISBN == isbn))
                {
                    Console.WriteLine("Ошибка: книга с таким ISBN уже существует.");
                }
                else
                {
                    AddBook(new Book(id, title, author, genre, publisher, isbn, gived));
                    Console.WriteLine("Книга добавлена.");
                    LoadBooks();
                    AddTextFilesBooks();
                }
                break;

            case "3":
                Console.Write("Введите ID читателя: ");
                if (!int.TryParse(Console.ReadLine(), out var readerId))
                {
                    Console.WriteLine("Некорректный ID читателя.");
                    break;
                }

                Console.Write("Введите ID книги: ");
                if (!int.TryParse(Console.ReadLine(), out int bookId))
                {
                    Console.WriteLine("Некорректный ID книги.");
                    break;
                }

                Console.WriteLine("Введите ID сотрудника: ");
                if (!int.TryParse(Console.ReadLine(), out int employeeId))
                {
                    Console.WriteLine("Некорректный ID сотрудника.");
                    break;
                }

                Book bookToUpdate = lists.books.Find(b => b.Id == bookId);
                Employee employeeToUpdate = lists.employees.Find(e => e.Id == employeeId);
                if (bookToUpdate != null)
                {
                    if (employeeToUpdate != null)
                    {
                        employeeToUpdate.GivedBooks++;
                        bookToUpdate.Gived++;
                        AddLoanRecord(new LoanRecord(readerId, bookId, false, employeeId, DateTime.Now));
                        Console.WriteLine("Книга выдана.");
                        AddTextFilesBooks();
                        AddTextFilesLoan();
                        accountManager.SaveEmployees(@"../Employees/employees.txt");
                    }
                }
                else
                {
                    Console.WriteLine("Книга с данным ID не найдена.");
                }
                break;

            case "4":
                System.Console.WriteLine("Записи о выдаче: ");
                foreach (var item in lists.loanRecords)
                {
                    System.Console.WriteLine(item);
                }
                Console.Write("Введите ID выданной книги для удаления: ");
                if (!int.TryParse(Console.ReadLine(), out int loanRecordId))
                {
                    Console.WriteLine("Некорректный ID записи.");
                    break;
                }
                bool isRemoved = RemoveLoanRecord(loanRecordId);
                
                if (isRemoved)
                {
                    Console.WriteLine("Запись успешно удалена.");
                }
                else
                {
                    Console.WriteLine("Запись с данным ID не найдена.");
                }
                break;
            case "5":
                return;
            default:
                Console.WriteLine("Неверный выбор. Попробуйте снова.");
                break;
        }
    }
}
    private bool RemoveLoanRecord(int loanRecordId)
    {
        var loanRecordToRemove = lists.loanRecords.Find(lr => lr.BookId == loanRecordId); 
        if (loanRecordToRemove != null){

            loanRecordToRemove.IsReturn = true;
            AddTextFilesLoan();
            return true;
        }
        return false;
    }





                    // загрузка данных из списков в файлы
    public void AddTextFilesBooks(){
        var booklines = lists.books.Select(b => $"{b.Id}#{b.Title}#{b.Author}#{b.Genre}#{b.Publisher}#{b.ISBN}#{b.Gived}").ToList();
        using(StreamWriter bookwriter = new StreamWriter(@"..\Books\books.txt", false)){
            foreach (var line in booklines)
            {
                bookwriter.WriteLine(line);        
            }
        }
    }
    public void AddTextFilesLoan(){
        var readerlines = lists.loanRecords.Select(l => $"{l.ReaderId}#{l.BookId}#{l.EmployeeId}#{l.IsReturn}#{l.Date}").ToList();
        using(StreamWriter loanwriter = new StreamWriter(@"..\LoanRecords\loanrecords.txt", false)){
            foreach (var line in readerlines)
            {
                loanwriter.WriteLine(line);        
            }
        }
    }
    public void AddTextFilesReader(){
        var lines = lists.readers.Select(r => $"{r.Id}#{r.FullName}#{r.PhoneNumber}#{r.Email}").ToList();
        using(StreamWriter readwriter = new StreamWriter(@"..\Readers\readers.txt", false)){
            foreach (var line in lines)
            {
                readwriter.WriteLine(line);        
            }
        }
    LibMenu();
    }


        // ЗаГлАвНыЕ БуКвЫ!!!

    public string FirstLetter(string author){
        if (string.IsNullOrEmpty(author)){
            return author;
        }
        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(author.ToLower());
    }

}