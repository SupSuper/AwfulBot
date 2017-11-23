using System;
using System.Threading.Tasks;
using DSharpPlus;

namespace AwfulBot
{
    class Program
    {
	    static DiscordClient discord;

		static async Task Main(string[] args)
        {
			await MainAsync();
		}

	    static async Task MainAsync()
	    {
		    discord = new DiscordClient(new DiscordConfiguration
		    {
			    Token = "<your token here>",
			    TokenType = TokenType.Bot
		    });

		    discord.MessageCreated += async e =>
		    {
			    if (e.Message.Content.ToLower().StartsWith(".ping"))
				    await e.Message.RespondAsync("pong!");
		    };

		    await discord.ConnectAsync();
		    await Task.Delay(-1);
	    }
	}
}
