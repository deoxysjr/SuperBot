using System;
using Discord;
using System.Collections.Generic;

namespace Superbot
{
    internal class commandhelp
    {
        internal static void Commandhelp(DiscordClient discord)
        {
            discord.MessageReceived += async (s, e) =>
            {
                var helpPrefix = "!.help";

                if (!e.User.IsBot)
                {
                    if(e.Message.Text.ToLower() == $"{helpPrefix} test")
                    {
                        var helpList = new List<string>();

                        helpList.Add("`usage`            : %test");
                        helpList.Add("`alias`            : this command has no alias");
                        helpList.Add("`description`: test commands");

                        await e.Channel.SendMessage(string.Join("\n", helpList));
                    }

                    if (e.Message.Text.ToLower() == $"{helpPrefix} test2")
                    {
                        var helpList = new List<string>();

                        helpList.Add("```");
                        helpList.Add("test");
                        helpList.Add("test");
                        helpList.Add("```");

                        await e.Channel.SendMessage(string.Join("\n", helpList));
                    }
                }
            };
        } 
    }
}