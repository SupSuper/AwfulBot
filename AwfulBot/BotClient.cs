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
		    discord.Ready += async (e) =>
		    {
			    e.Client.DebugLogger.LogMessage(LogLevel.Info, "AwfulBot", "Client is ready to process events.", DateTime.Now);
			    await Task.CompletedTask;
		    };
		    discord.ClientErrored += async (e) =>
		    {
			    e.Client.DebugLogger.LogMessage(LogLevel.Error, "AwfulBot", $"Exception occured: {e.Exception.GetType()}: {e.Exception.Message}", DateTime.Now);
			    await Task.CompletedTask;
			};

		    AppDomain.CurrentDomain.ProcessExit += async (sender, e) =>
		    {
			    await this.Disconnect();
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
