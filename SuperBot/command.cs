using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Discord;
using Discord.Commands;
using System.Timers;
using System.Net;
using System.Threading.Tasks;

namespace Superbot
{
    class command
    {
        internal static void Commands(CommandService commands, DiscordClient discord)
        {
            Random rand;

            string[] gifs;
            string[] funny;
            string[] music;
            string[] lol;
            string[] picture;

            gifs = new string[]
            {
                "./Gif's/gif1.gif", "./Gif's/gif2.gif", "./Gif's/gif3.gif", "./Gif's/gif4.gif", "./Gif's/gif5.gif", "./Gif's/gif6.gif", "./Gif's/gif7.gif",
                "./Gif's/gif8.gif", "./Gif's/gif9.gif", "./Gif's/gif10.gif", "./Gif's/gif11.gif",
            };

            funny = new string[]
            {
                "./funny picture/funny1.jpg", "./funny picture/funny2.jpg", "./funny picture/funny3.jpg", "./funny picture/funny4.jpg", "./funny picture/funny5.jpg", "./funny picture/funny6.jpg",
                "./funny picture/funny7.jpg", "./funny picture/funny8.jpg", "./funny picture/funny9.jpg", "./funny picture/funny10.jpg", "./funny picture/funny11.jpg", "./funny picture/funny12.jpg",
                "./funny picture/funny13.jpg", "./funny picture/funny14.jpg", "./funny picture/funny15.jpg", "./funny picture/funny16.jpg", "./funny picture/funny17.jpg", "./funny picture/funny18.jpg",
                "./funny picture/funny19.jpg", "./funny picture/funny20.jpg", "./funny picture/funny21.jpg", "./funny picture/funny22.jpg", "./funny picture/funny23.jpg", "./funny picture/funny24.jpg",
                "./funny picture/funny25.jpg", "./funny picture/funny26.jpg", "./funny picture/funny27.jpg", "./funny picture/funny28.jpg"
            };

            music = new string[]
            {
                "https://www.youtube.com/watch?v=vFMA5oAXZy8", "https://www.youtube.com/watch?v=WPXOGR8Z5S4&t=1541s", "https://www.youtube.com/watch?v=B5Puhn0uRb4",
                "https://www.youtube.com/watch?v=L9r-aOQb3XM", "https://www.youtube.com/watch?v=bDtam1dO_xI"
            };

            lol = new string[]
            {
                "https://www.youtube.com/watch?v=bhBGFy7SKCc", "https://www.youtube.com/watch?v=eCkho9tAhQo", "https://www.youtube.com/watch?v=2552wmwL3i0",
                "https://www.youtube.com/watch?v=YwKfUoJ_ERY", "http://imgur.com/V06OE0B"
            };

            picture = new string[]
            {
                "afbeeldingen/afbeelding1.jpg", "afbeeldingen/afbeelding2.jpg", "afbeeldingen/afbeelding3.jpg", "afbeeldingen/afbeelding4.jpg", "afbeeldingen/afbeelding5.jpg",
                "afbeeldingen/afbeelding6.jpg", "afbeeldingen/afbeelding7.jpg", "afbeeldingen/afbeelding8.jpg"
            };

            rand = new Random();

            //
            //                         Commands
            //

            commands.CreateCommand("Playing")
                .Alias(new string[] { "play" })
                .Parameter("text", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    CommandUsed.CommandAdd();

                    if (e.User.Id == "ID")
                    {
                        string text = e.Args[0];
                        await e.Channel.SendMessage("Your bot is now playing: " + $"(**{text}**)");
                        discord.SetGame(text);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"{e.User} used %playing");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"Your bot is now playing: ({text})");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        await e.Channel.SendMessage($"You can't do that {e.User.Name}");
                    }
                });

            commands.CreateCommand("clear")
                .Alias(new string[] { "clr" })
                .Do(async (e) =>
                {
                    CommandUsed.CommandAdd();
                    Message[] messagesToDelete;
                    //Message[] messagesToDelete2;
                    messagesToDelete = await e.Channel.DownloadMessages(100);
                    await e.Channel.DeleteMessages(messagesToDelete);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %clear and deleted ({messagesToDelete.Count()}) messages");
                    Console.ForegroundColor = ConsoleColor.White;
                    await Task.Delay(20);
                    await e.Channel.SendMessage($":white_check_mark: I cleared the chat {e.User.Name} I deleted ({messagesToDelete.Count()}) messages");
                    //await Task.Delay(2000);
                    //messagesToDelete2 = await e.Channel.DownloadMessages(3);
                    //await e.Channel.DeleteMessages(messagesToDelete2);
                });

            commands.CreateCommand("clear 10")
                .Alias(new string[] { "clr 10" })
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %clear 10");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    Message[] messagesToDelete;
                    Message[] messagesToDelete2;
                    messagesToDelete = await e.Channel.DownloadMessages(10);
                    await e.Channel.DeleteMessages(messagesToDelete);
                    await Task.Delay(20);
                    await e.Channel.SendMessage($"I deleted 10 messages {e.User.Name}");
                    await Task.Delay(2000);
                    messagesToDelete2 = await e.Channel.DownloadMessages(1);
                    await e.Channel.DeleteMessages(messagesToDelete2);
                });

            commands.CreateCommand("cat")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %cat");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    using (var client = new WebClient())
                    {
                        await e.Channel.SendMessage("Finding random cat image...");
                        try
                        {
                            client.DownloadFile("http://thecatapi.com/api/images/get?format=src&type=png", @"./cat.png");
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
                    }
                });

            commands.CreateCommand("lol lol")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %lol lol");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    int randomVideoIndex = rand.Next(lol.Length);
                    string videoToPost = lol[randomVideoIndex];
                    await e.Channel.SendMessage(videoToPost);
                });

            commands.CreateCommand("music")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %music");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    int randomMusicIndex = rand.Next(music.Length);
                    string musicToPost = music[randomMusicIndex];
                    await e.Channel.SendMessage(musicToPost);
                });

            commands.CreateCommand("gif")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %gif");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    int randomGif = rand.Next(gifs.Length);
                    string GifToPost = gifs[randomGif];
                    await e.Channel.SendFile(GifToPost);
                });

            commands.CreateCommand("funny")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %funny");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    int randomFunny = rand.Next(funny.Length);
                    string funnyToPost = funny[randomFunny];
                    await e.Channel.SendFile(funnyToPost);
                });

            commands.CreateCommand("test")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %test");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    await e.Channel.SendMessage("test");
                });

            commands.CreateCommand("rollDice")
                .Description("Rolls a random Number between Min and Max")
                .Parameter("Min")
                .Parameter("Max")
                .Do(async e =>
                {
                    int awnser = rand.Next(int.Parse(e.Args[0]), int.Parse(e.Args[1]) + 1);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %rolldice and rolled {awnser}");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    if (e.Args[0] == "")
                    {
                        await e.Channel.SendMessage("You need a minimum number");
                    }
                    if (e.Args[1] == "")
                    {
                        await e.Channel.SendMessage("You need a maximun number");
                    }
                    else
                    {
                        await e.Channel.SendMessage($"[{awnser}]");
                    }
                });

            commands.CreateCommand("CommandsCount")
                .Alias(new string[] { "cc" })
                .Do(async (e) =>
                {
                    using (TextReader reader = File.OpenText(@"./file/commandsused.txt"))
                    {
                        string text = reader.ReadLine();
                        string[] bits = text.Split(' ');
                        int c = int.Parse(bits[0]);
                        await e.Channel.SendMessage($"the amount of commands = **{c}**");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %CommandsCount = ({c})");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                });

            commands.CreateCommand("CommandsCountClear")
                .Alias(new string[] { "ccclear" })
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %CommandsCountClear");
                    Console.ForegroundColor = ConsoleColor.White;
                    if (e.User.Id == "ID")
                    {
                        int x = 0;
                        File.WriteAllText(@"./file/commandsused.txt", x.ToString());
                        await e.Channel.SendMessage("CommandsUsed is set to 0");
                    }
                    else
                        await e.Channel.SendMessage("You don't have that permision");

                });

            commands.CreateCommand("Maketxt")
                .Parameter("file", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %Maketxt and made {e.Args[0]}");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    var path = $@"./file/{e.Args[0]}.txt";
                    if (File.Exists($@"./file/{e.Args[0]}.txt"))
                    {
                        await e.Channel.SendMessage($"sorry but {e.Args[0]} already exists");
                    }
                    else
                    {
                        File.CreateText(path);
                        await e.Channel.SendMessage($"The file {e.Args[0]}.txt has been made");
                    }
                });

            commands.CreateCommand("write")
                .Parameter("file", ParameterType.Required)
                .Parameter("message", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %write and wrote {e.Args[0]}");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    var path = $@"./file/{e.Args[0]}.txt";
                    if (e.Args[1] == null)
                        await e.Channel.SendMessage("you need text to put in the file");
                    else
                    {
                        File.AppendAllText(path, $"\r\n{e.Args[1]}");
                        await e.Channel.SendTTSMessage($"the file {e.Args[0]} has been edited");
                    }
                });

            commands.CreateCommand("send")
                .Parameter("path", ParameterType.Required)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %send and send {e.Args[0]}.txt");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    await e.Channel.SendFile($@"./file/{e.Args[0]}.txt");
                });

            commands.CreateCommand("cleartext")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %cleartext and cleared the file text.txt");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    var path = @"./file/text.txt";
                    File.WriteAllText(path, " ");
                    await e.Channel.SendMessage("text.txt is cleared");
                });

            commands.CreateCommand("bye bye")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %bye bye");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    await e.Channel.SendMessage($":wave::skin-tone-1: Bye Bye! {e.User.Name} :wave::skin-tone-1:");
                });

            commands.CreateCommand("hello")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %hello");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    await e.Channel.SendMessage($"hello! {e.User.Name}");
                });

            commands.CreateCommand("dead")
                .Alias(new string[] { ":skull_crossbones:" })
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %dead");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    await e.Channel.SendMessage(":skull_crossbones: :knife: kill your self :knife: :skull_crossbones:");
                });

            commands.CreateCommand("NL")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %nl");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    await e.Channel.SendMessage(":flag_nl: + :flag_nl: + :flag_nl:");
                    await e.Channel.SendMessage(":flag_nl: +-+NL+-+ :flag_nl:");
                    await e.Channel.SendMessage(":flag_nl: + :flag_nl: + :flag_nl:");
                    await e.Channel.SendMessage(":flag_nl: +-+NL+-+ :flag_nl:");
                    await e.Channel.SendMessage(":flag_nl: + :flag_nl: + :flag_nl:");
                });

            commands.CreateCommand("happy")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %happy");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    await e.Channel.SendMessage(":grinning: :joy: :grinning: :joy: :grinning: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :joy: ");
                });

            commands.CreateCommand("lol")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %lol");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    await e.Channel.SendMessage("https://www.youtube.com/watch?v=bhBGFy7SKCc");
                });

            commands.CreateCommand("hi")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %hi");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    await e.Channel.SendIsTyping();
                    await Task.Delay(500);
                    await e.Channel.SendMessage("hi!");
                });

            commands.CreateCommand("say")
                .Description("Make the bot say something")
                .Alias("s")
                .Parameter("text", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    CommandUsed.CommandAdd();
                    string text = e.Args[0];
                    await e.Channel.SendMessage(text);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %say and said {text}");
                    Console.ForegroundColor = ConsoleColor.White;
                });

            commands.CreateCommand("nice")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %nice haha noob");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    var lols = new List<string>();

                    lols.Add("``");
                    lols.Add("lol");
                    lols.Add("lol");
                    lols.Add("lol");
                    lols.Add("lol");
                    lols.Add("lol");
                    lols.Add("lol");
                    lols.Add("lol");
                    lols.Add("lol");
                    lols.Add("lol");
                    lols.Add("``");

                    await e.Channel.SendMessage("lol");
                    await e.User.SendMessage(string.Join("\n", lols));
                });

            commands.CreateCommand("serverinfo")
                .Description("Get info about this server.")
                .Do(async e =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %serverinfo");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
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
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %userinfo");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
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
                        await e.Channel.SendMessage("You don't have the admin permission for this command!" +
                            "\nYou need the role Admin for this command");
                    }
                });

            commands.CreateCommand("time")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %time");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    var MessageSent = e.Message.Timestamp;


                    MyBot.month();
                    MyBot.speciald();
                    MyBot.dayOfweek(e);

                    var time = new List<string>();

                    time.Add("```ini");
                    time.Add($"Year   = ({MessageSent.Year})");
                    time.Add($"Month  = ({MessageSent.Month}, {MyBot.Month})");
                    time.Add($"Day    = ({MyBot.dayofweek + " Or " + MessageSent.DayOfWeek}  - {MessageSent.Day})");
                    time.Add($"Hour   = ({MessageSent.Hour + 2})");
                    time.Add($"Minute = ({MessageSent.Minute})");
                    time.Add($"Is it a specialday today: {MyBot.spday}");
                    if (MyBot.spday == true)
                    {
                        time.Add("today is a specialday it is");
                        time.Add($"         ({MyBot.specialday})");
                        time.Add($"{MyBot.specialdaydescription}");
                    }
                    time.Add("```");

                    await e.Channel.SendMessage(string.Join("\n", time));
                });

            commands.CreateCommand("date")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %date");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    MyBot.MessageSent = e.Message.Timestamp;
                    await e.Channel.SendMessage($"``{MyBot.MessageSent.DayOfWeek} {MyBot.MessageSent.Day}-{MyBot.MessageSent.Month}-{MyBot.MessageSent.Year} ``");
                });

            commands.CreateCommand("uptime")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %uptime");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    await e.Channel.SendMessage($"the bot is now {DateTime.Now - MyBot.StartupTime} active");
                });

            commands.CreateCommand("roll") //create command
                .Description("Rolls a die.") //add description, it will be shown when *help is used
                .Parameter("number", ParameterType.Optional) //as an argument, we have a person we want to greet
                .Do(async e =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %roll");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    if (e.Message.Text.Contains($"{MyBot.commandPrefix}roll "))
                    {
                        if (e.Message.Text.ToLower() == $"{MyBot.commandPrefix}roll 10")
                        {
                            int dice = rand.Next(1, 11);
                            await e.Channel.SendMessage($"The ten-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{MyBot.commandPrefix}roll 1")
                            await e.Channel.SendMessage($"The one-sided die rolled a... one! Wow, surprising!");
                        else if (e.Message.Text.ToLower() == $"{MyBot.commandPrefix}roll 2")
                        {
                            int dice = rand.Next(1, 3);
                            await e.Channel.SendMessage($"The two-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{MyBot.commandPrefix}roll 3")
                        {
                            int dice = rand.Next(1, 4);
                            await e.Channel.SendMessage($"The three-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{MyBot.commandPrefix}roll 4")
                        {
                            int dice = rand.Next(1, 5);
                            await e.Channel.SendMessage($"The four-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{MyBot.commandPrefix}roll 5")
                        {
                            int dice = rand.Next(1, 6);
                            await e.Channel.SendMessage($"The five-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{MyBot.commandPrefix}roll 6")
                        {
                            int dice = rand.Next(1, 7);
                            await e.Channel.SendMessage($"The six-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{MyBot.commandPrefix}roll 7")
                        {
                            int dice = rand.Next(1, 8);
                            await e.Channel.SendMessage($"The seven-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{MyBot.commandPrefix}roll 8")
                        {
                            int dice = rand.Next(1, 9);
                            await e.Channel.SendMessage($"The eight-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{MyBot.commandPrefix}roll 9")
                        {
                            int dice = rand.Next(1, 10);
                            await e.Channel.SendMessage($"The nine-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{MyBot.commandPrefix}roll 10")
                        {
                            int dice = rand.Next(1, 11);
                            await e.Channel.SendMessage($"The nine-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{MyBot.commandPrefix}roll 11")
                        {
                            int dice = rand.Next(1, 12);
                            await e.Channel.SendMessage($"The eleven-sided die rolled a... {dice}!");
                        }
                        else if (e.Message.Text.ToLower() == $"{MyBot.commandPrefix}roll 12")
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
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %gps");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    if (MyBot.gpsCooldownInt == 0)
                    {
                        MyBot.gpsCooldownInt = 15;
                        Timer gpsCooldownTimer = new Timer();
                        gpsCooldownTimer.Elapsed += new ElapsedEventHandler(MyBot.gpsCooldown);
                        gpsCooldownTimer.Interval = 1000;
                        gpsCooldownTimer.Enabled = true;
                        await e.Channel.SendMessage($"ᵂᵃᶦᵗ, ᵗʰᶦˢ ᶦˢ ᵃ ˢᵉʳᶦᵒᵘˢ ᶦˢˢᵘᵉ⋅ ᴸᵉᵗ ᵐᵉ ʳᵃᶰᵗ ᵃᵇᵒᵘᵗ ᶦᵗ ᶠᵒʳ ᵃ ᵇᶦᵗ⋅ ﹡ᵃᶜʰᵉᵐ﹡⋅ \nᵂʰᵃᵗ ᶦˢ ᵃ ᴳᴾˢ﹖\nᵀʰᵉ ᴳᴾˢ ᶦˢ ᵃ ˢʸˢᵗᵉᵐ ᵗᵒ ᵉˢᵗᶦᵐᵃᵗᵉ ᶫᵒᶜᵃᵗᶦᵒᶰ ᵒᶰ ᵉᵃʳᵗʰ ᵇʸ ᵘˢᶦᶰᵍ ˢᶦᵍᶰᵃᶫˢ ᶠʳᵒᵐ ᵃ ˢᵉᵗ ᵒᶠ ᵒʳᵇᶦᵗᶦᶰᵍ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ… ᵒʳ ˢᵒ ᵗʰᵉʸ ˢᵃʸ⋅ ᴮᵘᵗ ʷʰᵃᵗ ᵗʰᵉʸ'ʳᵉ ᶰᵒᵗ ᵗᵉᶫᶫᶦᶰᵍ ᵘˢ ᶦˢ ᵗʰᵉ ʰᵃʳᵐ ᶦᵗ ᶜᵒᶰˢᵗᵃᶰᵗᶫʸ ᶜᵃᵘˢᵉˢ ʷᵒʳᶫᵈʷᶦᵈᵉ ᵉᵛᵉʳʸ ᵈᵃʸ⋅ ᴿᵉᶜᵉᶰᵗᶫʸ, ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ʰᵃᵛᵉ ᵇᵉᵉᶰ ᵇᵉᶦᶰᵍ ᵈᵉˢᶦᵍᶰᵉᵈ ˢᵒᶫᵉᶫʸ ᶠᵒʳ ᵗʰᵉ ᵖᵘʳᵖᵒˢᵉ ᵒᶠ ᵇᵒᵒˢᵗᶦᶰᵍ ᴳᴾˢ ᵖᵉʳᶠᵒʳᵐᵃᶰᶜᵉ, ᵇᵘᵗ ᵗʰᵉʸ ʰᵃᵛᵉ ᵇᵉᵉᶰ ᵇᵘᶦᶫᵗ ʷᶦᵗʰ ˢᵒ ᶫᶦᵗᵗᶫᵉ ᶜᵃʳᵉ ᵗʰᵃᵗ ᵗʰᵉ ᵖʳᵒᵇᶫᵉᵐˢ ᵃʳᵉ ᵉᵛᵉᶰ ʷᵒʳˢᵉ, ᵍᶦᵛᵉᶰ ᵗʰᵃᵗ ᵗʰᵉʸ ᵃʳᵉᶰ'ᵗ ʳᵉᶫʸᶦᶰᵍ ᵒᶰ ᶫᵃʳᵍᵉʳ ᵗʰᶦʳᵈ ᵖᵃʳᵗʸ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵃᶰʸᵐᵒʳᵉ⋅ ᴺᵒᵗ ᵒᶰᶫʸ ᶦˢ ᵗʰᵉ ᶰᵃᵛᶦᵍᵃᵗᶦᵒᶰ ʷᵒʳˢᵉ, ᵇᵘᵗ ᶦᵗ’ˢ ᵃᶫˢᵒ ʰᵃʳᵐᶦᶰᵍ ᵒᵘʳ ᵒᵘᵗ⁻ᵒᶠ⁻ᵒʳᵇᶦᵗ ᵉᶰᵛᶦʳᵒᶰᵐᵉᶰᵗ⋅ ˢᵖᵃᶜᵉ ᵈᵉᵇʳᶦˢ ʰᵃˢ ᵇᵉᵉᶰ ᵃ ᵖʳᵒᵇᶫᵉᵐ ᵉᵛᵉʳ ˢᶦᶰᶜᵉ ʷᵉ ˢᵗᵃʳᵗᵉᵈ ᶫᵃᵘᶰᶜʰᶦᶰᵍ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᶦᶰᵗᵒ ˢᵖᵃᶜᵉ, ᵒᶠ ᶜᵒᵘʳˢᵉ, ᵇᵘᵗ⋅⋅ ᵂᶦᵗʰ ᶰᵉʷ ‘ʲᵃᶰᶦᵗᵒʳ’ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵃᶰᵈ ᶫᵃˢᵉʳ ᵗᵉᶜʰᶰᵒᶫᵒᵍʸ, ʷᵉ’ᵛᵉ ᵇᵉᵉᶰ ᵃᵇᶫᵉ ᵗᵒ ᵏᵉᵉᵖ ᵗʰᵃᵗ ᵘᶰᵈᵉʳ ᶜᵒᶰᵗʳᵒᶫ⋅ ᴮᵘᵗ ʷʰᶦᶫᵉ ᵗʰᵉ ᵒᵛᵉʳᵃᶫᶫ ᵇᵘᶦᶫᵈ ᶠᵒʳ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵃʳᵉ ᵘˢᵘᵃᶫᶫʸ ˢᵗᵘʳᵈʸ ᵉᶰᵒᵘᵍʰ ᵗᵒ ʷᶦᵗʰˢᵗᵃᶰᵈ ˢᵖᵃᶜᶦᵒᵘˢ ᶜᵒᶰᵈᶦᵗᶦᵒᶰˢ ᶠᵒʳ ᵃ ᶫᵉᶰᵍᵗʰʸ ᵃᵐᵒᵘᶰᵗ ᵒᶠ ᵗᶦᵐᵉ, ᵗʰᵉˢᵉ ᴳᴾˢ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵃʳᵉ ʲᵘˢᵗ ᶫᵃᵘᶰᶜʰᶦᶰᵍ ᵖᶦᵉᶜᵉˢ ᵒᶠ ᵗʰᵉᶦʳ ᵒʷᶰ ᵈᵉᵇʳᶦˢ ᶦᶰᵗᵒ ˢᵖᵃᶜᵉ﹔ ˢᵗᶦᶫᶫ ᶦᶰ ᵒᵘʳ ᵒʳᵇᶦᵗ ᵇᵘᵗ ᵒᵘᵗ ᵒᶠ ᵒᵘʳ ᵃᵗᵐᵒˢᵖʰᵉʳᵉ, ʷʰᶦᶜʰ ᶦˢ ᵗᵃᵏᶦᶰᵍ ᵘˢ ᵃᵗ ᶫᵉᵃˢᵗ ᵗᵉᶰ ʸᵉᵃʳˢ ᵇᵃᶜᵏ ᶦᶰ ᵗᵉʳᵐˢ ᵒᶠ ˢᵖᵃᶜᵉ ᵗᵉᶜʰᶰᵒᶫᵒᵍʸ ᵈᵉᵛᵉᶫᵒᵖᵐᵉᶰᵗ⋅ ᴺᵒᵗ ᵒᶰᶫʸ ᵗʰᵃᵗ, ᵇᵘᵗ ᵗʰᵉ ᵐᵃᶦᶰ ᵖʳᵒᵇᶫᵉᵐ ʷᶦᵗʰ ˢᵖᵃᶜᵉ ᵈᵉᵇʳᶦˢ ᶦᶰ ᵍᵉᶰᵉʳᵃᶫ ᶦˢ ᵗʰᵃᵗ ᶦᵗ ᶦˢ ᶜᵃᵘˢᶦᶰᵍ ᵉˣᵗʳᵉᵐᵉ ᶫᵉᵛᵉᶫˢ ᵒᶠ ᶜᵒᶫᶫᶦˢᶦᵒᶰ ʷᶦᵗʰ ᵒᵗʰᵉʳ ᵛᶦᵗᵃᶫ ᵃᶰᵈ ᵉˣᵖᵉᶰˢᶦᵛᵉ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵗʰᵃᵗ ᵃʳᵉ ᵘˢᵉᵈ ᶠᵒʳ ᶦᵐᵖᵒʳᵗᵃᶰᵗ ᵃᶰᵈ ᶦᶰ ᵍᵉᶰᵉʳᵃᶫ ʰᵃᵛᵉ ᵃ ˢʷᵉᶫᶫ ᶦᶰᵗᵉᶰᵗᶦᵒᶰ ᵍᵒᶦᶰᵍ ᵇʸ ᵗʰᵉᶦʳ ᵃᶜᵗᶦᵛᶦᵗᶦᵉˢ⋅ ᵀʰᵉʳᵉᶠᵒʳᵉ ᵗʰᵉˢᵉ ᴳᴾˢ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ, ʷʰᶦᶫᵉ ᵃᶫʳᵉᵃᵈʸ ᵇᵉᶦᶰᵍ ᵃᵇʰᵒʳʳᵉᶰᵗ ᵉᶰᵒᵘᵍʰ, ᵃʳᵉ ʳᵉᵈᵘᶜᶦᶰᵍ ᵗʰᵉ ᑫᵘᵃᶫᶦᵗʸ ᵒᶠ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵃᶜᵗᵘᵃᶫᶫʸ ᵐᵃᵈᵉ ʷᶦᵗʰ ᵃ ˢᶦᶰᵍᶫᵉ ᵒᵘᶰᶜᵉ ᵒᶠ ᶦᶰᵗᵉᵍʳᶦᵗʸ﹗ ᵀʰᵉʸ ᵃʳᵉ ᵐᵃᵏᶦᶰᵍ ᶫᶦᶠᵉ ʷᵒʳˢᵉ ᶠᵒʳ ᵉᵛᵉʳʸ ˢᶦᶰᵍᶫᵉ ᵖᵉʳˢᵒᶰ ᵘˢᶦᶰᵍ ᵃ ˢᵉʳᵛᶦᶜᵉ ᵖʳᵒᵛᶦᵈᵉᵈ ᵇʸ ᵃᶰʸ ᵒᶠ ᵗʰᵉˢᵉ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵗʰᵃᵗ ʷᵉʳᵉ ᶦᶰ ᶜᵒᶰᵗᵃᶜᵗ ʷᶦᵗʰ ᵗʰᵉᵐ⋅ ᴵ ˢᶦᵐᵖᶫᵉ ᶜᵃᶰ’ᵗ ᵘᶰᵈᵉʳˢᵗᵃᶰᵈ ʰᵒʷ ʷᵉ ᶜᵃᶰ ᵗᵒᶫᵉʳᵃᵗᵉ ᵗʰᵉˢᵉ ᵖᵉᵒᵖᶫᵉ⋅ ᴬᶫʳᶦᵍʰᵗ, ᶫᵉᵗ’ˢ ᵍᵉᵗ ᵇᵃᶜᵏ ᵗᵒ ᵗʰᵉ ᶰᶦᵗᵗʸ ᵍʳᶦᵗᵗʸ ᵒᶠ ᵗʰᶦˢ ᵗʰᶦᶰᵍ⋅ ᴬˢ ᴵ ʷᵃˢ ˢᵃʸᶦᶰᵍ ᵉᵃʳᶫᶦᵉʳ, ᶜᵒᶫᶫᶦˢᶦᵒᶰˢ ᵈᵉˢᵗʳᵒʸᵉᵈ ᵇᵒᵗʰ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵃᶰᵈ ᵗʰᵒˢᵉ ᵗʰᵃᵗ ʰᵃᵈ ᶜʳᵉᵃᵗᵉᵈ ᵃ ᶠᶦᵉᶫᵈ ᵒᶠ ᵈᵉᵇʳᶦˢ ᵗʰᵃᵗ ᵇᵉᶜᵒᵐᵉ ᵃ ᵈᵃᶰᵍᵉʳ ᵗᵒ ᵒᵗʰᵉʳ […]");
                    }
                    else
                        await e.Channel.SendMessage($"This command is currently on a cooldown. Please try again in {MyBot.gpsCooldownInt} seconds.");
                });

            commands.CreateCommand("picture")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %picture");
                    Console.ForegroundColor = ConsoleColor.White;
                    CommandUsed.CommandAdd();
                    int randomPictueIndex = rand.Next(picture.Length);
                    string pictureToPost = picture[randomPictueIndex];
                    await e.Channel.SendFile(pictureToPost);
                });

            commands.CreateCommand("airlock")
                .Parameter("name", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    bool ja = true;
                    if (ja == true)
                    {
                        ulong id;
                        User u = null;
                        string findUser = e.Args[0];
                        if (!string.IsNullOrWhiteSpace(findUser))
                        {
                            if (e.Message.MentionedUsers.Count() == 1)
                                u = e.Message.MentionedUsers.FirstOrDefault();
                            else if (e.Server.FindUsers(findUser).Any())
                                u = e.Server.FindUsers(findUser).FirstOrDefault();
                            else if (ulong.TryParse(findUser, out id))
                                u = e.Server.GetUser(id);
                        }

                        if (u == null)
                        {
                            await e.Channel.SendMessage($"I was unable to find a user like `{findUser}`");
                            return;
                        }
                        if (u.id == "ID")
                        {
                            await e.Channel.SendMessage("Sorry I can't do it I won't airlock him");
                        }
                        else
                        {
                            await e.Channel.SendMessage(":warning: WARNING! Depressurization in progress WARNING! :warning:");
                            await u.Kick();
                            await e.Channel.SendMessage($"{u.Name} has been sucked into the vacuum of space");
                        }
                    }
                    else
                    {
                        await e.Channel.SendMessage(":warning: under construction sorry you can't enter :no_entry:");
                    }
                });
        }
    }
}
