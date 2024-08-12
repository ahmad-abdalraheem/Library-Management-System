using Domain.Entities;
using Library.Application.Service;
using Member = AutoMapper.Execution.Member;

namespace ConsoleApp;
public class BorrowScreen(LibraryService libraryService)
{
    private LibraryService _libraryService = libraryService;
    
    public List<Book>? BorrowedBooks { get; set; }
    private List<Book>? AvailableBooks { get; set; }
    private List<Member>? Members { get; set; }
    public int BorrowBookMenu ()
    {
        BorrowedBooks = _libraryService.GetBorrowed();
        bool isExit = false;
        while (!isExit)
        {
            if (BorrowedBooks?.Count == 0)
            {
                Console.Clear();
                Console.Write(Ansi.Red + "No Borrowed books. press on ADD button to borrow one or Backspace to get back." +Ansi.Reset);
                switch (UserInteraction.GetUserSelection(["Borrow new book.", "Get Back."]))
                {
                    case 0:
                        
                        break;
                    default:
                        isExit = true;
                        break;
                }
            }
            else
            {;
                DisplayBorrowedBooks(library, books, 24);
                isExit = BorrowOpearion(library, ref books);
            }
        }
        return 0;
    }
    public bool BorrowOpearion()
    {
        int selection = 0;
        PrintRow(books[selection], selection+3, 24, Ansi.Blue);
        while (true)
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if (selection > 0)
                    {
                        PrintRow(books[selection], selection+3, 24, Ansi.Reset); 
                        selection--;
                        PrintRow(books[selection], selection+3, 24, Ansi.Blue);
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (selection < books.Count - 1)
                    {
                        PrintRow(books[selection], selection+3, 24, Ansi.Reset); 
                        selection++;
                        PrintRow(books[selection], selection+3, 24, Ansi.Blue);
                    }
                    break;
                case ConsoleKey.Enter:
                    books[selection].IsBorrowed = false;
                    books[selection].BorrowedBy = null;
                    books[selection].BorrowedDate = null;
                    library.UpdateBook(books[selection]);
                    books.RemoveAt(selection);
                    return false;
                case ConsoleKey.Add:
                    BorrowBook(library, ref books);
                    return false;
                case ConsoleKey.Backspace:
                    return true;
            }
        }
    }
    public void DisplayBorrowedBooks()
    {
        Console.Clear();
        if (BorrowedBooks?.Count == 0)
            return;
        int currentRow = 1;
        Console.Write($"ID{Ansi.CursorPosition(1, 5)}Title{Ansi.CursorPosition(1, 40)}Author" +
                      $"{Ansi.CursorPosition(1, 68)}Borrowed By" +
                      $"{Ansi.CursorPosition(1, 93)}Borrowed Date");
        Console.Write("\n______________________________________________________________\n" + Ansi.Reset);
        currentRow += 2;
        foreach (Book book in BorrowedBooks)
        {
            PrintRow(book, currentRow++, Ansi.Reset);
        }
        Console.WriteLine(Ansi.Yellow + "\nUse Arrow (Up/Down) To select Record, then press:");
        Console.WriteLine("- Enter Key -> Return the book");
        Console.WriteLine("- Plus (+) Key -> Borrow a new book");
        Console.WriteLine("- Backspace Key -> Get back to Main Menu." + Ansi.Reset);
    }
    public Book? SelectAvailableBook()
    {
        Console.Write(Ansi.Yellow + "ID   Title                              Author\n"+
                      "__________________________________________________________\n" + Ansi.Reset);
        int selection = 0;
        foreach (Book book in AvailableBooks)
        {
            Console.Write(Ansi.CursorPosition(selection + 3, 1) + book.Id + Ansi.CursorPosition(selection+3, 5)+
                          (book?.Title?.Length > 30 ? book.Title.Substring(0, 30):book?.Title) +
                           Ansi.CursorPosition(selection + 3, 40) + book?.Author);
            selection++;
        }
        selection = 0;
        Console.WriteLine("\nSelect a book to borrow, Backspace to get back");
        Console.Write(Ansi.Blue + Ansi.CursorPosition(3,1) + AvailableBooks[selection].Id + Ansi.CursorPosition(3,5) +
                      (AvailableBooks[selection]?.Title?.Length > 30
                          ? AvailableBooks[selection]?.Title?.Substring(0, 30) + "..."
                          : AvailableBooks[selection]?.Title)
                      + Ansi.CursorPosition(3,40) + AvailableBooks[selection]?.Author + Ansi.Reset);
        while (true)
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if (selection > 0)
                    {
                        Console.Write(Ansi.ToLineStart + AvailableBooks[selection].Id + Ansi.CursorPosition(selection + 3,5) +
                                      (AvailableBooks[selection]?.Title?.Length > 30
                                          ? AvailableBooks[selection]?.Title?.Substring(0, 30) + "..."
                                          : AvailableBooks[selection].Title)
                                      + Ansi.CursorPosition(selection + 3,40) + AvailableBooks[selection].Author);
                        selection--;
                        Console.Write(Ansi.ToLineStart + Ansi.Blue + AvailableBooks[selection].Id + Ansi.CursorPosition(selection + 3,5) +
                                      (AvailableBooks[selection]?.Title?.Length > 30
                                          ? AvailableBooks[selection]?.Title?.Substring(0, 30) + "..."
                                          : AvailableBooks[selection].Title)
                                      + Ansi.CursorPosition(selection + 3,40) + AvailableBooks[selection].Author);
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (selection < AvailableBooks.Count - 1)
                    {
                        Console.Write(Ansi.ToLineStart + AvailableBooks[selection].Id + Ansi.CursorPosition(selection + 3,5) +
                                      (AvailableBooks[selection]?.Title?.Length > 30
                                          ? AvailableBooks[selection]?.Title?.Substring(0, 30) + "..."
                                          : AvailableBooks[selection].Title)
                                      + Ansi.CursorPosition(selection + 3,40) + AvailableBooks[selection].Author);
                        selection++;
                        Console.Write(Ansi.ToLineStart + Ansi.Blue + AvailableBooks[selection].Id + Ansi.CursorPosition(selection + 3,5) +
                                      (AvailableBooks[selection]?.Title?.Length > 30
                                          ? AvailableBooks[selection]?.Title?.Substring(0, 30) + "..."
                                          : AvailableBooks[selection].Title)
                                      + Ansi.CursorPosition(selection + 3,40) + AvailableBooks[selection].Author);
                    }
                    break;
                case ConsoleKey.Enter:
                    return AvailableBooks[selection];
                case ConsoleKey.Backspace:
                    return null;
            }
        }
    }
    public Member? SelectMember()
    {
        Console.Clear();
        Console.Write(Ansi.Yellow + "ID     Name\n" + "______________________________\n" + Ansi.Reset);
        int selection = 0;
        foreach (var member in members)
        {
               Console.WriteLine(member.Id + Ansi.CursorPosition(selection+3, 5) + member.);
               selection++;
        }
        Console.Write("Select the member or press Backspace to get back.\n\n");
        selection = 0;
        Console.Write(Ansi.Blue + Ansi.CursorPosition(3, 1) + members[selection].MemberId + 
                      Ansi.CursorPosition(3,5) + members[selection].Name + Ansi.Reset);
        while (true)
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if (selection > 0)
                    {
                        Console.Write(Ansi.ToLineStart + members[selection].MemberId +
                                      Ansi.CursorPosition(selection + 3, 5) + members[selection].Name);
                        selection--;
                        Console.Write(Ansi.LineUp + Ansi.ToLineStart + Ansi.Blue + members[selection].MemberId +
                                      Ansi.CursorPosition(selection + 3, 5) + members[selection].Name + Ansi.Reset);
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (selection < members.Count - 1)
                    {
                        Console.Write(Ansi.ToLineStart + members[selection].MemberId +
                                      Ansi.CursorPosition(selection + 3, 5) + members[selection].Name);
                        selection++;
                        Console.Write(Ansi.LineDown + Ansi.ToLineStart + Ansi.Blue + members[selection].MemberId +
                                      Ansi.CursorPosition(selection + 3, 5) + members[selection].Name + Ansi.Reset);
                    }
                    break;
                case ConsoleKey.Enter:
                    return members[selection];
                case ConsoleKey.Backspace:
                    return null;
            }
        }
    }
    private void PrintRow(Book book, int row, string color)
    {
        Console.Write(color);
        Console.WriteLine(Ansi.CursorPosition(row, 1) + book.Id + Ansi.CursorPosition(row, 5) + 
                      (book.Title?.Length > 30 ? book.Title.Substring(0, 30) + "..." : book.Title) +
                      Ansi.CursorPosition(row, 40) + 
                      (book.Author?.Length > 25 ? book.Author.Substring(0, 22)+"..." : book.Author) + 
                      Ansi.CursorPosition(row, 68) + book.MemberName + Ansi.CursorPosition(row, 93) +
                      book.BorrowedDate.Value.ToShortDateString() + Ansi.Reset);
    }
}