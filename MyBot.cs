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
using System.Timers;

namespace Superbot
{
    class MyBot
    {
        DiscordClient discord;
        CommandService commands;

        Random rand;

        string[] lol;
        string[] picture;
        string[] music;

        public static string commandPrefix = "%";
        public static int CommandsUsed = 0;
        public static DateTime MessageSent;
        public static DateTime StartupTime = DateTime.Now;
        public static int gpsCooldownInt = 0;

        public MyBot()
        {
            rand = new Random();

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
                //x.HelpMode = HelpMode.Public;
            });

            commands = discord.GetService<CommandService>();

            RegisterClearCommand();
            ReristerPictureCommand();
            Commands();
            Help();

            discord.ExecuteAndWait(async () =>
            {
                while (true)
                {
                    await discord.Connect("token", TokenType.Bot);
                    Console.WriteLine("Bot connected correctly");
                    discord.SetGame("%help for commands");
                    commands.CreateCommand("Playing")
                        .Alias(new string[] { "play" })
                        .Parameter("text", ParameterType.Unparsed)
                        .Do(async (e) =>
                        {
                            string text = e.Args[0];
                            await e.Channel.SendMessage("Your bot is now playing: " + text);
                            discord.SetGame(text);
                        });
                    break;
                }
            });

            discord.UserLeft += async (s, e) =>
            {
                var channel = e.Server.FindChannels("log", ChannelType.Text).FirstOrDefault();

                var user = e.User;

                await channel.SendMessage(string.Format("{0} has left the channel!", user.Name));
            };

            discord.UserBanned += async (s, e) =>
            {
                var channel = e.Server.FindChannels("log", ChannelType.Text).FirstOrDefault();

                var user = e.User;

                await channel.SendMessage(string.Format("{0} is banned from the channel!", user.Name));
            };

