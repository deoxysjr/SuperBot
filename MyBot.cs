using Discord;
using Discord.Commands;
using Discord.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Superbot
{
    class MyBot
    { 
        DiscordClient discord;
        CommandService commands;

        Random rand;
        
        string[] randGameIndex;
        string[] lol;
        string[] picture;
        string[] music;

        public MyBot()
        {
            rand = new Random();
            
            randGameIndex = new string[]
            {
                "test"
            };

            music = new string[]
            {
                "https://www.youtube.com/watch?v=vFMA5oAXZy8",
                "https://www.youtube.com/watch?v=WPXOGR8Z5S4&t=1541s",
                "https://www.youtube.com/watch?v=B5Puhn0uRb4",
                "https://www.youtube.com/watch?v=L9r-aOQb3XM",
                "https://www.youtube.com/watch?v=bDtam1dO_xI"
            };

            lol = new string[]
            {
                "https://www.youtube.com/watch?v=bhBGFy7SKCc",
                "https://www.youtube.com/watch?v=eCkho9tAhQo",
                "https://www.youtube.com/watch?v=2552wmwL3i0",
                "https://www.youtube.com/watch?v=YwKfUoJ_ERY"
            };

            picture = new string[]
            {
                "afbeeldingen/afbeelding1.jpg", //0
                "afbeeldingen/afbeelding2.jpg", //1
                "afbeeldingen/afbeelding3.jpg", //2
                "afbeeldingen/afbeelding4.jpg", //3
                "afbeeldingen/afbeelding5.jpg", //4
                "afbeeldingen/afbeelding6.jpg", //5
                "afbeeldingen/afbeelding7.jpg", //6
                "afbeeldingen/afbeelding8.jpg"  //7
            };

            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '%';
                x.AllowMentionPrefix = true;
            });

            commands = discord.GetService<CommandService>();

            RegisterMemeCommand();
            RegisterClearCommand();
            ReristerPictureCommand();
            Commands();

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("Token", TokenType.Bot);
            });
            
            commands.CreateCommand("Playing")
                .Do(async (e) =>
                {
                    int randGame = rand.Next(randGameIndex.Length);
                    string message = randGameIndex[randGame];
                    await e.Channel.SendMessage("Now playing" + message);
                    discord.SetGame(message);
                });

            discord.UserBanned += async (s, e) => //banned
            {
                var logChannel = e.Server.FindChannels("#logs").FirstOrDefault();
                await logChannel.SendMessage($"User Banned: {e.User.Name}");
            };

            discord.UserUnbanned += async (s, e) => //banned
            {
                var logChannel = e.Server.FindChannels("logs").FirstOrDefault();
                await logChannel.SendMessage($"User UnBanned: {e.User.Name}");
            };

            discord.UserLeft += async (s, e) => //left or kickt
            {
                var logChannel = e.Server.FindChannels("logs").FirstOrDefault();
                await logChannel.SendMessage($"User left or kickt: {e.User.Name}");
            };
        }

        private void RegisterMemeCommand()
        {
            
        }

        private void RegisterClearCommand()
        {
            commands.CreateCommand("clear")
                .Alias(new string[] {"clr"})
                .Do(async (e) =>
                {
                    Message[] messagesToDelete;
                    messagesToDelete = await e.Channel.DownloadMessages(100);
                    await e.Channel.DeleteMessages(messagesToDelete);
                });

            commands.CreateCommand("clear 10")
                .Alias(new string[] {"clr 10"})
                .Do(async (e) =>
                {
                    Message[] messagesToDelete;
                    messagesToDelete = await e.Channel.DownloadMessages(10);
                    await e.Channel.DeleteMessages(messagesToDelete);
                });
        }

        private void ReristerPictureCommand()
        {
            commands.CreateCommand("picture")
                .Do(async (e) =>
                {
                    int randomPictueIndex = rand.Next(picture.Length);
                    string pictureToPost = picture[randomPictueIndex];
                    await e.Channel.SendFile(pictureToPost);
                });
                
        }

        private void Commands()
        {
            commands.CreateCommand("bye bye")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage(":wave::skin-tone-1: Bye Bye! :wave::skin-tone-1:");
                });

            commands.CreateCommand("hello")
                .Alias(new string[] { "hi" })
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("hi!");
                });

            commands.CreateCommand("dead")
                .Alias(new string[] { ":skull_crossbones:" })
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage(":skull_crossbones: :knife: kill your self :knife: :skull_crossbones:");
                });

            commands.CreateCommand("NL")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage(":flag_nl: + :flag_nl: + :flag_nl:");
                    await e.Channel.SendMessage(":flag_nl: +-+NL+-+ :flag_nl:");
                    await e.Channel.SendMessage(":flag_nl: + :flag_nl: + :flag_nl:");
                    await e.Channel.SendMessage(":flag_nl: +-+NL+-+ :flag_nl:");
                    await e.Channel.SendMessage(":flag_nl: + :flag_nl: + :flag_nl:");
                });

            commands.CreateCommand("happy")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage(":grinning: :joy: :grinning: :joy: :grinning: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :joy: ");
                });

            commands.CreateCommand("lol")
                .Do(async (e) =>
                 {
                     await e.Channel.SendMessage("https://www.youtube.com/watch?v=bhBGFy7SKCc");
                 });

            commands.CreateCommand("lol lol")
                .Do(async (e) =>
                {
                    int randomVideoIndex = rand.Next(lol.Length);
                    string videoToPost = lol[randomVideoIndex];
                    await e.Channel.SendMessage(videoToPost);
                });

            commands.CreateCommand("music")
                .Do(async (e) =>
                {
                    int randomMusicIndex = rand.Next(music.Length);
                    string musicToPost = music[randomMusicIndex];
                    await e.Channel.SendMessage(musicToPost);
                });

            commands.CreateCommand("hi")
                .Do(async (e) =>
                {
                    await e.User.SendMessage("hi");
                });

            commands.CreateCommand("")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage(e.User.Mention + " look in your personal messages");
                    await e.User.SendMessage("here are all the commands");
                    await e.User.SendMessage("%hello - hi!");
                    await e.User.SendMessage("%bye bye - bye");
                    await e.User.SendMessage("%clear - clears 100 messages");
                    await e.User.SendMessage("%dead - :skull_crossbones:dead:skull_crossbones:");
                });

            commands.CreateCommand("help")
                .Alias(new string[] {"h"})
                .Do(async (e) =>
                {
                    var helpList = new List<string>();

                    helpList.Add("```erlang");
                    helpList.Add("The prefix = %");
                    helpList.Add("");
                    helpList.Add("");
                    helpList.Add("");
                    helpList.Add("");
                    helpList.Add("");
                    helpList.Add("```");

                    await e.User.SendMessage(string.Join("\n", helpList));
                });

            commands.CreateCommand("nice")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("lol");
                    await e.User.SendMessage("lol");
                    await e.User.SendMessage("lol");
                    await e.User.SendMessage("lol");
                    await e.User.SendMessage("lol");
                });

            commands.CreateCommand("serverinfo")
                .Description("Get info about this server.")
                .Do(async e =>
                    {
                        var infomsg = new List<string>();

                        infomsg.Add("```erlang");
                        infomsg.Add("  Server: " + e.Server.Name);
                        infomsg.Add("      Id: " + e.Server.Id);
                        infomsg.Add("  Region: " + e.Server.Region.Name);
                        infomsg.Add("   Users: " + e.Server.UserCount);
                        infomsg.Add($"Channels: ({e.Server.TextChannels.Count()})text " +
                                              $"({e.Server.VoiceChannels.Count()})voice " +
                                              $"({e.Server.TextChannels.Count(x => x.Users.Count() < e.Server.Users.Count())})hidden");
                        infomsg.Add($"   Owner: {e.Server.Owner.Name}#{e.Server.Owner.Discriminator}");
                        infomsg.Add("    Icon: " + e.Server.IconUrl);
                        infomsg.Add("   Roles: " + string.Join(", ", e.Server.Roles.Where(x => !x.Name.Contains("@everyone"))));
                        infomsg.Add("```");

                        await e.Channel.SendMessage(string.Join("\n", infomsg));
                    });

            commands.CreateCommand("userinfo")
                    .Description("Get info about the (optionally) specified user.")
                    .Parameter("user", ParameterType.Unparsed)
                    .Do(async e =>
                    {
                    var userRoles = e.User.Roles;

                        if (userRoles.Any(input => input.Name.ToUpper() == "ADMIN"))
                        {
                            User u = null;
                            string findUser = e.Args[0];
                            if (!string.IsNullOrWhiteSpace(findUser))
                            {
                                if (e.Message.MentionedUsers.Count() == 1)
                                    u = e.Message.MentionedUsers.FirstOrDefault();
                                else if (e.Server.FindUsers(findUser).Any())
                                    u = e.Server.FindUsers(findUser).FirstOrDefault();
                                else
                                    await e.Channel.SendMessage($"I was unable to find a user like `{findUser}`");
                            }
                            else
                            {
                                u = e.User;
                            }

                            var infomsg = new List<string>();

                            infomsg.Add("```erlang");

                            if (!string.IsNullOrWhiteSpace(u.Nickname))
                            {
                                infomsg.Add($"       Nick: {u.Nickname}");
                            }

                            infomsg.Add($"       Name: {u.Name}#{u.Discriminator}");
                            infomsg.Add($"         Id: {u.Id}");

                            if (u.CurrentGame != null)
                            {
                                if (!string.IsNullOrWhiteSpace(u.CurrentGame.Value.Url))
                                {
                                    infomsg.Add($"  Streaming: {u.CurrentGame.Value.Name} at {u.CurrentGame.Value.Url}");
                                }
                                else
                                {
                                    infomsg.Add($"    Playing: {u.CurrentGame.Value.Name}");
                                }
                            }

                            if (u.JoinedAt != null)
                            {
                                var jspan = DateTime.Now - u.JoinedAt;
                                infomsg.Add($"     Joined: {Math.Round(jspan.TotalDays, 1)} days ago ({u.JoinedAt.ToUniversalTime()})");
                            }
                            if (u.LastActivityAt != null)
                            {
                                var aspan = DateTime.Now - DateTime.Parse(u.LastActivityAt.ToString());
                                infomsg.Add($"Last Active: {Math.Round(aspan.TotalDays, 1)} days ago ({DateTime.Parse(u.LastActivityAt.ToString()).ToUniversalTime()})");
                            }
                            if (u.LastOnlineAt != null)
                            {
                                var ospan = DateTime.Now - DateTime.Parse(u.LastOnlineAt.ToString());
                                infomsg.Add($"Last Online: {Math.Round(ospan.TotalDays, 1)} days ago ({DateTime.Parse(u.LastOnlineAt.ToString()).ToUniversalTime()})");
                            }

                            infomsg.Add($"       Icon: {u.AvatarUrl}");
                            infomsg.Add($"      Roles: {string.Join(", ", u.Roles.Where(x => !x.Name.Contains("@everyone")))}");
                            infomsg.Add("```");

                            await e.Channel.SendMessage(string.Join("\n", infomsg));
                        }
                        else
                        {
                            await e.Channel.SendMessage("You don't have the admin permission for this command!");
                        }
                    });
                    
                }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine($"[{e.Source}] {e.Message}");
        }
    }
}
