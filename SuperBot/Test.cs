using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.IO;
using System.Net;

namespace Superbot
{
    class Test
    {
        public static void testCommand(CommandService commands, DiscordClient discord)
        {
            commands.CreateCommand("test")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %test");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    /*using (var client = new WebClient())
                    {
                        await e.Channel.SendMessage("Finding random cat image...");
                        try
                        {
                            client.DownloadFile(e.Message.Attachments.ToString(), @"./cat.png");
                            await e.Channel.SendFile(@"./cat.png");
                            if (File.Exists(@"./cat.png"))
                            {
                                File.Delete(@"./cat.png");
                            }
                            if (File.Exists(@"./cat.gif"))
                            {
                                File.Delete(@"./cat.gif");
                            }
                        }
                        catch (Exception)
                        {
                            await e.Channel.SendMessage("An error occured :(");
                        }
                    }*/
                    await e.Channel.SendMessage("");
                    //await e.Channel.SendMessage("noting needs to be tested");
                });
        }
    }
}
