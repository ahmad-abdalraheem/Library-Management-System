using System.Runtime.InteropServices;
using System.Text.Json;
using Domain.Entities;
using Domain.Repository;

namespace Application;
public class Library
{
	private readonly IMemberRepository _memberRepository;
	public List<Book>? Books { get; set; }
	public List<Member>? Members { get; set; }

	public Library(IMemberRepository memberRepository)
	{
		_memberRepository = memberRepository;
	}

	public bool AddMember(Member member)
	{
		if (Members == null && GetAllMembers() == null)
			return false;
		member.MemberId = Members.Count == 0? 1: Members.Max(x => x.MemberId) + 1;
		Members?.Add(member);
		
		string updatedJsonString = JsonSerializer.Serialize(Members, new JsonSerializerOptions { WriteIndented = true });

		return _memberRepository.WriteMembers(updatedJsonString);
	}
	public bool UpdateMember(Member member, int memberIndex)
	{
		if (Members == null && GetAllMembers() == null)
			return false;
		Members[memberIndex] = member;
		string updatedJsonString = JsonSerializer.Serialize(Members, new JsonSerializerOptions { WriteIndented = true });

		return _memberRepository.WriteMembers(updatedJsonString);
	}
	public bool DeleteMember(Member member)
	{
		if (Members == null && GetAllMembers() == null) // Members not loaded yet
			return false;
		if (Members?.Remove(member) == false) // member already not exist
			return false;
		string updatedJsonString = JsonSerializer.Serialize(Members, new JsonSerializerOptions { WriteIndented = true });

		return _memberRepository.WriteMembers(updatedJsonString);
	}
	public List<Member>? GetAllMembers()
	{
		Members = _memberRepository.ReadMembers();
		return Members;	
	}
	public void SaveMembers()
	{
		
	}
}