            discord.UserUnbanned += async (s, e) =>
            {
                var channel = e.Server.FindChannels("log", ChannelType.Text).FirstOrDefault();

                var user = e.User;
                
                await channel.SendMessage(string.Format("{0} is unbanned from the channel!", user.Name));
            };

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
                    await e.User.SendMessage("%dead - :skull_crossbones::dead::skull_crossbones:");
                });

            commands.CreateCommand("say")
                .Description("Make the bot say something")
                .Alias("s")
                .Parameter("text", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    string text = e.Args[0];
                    await e.Channel.SendMessage(text);
                });

            /*commands.CreateCommand("help")
                .Alias(new string[] { "h" })
                .Do(async (e) =>
                {
                    var name = e.User.Nickname != null ? e.User.Nickname : e.User.Name;

                    var helpList = new List<string>();

                    helpList.Add("```erlang");
                    helpList.Add("The prefix = %");
                    helpList.Add("%help 1 = commands");
                    helpList.Add("%help 2 = info");
                    helpList.Add("%help 3 = admin");
                    helpList.Add("%help 4 = ?");
                    helpList.Add("%help 5 = ?");
                    helpList.Add("```");

                    await e.Channel.SendMessage(name + " look in you inbox");
                    await e.User.SendMessage(string.Join("\n", helpList));
                });

            commands.CreateCommand("help 1")
                .Alias(new string[] { "h 1" })
                .Do(async (e) =>
                {
                    var helpList = new List<string>();

                    helpList.Add("```erlang");
                    helpList.Add("%help - help");
                    helpList.Add("%say - let the bot say someting");
                    helpList.Add("%hello - hi!");
                    helpList.Add("%bye bye - bye");
                    helpList.Add("%dead - :skull_crossbones::dead::skull_crossbones:");
                    helpList.Add("%nice - lol");
                    helpList.Add("```");

                    await e.User.SendMessage(string.Join("\n", helpList));
                });*/

            commands.CreateCommand("nice")
                .Do(async (e) =>
                {
                    var lol = new List<string>();

                    lol.Add("``");
                    lol.Add("lol");
                    lol.Add("lol");
                    lol.Add("lol");
                    lol.Add("lol");
                    lol.Add("lol");
                    lol.Add("lol");
                    lol.Add("lol");
                    lol.Add("lol");
                    lol.Add("lol");
                    lol.Add("``");

                    await e.Channel.SendMessage("lol");
                    await e.User.SendMessage(string.Join("\n", lol));
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

            commands.CreateCommand("roll")
                .Description("Rolls a die.")
                .Parameter("number", ParameterType.Optional)
                .Do(async e =>
                {
                    CommandsUsed++;
                    if (e.Message.Text.Contains($"{commandPrefix}roll "))
                    {
                        if (e.Message.Text.ToLower() == $"{commandPrefix}roll 10")
                        {
                            int dice = rand.Next(1, 11);
                            await e.Channel.SendMessage($"The ten-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 1")
                            await e.Channel.SendMessage($"The one-sided die rolled a... one! Wow, surprising!");
                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 2")
                        {
                            int dice = rand.Next(1, 3);
                            await e.Channel.SendMessage($"The two-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 3")
                        {
                            int dice = rand.Next(1, 4);
                            await e.Channel.SendMessage($"The three-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 4")
                        {
                            int dice = rand.Next(1, 5);
                            await e.Channel.SendMessage($"The four-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 5")
                        {
                            int dice = rand.Next(1, 6);
                            await e.Channel.SendMessage($"The five-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 6")
                        {
                            int dice = rand.Next(1, 7);
                            await e.Channel.SendMessage($"The six-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 7")
                        {
                            int dice = rand.Next(1, 8);
                            await e.Channel.SendMessage($"The seven-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 8")
                        {
                            int dice = rand.Next(1, 9);
                            await e.Channel.SendMessage($"The eight-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 9")
                        {
                            int dice = rand.Next(1, 10);
                            await e.Channel.SendMessage($"The nine-sided die rolled a... {dice}!");
                        }
                        //d10 is up above because reasons
                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 11")
                        {
                            int dice = rand.Next(1, 12);
                            await e.Channel.SendMessage($"The eleven-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 12")
                        {
                            int dice = rand.Next(1, 13);
                            await e.Channel.SendMessage($"The twelve-sided die rolled a... {dice}!");
                        }
                        else
                            await e.Channel.SendMessage($"You must pick a number between 1 and 12!");
                    }
                    else
                    {
                        int dice = rand.Next(1, 7);
                        await e.Channel.SendMessage($"The six-sided die rolled a... {dice}!");
                    }
                });

            commands.CreateCommand("gps")
                .Description("The GPS rant™")
                .Do(async e =>
                {
                    CommandsUsed++;
                    if (gpsCooldownInt == 0)
                    {
                        gpsCooldownInt = 15;
                        Timer gpsCooldownTimer = new Timer();
                        gpsCooldownTimer.Elapsed += new ElapsedEventHandler(gpsCooldown);
                        gpsCooldownTimer.Interval = 1000;
                        gpsCooldownTimer.Enabled = true;
                        await e.Channel.SendMessage($"ᵂᵃᶦᵗ, ᵗʰᶦˢ ᶦˢ ᵃ ˢᵉʳᶦᵒᵘˢ ᶦˢˢᵘᵉ⋅ ᴸᵉᵗ ᵐᵉ ʳᵃᶰᵗ ᵃᵇᵒᵘᵗ ᶦᵗ ᶠᵒʳ ᵃ ᵇᶦᵗ⋅ ﹡ᵃᶜʰᵉᵐ﹡⋅ \nᵂʰᵃᵗ ᶦˢ ᵃ ᴳᴾˢ﹖\nᵀʰᵉ ᴳᴾˢ ᶦˢ ᵃ ˢʸˢᵗᵉᵐ ᵗᵒ ᵉˢᵗᶦᵐᵃᵗᵉ ᶫᵒᶜᵃᵗᶦᵒᶰ ᵒᶰ ᵉᵃʳᵗʰ ᵇʸ ᵘˢᶦᶰᵍ ˢᶦᵍᶰᵃᶫˢ ᶠʳᵒᵐ ᵃ ˢᵉᵗ ᵒᶠ ᵒʳᵇᶦᵗᶦᶰᵍ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ… ᵒʳ ˢᵒ ᵗʰᵉʸ ˢᵃʸ⋅ ᴮᵘᵗ ʷʰᵃᵗ ᵗʰᵉʸ'ʳᵉ ᶰᵒᵗ ᵗᵉᶫᶫᶦᶰᵍ ᵘˢ ᶦˢ ᵗʰᵉ ʰᵃʳᵐ ᶦᵗ ᶜᵒᶰˢᵗᵃᶰᵗᶫʸ ᶜᵃᵘˢᵉˢ ʷᵒʳᶫᵈʷᶦᵈᵉ ᵉᵛᵉʳʸ ᵈᵃʸ⋅ ᴿᵉᶜᵉᶰᵗᶫʸ, ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ʰᵃᵛᵉ ᵇᵉᵉᶰ ᵇᵉᶦᶰᵍ ᵈᵉˢᶦᵍᶰᵉᵈ ˢᵒᶫᵉᶫʸ ᶠᵒʳ ᵗʰᵉ ᵖᵘʳᵖᵒˢᵉ ᵒᶠ ᵇᵒᵒˢᵗᶦᶰᵍ ᴳᴾˢ ᵖᵉʳᶠᵒʳᵐᵃᶰᶜᵉ, ᵇᵘᵗ ᵗʰᵉʸ ʰᵃᵛᵉ ᵇᵉᵉᶰ ᵇᵘᶦᶫᵗ ʷᶦᵗʰ ˢᵒ ᶫᶦᵗᵗᶫᵉ ᶜᵃʳᵉ ᵗʰᵃᵗ ᵗʰᵉ ᵖʳᵒᵇᶫᵉᵐˢ ᵃʳᵉ ᵉᵛᵉᶰ ʷᵒʳˢᵉ, ᵍᶦᵛᵉᶰ ᵗʰᵃᵗ ᵗʰᵉʸ ᵃʳᵉᶰ'ᵗ ʳᵉᶫʸᶦᶰᵍ ᵒᶰ ᶫᵃʳᵍᵉʳ ᵗʰᶦʳᵈ ᵖᵃʳᵗʸ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵃᶰʸᵐᵒʳᵉ⋅ ᴺᵒᵗ ᵒᶰᶫʸ ᶦˢ ᵗʰᵉ ᶰᵃᵛᶦᵍᵃᵗᶦᵒᶰ ʷᵒʳˢᵉ, ᵇᵘᵗ ᶦᵗ’ˢ ᵃᶫˢᵒ ʰᵃʳᵐᶦᶰᵍ ᵒᵘʳ ᵒᵘᵗ⁻ᵒᶠ⁻ᵒʳᵇᶦᵗ ᵉᶰᵛᶦʳᵒᶰᵐᵉᶰᵗ⋅ ˢᵖᵃᶜᵉ ᵈᵉᵇʳᶦˢ ʰᵃˢ ᵇᵉᵉᶰ ᵃ ᵖʳᵒᵇᶫᵉᵐ ᵉᵛᵉʳ ˢᶦᶰᶜᵉ ʷᵉ ˢᵗᵃʳᵗᵉᵈ ᶫᵃᵘᶰᶜʰᶦᶰᵍ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᶦᶰᵗᵒ ˢᵖᵃᶜᵉ, ᵒᶠ ᶜᵒᵘʳˢᵉ, ᵇᵘᵗ⋅⋅ ᵂᶦᵗʰ ᶰᵉʷ ‘ʲᵃᶰᶦᵗᵒʳ’ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵃᶰᵈ ᶫᵃˢᵉʳ ᵗᵉᶜʰᶰᵒᶫᵒᵍʸ, ʷᵉ’ᵛᵉ ᵇᵉᵉᶰ ᵃᵇᶫᵉ ᵗᵒ ᵏᵉᵉᵖ ᵗʰᵃᵗ ᵘᶰᵈᵉʳ ᶜᵒᶰᵗʳᵒᶫ⋅ ᴮᵘᵗ ʷʰᶦᶫᵉ ᵗʰᵉ ᵒᵛᵉʳᵃᶫᶫ ᵇᵘᶦᶫᵈ ᶠᵒʳ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵃʳᵉ ᵘˢᵘᵃᶫᶫʸ ˢᵗᵘʳᵈʸ ᵉᶰᵒᵘᵍʰ ᵗᵒ ʷᶦᵗʰˢᵗᵃᶰᵈ ˢᵖᵃᶜᶦᵒᵘˢ ᶜᵒᶰᵈᶦᵗᶦᵒᶰˢ ᶠᵒʳ ᵃ ᶫᵉᶰᵍᵗʰʸ ᵃᵐᵒᵘᶰᵗ ᵒᶠ ᵗᶦᵐᵉ, ᵗʰᵉˢᵉ ᴳᴾˢ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵃʳᵉ ʲᵘˢᵗ ᶫᵃᵘᶰᶜʰᶦᶰᵍ ᵖᶦᵉᶜᵉˢ ᵒᶠ ᵗʰᵉᶦʳ ᵒʷᶰ ᵈᵉᵇʳᶦˢ ᶦᶰᵗᵒ ˢᵖᵃᶜᵉ﹔ ˢᵗᶦᶫᶫ ᶦᶰ ᵒᵘʳ ᵒʳᵇᶦᵗ ᵇᵘᵗ ᵒᵘᵗ ᵒᶠ ᵒᵘʳ ᵃᵗᵐᵒˢᵖʰᵉʳᵉ, ʷʰᶦᶜʰ ᶦˢ ᵗᵃᵏᶦᶰᵍ ᵘˢ ᵃᵗ ᶫᵉᵃˢᵗ ᵗᵉᶰ ʸᵉᵃʳˢ ᵇᵃᶜᵏ ᶦᶰ ᵗᵉʳᵐˢ ᵒᶠ ˢᵖᵃᶜᵉ ᵗᵉᶜʰᶰᵒᶫᵒᵍʸ ᵈᵉᵛᵉᶫᵒᵖᵐᵉᶰᵗ⋅ ᴺᵒᵗ ᵒᶰᶫʸ ᵗʰᵃᵗ, ᵇᵘᵗ ᵗʰᵉ ᵐᵃᶦᶰ ᵖʳᵒᵇᶫᵉᵐ ʷᶦᵗʰ ˢᵖᵃᶜᵉ ᵈᵉᵇʳᶦˢ ᶦᶰ ᵍᵉᶰᵉʳᵃᶫ ᶦˢ ᵗʰᵃᵗ ᶦᵗ ᶦˢ ᶜᵃᵘˢᶦᶰᵍ ᵉˣᵗʳᵉᵐᵉ ᶫᵉᵛᵉᶫˢ ᵒᶠ ᶜᵒᶫᶫᶦˢᶦᵒᶰ ʷᶦᵗʰ ᵒᵗʰᵉʳ ᵛᶦᵗᵃᶫ ᵃᶰᵈ ᵉˣᵖᵉᶰˢᶦᵛᵉ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵗʰᵃᵗ ᵃʳᵉ ᵘˢᵉᵈ ᶠᵒʳ ᶦᵐᵖᵒʳᵗᵃᶰᵗ ᵃᶰᵈ ᶦᶰ ᵍᵉᶰᵉʳᵃᶫ ʰᵃᵛᵉ ᵃ ˢʷᵉᶫᶫ ᶦᶰᵗᵉᶰᵗᶦᵒᶰ ᵍᵒᶦᶰᵍ ᵇʸ ᵗʰᵉᶦʳ ᵃᶜᵗᶦᵛᶦᵗᶦᵉˢ⋅ ᵀʰᵉʳᵉᶠᵒʳᵉ ᵗʰᵉˢᵉ ᴳᴾˢ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ, ʷʰᶦᶫᵉ ᵃᶫʳᵉᵃᵈʸ ᵇᵉᶦᶰᵍ ᵃᵇʰᵒʳʳᵉᶰᵗ ᵉᶰᵒᵘᵍʰ, ᵃʳᵉ ʳᵉᵈᵘᶜᶦᶰᵍ ᵗʰᵉ ᑫᵘᵃᶫᶦᵗʸ ᵒᶠ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵃᶜᵗᵘᵃᶫᶫʸ ᵐᵃᵈᵉ ʷᶦᵗʰ ᵃ ˢᶦᶰᵍᶫᵉ ᵒᵘᶰᶜᵉ ᵒᶠ ᶦᶰᵗᵉᵍʳᶦᵗʸ﹗ ᵀʰᵉʸ ᵃʳᵉ ᵐᵃᵏᶦᶰᵍ ᶫᶦᶠᵉ ʷᵒʳˢᵉ ᶠᵒʳ ᵉᵛᵉʳʸ ˢᶦᶰᵍᶫᵉ ᵖᵉʳˢᵒᶰ ᵘˢᶦᶰᵍ ᵃ ˢᵉʳᵛᶦᶜᵉ ᵖʳᵒᵛᶦᵈᵉᵈ ᵇʸ ᵃᶰʸ ᵒᶠ ᵗʰᵉˢᵉ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵗʰᵃᵗ ʷᵉʳᵉ ᶦᶰ ᶜᵒᶰᵗᵃᶜᵗ ʷᶦᵗʰ ᵗʰᵉᵐ⋅ ᴵ ˢᶦᵐᵖᶫᵉ ᶜᵃᶰ’ᵗ ᵘᶰᵈᵉʳˢᵗᵃᶰᵈ ʰᵒʷ ʷᵉ ᶜᵃᶰ ᵗᵒᶫᵉʳᵃᵗᵉ ᵗʰᵉˢᵉ ᵖᵉᵒᵖᶫᵉ⋅ ᴬᶫʳᶦᵍʰᵗ, ᶫᵉᵗ’ˢ ᵍᵉᵗ ᵇᵃᶜᵏ ᵗᵒ ᵗʰᵉ ᶰᶦᵗᵗʸ ᵍʳᶦᵗᵗʸ ᵒᶠ ᵗʰᶦˢ ᵗʰᶦᶰᵍ⋅ ᴬˢ ᴵ ʷᵃˢ ˢᵃʸᶦᶰᵍ ᵉᵃʳᶫᶦᵉʳ, ᶜᵒᶫᶫᶦˢᶦᵒᶰˢ ᵈᵉˢᵗʳᵒʸᵉᵈ ᵇᵒᵗʰ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵃᶰᵈ ᵗʰᵒˢᵉ ᵗʰᵃᵗ ʰᵃᵈ ᶜʳᵉᵃᵗᵉᵈ ᵃ ᶠᶦᵉᶫᵈ ᵒᶠ ᵈᵉᵇʳᶦˢ ᵗʰᵃᵗ ᵇᵉᶜᵒᵐᵉ ᵃ ᵈᵃᶰᵍᵉʳ ᵗᵒ ᵒᵗʰᵉʳ […]");
                    }
                    else
                        await e.Channel.SendMessage($"This command is currently on a cooldown. Please try again in {gpsCooldownInt} seconds.");
                });
        }

        private void Help()
        {
            commands.CreateGroup("help", cgb =>
            {
                cgb.CreateCommand("")
                .Alias(new string[] {""})
                .Do(async (e) =>
                {
                    var name = e.User.Nickname != null ? e.User.Nickname : e.User.Name;

                    var helpList = new List<string>();

                    helpList.Add("```");
                    helpList.Add("The prefix = %");
                    helpList.Add("%help 1 = commands");
                    helpList.Add("%help 2 = info");
                    helpList.Add("%help 3 = admin");
                    helpList.Add("%help 4 = ?");
                    helpList.Add("%help 5 = ?");
                    helpList.Add("```");

                    await e.Channel.SendMessage(name + " look in you inbox");
                    await e.User.SendMessage(string.Join("\n", helpList));
                });

                cgb.CreateCommand("1")
                    .Alias(new string[] { "1" })
                    .Do(async (e) =>
                    {
                        var helpList = new List<string>();

                        helpList.Add("```");
                        helpList.Add("This is the list of Commands");
                        helpList.Add(" ");
                        helpList.Add("%help - help");
                        helpList.Add("%say - let the bot say someting");
                        helpList.Add("%hello - hi!");
                        helpList.Add("%bye bye - bye");
                        helpList.Add("%dead - :skull_crossbones::dead::skull_crossbones:");
                        helpList.Add("%nice - lol");
                        helpList.Add("%roll + number up to 12 - roles a random number");
                        helpList.Add("```");

                        await e.User.SendMessage(string.Join("\n", helpList));
                    });

                cgb.CreateCommand("2")
                    .Alias(new string[] { "2" })
                    .Do(async (e) =>
                    {
                        var helpList = new List<string>();

                        helpList.Add("```");
                        helpList.Add("This is the list of info Commands");
                        helpList.Add(" ");
                        helpList.Add("%serverinfo - gives info about the server");
                        helpList.Add("%userinfo @user - gives info about a user in the server");
                        helpList.Add("%ping - test respond time");
                        helpList.Add("%uptime - says the time the bot is active");
                        helpList.Add("```");

                        await e.User.SendMessage(string.Join("\n", helpList));
                    });

                cgb.CreateCommand("3")
                .Alias(new string[] { "3" })
                .Do(async (e) =>
                {
                    var helpList = new List<string>();

                    helpList.Add("```");
                    helpList.Add("This is the of the admin commands");
                    helpList.Add(" ");
                    helpList.Add("%playing + game - sets the playing game of the bot");
                    helpList.Add("%clear / clr - clears the chat");
                    helpList.Add("```");

                    await e.User.SendMessage(string.Join("\n", helpList));
                });
            });
        }

        private static void gpsCooldown(object source, ElapsedEventArgs e)
        {
            if (gpsCooldownInt > 0)
                gpsCooldownInt--;
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine($"[{e.Source}] {e.Message}");
        }
    }
}
