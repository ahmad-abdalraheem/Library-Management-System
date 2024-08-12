using Application.Repository;
using Domain.Entities;
using Domain.Repository;
using static System.Console;

namespace Library.Application.Service;

public class MemberService
{
	private readonly IMemberRepository _memberRepository;

	public MemberService(IMemberRepository memberRepository)
	{
		this._memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
	}
	public bool Add(Member member)
	{
		try
		{
			return _memberRepository.Add(member);
		}
		catch (Exception e)
		{
			WriteLine("Error while Adding Member : {0}", e.Message);
			return false;
		}
	}
	public bool Update(Member member)
	{
		try
		{
			return _memberRepository.Update(member);
		}
		catch (Exception e)
		{
			WriteLine("Error While Updating Member : {0}", e.Message);
			return false;
		}
	}
	public bool Delete(int memberId)
	{
		try
		{
			return _memberRepository.Delete(memberId);
		}
		catch (Exception e)
		{
			WriteLine("Error while deleting member : {0}", e.Message);
			return false;
		}
	}
	public List<Member>? Get()
	{
		try
		{
			return _memberRepository.Get();
		}
		catch (Exception e)
		{
			WriteLine("Error while fetching members : {0}", e.Message);
			return null;
		}
	}
}