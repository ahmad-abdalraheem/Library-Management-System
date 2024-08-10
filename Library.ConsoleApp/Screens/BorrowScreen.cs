namespace ConsoleApp;
public class BorrowScreen
{
    public static int BorrowBookMenu (Application.Library library)
    {
        List<string> options = ["Borrow Book", "Return Book", "Back To Menu", "Exit"];
        Console.Write(Ansi.HideCursor);
        int userSelection = UserInteraction.GetUserSelection(options);
        return 0;
    }
}