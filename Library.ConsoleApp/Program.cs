using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Application;
using ConsoleApp;
using Domain.Entities;
using Domain.Repository;
using Infrastructure.FileModule;


namespace ConsoleApp
{
	public class Program
	{
		public static int Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();
			var library = host.Services.GetRequiredService<Application.Library>();

			MainScreen.MainScreenMenu(library);
			return 0;
		}

		private static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureServices((context, services) =>
				{
					services.AddScoped<IMemberRepository, MemberRepository>();
					services.AddSingleton<Application.Library>();
					
					services.AddScoped<IBookRepository, BookRepository>();
					services.AddSingleton<Application.Library>();
				});
	}
}