using System.Text.Json;
using Domain.Entities;
using Domain.FileHandler;

namespace Infrastructure.FileModule;

public class MemberHandler : IMemberHandler
{
	private readonly string _filePath = "/home/ahmadabdalraheem/RiderProjects/LibraryTask/Library.Infrastructure/Data/Members.json";
	public bool Write(List<Member> members)
	{
		try
		{
			string json = JsonSerializer.Serialize(members, new JsonSerializerOptions { WriteIndented = true });
			File.WriteAllText(_filePath, json);
			return true;
		}
		catch (Exception e)
		{
			Console.WriteLine("Error While Writing Data : " + e.Message);
			return false;
		}
	}
	public List<Member>? Read()
	{
		try
		{
			string json = File.ReadAllText(_filePath);

			return JsonSerializer.Deserialize<List<Member>>(json);
		}
		catch (Exception e)
		{
			Console.WriteLine("Error While Reading Data : " + e.Message);
			return null;
		}
	}
}