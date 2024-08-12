// namespace ConsoleApp;
// public class MainScreen
// {
//     public static int MainScreenMenu (Application.Library library)
//     {
//         List<string> options = ["Members.", "Books.", "Borrow Books.", "Exit"];
//         Console.Write(Ansi.HideCursor);
//         while (true)
//         {
//             int userSelection = UserInteraction.GetUserSelection(options);
//             switch (userSelection)
//             {
//                 case 0:
//                     Console.Clear();
//                     Members.MembersMenu(library);
//                     break;
//                 case 1:
//                     Console.Clear();
//                     Books.BooksMenu(library);
//                     break;
//                 case 2:
//                     BorrowScreen.BorrowBookMenu(library);
//                     break;
//                 default:
//                     return 0;
//             }
//         }
//     }
// }