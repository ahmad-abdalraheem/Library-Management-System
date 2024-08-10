using Domain.Entities;
using System.Collections.Generic;

namespace ConsoleApp;
public class Members
{
    public static int MembersMenu (Application.Library library)
    {
        List<Member>? members = library.GetAllMembers();
        if(members == null)
        {
            Console.WriteLine("Sorry, Data is not available right now.");
            return 0;
        }
        Console.Write(Ansi.SavePosition);

        bool isExit = false;
        while (!isExit)
        {
            Console.Clear();
            if (members.Count == 0)
            {
                Console.WriteLine("No members found. Please add a new member.");
                library.AddMember(AddMember());
            }
            else
            {
                Console.Write(Ansi.SavePosition);
                Displaymembers(ref members);
                isExit = MemberOperation(ref members, ref library);
            }
        }
        return 0;
    }
    public static void Displaymembers(ref List<Member> members)
    {
        if (members.Count == 0)
        {
            Console.WriteLine(Ansi.Red + "No Records." + Ansi.Reset);
            return;
        }
        int currentRow = 1, nameWidth = members.Max(m => m.Name.Length) + 7;
        Console.Write(Ansi.CursorPosition(1, 1) + Ansi.Clear + Ansi.Yellow);
        Console.Write($"ID{Ansi.CursorPosition(1,5)}Name{Ansi.CursorPosition(1, nameWidth)}Email");
        Console.Write("\n__________________________________________________\n" + Ansi.Reset);
        currentRow += 2;
        foreach (Member member in members)
        {
            Console.WriteLine($"{member.MemberId}{Ansi.CursorPosition(currentRow, 5)}{member.Name}{Ansi.CursorPosition(currentRow, nameWidth)}{member.Email}");
            currentRow++;
        }
        Console.WriteLine(Ansi.Yellow + "\nUse Arrow (Up/Down) To select Record, then press:");
        Console.WriteLine("- Delete Key -> Delete selected record.");
        Console.WriteLine("- Enter Key -> Update selected record.");
        Console.WriteLine("- Plus (+) Key -> Add a new record.");
        Console.WriteLine("- ESC (Escape) Key -> Get back to Main Menu." + Ansi.Reset);
    }
    public static bool MemberOperation (ref List<Member> members, ref Application.Library library)
    {
        int  nameWidth = members.Max(m => m.Name.Length) + 7, selected = 0;
        Console.Write(Ansi.RestorePosition + Ansi.CursorPosition(selected + 3, 1) + Ansi.ClearLine + Ansi.Blue);
        Console.Write(members[selected].MemberId + Ansi.CursorPosition(selected + 3, 5)
                    + members[selected].Name + Ansi.CursorPosition(selected + 3, nameWidth)
                    + members[selected].Email + Ansi.Reset + Ansi.ToLineStart);
        bool loopControl = true;
        while (loopControl)
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            switch(key)
            {
                case ConsoleKey.UpArrow:
                    if(selected > 0)
                    {
                        Console.Write(Ansi.ClearLine + members[selected].MemberId 
                                    + Ansi.CursorPosition(selected + 3, 5) + members[selected].Name 
                                    + Ansi.CursorPosition(selected + 3, nameWidth) + members[selected].Email 
                                    + Ansi.Reset + Ansi.ToLineStart);
                        Console.Write(Ansi.LineUp + Ansi.ClearLine + Ansi.Blue);
                        selected--;
                        Console.Write(Ansi.ClearLine + members[selected].MemberId
                                    + Ansi.CursorPosition(selected + 3, 5) + members[selected].Name
                                    + Ansi.CursorPosition(selected + 3, nameWidth) + members[selected].Email
                                    + Ansi.Reset + Ansi.ToLineStart);
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if(selected < members.Count - 1)
                    {
                        Console.Write(Ansi.ClearLine + members[selected].MemberId
                                    + Ansi.CursorPosition(selected + 3, 5) + members[selected].Name
                                    + Ansi.CursorPosition(selected + 3, nameWidth) + members[selected].Email
                                    + Ansi.Reset + Ansi.ToLineStart);
                        Console.Write(Ansi.LineDown + Ansi.ClearLine + Ansi.Blue);
                        selected++;
                        Console.Write(Ansi.ClearLine + members[selected].MemberId
                                    + Ansi.CursorPosition(selected + 3, 5) + members[selected].Name
                                    + Ansi.CursorPosition(selected + 3, nameWidth) + members[selected].Email
                                    + Ansi.Reset + Ansi.ToLineStart);
                    }

                    break;
                case ConsoleKey.Enter:
                    members[selected] = UpdateMember(members[selected]);
                    return false;
                case ConsoleKey.Delete:
                    library.DeleteMember(members[selected]);
                    members = library.GetAllMembers();
                    return false;
                case ConsoleKey.Add:
                    library.AddMember(AddMember());
                    return false;
                case ConsoleKey.Escape:
                    return true;
            }
        }
        return true;
    }
    public static Member AddMember()
    {
        Console.Clear();
        Member member = new Member();
        Console.Write(Ansi.Yellow + "Member Name : " + Ansi.Reset + Ansi.ShowCursor);
        string input;
        do
        {
            input = Console.ReadLine().Trim();
            if (input != String.Empty)
                break;
            Console.Write($"Name cannot be empty. Enter name please or press {Ansi.Green}Esape{Ansi.Reset} to exit");
            Console.Write(Ansi.ToLineStart + Ansi.LineUp + Ansi.MoveRight(15));

        } while(true);
        member.Name = input;
        Console.Write(Ansi.ClearLine + Ansi.Yellow + "Email : " + Ansi.Reset);
        input = Console.ReadLine().Trim();
        member.Email = (input == String.Empty) ? "Undefined" : input;
        Console.Write(Ansi.HideCursor);
        return member;
    }
    public static Member UpdateMember(Member member)
    {

        Console.Clear();
        Console.WriteLine(Ansi.Yellow + "Member ID : " + Ansi.Reset + member.MemberId);
        Console.Write(Ansi.Yellow + "Mamber Name : " +Ansi.SavePosition + Ansi.Reset);
        string input = Console.ReadLine();
        Console.Write(Ansi.RestorePosition + input + "\n");
        member.Name = input == String.Empty ? member.Name : input;
        Console.Write(Ansi.Yellow + "Member Email : " + Ansi.Reset);
        input = Console.ReadLine();
        member.Email = input == String.Empty ? member.Email : input;
        return member;
    }
        
}