namespace ConsoleApp;
public class Books
{
    public static int BooksMenu (Application.Library library)
    {
        List<string> options = ["Members.", "Books.", "Borrowed Books.", "Borrow/Return a book.", "Exit"];
        Console.Write(Ansi.HideCursor);
        int userSelection = UserInteraction.GetUserSelection(options);

        Console.WriteLine(Ansi.Clear + $"You have select {userSelection}");
        return 0;
    }
}