using Domain.Entities;
using Library.Application.Service;

namespace ConsoleApp;
public class BooksScreen(BookService bookService)
{
    private List<Book>? _books = bookService.Get();
    public int BooksMenu ()
    {
        if((_books = bookService.Get()) == null)
        {
            return 0;
        }
        bool isExit = false;
        while (!isExit)
        {
            Console.Clear();
            if (_books?.Count == 0)
            {
                Console.WriteLine(Ansi.Red+"No Books found." + Ansi.Reset);
                switch (UserInteraction.GetUserSelection(["Add a new book", "Back to main menu."]))
                {
                    case 0:
                        bookService.Add(AddBook());
                        _books = bookService.Get();
                        break;
                    default:
                        isExit = true;
                        break;
                }
            }
            else
            {
                DisplayBooks();
                isExit = BooksOperation();
            }
        }
        return 0;
    }
    private void DisplayBooks()
    {
        Console.Clear();
        if (_books?.Count == 0)
            return;
        int currentRow = 1;
        Console.Write($"ID{Ansi.CursorPosition(1, 5)}Title{Ansi.CursorPosition(1, 40)}Author" +
                      $"{Ansi.CursorPosition(1, 67)}Status{Ansi.CursorPosition(1,79)}");
        Console.Write("\n______________________________________________________________\n" + Ansi.Reset);
        currentRow += 2;
        foreach (Book book in _books)
        {
            PrintRow(book, currentRow++,Ansi.Reset);
            Console.WriteLine();
        }
        
        Console.WriteLine(Ansi.Yellow + "\nUse Arrow (Up/Down) To select Record, then press:");
        Console.WriteLine("- Delete Key -> Delete selected record.");
        Console.WriteLine("- Enter Key -> Update selected record.");
        Console.WriteLine("- Plus (+) Key -> Add a new record.");
        Console.WriteLine("- Backspace Key -> Get back to Main Menu." + Ansi.Reset);
    }
    private bool BooksOperation()
    {
        int selected = 0;
        PrintRow(_books[selected], 3, Ansi.Blue);
        while (true)
        {
            switch(Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    if(selected > 0)
                    {
                        PrintRow(_books[selected], selected + 3, Ansi.Reset);
                        selected--;
                        PrintRow(_books[selected], selected + 3, Ansi.Blue);
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if(selected < _books.Count - 1)
                    {                        
                        PrintRow(_books[selected], selected + 3, Ansi.Reset);
                        selected++;
                        PrintRow(_books[selected], selected + 3, Ansi.Blue);
                    }
                    break;
                case ConsoleKey.Enter:
                    _books[selected] = UpdateBook(_books[selected]);
                    bookService.Update(_books[selected]);
                    _books = bookService.Get();
                    return false;
                case ConsoleKey.Delete:
                    bookService.Delete(_books[selected].Id);
                    _books = bookService.Get();
                    return false;
                case ConsoleKey.Add: 
                    bookService.Add(AddBook());
                    _books = bookService.Get();
                    return false;
                case ConsoleKey.Backspace:
                    return true;
            }
        }
    }
    private Book AddBook()
    {
        Console.Clear();
        Book book = new Book();
        Console.Write(Ansi.Yellow + "Book Title : " + Ansi.Reset + Ansi.ShowCursor);
        book.Title = Console.ReadLine()?.Trim();
        Console.Write(Ansi.Yellow + "Book Author : " + Ansi.Reset + Ansi.ShowCursor);
        book.Author = Console.ReadLine()?.Trim();
        Console.Write(Ansi.HideCursor);
        
        book.IsBorrowed = false;
        book.BorrowedDate = null;
        book.BorrowedBy = null;
        
        return book;
    }
    private Book UpdateBook(Book book)
    {
        Console.Clear();
        Console.WriteLine(Ansi.Yellow + "Book ID : " + Ansi.Reset + book.Id);
        Console.Write(Ansi.Yellow + "Book Title : " + Ansi.Reset);
        string? input = Console.ReadLine()?.Trim();
        book.Title = input == String.Empty ? book.Title : input;
        Console.Write(Ansi.LineUp + Ansi.MoveRight(13) + book.Title + "\n");
        Console.Write(Ansi.Yellow + "Book Author : " + Ansi.Reset);
        input = Console.ReadLine()?.Trim();
        book.Author = input == String.Empty ? book.Author : input;
        Console.Write(Ansi.LineUp + Ansi.MoveRight(14) + book.Author + "\n");
        return book;
    }
    private void PrintRow(Book book, int row, string color)
    {
        Console.Write(color);
        Console.Write(Ansi.CursorPosition(row, 1) + book.Id + Ansi.CursorPosition(row, 5) + 
                      (book.Title?.Length > 30 ? book.Title.Substring(0, 30) + "..." : book.Title) +
                      Ansi.CursorPosition(row, 40) + 
                      (book.Author?.Length > 25 ? book.Author.Substring(0, 22)+"..." : book.Author) + 
                      Ansi.CursorPosition(row, 68) + (book.IsBorrowed ? $"{Ansi.Red}Borrowed" : $"{Ansi.Green}Available") 
                      + Ansi.CursorPosition(row, 79) + color +
                      (book.BorrowedDate != null ? book.BorrowedDate.Value.ToShortDateString() : "***") );
        Console.Write(Ansi.Reset);
    }
}