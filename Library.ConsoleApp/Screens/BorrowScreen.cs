using Domain.Entities;
using Member = AutoMapper.Execution.Member;

namespace ConsoleApp;
public class BorrowScreen
{
    public static int BorrowBookMenu (Application.Library library)
    {
        Console.Write(Ansi.HideCursor);
        List<Book> books = library.GetBorrowedBooks();

        bool isExit = false;
        while (!isExit)
        {
            if (books.Count == 0)
            {
                Console.Clear();
                Console.Write(Ansi.Red + "No Borrowed books. press on ADD button to borrow one or Backspace to get back." +Ansi.Reset);
                switch (UserInteraction.GetUserSelection(["Borrow new book.", "Get Back."]))
                {
                    case 0:
                        BorrowBook(library, ref books);
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
    public static bool BorrowOpearion(Application.Library library,ref List<Book> books)
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
    public static void DisplayBorrowedBooks(Application.Library library, List<Book> books, int maxAuthor)
    {
        Console.Clear();
        if (books.Count == 0)
            return;
        int currentRow = 1;
        Console.Write($"ID{Ansi.CursorPosition(1, 5)}Title{Ansi.CursorPosition(1, 40)}Author" +
                      $"{Ansi.CursorPosition(1, maxAuthor + 44)}Borrowed By" +
                      $"{Ansi.CursorPosition(1, maxAuthor + 64)}Borrowed Date");
        Console.Write("\n______________________________________________________________\n" + Ansi.Reset);
        currentRow += 2;
        foreach (Book book in books)
        {
            PrintRow(book, currentRow++, maxAuthor, Ansi.Reset);
            Console.WriteLine();
        }
        Console.WriteLine(Ansi.Yellow + "\nUse Arrow (Up/Down) To select Record, then press:");
        Console.WriteLine("- Enter Key -> Return the book");
        Console.WriteLine("- Plus (+) Key -> Borrow a new book");
        Console.WriteLine("- Backspace Key -> Get back to Main Menu." + Ansi.Reset);
    }
    public static bool BorrowBook(Application.Library library, ref List<Book> books)
    {
        Console.Clear();
        List<Book> availableBooks = library.GetAvailableBooks();
        List<Domain.Entities.Member> members = library.GetAllMembers();
        if (availableBooks.Count == 0 || members.Count == 0)
        {
            Console.WriteLine(Ansi.Yellow + "No Available books/members.");
            Console.ReadKey();
            return false;
        }
        Book newBorrowedBook = SelectAvailableBook(availableBooks);
        var id = SelectMember(members).MemberId;
        if (newBorrowedBook != null && id != null)
        {
                newBorrowedBook.IsBorrowed = true;
                newBorrowedBook.BorrowedBy = id;
                newBorrowedBook.BorrowedDate = new DateTime();
                books.Remove(newBorrowedBook);
                books.Add(newBorrowedBook);
                library.UpdateBook(newBorrowedBook);
                return true;
        }
        return false;
    }
    public static Book? SelectAvailableBook(List<Book> books)
    {
        Console.Clear();
        Console.Write(Ansi.Yellow + "ID   Title                              Author\n"+
                      "__________________________________________________________\n" + Ansi.Reset);
        int selection = 0;
        foreach (Book book in books)
        {
            Console.Write(Ansi.CursorPosition(selection + 3, 1) + book.BookID + Ansi.CursorPosition(selection+3, 5)+
                          (book.Title.Length > 30 ? book.Title.Substring(0, 30):book.Title) +
                           Ansi.CursorPosition(selection + 3, 40) + book.Author);
            selection++;
        }

        selection = 0;
        Console.WriteLine("\nSelect a book to borrow, Backspace to get back");
        Console.Write(Ansi.Blue + Ansi.CursorPosition(3,1) + books[selection].BookID + Ansi.CursorPosition(3,5) +
                      (books[selection].Title.Length > 30
                          ? books[selection].Title.Substring(0, 30) + "..."
                          : books[selection].Title)
                      + Ansi.CursorPosition(3,40) + books[selection].Author + Ansi.Reset);
        while (true)
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if (selection > 0)
                    {
                        Console.Write(Ansi.ToLineStart + books[selection].BookID + Ansi.CursorPosition(selection + 3,5) +
                                      (books[selection].Title.Length > 30
                                          ? books[selection].Title.Substring(0, 30) + "..."
                                          : books[selection].Title)
                                      + Ansi.CursorPosition(selection + 3,40) + books[selection].Author);
                        selection--;
                        Console.Write(Ansi.LineUp + Ansi.ToLineStart + Ansi.Blue + books[selection].BookID +
                                      Ansi.CursorPosition(selection + 3,5) +
                                      (books[selection].Title.Length > 30
                                          ? books[selection].Title.Substring(0, 30) + "..."
                                          : books[selection].Title)
                                      + Ansi.CursorPosition(selection + 3,40) + books[selection].Author + Ansi.Reset);
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (selection < books.Count - 1)
                    {
                        Console.Write(Ansi.ToLineStart + books[selection].BookID + Ansi.CursorPosition(selection + 3,5) +
                                      (books[selection].Title.Length > 30
                                          ? books[selection].Title.Substring(0, 30) + "..."
                                          : books[selection].Title)
                                      + Ansi.CursorPosition(selection + 3,40) + books[selection].Author);
                        selection++;
                        Console.Write(Ansi.LineDown + Ansi.ToLineStart + Ansi.Blue + books[selection].BookID +
                                      Ansi.CursorPosition(selection + 3,5) +
                                      (books[selection].Title.Length > 30
                                          ? books[selection].Title.Substring(0, 30) + "..."
                                          : books[selection].Title)
                                      + Ansi.CursorPosition(selection + 3,40) + books[selection].Author + Ansi.Reset);
                    }
                    break;
                case ConsoleKey.Enter:
                    return books[selection];
                case ConsoleKey.Backspace:
                    return null;
            }
        }
    }
    public static Domain.Entities.Member? SelectMember(List<Domain.Entities.Member> members)
    {
        Console.Clear();
        Console.Write(Ansi.Yellow + "ID     Name\n" + "______________________________\n" + Ansi.Reset);
        int selection = 0;
        foreach (var member in members)
        {
               Console.WriteLine(member.MemberId + Ansi.CursorPosition(selection+3, 5) + member.Name);
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
    public static void PrintRow(Book book, int currentRow, int maxAuthor, string color)
    {
        Console.Write(color);
        Console.Write(Ansi.CursorPosition(currentRow,1) + book.BookID + Ansi.CursorPosition(currentRow, 5) + 
                      (book.Title.Length > 30 ? book.Title.Substring(0, 30) + "..." : book.Title) +
                      Ansi.CursorPosition(currentRow, 40) + book.Author + 
                      Ansi.CursorPosition(currentRow, maxAuthor + 44) + book.MemberName
                      + Ansi.CursorPosition(currentRow, maxAuthor + 64) + // 64 + 24
                      (book.BorrowedDate != null ? book.BorrowedDate.Value.ToShortDateString() : "***") );
        Console.Write(Ansi.Reset);
    }
}