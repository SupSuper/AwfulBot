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

		[Command("quit")]
		[Description("Disconnect and exit bot")]
		[Aliases("kill")]
		public async Task Quit(CommandContext ctx)
		{
			await ctx.Client.DisconnectAsync();
			Environment.Exit(0);
		}
	}
}