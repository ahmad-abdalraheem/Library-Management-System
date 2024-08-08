namespace Domain.Entities;
public class Book
{
	public int BookID { get; set; }
	public string Title { get; set; }
	public string Author { get; set; }
	public bool IsBorrowed { get; set; }
	public DateTime? BorrowedDate { get; set; }
	/// <summary>
	///  Borrowed Member ID
	/// </summary>
	public int? BorrowedBy { get; set; }
}