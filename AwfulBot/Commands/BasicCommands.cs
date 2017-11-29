using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace AwfulBot.Commands
{
    public class BasicCommands
	{
		[Command("ping")]
		[Description("Check ping")]
		[Aliases("pong")]
		public async Task Ping(CommandContext ctx)
		{
			await ctx.TriggerTypingAsync();

			await ctx.RespondAsync($"Pong! Ping: {ctx.Client.Ping}ms");
		}
	}
}
