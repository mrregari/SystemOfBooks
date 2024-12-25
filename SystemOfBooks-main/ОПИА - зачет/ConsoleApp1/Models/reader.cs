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