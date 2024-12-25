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
        return $"ID: {Id}, ФИО: {FullName}, Номер телефона: {PhoneNumber}, Почта: {Email}, Должность: {Position}, Рейтинг: {GivedBooks}";
    }
}
