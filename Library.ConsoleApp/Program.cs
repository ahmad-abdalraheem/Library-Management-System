using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Library.Application.Service;

namespace ConsoleApp
{
	public abstract class Program
	{
		public static Task<int> Main(string[] args)
		{
			var host = Host.CreateDefaultBuilder(args)
				.ConfigureServices((services) =>
				{
					services.AddApplicationServices();
				})
				.Build();

			MemberService memberService = host.Services.GetRequiredService<MemberService>();
			BookService bookService = host.Services.GetRequiredService<BookService>();
			LibraryService libraryService = new LibraryService(bookService, memberService);
			
			MembersScreen membersScreen = new MembersScreen(memberService);
			BooksScreen booksScreen = new BooksScreen(bookService);
			
			Console.WriteLine(Ansi.HideCursor);
			List<String> options =["Members", "Books", "Return/Borrow book", "Exit"];

			while (true)
			{
				var selection = UserInteraction.GetUserSelection(options);
				switch (selection)
				{
					case 0:
						membersScreen.MembersMenu();
						break;
					case 1:
						booksScreen.BooksMenu();			
						break;
					case 2:
						break;
					case 3:
						return Task.FromResult(0);
					default:
						return Task.FromResult(0);
				}
			}
		}
	}
}