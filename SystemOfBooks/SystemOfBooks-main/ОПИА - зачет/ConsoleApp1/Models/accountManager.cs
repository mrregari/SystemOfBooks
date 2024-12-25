
public class AccountManager
{
    LibrarySystem librarySystem = new LibrarySystem();
    public List<Employee> employees = new List<Employee>();

    public void AccountManagerMenu()
    {
        Console.WriteLine("1. Добавить читателя");
        Console.WriteLine("2. Удалить читателя");
        Console.WriteLine("3. Создать сотрудника");
        Console.WriteLine("4. Уволить сотрудника");
        Console.WriteLine("5. Выход (Два раза джля подтверждения)");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                CreateReader();
                break;
            case "2":
                Console.Write("Введите ID читателя для удаления: ");
                int readerId = Convert.ToInt32(Console.ReadLine());
                DeleteReader(readerId);
                break;
            case "3":
                CreateEmployee();
                break;
            case "4":
                Console.Write("Введите ID сотрудника для увольнения: ");
                int employeeId = Convert.ToInt32(Console.ReadLine());
                DeleteEmployee(employeeId);
                break;
            case "5":
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

    public void DeleteReader(int id)
    {
        var reader = librarySystem.readers.FirstOrDefault(r => r.Id == id);
        if (reader != null)
        {
            librarySystem.readers.Remove(reader);
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

