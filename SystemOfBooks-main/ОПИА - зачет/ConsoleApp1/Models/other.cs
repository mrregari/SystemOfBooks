public class Employee
{
    public int Id { get; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Position { get; set; }
    public int GivedBooks {get; set;}

    public Employee(int id, string fullName, string phoneNumber, string email, string position, int givedBooks)
    {
        Id = id;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Email = email;
        Position = position;
        GivedBooks = givedBooks;
    }

    public override string ToString()
    {
        return $"{Id}#{FullName}#{PhoneNumber}#{Email}#{Position}";
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
            employees.Add(new Employee(Convert.ToInt16(parts[0]), parts[1], parts[2], parts[3], parts[4], Convert.ToInt16(parts[5])));
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
