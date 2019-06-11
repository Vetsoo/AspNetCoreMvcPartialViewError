using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace PartialViewsErrorTestApp
{
	public class Program
	{
		/// <summary>
		/// The main.
		/// </summary>
		/// <param name="args">
		/// The args.
		/// </param>
		public static void Main(string[] args)
		{
			BuildWebHost(args).Run();
		}

		/// <summary>
		/// The build web host.
		/// </summary>
		/// <param name="args">
		/// The args.
		/// </param>
		/// <returns>
		/// The <see cref="IWebHost"/>.
		/// </returns>
		public static IWebHost BuildWebHost(string[] args)
		{
			var webHost = WebHost
				.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.UseIISIntegration();

			return webHost.Build();
		}
	}
}
