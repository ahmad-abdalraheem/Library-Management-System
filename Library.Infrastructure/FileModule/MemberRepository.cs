using System.Text.Json;
using Domain.Entities;
using Domain.Repository;

namespace Infrastructure.FileModule;

public class MemberRepository : IMemberRepository
{
//	const string FilePath = "/home/ahmadabdalraheem/RiderProjects/LibraryTask/Library.Infrastructure/Data/Members.json";
	const string FilePath = "C:\\Users\\NTC\\Source\\Repos\\Library-Management-System\\Library.Infrastructure\\Data\\Members.json";

	public bool WriteMembers(string jsonString)
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
	public List<Member>? ReadMembers()
	{
		try
		{
			var jsonString = File.ReadAllText(FilePath);
			List<Member>? members = JsonSerializer.Deserialize<List<Member>>(jsonString);
			return members;
		}
		catch (Exception ex)
		{
			// add logger later
			return null;
		}
	}
}