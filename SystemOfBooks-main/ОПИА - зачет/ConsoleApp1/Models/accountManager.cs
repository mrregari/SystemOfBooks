
public class AccountManager
{
    LibrarySystem librarySystem = new LibrarySystem();
    public List<Employee> employees = new List<Employee>();

    public void AccountManagerMenu()
    {
        Console.WriteLine("1. Добавить читателя");
        Console.WriteLine("2. Создать сотрудника");
        Console.WriteLine("3. Выход");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                CreateReader();
                break;
            case "2":
                CreateEmployee();
                break;
            case "3":
                Console.WriteLine("Выход из программы...");
                return;
            default:
                Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте еще раз.");
                break;
        }
    }

    public void LoadReaders(string filePath)
    {
        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split('#');
            librarySystem.readers.Add(new Reader(Convert.ToInt16(parts[0]), parts[1], parts[2], parts[3]));
        }
    }

    public void LoadEmployees(string filePath)
    {
        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split('#');
            employees.Add(new Employee(Convert.ToInt16(parts[0]), parts[1], parts[2], parts[3], parts[4], Convert.ToInt16(parts[5])));
        }
    }

    public void SaveReaders(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            foreach (var reader in librarySystem.readers)
            {
                writer.WriteLine(reader);
            }
        }
    }

    public void SaveEmployees(string filePath)
    {
        var lines = employees.Select(r => $"{r.Id}#{r.FullName}#{r.PhoneNumber}#{r.Email}#{r.Position}#{r.GivedBooks}");
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            foreach (var employee in lines)
            {
                writer.WriteLine(employee);
            }
        }
    }

    public void AddReader(Reader reader)
    {
        librarySystem.readers.Add(reader);
        SaveReaders(@"..\Readers\readers.txt");
    }

    public void AddEmployee(Employee employee)
    {
        employees.Add(employee);
        SaveEmployees(@"..\Employees\employees.txt");
    }

public void CreateReader()
    {
        int id = librarySystem.readers?.Any() == true ? librarySystem.readers.Max(r => r.Id) + 1 : 1;

        Console.Write("Введите ФИО читателя: ");
        string fullName = Console.ReadLine();

        Console.Write("Введите номер телефона читателя: ");
        string phoneNumber = Console.ReadLine();

        Console.Write("Введите почту читателя: ");
        string email = Console.ReadLine();

        
        AddReader(new Reader(id, fullName, phoneNumber, email));
        Console.WriteLine("Читатель успешно добавлен.");
    }


    public void CreateEmployee()
    {
        int id = employees?.Any() == true ? employees.Max(r => r.Id) + 1 : 1;

        Console.Write("Введите ФИО сотрудника: ");
        string fullName = Console.ReadLine();

        Console.Write("Введите номер телефона сотрудника: ");
        string phoneNumber = Console.ReadLine();

        Console.Write("Введите почта сотрудника: ");
        string email = Console.ReadLine();

        Console.Write("Введите должность сотрудника: ");
        string position = Console.ReadLine();

        int givedBooks = 0;
        AddEmployee(new Employee(id, fullName, phoneNumber, email, position, givedBooks));
        Console.WriteLine("Сотрудник успешно добавлен.");
    }


}

