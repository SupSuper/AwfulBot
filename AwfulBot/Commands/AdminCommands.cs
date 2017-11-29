using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace AwfulBot.Commands
{
	[Group("admin")]
	[Description("Administrative commands.")]
	[Hidden]
	[RequirePermissions(Permissions.ManageGuild)]
	public class AdminCommands
	{
		[Command("restart")]
		[Description("Reconnect to Discord")]
		public async Task Restart(CommandContext ctx)
		{
			await ctx.Client.ReconnectAsync();
		}

		[Command("reload")]
		[Description("Reload config")]
		public async Task ReloadConfig(CommandContext ctx)
		{
			await ctx.TriggerTypingAsync();

			var bot = ctx.Dependencies.GetDependency<BotClient>();
			bot.LoadConfig();

			await ctx.RespondAsync("Reload successful");
		}

		[Command("quit")]
		[Description("Disconnect and exit bot")]
		[Aliases("kill")]
		public async Task Quit(CommandContext ctx)
		{
			var bot = ctx.Dependencies.GetDependency<BotClient>();
			await bot.Stop();
			Environment.Exit(0);
		}
	}
}