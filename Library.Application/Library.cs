using System.Runtime.InteropServices;
using System.Text.Json;
using Domain.Entities;
using Domain.Repository;

namespace Application;
public class Library
{
	private readonly IMemberRepository _memberRepository;
	private readonly IBookRepository _bookRepository;
	public List<Book>? Books { get; set; }
	public List<Member>? Members { get; set; }
	public Library(IMemberRepository memberRepository, IBookRepository bookRepository)
	{
		_memberRepository = memberRepository;
		_bookRepository = bookRepository;
	}
	/// <summary>
	/// The 4 CRUD methods for members data (Create, Retrieve, Update, Delete) 
	/// </summary>
	public bool AddMember(Member member)
	{
		if (Members == null && GetAllMembers() == null)
			return false;
		member.MemberId = Members.Count == 0? 1: Members.Max(m => m.MemberId) + 1;
		Members?.Add(member);
		
		string updatedJsonString = JsonSerializer.Serialize(Members, new JsonSerializerOptions { WriteIndented = true });

		return _memberRepository.WriteMembers(updatedJsonString);
	}
	public bool UpdateMember(Member member, int memberIndex)
	{
		if (Members == null && GetAllMembers() == null) // Members cannot be loaded right now
			return false;
		if (Members[memberIndex].MemberId != member.MemberId) // MemberId cannot be changed
			return false;
		Members[memberIndex] = member;
		string updatedJsonString = JsonSerializer.Serialize(Members, new JsonSerializerOptions { WriteIndented = true });

		return _memberRepository.WriteMembers(updatedJsonString);
	}
	public bool DeleteMember(Member member)
	{
		if (Members == null && GetAllMembers() == null) // Members not loaded yet
			return false;
		List<Book> borrowedBooks = GetBorrowedBooks();
		if (borrowedBooks.FindIndex(b => b.BorrowedBy == member.MemberId) >= 0)
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
	/// <summary>
	/// The 4 CRUD methods for books data (Create, Retrieve, Update, Delete) 
	/// </summary>
	public bool AddBook(Book book)
	{
		if (Books == null && GetAllBooks() == null)
			return false;
		book.BookID = Books.Count == 0 ? 1 : 1 + Books.Max(b => b.BookID);
		Books.Add(book);
		
		string updatedJsonString = JsonSerializer.Serialize(Books, new JsonSerializerOptions { WriteIndented = true });
		return _bookRepository.WriteBooks(updatedJsonString);
	}
	public bool UpdateBook(Book book, int bookIndex = -1)
	{
		bookIndex = bookIndex == -1 ? Books.FindIndex(b => b.BookID == book.BookID) : bookIndex; 
		if (Books == null && GetAllBooks() == null) // Books cannot be loaded right now
			return false;
		if (Books[bookIndex].BookID != book.BookID) // BookId cannot be changed
			return false;
		Books[bookIndex] = book;
		string updatedJsonString = JsonSerializer.Serialize(Books, new JsonSerializerOptions { WriteIndented = true });

		return _bookRepository.WriteBooks(updatedJsonString);
	}
	public bool DeleteBook(Book book)
	{
		if (Books == null && GetAllBooks() == null)
			return false;
		if (Books?.Remove(book) == false)
			return false;
		string updatedJsonString = JsonSerializer.Serialize(Books, new JsonSerializerOptions { WriteIndented = true });

		return _bookRepository.WriteBooks(updatedJsonString);
	}
	public List<Book>? GetAllBooks()
	{
		Books = _bookRepository.ReadBooks();
		return Books;
	}

	public List<Book>? GetBorrowedBooks()
	{
		if (Books == null)
			Books = _bookRepository.ReadBooks();
		if (Members == null)
			Members = _memberRepository.ReadMembers();
		List<Book> borrowedBooks = new List<Book>();
		var memberDictionary = Members.ToDictionary(m => m.MemberId, m => m.Name);
        
		foreach (Book book in Books)
		{
			if (!book.IsBorrowed)
				continue;
			book.MemberName = memberDictionary[book.BorrowedBy.Value];
			borrowedBooks.Add(book);
		}
		return borrowedBooks;
	}
	public List<Book>? GetAvailableBooks()
	{
		if (Books == null)
			Books = _bookRepository.ReadBooks();
		
		return Books.FindAll(b => b.IsBorrowed == false);
	}
}