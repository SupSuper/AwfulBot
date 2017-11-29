using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AwfulBot.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace AwfulBot
{
    public class BotClient
	{
		private BotConfig config;

		private DiscordClient discord;
		private CommandsNextModule commands;

	    public BotClient()
	    {
			// Load config
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

			// Set up commands
		    commands = discord.UseCommandsNext(new CommandsNextConfiguration()
		    {
				StringPrefix = config.CommandPrefix
		    });
			
		    commands.RegisterCommands<BasicCommands>();
		    commands.RegisterCommands<AdminCommands>();

		    // Set up error handlers
		    discord.Ready += (e) =>
		    {
			    e.Client.DebugLogger.LogMessage(LogLevel.Info, "AwfulBot", "Client started successfully.", DateTime.Now);
			    return Task.CompletedTask;
		    };
		    discord.ClientErrored += (e) =>
		    {
			    e.Client.DebugLogger.LogMessage(LogLevel.Error, "AwfulBot",
				    $"Exception occured: {e.Exception.GetType()}: {e.Exception.Message}", DateTime.Now);
			    return Task.CompletedTask;
		    };
		    commands.CommandExecuted += (e) =>
		    {
			    e.Context.Client.DebugLogger.LogMessage(LogLevel.Info, "AwfulBot",
				    $"{e.Context.User.Username} executed '{e.Command.QualifiedName}'", DateTime.Now);
			    return Task.CompletedTask;
		    };
		    commands.CommandErrored += (e) =>
		    {
			    e.Context.Client.DebugLogger.LogMessage(LogLevel.Error, "AwfulBot",
				    $"{e.Context.User.Username} tried executing '{e.Command?.QualifiedName ?? "<unknown command>"}' but it failed: {e.Exception.GetType()}: {e.Exception.Message ?? "<no message>"}",
				    DateTime.Now);
			    return Task.CompletedTask;
		    };

			// Disconnect on exit
			AppDomain.CurrentDomain.ProcessExit += async (sender, e) =>
		    {
			    await this.Stop();
		    };
		}

	    public async Task Start()
		{
			await discord.ConnectAsync();
		}

	    public async Task Stop()
		{
			await discord.DisconnectAsync();
		}
    }
}
