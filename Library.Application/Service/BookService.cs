using Domain.Entities;
using Domain.Repository;
using static System.Console;

namespace Library.Application.Service;

public class BookService
{
	private readonly IBookRepository _bookRepository;

	public BookService(IBookRepository bookRepository)
	{
		this._bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
	}
	public bool Add(Book book)
	{
		try
		{
			return _bookRepository.Add(book);
		}
		catch (Exception e)
		{
			WriteLine("Error while Adding Book : {0}", e.Message);
			return false;
		}
	}
	public bool Update(Book book)
	{
		try
		{
			return _bookRepository.Update(book);
		}
		catch (Exception e)
		{
			WriteLine("Error While Updating Book : {0}", e.Message);
			return false;
		}
	}
	public bool Delete(int bookId)
	{
		try
		{
			return _bookRepository.Delete(bookId);
		}
		catch (Exception e)
		{
			WriteLine("Error while deleting book : {0}", e.Message);
			return false;
		}
	}
	public List<Book>? Get()
	{
		try
		{
			return _bookRepository.Get();
		}
		catch (Exception e)
		{
			WriteLine("Error while fetching books : {0}", e.Message);
			return null;
		}
	}
}