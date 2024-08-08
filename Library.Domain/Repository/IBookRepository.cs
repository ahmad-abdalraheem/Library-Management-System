using Domain.Entities;

namespace Domain.Repository;
public interface IBookRepository
{
	void AddBook(Book book);
	void UpdateBook(Book book);
	void DeleteBook(int bookId);
	abstract Book? GetBookById(int bookId);
	abstract ICollection<Book>? GetAllBooks();
}