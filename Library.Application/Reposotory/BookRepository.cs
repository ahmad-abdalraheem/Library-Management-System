using Domain.Entities;
using Domain.Repository;
using Domain.FileHandler;

namespace Application.Repository;

public class BookRepository:IBookRepository
{
	private readonly IBookHandler _bookHandler;

	public BookRepository(IBookHandler bookHandler)
	{
		this._bookHandler = bookHandler;
	}
	private List<Book>? _books;
	public bool Add(Book book)
	{
		if (_books == null && Get() == null)
			throw new FileLoadException();
		if (_books != null)
		{
			book.Id = _books.Count == 0 ? 1 : _books.Max(m => m.Id) + 1;
			book.Title = book.Title?.Trim().Length == 0 ? "Undefined" : book.Title?.Trim();
			book.Author = book.Author?.Trim().Length == 0 ? "Undefined" : book.Author?.Trim();
			_books.Add(book);
		}
		return _books != null && _bookHandler.Write(_books);
	}
	public bool Update(Book book)
	{
		if (_books == null && Get() == null)
			throw new FileLoadException();
		if (_books != null)  // Always True, added to remove warning
		{
			int index = _books.FindIndex(m => m.Id == book.Id);
			_books[index] = book;
		}
		return _books != null && _bookHandler.Write(_books);
	}
	public bool Delete(int bookId)
	{
		if (_books == null && Get() == null)
			throw new FileLoadException();
		_books?.Remove(_books.Find(m => m.Id == bookId));
		return _books != null && _bookHandler.Write(_books);
	}
	public List<Book>? Get() => _books ??= _bookHandler.Read();

	public Book? GetById(int bookId) => Get()?.Find(m => m.Id == bookId);
}