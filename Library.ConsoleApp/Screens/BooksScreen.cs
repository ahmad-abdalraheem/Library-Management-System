using Domain.Entities;

namespace ConsoleApp;
public class Books
{
    public static int BooksMenu (Application.Library library)
    {
        List<Book>? books = library.GetAllBooks();
        if(books == null)
        {
            Console.WriteLine("Sorry, Data is not available right now. press any key to get back.");
            Console.ReadKey();
            return 0;
        }
        
        bool isExit = false;
        while (!isExit)
        {
            Console.Clear();
            if (books.Count == 0)
            {
                Console.WriteLine(Ansi.Red+"No Books found." + Ansi.Reset);
                switch (UserInteraction.GetUserSelection(["Add a new book", "Back to main menu."]))
                {
                    case 0:
                        library.AddBook(AddBook());
                        break;
                    default:
                        isExit = true;
                        break;
                }
            }
            else
            {
                int maxAuthor = books.Max(b => b.Author).Length;
                DisplayBooks(books, maxAuthor);
                isExit = BooksOperation(library, ref books, maxAuthor);
            }
        }
        return 0;
    }

    public static void DisplayBooks(List<Book> books, int maxAuthor)
    {
        Console.Clear();
        if (books.Count == 0)
            return;
        int currentRow = 1;
        Console.Write($"ID{Ansi.CursorPosition(1, 5)}Title{Ansi.CursorPosition(1, 40)}Author" +
                      $"{Ansi.CursorPosition(1, maxAuthor + 44)}Status{Ansi.CursorPosition(1, maxAuthor + 58)}");
        Console.Write("\n______________________________________________________________\n" + Ansi.Reset);
        currentRow += 2;
        foreach (Book book in books)
        {
            PrintRow(book, currentRow++, maxAuthor, Ansi.Reset);
            Console.WriteLine();
        }
        Console.WriteLine(Ansi.Yellow + "\nUse Arrow (Up/Down) To select Record, then press:");
        Console.WriteLine("- Delete Key -> Delete selected record.");
        Console.WriteLine("- Enter Key -> Update selected record.");
        Console.WriteLine("- Plus (+) Key -> Add a new record.");
        Console.WriteLine("- Backspace Key -> Get back to Main Menu." + Ansi.Reset);
    }

    public static bool BooksOperation(Application.Library library,ref List<Book> books,int maxAuthor)
    {
        int selected = 0;
        Console.Write(Ansi.CursorPosition(selected + 3, 1) + Ansi.ClearLine);
        PrintRow(books[selected], selected + 3, maxAuthor, Ansi.Blue);
        bool loopControl = true;
        while (loopControl)
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            switch(key)
            {
                case ConsoleKey.UpArrow:
                    if(selected > 0)
                    {
                        Console.Write(Ansi.ClearLine + Ansi.ToLineStart);
                        PrintRow(books[selected], selected + 3, maxAuthor, Ansi.Reset);
                        selected--;
                        Console.Write(Ansi.LineUp + Ansi.ClearLine + Ansi.ToLineStart);
                        PrintRow(books[selected], selected + 3, maxAuthor, Ansi.Blue);
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if(selected < books.Count - 1)
                    {                        
                        Console.Write(Ansi.ClearLine + Ansi.ToLineStart);
                        PrintRow(books[selected], selected + 3, maxAuthor, Ansi.Reset);
                        selected++;
                        Console.Write(Ansi.LineDown + Ansi.ClearLine + Ansi.ToLineStart);
                        PrintRow(books[selected], selected + 3, maxAuthor, Ansi.Blue);
                    }
                    break;
                
                case ConsoleKey.Enter:
                    books[selected] = UpdateBook(books[selected]);
                    if(library.UpdateBook(books[selected], selected))
                        return false;
                    Console.Clear();
                    Console.Write(Ansi.Red + "Record Cannot be updated right now. press and key to continue" + Ansi.Reset);
                    Console.ReadKey();
                    return false;
                case ConsoleKey.Delete:
                    library.DeleteBook(books[selected]);
                    books = library.GetAllBooks();
                    return false;
                case ConsoleKey.Add:
                    library.AddBook(AddBook());
                    return false;
                case ConsoleKey.Backspace:
                    return true;
            }
        }
        return true;    
    }
    public static Book AddBook()
    {
        Console.Clear();
        Book book = new Book();
        Console.Write(Ansi.Yellow + "Book Title : " + Ansi.Reset + Ansi.ShowCursor);
        string input;
        do
        {
            input = Console.ReadLine().Trim();
            if (input != String.Empty)
                break;
            Console.Write("Title cannot be empty.");
            Console.Write(Ansi.ToLineStart + Ansi.LineUp + Ansi.MoveRight(13));
        } while(true);
        book.Title = input;
        Console.Write(Ansi.ClearLine + Ansi.Yellow + "Author : " + Ansi.Reset);
        input = Console.ReadLine().Trim();
        book.Author = (input == String.Empty) ? "Undefined" : input;
        Console.Write(Ansi.HideCursor);
        book.IsBorrowed = false;
        book.BorrowedDate = null;
        book.BorrowedBy = null;
        return book;
    }
    public static Book UpdateBook(Book book)
    {
        Console.Clear();
        Console.WriteLine(Ansi.Yellow + "Book ID : " + Ansi.Reset + book.BookID);
        
        Console.Write(Ansi.Yellow + "Book Title : " + Ansi.Reset);
        string input = Console.ReadLine().Trim();
        book.Title = input == String.Empty ? book.Title : input;
        Console.Write(Ansi.LineUp + Ansi.MoveRight(13) + book.Title + "\n");
        
        Console.Write(Ansi.Yellow + "Book Author : " + Ansi.Reset);
        input = Console.ReadLine().Trim();
        book.Author = input == String.Empty ? book.Author : input;
        Console.Write(Ansi.LineUp + Ansi.MoveRight(14) + book.Author + "\n");
        return book;
    }
    public static void PrintRow(Book book, int currentRow, int maxAuthor, string color)
    {
        Console.Write(color);
        Console.Write(book.BookID + Ansi.CursorPosition(currentRow, 5) + 
                      (book.Title.Length > 30 ? book.Title.Substring(0, 30) + "..." : book.Title) +
                      Ansi.CursorPosition(currentRow, 40) + book.Author + 
                      Ansi.CursorPosition(currentRow, maxAuthor + 44) +
                      (book.IsBorrowed ? $"{Ansi.Red}Borrowed" : $"{Ansi.Green}Available") 
                      + Ansi.CursorPosition(currentRow, maxAuthor + 58) + color +
                      (book.BorrowedDate != null ? book.BorrowedDate.Value.ToShortDateString() : "***") );
        Console.Write(Ansi.Reset);
    }
}