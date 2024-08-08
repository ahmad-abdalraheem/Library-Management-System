using Domain.Entities;

namespace Domain.Repository;
public interface IMemberRepository
{
	bool WriteMembers(string jsonString);
	List<Member>? ReadMembers();
}