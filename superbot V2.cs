using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superbot1
{
    public class DiscordBot
    {
        DiscordClient client;
        CommandService commands;

        public static DateTime StartupTime = DateTime.Now;
        public static DateTime MessageSent;
        public static string botprefix = "!h.";

        public DiscordBot()
        {
            client = new DiscordClient(input =>
            {
                input.LogLevel = LogSeverity.Info;
                input.LogHandler = Log;
            });

            client.UsingCommands(input =>
            {
                input.PrefixChar = '$';
                input.AllowMentionPrefix = true;
            });

            commands = client.GetService<CommandService>();

            Announcements();
            Commands();
            Help();
            bot();

            client.ExecuteAndWait(async () =>
            {
                await client.Connect("Token", TokenType.Bot);
                client.SetGame("lol");
                commands.CreateCommand("Playing")
                        .Alias(new string[] { "play" })
                        .Parameter("text", ParameterType.Unparsed)
                        .Do(async (e) =>
                        {
                            string text = e.Args[0];
                            await e.Channel.SendMessage("Your bot is now playing: " + text);
                            client.SetGame(text);
                        });
            });

        }

        private void Announcements()
        {
            client.UserJoined += async (s, e) =>
            {
                var channel = e.Server.FindChannels("log", ChannelType.Text).FirstOrDefault();

                var user = e.User;

                var log = new List<string>();
                log.Add(" ");
                log.Add("log Entry");
                log.Add(" ");
                log.Add($"{user} joined at: {DateTime.Now.ToLongDateString()}, {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}:{DateTime.Now.Millisecond}");
                log.Add($"{user} id: {e.User.Id}");
                log.Add($"{e.User.Name} is bot: {e.User.IsBot}");
                log.Add("----------------------------------------");

                var path = $@"D:\Super Bot\users\{e.User.Name}.txt";
                File.AppendAllText(path, $"{string.Join("\r\n", log)}");

                await channel.SendMessage(string.Format("{0} has joined the channel!", user.Name));
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(e.User.Name + " has joined the server");
                AddDefoultRoleToUser(e);

                File.AppendAllText("Memberslist.csv", $"{DateTime.Now},{user.Name},{user.IsBot},{user.Roles.ToString()}" + Environment.NewLine);
            };

            client.UserLeft += async (s, e) =>
            {
                var channel = e.Server.FindChannels("log", ChannelType.Text).FirstOrDefault();

                var user = e.User;

                var log = new List<string>();
                log.Add(" ");
                log.Add($"{e.User.Name} joined at {e.User.JoinedAt.ToLongDateString()}, {e.User.JoinedAt.AddHours(2).Hour}:{e.User.JoinedAt.Minute}:{e.User.JoinedAt.Second}:{e.User.JoinedAt.Millisecond}");
                log.Add($"{e.User.Name} left at: {DateTime.Now.ToLongDateString()}, {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}:{DateTime.Now.Millisecond}");
                log.Add($"{e.User.Name}'s avatar {e.User.AvatarUrl}");
                log.Add($"{e.User.Name} is bot: {e.User.IsBot}");
                log.Add("----------------------------------------");

                var path = $@"D:\Super Bot\users\{e.User.Name}.txt";
                File.AppendAllText(path, $"{string.Join("\r\n", log)}");

                await channel.SendMessage(string.Format("{0} has left the channel!", user.Name));

            };

            client.UserBanned += async (s, e) =>
            {
                var channel = e.Server.FindChannels("log", ChannelType.Text).FirstOrDefault();

                var user = e.User;

                await channel.SendMessage(string.Format("{0} is banned from the channel!", user.Name));
            };

            client.UserUnbanned += async (s, e) =>
            {
                var channel = e.Server.FindChannels("log", ChannelType.Text).FirstOrDefault();

                var user = e.User;

                await channel.SendMessage(string.Format("{0} is unbanned from the channel!", user.Name));
           };
        }

        private static async void AddDefoultRoleToUser(UserEventArgs e)
        {
            foreach (Role role in e.Server.Roles)
            {
                if (role.Name == "NOOB")
                {
                    if (!e.User.HasRole(role))
                    {
                        Role[] rolesToAdd = new Role[1];
                        rolesToAdd[0] = role;
                        await e.User.AddRoles(rolesToAdd);
                        Console.WriteLine(e.User.Name + " has been granted the role: " + role);
                    }
                }
            }
        }

        private void Commands()
        {
            commands.CreateCommand("MakeFile")
                .Parameter("file", ParameterType.Unparsed)
                .Do(async (e) =>
                {

                    var path = $@"C:\Users\deoxysjr\Documents\Visual Studio 2015\Projects\Superbot1\Superbot1\bin\Debug\file\{e.Args[0]}.txt";
                    File.AppendText(path);
                    await e.Channel.SendMessage("The file has been made");
                });

            commands.CreateCommand("sendlog")
                .Parameter("line", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("here you have it");
                    await e.Channel.SendFile($@"D:\Super Bot\users\{e.Args[0]}.txt");
                });

            commands.CreateCommand("announce")
                .Alias(new string[] {"an"})
                .Parameter("message", ParameterType.Multiple).Do(async (e) =>
            {
                await DoAnnouncement(e);
                
            });
            commands.CreateCommand("hello")
                .Do(async (e) =>
                {
                    var userRoles = e.User.Roles;

                    if (userRoles.Any(input => input.Name.ToUpper() == "ADMIN"))
                    {
                        var channel = e.Server.FindChannels("log", ChannelType.Text).FirstOrDefault();
                        await e.Channel.SendMessage("hello");
                    }
                    else
                    {
                        await e.Channel.SendMessage("you dont have the permision");
                    }
                });

            commands.CreateCommand("ping")
                .Do(async (e) =>
                {
                    var ping = e.Message.Timestamp - MessageSent;
                    await e.Channel.SendMessage($"Responded in " + (ping.Seconds) + "." + (ping.Milliseconds) + " seconds.");
                });

            commands.CreateCommand("uptime")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage($"the bot is now {DateTime.Now - StartupTime} active");
                });
        }

        private async Task DoAnnouncement(CommandEventArgs e)
        {
            var channel = e.Server.FindChannels(e.Args[0], ChannelType.Text).FirstOrDefault();
            
            var message = ConstructMessage(e, channel != null);

            var userRoles = e.User.Roles;

            if (userRoles.Any(input => input.Name.ToUpper() == "ADMIN"))
            {
                if (channel != null)
                {
                    await channel.SendMessage(message);
                }
                else
                {
                    await e.Channel.SendMessage(message);
                }

            }
            else
            {
                await e.Channel.SendMessage("You don't have the admin permission for this command!");
            }
        }

        private string ConstructMessage(CommandEventArgs e, bool firstArgIsChannel)
        {
            string message = "";

            var name = e.User.Nickname != null ? e.User.Nickname : e.User.Name;

            var startIndex = firstArgIsChannel ? 1 : 0;

            for(int i = startIndex; i < e.Args.Length; i++)
            {
                message += e.Args[i].ToString() + " ";
            }

            var result = name + " says: " + message;

            return result;
        }

        private void Help()
        {
            commands.CreateCommand("help")
                .Alias(new string[] { "h" })
                .Do(async (e) =>
                {
                    var name = e.User.Nickname != null ? e.User.Nickname : e.User.Name;

                    var helpList = new List<string>();

                    helpList.Add("```erlang");
                    helpList.Add("The prefix = %");
                    helpList.Add(" ");
                    helpList.Add("1 = commands");
                    helpList.Add("2 = info");
                    helpList.Add("3 = ?");
                    helpList.Add("4 = ?");
                    helpList.Add("```");

                    await e.Channel.SendMessage(name);
                    await e.User.SendMessage(string.Join("\n", helpList));
                });
        }

        private void bot()
        {
            client.MessageReceived += async (s, e) =>
            {
                if (!e.User.IsBot)
                {
                    if(e.Message.Text.ToLower() == $"{botprefix}hallo")
                    {
                        await e.Channel.SendMessage($"id lol: {e.User.IsBot}");
                    }
                }
            };
        }

        private void Log(object sender, LogMessageEventArgs e)
        {

            if(e.Severity == LogSeverity.Info)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{e.Source}] {e.Message}");
            }
            
        }
    }
}
