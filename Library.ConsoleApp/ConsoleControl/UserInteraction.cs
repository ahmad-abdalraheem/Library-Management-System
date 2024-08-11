using Domain.Entities;
namespace ConsoleApp;
public class UserInteraction
{
    public static int GetUserSelection(List<string> options, string instruction = "Use Arrows(Up/Down) Then Enter to submit")
    {
        int selection = 0;
        Console.Write(Ansi.Clear);
        Console.Write(Ansi.CursorPosition(1, 1));
        foreach (string option in options)
        {
            Console.WriteLine($">> {option}");
        }
        Console.WriteLine(Ansi.Yellow + instruction + Ansi.Reset);
        Console.Write(Ansi.CursorPosition(1, 1) + Ansi.ClearLine + Ansi.Blue + ">> " + options[0] + Ansi.Reset + Ansi.ToLineStart);
        
        bool LoopControl = true;
        while (LoopControl)
        {
            ConsoleKey input = Console.ReadKey().Key;
            switch (input.ToString())
            {
                case "UpArrow":
                    if (selection > 0)
                    {
                        Console.Write(Ansi.ClearLine + ">> " + options[selection]);
                        selection--;
                        Console.Write(Ansi.LineUp + Ansi.ClearLine + Ansi.Blue + ">> " + options[selection] + Ansi.Reset + Ansi.ToLineStart);
                    }
                    break;
                case "DownArrow":
                    if (selection < options.Count - 1)
                    {
                        Console.Write(Ansi.ClearLine + ">> " + options[selection]);
                        selection++;
                        Console.Write(Ansi.LineDown + Ansi.ClearLine + Ansi.Blue + ">> " + options[selection] + Ansi.Reset + Ansi.ToLineStart);
                    }
                    break;
                case "Enter":
                    LoopControl = false;
                    break;
            }
        }
        return selection;
    }
}