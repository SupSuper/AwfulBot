using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DSharpPlus;

namespace AwfulBot
{
    public class BotClient
    {
	    private DiscordClient discord;
	    private BotConfig config;

	    public BotClient()
	    {
		    XmlSerializer ser = new XmlSerializer(typeof(BotConfig));
		    using (Stream stream = new FileStream("Resources/Config.xml", FileMode.Open))
		    {
			    config = ser.Deserialize(stream) as BotConfig;
		    }

		    discord = new DiscordClient(new DiscordConfiguration
		    {
			    Token = config.Token,
			    TokenType = TokenType.Bot,
				
			    LogLevel = LogLevel.Debug,
			    UseInternalLogHandler = true
			});

		    AppDomain.CurrentDomain.ProcessExit += async (sender, e) =>
		    {
				await this.Disconnect();
		    };

		    discord.MessageCreated += async e =>
		    {
			    if (e.Message.Content.ToLower().StartsWith(".ping"))
				    await e.Message.RespondAsync("pong!");
		    };
		}

	    public async Task Connect()
		{
			await discord.ConnectAsync();
		}

	    public async Task Disconnect()
		{
			await discord.DisconnectAsync();
		}
    }
}
