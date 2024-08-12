using System.Text.Json;
using Domain.Entities;
using Domain.FileHandler;

namespace Infrastructure.FileModule;

public class BookHandler:IBookHandler
{
	private readonly string _filePath = "/home/ahmadabdalraheem/RiderProjects/LibraryTask/Library.Infrastructure/Data/Books.json";

	public bool Write(List<Book> books)
	{
		try
		{
			string json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
			File.WriteAllText(_filePath, json);
			return true;
		}
		catch (Exception e)
		{
			Console.WriteLine("Error While Writing Data : " + e.Message);
			return false;
		}
	}
	public List<Book>? Read()
	{
		try
		{
			string json = File.ReadAllText(_filePath);

			return JsonSerializer.Deserialize<List<Book>>(json);
		}
		catch (Exception e)
		{
			Console.WriteLine("Error While Reading Data : " + e.Message);
			return null;
		}
	}
}