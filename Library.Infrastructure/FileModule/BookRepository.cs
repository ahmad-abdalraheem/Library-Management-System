using System.Text.Json;
using Domain.Entities;
using Domain.Repository;
using Member = AutoMapper.Execution.Member;

namespace Infrastructure.FileModule;
public class BookRepository : IBookRepository
{
	const string FilePath = "/home/ahmadabdalraheem/RiderProjects/LibraryTask/Library.Infrastructure/Data/Books.json";
	
	public bool WriteBooks(string jsonString)
	{
		try
		{
			File.WriteAllText(FilePath, jsonString);
			return true;
		}
		catch (Exception ex)
		{
			// add logger later
			return false;
		}
	}
	public List<Book>? ReadBooks()
	{
		try
		{
			var jsonString = File.ReadAllText(FilePath);
			List<Book>? books = JsonSerializer.Deserialize<List<Book>>(jsonString);
			return books;
		}
		catch (Exception ex)
		{
			// add logger later
			return null;
		}
	}
}