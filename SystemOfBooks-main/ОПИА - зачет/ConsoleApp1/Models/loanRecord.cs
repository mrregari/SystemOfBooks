using System.ComponentModel;

public class LoanRecord
{
    public int ReaderId { get; set; }
    public int BookId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public bool IsReturn { get; set; }

    public LoanRecord(int readerId, int bookId, bool isReturn, int employeeid, DateTime date)
    {
        ReaderId = readerId;
        BookId = bookId;
        EmployeeId = employeeid;
        IsReturn = isReturn;
        Date = date;
    }

    public override string ToString()
    {
        return $"ReaderID:{ReaderId}, BookID: {BookId}, IsReturn: {IsReturn}, Date {Date}";
    }
}