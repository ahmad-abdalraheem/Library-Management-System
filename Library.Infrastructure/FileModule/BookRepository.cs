using System.Text.Json;
using Domain.Entities;
using Domain.Repository;

namespace Infrastructure.FileModule;
public class BookRepository : IBookRepository
{
	const string FilePath = "/home/ahmadabdalraheem/RiderProjects/LibraryTask/Library.Infrastructure/Data/Books.json";
	public void AddBook(Book book)
	{
		
	}

	public void UpdateBook(Book book)
	{
	}

	public void DeleteBook(int bookId)
	{
	}

	public Book? GetBookById(int bookId)
	{
		try
		{
			var jsonString = File.ReadAllText(FilePath);
			List<Book?>? books = JsonSerializer.Deserialize<List<Book>>(jsonString);

			return books?.FirstOrDefault(book => book.BookID == bookId);
		}
		catch (Exception ex)
		{
			return null;
		}
	}

	public ICollection<Book>? GetAllBooks()
	{
		try
		{
			var jsonString = File.ReadAllText(FilePath);
			List<Book>? books = JsonSerializer.Deserialize<List<Book>>(jsonString);

			return books;
		}
		catch (Exception ex)
		{
			return null;
		}
	}
}