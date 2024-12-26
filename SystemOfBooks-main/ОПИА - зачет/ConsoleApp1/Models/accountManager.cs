
using System.ComponentModel.Design;

public class AccountManager
{

    Lists lists = new Lists();
    public void AccountManagerMenu()  // Менюшка
    {
        Console.WriteLine("1. Добавить читателя");
        Console.WriteLine("2. Создать сотрудника");
        Console.WriteLine("3. Посмотреть список читателей");
        Console.WriteLine("4. Выход");

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
                CheckAllReaders();
                break;
            case "4":
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
            lists.readers.Add(new Reader(Convert.ToInt16(parts[0]), parts[1], parts[2], parts[3]));
        }
    }

    public void LoadEmployees(string filePath)
    {
        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split('#');
            lists.employees.Add(new Employee(Convert.ToInt16(parts[0]), parts[1], parts[2], parts[3], parts[4], Convert.ToInt16(parts[5])));
        }
    }

    public void SaveReaders(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            foreach (var reader in lists.readers)
            {
                writer.WriteLine(reader);
            }
        }
    }

    public void CheckAllReaders(){
        System.Console.WriteLine("Список всех читателей: \n");
        foreach(var item in lists.readers){
            System.Console.WriteLine(item);
        }
    }

    public void SaveEmployees(string filePath)
    {
        var lines = lists.employees.Select(r => $"{r.Id}#{r.FullName}#{r.PhoneNumber}#{r.Email}#{r.Position}#{r.GivedBooks}");
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            foreach (var employee in lines)
            {
                writer.WriteLine(employee);
            }
        }
    }

    public void AddReader(Reader reader)
    {
        lists.readers.Add(reader);
        SaveReaders(@"..\Readers\readers.txt");
    }

    public void AddEmployee(Employee employee)
    {
        lists.employees.Add(employee);
        SaveEmployees(@"..\Employees\employees.txt");
    }

public void CreateReader()
    {
        int id = lists.readers?.Any() == true ? lists.readers.Max(r => r.Id) + 1 : 1;

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
        int id = lists.employees?.Any() == true ? lists.employees.Max(r => r.Id) + 1 : 1;

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

