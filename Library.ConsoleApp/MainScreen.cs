namespace ConsoleApp;
public class MainScreen
{
	public static int MainScreenMenu(Application.Library library)
	{
		AnsiCommands con = new AnsiCommands();
		Console.WriteLine($"{con.Clear}\t\tWelcome to OP Library System\n");
		Console.Write("1- Members.\n2- Books\n3- Borrowed Books\n4- Borrow Book\n5- Return Book.\n0- Exit");
		Console.WriteLine($"\n\nUse {con.Yellow + con.Bold}Arrows(up & down){con.Reset} then {con.Yellow + con.Bold}Enter{con.Reset} to select option.");
		return 0;
	}
}