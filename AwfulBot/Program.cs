using System;
using System.Threading.Tasks;

namespace AwfulBot
{
    internal class Program
    {
	    private static BotClient bot;

		static async Task Main(string[] args)
		{
			bot = new BotClient();
			await bot.Connect();
			await Task.Delay(-1);
		}
	}
}
