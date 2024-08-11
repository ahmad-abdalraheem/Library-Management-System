using Domain.Entities;

namespace Domain.Repository;
public interface IBookRepository
{
	public bool WriteBooks(string jsonString);
	public List<Book>? ReadBooks();
}