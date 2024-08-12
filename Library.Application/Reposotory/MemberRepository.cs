using Domain.Entities;
using Domain.Repository;
using Library.Application.Service;
using Domain.FileHandler;

namespace Application.Repository;
public class MemberRepository : IMemberRepository
{
	private readonly IMemberHandler _memberHandler;

	public MemberRepository(IMemberHandler memberHandler)
	{
		this._memberHandler = memberHandler;
	}
	private List<Member>? _members;
	public bool Add(Member member)
	{
		if (_members == null && Get() == null)
			throw new FileLoadException();
		if (_members != null)
		{
			member.Id = _members.Max(m => m.Id) + 1;
			member.Name = member.Name?.Trim().Length == 0 ? "Undefined" : member.Name?.Trim();
			member.Email = member.Email?.Trim().Length == 0 ? "Undefined" : member.Email?.Trim();
			_members.Add(member);
		}
		return _members != null && _memberHandler.Write(_members);
	}
	public bool Update(Member member)
	{
		if (_members == null && Get() == null)
			throw new FileLoadException();
		if (_members != null)  // Always True, added to remove warning
		{
			int index = _members.FindIndex(m => m.Id == member.Id);
			_members[index] = member;
		}
		return _members != null && _memberHandler.Write(_members);
	}
	public bool Delete(int memberId)
	{
		if (_members == null && Get() == null)
			throw new FileLoadException();
		_members?.Remove(_members.Find(m => m.Id == memberId));
		return _members != null && _memberHandler.Write(_members);
	}
	public List<Member>? Get() => _members ??= _memberHandler.Read();

	public Member? GetById(int memberId) => Get()?.Find(m => m.Id == memberId);
}