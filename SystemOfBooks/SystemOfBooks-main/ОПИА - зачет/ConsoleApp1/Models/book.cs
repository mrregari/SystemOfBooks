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
        return $"ID: {Id}, Название: {Title}, Автор: {Author}, Жанр: {Genre}, Издатель: {Publisher}, ISBN: {ISBN} Рейтинг: {Gived}";
    }
}