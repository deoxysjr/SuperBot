using Discord;
using Discord.Audio;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.IO;
using NAudio.Wave;
using WrapYoutubeDl;
using System.Threading.Tasks;

namespace Superbot
{
    class MyBot
    {
        public DiscordClient discord;
        public CommandService commands;        
        
        Random rand;
        
        string[] lol;
        string[] picture;
        string[] music;
        
        public static string commandPrefix = "%";
        public static int CommandsUsed = 0;
        public static DateTime MessageSent;
        public static int gpsCooldownInt = 0;
        public static string Month = "januari";
        
        //StartUpTime
        public static DateTime StartupTime = DateTime.Now;
        
        //music
        public static string BotPrefix = "&";
        public static Message playMessage;
        public static IAudioClient _vClient;
        public static string videoName = "Rick Astley - Never Gonna Give You Up";
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
                "https://www.youtube.com/watch?v=YwKfUoJ_ERY",
                "http://imgur.com/V06OE0B"
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
            
            discord.UsingAudio(x =>
            {
                x.Mode = AudioMode.Outgoing;
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
            CommandHelp();
            
            discord.ExecuteAndWait(async () =>
            {
                while (true)
                {
                    await discord.Connect(token, TokenType.Bot);
                    var now = DateTime.Now;
                    await Task.Delay(200);
                    Console.WriteLine("Bot connected correctly");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"StartUp time is: {now - StartupTime}");
                    discord.SetGame("%help for commands");
                    
                    commands.CreateGroup("status", cgb =>
                    {
                        cgb.CreateCommand("idle")
                            .Do(async (e) =>
                          {
                              discord.SetStatus(UserStatus.Idle);
                              await e.Channel.SendMessage("You bot has been set to: **Idle**");
                              CommandsUsed++;
                              Console.ForegroundColor = ConsoleColor.Cyan;
                              var path = @"D:\Super Bot\Commands\Commands.txt";
                              string line = File.ReadLines(path).Skip(2).Take(1).First();
                              Console.ForegroundColor = ConsoleColor.Cyan;
                              Console.WriteLine(line);
                          });
                          
                        cgb.CreateCommand("Invisible")
                            .Alias(new string[] {"inv"})
                            .Do(async (e) =>
                            {
                                discord.SetStatus(UserStatus.Invisible);
                                await e.Channel.SendMessage("You bot has been set to: **Invisible**");
                                CommandsUsed++;
                            });
                        
                        cgb.CreateCommand("DoNotDisturb")
                            .Alias(new string[] {"dnd"})
                            .Do(async (e) =>
                                {
                                    CommandsUsed++;
                                    discord.SetStatus(UserStatus.DoNotDisturb);
                                    await e.Channel.SendMessage("You bot has been set to: **DoNotDisturb**");
                                });
                        
                        cgb.CreateCommand("Online")
                            .Do(async (e) =>
                                {
                                    CommandsUsed++;
                                    discord.SetStatus(UserStatus.Online);
                                    await e.Channel.SendMessage("You bot has been set to: **Online**");
                                });
                    });
                    
                    commands.CreateCommand("Playing")
                        .Alias(new string[] { "play" })
                        .Parameter("text", ParameterType.Unparsed)
                        .Do(async (e) =>
                            {
                                CommandsUsed++;
                                if (e.User.Id == "ID")
                                {
                                    string text = e.Args[0];
                                    await e.Channel.SendMessage("Your bot is now playing: " + $"(**{text}**)");
                                    discord.SetGame(text);
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("%playing is used");
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.WriteLine($"you bot is now playing: ({text})");
                                }
                                else
                                {
                                    await e.Channel.SendMessage($"you can't do that {e.User.Name}");
                                }
                            });
                    break;
                }
            });
        }
        
        private void RegisterClearCommand()
        {
            commands.CreateCommand("clear")
                .Alias(new string[] {"clr"})
                .Do(async (e) =>
                    {
                        CommandsUsed++;
                        Message[] messagesToDelete;
                        Message[] messagesToDelete2;
                        messagesToDelete = await e.Channel.DownloadMessages(100);
                        messagesToDelete2 = await e.Channel.DownloadMessages(100);
                        await e.Channel.DeleteMessages(messagesToDelete);
                        /*await Task.Delay(500);
                        await e.Channel.DeleteMessages(messagesToDelete2);
                        */
                    });
            
            commands.CreateCommand("clear 10")
                .Alias(new string[] {"clr 10"})
                .Do(async (e) =>
                    {
                        CommandsUsed++;
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
                        CommandsUsed++;
                        int randomPictueIndex = rand.Next(picture.Length);
                        string pictureToPost = picture[randomPictueIndex];
                        await e.Channel.SendFile(pictureToPost);
                    });
        }
        
        private void Commands()
        {
            commands.CreateCommand("close")
                .Do(async (e) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("SomeOne used the close command");
                        if(e.User.Id == "ID")
                        {
                            await e.Channel.SendMessage($"{e.User} Super Bot is stoping");
                            await e.Channel.SendMessage("confirm the stop in the Consol");
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.WriteLine("Do you want to stop the bot");
                            Console.WriteLine("Yes or No");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Black;
                            if(Console.ReadLine() == "Yes")
                            {
                                await e.Channel.SendMessage("The bot has been stoped");
                                await discord.Disconnect();
                                Console.WriteLine("The Bot has been Disconected");
                            }
                            if (Console.ReadLine() == "yes")
                            {
                                await e.Channel.SendMessage("The bot has been stoped");
                                await discord.Disconnect();
                                Console.WriteLine("The Bot has been Disconected");
                            }
                            if (Console.ReadLine() == "No")
                            {
                                await e.Channel.SendMessage("The bot has not been stoped");
                                Console.WriteLine("haha I tryed");
                            }
                            if (Console.ReadLine() == "no")
                            {
                                await e.Channel.SendMessage("The bot has not been stoped");
                                Console.WriteLine("haha I tryed");
                            }
                        }
                    });
            
            commands.CreateCommand("CommandsCount")
                .Alias(new string[] {"cc"})
                .Do(async (e) =>
                    {
                        

                        await e.Channel.SendMessage($"the amount of commands = **{CommandsUsed}**");
                    });
            
            commands.CreateCommand("CommandsCountClear")
                .Alias(new string[] {"ccclear"})
                .Do(async (e) =>
                    {
                        CommandsUsed = 0;
                        await e.Channel.SendMessage($"the amount of commands used are now set to **{CommandsUsed}**");
                    });
            
            commands.CreateCommand("Maketxt")
                .Parameter("file", ParameterType.Unparsed)
                .Do(async (e) =>
                    {
                        CommandsUsed++;
                        var path = $@"C:\Users\deoxysjr\Documents\Visual Studio 2015\Projects\Superbot\Superbot\bin\Debug\file\{e.Args[0]}.txt";
                        var file = $"{Directory.GetCurrentDirectory()}\\file\\"; // Grab video folder
                        File.CreateText(path); // Create video folder if not found
                        await e.Channel.SendMessage("The file is been made");
                        await e.Channel.SendFile($@"C:\Users\deoxysjr\Documents\Visual Studio 2015\Projects\Superbot\Superbot\bin\Debug\file\{e.Args[0]}.txt");
                    });
            
            commands.CreateCommand("write")
                .Parameter("line", ParameterType.Unparsed)
                .Do(async (e) =>
                    {
                        CommandsUsed++;
                        var path = @"C:\users\deoxysjr\Documents\Visual Studio 2015\Projects\Superbot\Superbot\bin\Debug\file\text.txt";
                        File.AppendAllText(path, $"\r\n{e.Args[0]}");
                        await e.Channel.SendFile(@"C:\users\deoxysjr\Documents\Visual Studio 2015\Projects\Superbot\Superbot\bin\Debug\file\text.txt");
                    });
            
            commands.CreateCommand("cleartext")
                .Do(async (e) =>
                    {
                        CommandsUsed++;
                        var path = @"C:\users\deoxysjr\Documents\Visual Studio 2015\Projects\Superbot\Superbot\bin\Debug\file\text.txt";
                        File.WriteAllText(path, " ");
                        await e.Channel.SendMessage("text.txt is cleared");
                    });
            
            commands.CreateCommand("bye bye")
                .Do(async (e) =>
                    {
                        CommandsUsed++;
                        await e.Channel.SendMessage(":wave::skin-tone-1: Bye Bye! :wave::skin-tone-1:");
                    });
            
            commands.CreateCommand("hello")
                .Alias(new string[] { "hi" })
                .Do(async (e) =>
                    {
                        CommandsUsed++;
                        await e.Channel.SendMessage("hi!");
                    });
            
            commands.CreateCommand("dead")
                .Alias(new string[] { ":skull_crossbones:" })
                .Do(async (e) =>
                    {
                        CommandsUsed++;
                        await e.Channel.SendMessage(":skull_crossbones: :knife: kill your self :knife: :skull_crossbones:");
                    });
            
            commands.CreateCommand("NL")
                .Do(async (e) =>
                    {
                        CommandsUsed++;
                        await e.Channel.SendMessage(":flag_nl: + :flag_nl: + :flag_nl:");
                        await e.Channel.SendMessage(":flag_nl: +-+NL+-+ :flag_nl:");
                        await e.Channel.SendMessage(":flag_nl: + :flag_nl: + :flag_nl:");
                        await e.Channel.SendMessage(":flag_nl: +-+NL+-+ :flag_nl:");
                        await e.Channel.SendMessage(":flag_nl: + :flag_nl: + :flag_nl:");
                    });
            
            commands.CreateCommand("happy")
                .Do(async (e) =>
                    {
                        CommandsUsed++;
                        await e.Channel.SendMessage(":grinning: :joy: :grinning: :joy: :grinning: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :joy: ");
                    });
            
            commands.CreateCommand("lol")
                .Do(async (e) =>
                    {
                        CommandsUsed++;
                        await e.Channel.SendMessage("https://www.youtube.com/watch?v=bhBGFy7SKCc");
                    });
            
            commands.CreateCommand("lol lol")
                .Do(async (e) =>
                    {
                        CommandsUsed++;
                        int randomVideoIndex = rand.Next(lol.Length);
                        string videoToPost = lol[randomVideoIndex];
                        await e.Channel.SendMessage(videoToPost);
                    });
            
            commands.CreateCommand("music")
                .Do(async (e) =>
                    {
                        CommandsUsed++;
                        int randomMusicIndex = rand.Next(music.Length);
                        string musicToPost = music[randomMusicIndex];
                        await e.Channel.SendMessage(musicToPost);
                    });
            
            commands.CreateCommand("hi")
                .Do(async (e) =>
                    {
                        CommandsUsed++;
                        await e.User.SendMessage("hi");
                    });
            
            commands.CreateCommand("say")
                .Description("Make the bot say something")
                .Alias("s")
                .Parameter("text", ParameterType.Unparsed)
                .Do(async (e) =>
                    {
                        CommandsUsed++;
                        string text = e.Args[0];
                        await e.Channel.SendMessage(text);
                    });
            
            commands.CreateCommand("nice")
                .Do(async (e) =>
                    {
                        CommandsUsed++;
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
                        CommandsUsed++;
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
                        CommandsUsed++;
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
            
            commands.CreateCommand("time")
                .Do(async (e) =>
                    {
                        CommandsUsed++;
                        var MessageSent = e.Message.Timestamp;
                        
                        if (MessageSent.Month == 1)
                        {
                            Month = "januari";
                        }
                        if (MessageSent.Month == 2)
                        {
                            Month = "feberuari";
                        }
                        if (MessageSent.Month == 3)
                        {
                            Month = "maart";
                        }
                        if (MessageSent.Month == 4)
                        {
                            Month = "april";
                        }
                        if (MessageSent.Month == 5)
                        {
                            Month = "mei";
                        }
                        if (MessageSent.Month == 6)
                        {
                            Month = "juni";
                        }
                        if (MessageSent.Month == 7)
                        {
                            Month = "juli";
                        }
                        if (MessageSent.Month == 8)
                        {
                            Month = "augustus";
                        }
                        if (MessageSent.Month == 9)
                        {
                            Month = "septemder";
                        }
                        if (MessageSent.Month == 10)
                        {
                            Month = "oktober";
                        }
                        if (MessageSent.Month == 11)
                        {
                            Month = "november";
                        }
                        if (MessageSent.Month == 12)
                        {
                            Month = "december";
                        }
                        
                        var time = new List<string>();
                        
                        time.Add("```ini");
                        time.Add($"Year   = ({MessageSent.Year})");
                        time.Add($"Month  = ({MessageSent.Month}, {Month})");
                        time.Add($"Day    = ({MessageSent.DayOfWeek} - {MessageSent.Day})");
                        time.Add($"Hour   = ({MessageSent.Hour + 2})");
                        time.Add($"Minute = ({MessageSent.Minute})");
                        time.Add("```");
                        
                        await e.Channel.SendMessage(string.Join("\n", time));
                    });
            
            commands.CreateCommand("date")
                .Do(async (e) =>
                    {
                        CommandsUsed++;
                        MessageSent = e.Message.Timestamp;
                        await e.Channel.SendMessage($"``{MessageSent.DayOfWeek} {MessageSent.Day}-{MessageSent.Month}-{MessageSent.Year} ``");
                    });
            
            commands.CreateCommand("uptime")
                .Do(async (e) =>
                    {
                        CommandsUsed++;
                        await e.Channel.SendMessage($"the bot is now {DateTime.Now - StartupTime} active");
                    });
            
            commands.CreateCommand("roll") //create command
                .Description("Rolls a die.") //add description, it will be shown when *help is used
                .Parameter("number", ParameterType.Optional) //as an argument, we have a person we want to greet
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
                            else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 1")                            await e.Channel.SendMessage($"The one-sided die rolled a... one! Wow, surprising!");                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 2")                        {                            int dice = rand.Next(1, 3);                            await e.Channel.SendMessage($"The two-sided die rolled a... {dice}!");                        }                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 3")                        {                            int dice = rand.Next(1, 4);                            await e.Channel.SendMessage($"The three-sided die rolled a... {dice}!");                        }                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 4")                        {                            int dice = rand.Next(1, 5);                            await e.Channel.SendMessage($"The four-sided die rolled a... {dice}!");                        }                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 5")                        {                            int dice = rand.Next(1, 6);                            await e.Channel.SendMessage($"The five-sided die rolled a... {dice}!");                        }                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 6")                        {                            int dice = rand.Next(1, 7);                            await e.Channel.SendMessage($"The six-sided die rolled a... {dice}!");                        }                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 7")                        {                            int dice = rand.Next(1, 8);                            await e.Channel.SendMessage($"The seven-sided die rolled a... {dice}!");                        }                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 8")                        {                            int dice = rand.Next(1, 9);                            await e.Channel.SendMessage($"The eight-sided die rolled a... {dice}!");                        }                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 9")                        {                            int dice = rand.Next(1, 10);                            await e.Channel.SendMessage($"The nine-sided die rolled a... {dice}!");                        }                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 10")                        {                            int dice = rand.Next(1, 11);                            await e.Channel.SendMessage($"The nine-sided die rolled a... {dice}!");                        }                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 11")                        {                            int dice = rand.Next(1, 12);                            await e.Channel.SendMessage($"The eleven-sided die rolled a... {dice}!");                        }                        else if (e.Message.Text.ToLower() == $"{commandPrefix}roll 12")                        {                            int dice = rand.Next(1, 13);                            await e.Channel.SendMessage($"The twelve-sided die rolled a... {dice}!");                        }                        else                            await e.Channel.SendMessage($"You must pick a number between 1 and 12!");                    }                    else                    {                        int dice = rand.Next(1, 7);                        await e.Channel.SendMessage($"The six-sided die rolled a... {dice}!");                    }                });
            commands.CreateCommand("gps")                .Description("The GPS rant™")                .Do(async e =>                {                    CommandsUsed++;                    if (gpsCooldownInt == 0)                    {                        gpsCooldownInt = 15;                        Timer gpsCooldownTimer = new Timer();                        gpsCooldownTimer.Elapsed += new ElapsedEventHandler(gpsCooldown);                        gpsCooldownTimer.Interval = 1000;                        gpsCooldownTimer.Enabled = true;                        await e.Channel.SendMessage($"ᵂᵃᶦᵗ, ᵗʰᶦˢ ᶦˢ ᵃ ˢᵉʳᶦᵒᵘˢ ᶦˢˢᵘᵉ⋅ ᴸᵉᵗ ᵐᵉ ʳᵃᶰᵗ ᵃᵇᵒᵘᵗ ᶦᵗ ᶠᵒʳ ᵃ ᵇᶦᵗ⋅ ﹡ᵃᶜʰᵉᵐ﹡⋅ \nᵂʰᵃᵗ ᶦˢ ᵃ ᴳᴾˢ﹖\nᵀʰᵉ ᴳᴾˢ ᶦˢ ᵃ ˢʸˢᵗᵉᵐ ᵗᵒ ᵉˢᵗᶦᵐᵃᵗᵉ ᶫᵒᶜᵃᵗᶦᵒᶰ ᵒᶰ ᵉᵃʳᵗʰ ᵇʸ ᵘˢᶦᶰᵍ ˢᶦᵍᶰᵃᶫˢ ᶠʳᵒᵐ ᵃ ˢᵉᵗ ᵒᶠ ᵒʳᵇᶦᵗᶦᶰᵍ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ… ᵒʳ ˢᵒ ᵗʰᵉʸ ˢᵃʸ⋅ ᴮᵘᵗ ʷʰᵃᵗ ᵗʰᵉʸ'ʳᵉ ᶰᵒᵗ ᵗᵉᶫᶫᶦᶰᵍ ᵘˢ ᶦˢ ᵗʰᵉ ʰᵃʳᵐ ᶦᵗ ᶜᵒᶰˢᵗᵃᶰᵗᶫʸ ᶜᵃᵘˢᵉˢ ʷᵒʳᶫᵈʷᶦᵈᵉ ᵉᵛᵉʳʸ ᵈᵃʸ⋅ ᴿᵉᶜᵉᶰᵗᶫʸ, ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ʰᵃᵛᵉ ᵇᵉᵉᶰ ᵇᵉᶦᶰᵍ ᵈᵉˢᶦᵍᶰᵉᵈ ˢᵒᶫᵉᶫʸ ᶠᵒʳ ᵗʰᵉ ᵖᵘʳᵖᵒˢᵉ ᵒᶠ ᵇᵒᵒˢᵗᶦᶰᵍ ᴳᴾˢ ᵖᵉʳᶠᵒʳᵐᵃᶰᶜᵉ, ᵇᵘᵗ ᵗʰᵉʸ ʰᵃᵛᵉ ᵇᵉᵉᶰ ᵇᵘᶦᶫᵗ ʷᶦᵗʰ ˢᵒ ᶫᶦᵗᵗᶫᵉ ᶜᵃʳᵉ ᵗʰᵃᵗ ᵗʰᵉ ᵖʳᵒᵇᶫᵉᵐˢ ᵃʳᵉ ᵉᵛᵉᶰ ʷᵒʳˢᵉ, ᵍᶦᵛᵉᶰ ᵗʰᵃᵗ ᵗʰᵉʸ ᵃʳᵉᶰ'ᵗ ʳᵉᶫʸᶦᶰᵍ ᵒᶰ ᶫᵃʳᵍᵉʳ ᵗʰᶦʳᵈ ᵖᵃʳᵗʸ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵃᶰʸᵐᵒʳᵉ⋅ ᴺᵒᵗ ᵒᶰᶫʸ ᶦˢ ᵗʰᵉ ᶰᵃᵛᶦᵍᵃᵗᶦᵒᶰ ʷᵒʳˢᵉ, ᵇᵘᵗ ᶦᵗ’ˢ ᵃᶫˢᵒ ʰᵃʳᵐᶦᶰᵍ ᵒᵘʳ ᵒᵘᵗ⁻ᵒᶠ⁻ᵒʳᵇᶦᵗ ᵉᶰᵛᶦʳᵒᶰᵐᵉᶰᵗ⋅ ˢᵖᵃᶜᵉ ᵈᵉᵇʳᶦˢ ʰᵃˢ ᵇᵉᵉᶰ ᵃ ᵖʳᵒᵇᶫᵉᵐ ᵉᵛᵉʳ ˢᶦᶰᶜᵉ ʷᵉ ˢᵗᵃʳᵗᵉᵈ ᶫᵃᵘᶰᶜʰᶦᶰᵍ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᶦᶰᵗᵒ ˢᵖᵃᶜᵉ, ᵒᶠ ᶜᵒᵘʳˢᵉ, ᵇᵘᵗ⋅⋅ ᵂᶦᵗʰ ᶰᵉʷ ‘ʲᵃᶰᶦᵗᵒʳ’ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵃᶰᵈ ᶫᵃˢᵉʳ ᵗᵉᶜʰᶰᵒᶫᵒᵍʸ, ʷᵉ’ᵛᵉ ᵇᵉᵉᶰ ᵃᵇᶫᵉ ᵗᵒ ᵏᵉᵉᵖ ᵗʰᵃᵗ ᵘᶰᵈᵉʳ ᶜᵒᶰᵗʳᵒᶫ⋅ ᴮᵘᵗ ʷʰᶦᶫᵉ ᵗʰᵉ ᵒᵛᵉʳᵃᶫᶫ ᵇᵘᶦᶫᵈ ᶠᵒʳ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵃʳᵉ ᵘˢᵘᵃᶫᶫʸ ˢᵗᵘʳᵈʸ ᵉᶰᵒᵘᵍʰ ᵗᵒ ʷᶦᵗʰˢᵗᵃᶰᵈ ˢᵖᵃᶜᶦᵒᵘˢ ᶜᵒᶰᵈᶦᵗᶦᵒᶰˢ ᶠᵒʳ ᵃ ᶫᵉᶰᵍᵗʰʸ ᵃᵐᵒᵘᶰᵗ ᵒᶠ ᵗᶦᵐᵉ, ᵗʰᵉˢᵉ ᴳᴾˢ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵃʳᵉ ʲᵘˢᵗ ᶫᵃᵘᶰᶜʰᶦᶰᵍ ᵖᶦᵉᶜᵉˢ ᵒᶠ ᵗʰᵉᶦʳ ᵒʷᶰ ᵈᵉᵇʳᶦˢ ᶦᶰᵗᵒ ˢᵖᵃᶜᵉ﹔ ˢᵗᶦᶫᶫ ᶦᶰ ᵒᵘʳ ᵒʳᵇᶦᵗ ᵇᵘᵗ ᵒᵘᵗ ᵒᶠ ᵒᵘʳ ᵃᵗᵐᵒˢᵖʰᵉʳᵉ, ʷʰᶦᶜʰ ᶦˢ ᵗᵃᵏᶦᶰᵍ ᵘˢ ᵃᵗ ᶫᵉᵃˢᵗ ᵗᵉᶰ ʸᵉᵃʳˢ ᵇᵃᶜᵏ ᶦᶰ ᵗᵉʳᵐˢ ᵒᶠ ˢᵖᵃᶜᵉ ᵗᵉᶜʰᶰᵒᶫᵒᵍʸ ᵈᵉᵛᵉᶫᵒᵖᵐᵉᶰᵗ⋅ ᴺᵒᵗ ᵒᶰᶫʸ ᵗʰᵃᵗ, ᵇᵘᵗ ᵗʰᵉ ᵐᵃᶦᶰ ᵖʳᵒᵇᶫᵉᵐ ʷᶦᵗʰ ˢᵖᵃᶜᵉ ᵈᵉᵇʳᶦˢ ᶦᶰ ᵍᵉᶰᵉʳᵃᶫ ᶦˢ ᵗʰᵃᵗ ᶦᵗ ᶦˢ ᶜᵃᵘˢᶦᶰᵍ ᵉˣᵗʳᵉᵐᵉ ᶫᵉᵛᵉᶫˢ ᵒᶠ ᶜᵒᶫᶫᶦˢᶦᵒᶰ ʷᶦᵗʰ ᵒᵗʰᵉʳ ᵛᶦᵗᵃᶫ ᵃᶰᵈ ᵉˣᵖᵉᶰˢᶦᵛᵉ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵗʰᵃᵗ ᵃʳᵉ ᵘˢᵉᵈ ᶠᵒʳ ᶦᵐᵖᵒʳᵗᵃᶰᵗ ᵃᶰᵈ ᶦᶰ ᵍᵉᶰᵉʳᵃᶫ ʰᵃᵛᵉ ᵃ ˢʷᵉᶫᶫ ᶦᶰᵗᵉᶰᵗᶦᵒᶰ ᵍᵒᶦᶰᵍ ᵇʸ ᵗʰᵉᶦʳ ᵃᶜᵗᶦᵛᶦᵗᶦᵉˢ⋅ ᵀʰᵉʳᵉᶠᵒʳᵉ ᵗʰᵉˢᵉ ᴳᴾˢ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ, ʷʰᶦᶫᵉ ᵃᶫʳᵉᵃᵈʸ ᵇᵉᶦᶰᵍ ᵃᵇʰᵒʳʳᵉᶰᵗ ᵉᶰᵒᵘᵍʰ, ᵃʳᵉ ʳᵉᵈᵘᶜᶦᶰᵍ ᵗʰᵉ ᑫᵘᵃᶫᶦᵗʸ ᵒᶠ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵃᶜᵗᵘᵃᶫᶫʸ ᵐᵃᵈᵉ ʷᶦᵗʰ ᵃ ˢᶦᶰᵍᶫᵉ ᵒᵘᶰᶜᵉ ᵒᶠ ᶦᶰᵗᵉᵍʳᶦᵗʸ﹗ ᵀʰᵉʸ ᵃʳᵉ ᵐᵃᵏᶦᶰᵍ ᶫᶦᶠᵉ ʷᵒʳˢᵉ ᶠᵒʳ ᵉᵛᵉʳʸ ˢᶦᶰᵍᶫᵉ ᵖᵉʳˢᵒᶰ ᵘˢᶦᶰᵍ ᵃ ˢᵉʳᵛᶦᶜᵉ ᵖʳᵒᵛᶦᵈᵉᵈ ᵇʸ ᵃᶰʸ ᵒᶠ ᵗʰᵉˢᵉ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵗʰᵃᵗ ʷᵉʳᵉ ᶦᶰ ᶜᵒᶰᵗᵃᶜᵗ ʷᶦᵗʰ ᵗʰᵉᵐ⋅ ᴵ ˢᶦᵐᵖᶫᵉ ᶜᵃᶰ’ᵗ ᵘᶰᵈᵉʳˢᵗᵃᶰᵈ ʰᵒʷ ʷᵉ ᶜᵃᶰ ᵗᵒᶫᵉʳᵃᵗᵉ ᵗʰᵉˢᵉ ᵖᵉᵒᵖᶫᵉ⋅ ᴬᶫʳᶦᵍʰᵗ, ᶫᵉᵗ’ˢ ᵍᵉᵗ ᵇᵃᶜᵏ ᵗᵒ ᵗʰᵉ ᶰᶦᵗᵗʸ ᵍʳᶦᵗᵗʸ ᵒᶠ ᵗʰᶦˢ ᵗʰᶦᶰᵍ⋅ ᴬˢ ᴵ ʷᵃˢ ˢᵃʸᶦᶰᵍ ᵉᵃʳᶫᶦᵉʳ, ᶜᵒᶫᶫᶦˢᶦᵒᶰˢ ᵈᵉˢᵗʳᵒʸᵉᵈ ᵇᵒᵗʰ ˢᵃᵗᵉᶫᶫᶦᵗᵉˢ ᵃᶰᵈ ᵗʰᵒˢᵉ ᵗʰᵃᵗ ʰᵃᵈ ᶜʳᵉᵃᵗᵉᵈ ᵃ ᶠᶦᵉᶫᵈ ᵒᶠ ᵈᵉᵇʳᶦˢ ᵗʰᵃᵗ ᵇᵉᶜᵒᵐᵉ ᵃ ᵈᵃᶰᵍᵉʳ ᵗᵒ ᵒᵗʰᵉʳ […]");                    }                    else                        await e.Channel.SendMessage($"This command is currently on a cooldown. Please try again in {gpsCooldownInt} seconds.");                });
            discord.MessageReceived += async (s, e) =>            {                if (!e.User.IsBot)                {                    if (e.Message.Text.ToLower() == "lol")                    {                        await e.Channel.SendMessage("lol");                    }
                    if (e.Message.Text.ToLower() == "ja")                    {                        await e.Channel.SendMessage("nee");                    }
                    if (e.Message.Text.ToLower() == "ja")                    {                        await e.Channel.SendMessage("nee");                    }
                    if (e.Message.Text.ToLower() == "*triggerd*")                    {                        await e.Channel.SendMessage($"*Did you just gender my assumption?*");                    }
                    if (e.Message.Text.ToLower() == "yuki")                    {                        await e.Channel.SendMessage("YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKIYUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKIYUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKIYUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI");                    }
                    if (e.Message.Text.ToLower() == "heil")                    {                        var heil = new List<string>();
                        heil.Add(":no_mouth: :no_mouth: :100: :100: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth:");                        heil.Add(":no_mouth: :no_mouth: :100: :100: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth:");                        heil.Add(":no_mouth: :no_mouth: :100: :100: :no_mouth: :no_mouth: :100: :100: :100: :100:");                        heil.Add(":no_mouth: :no_mouth: :100: :100: :no_mouth: :no_mouth: :100: :100: :100: :100:");                        heil.Add(":no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth:");                        heil.Add(":no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth:");                        heil.Add(":100: :100: :100: :100: :no_mouth: :no_mouth: :100: :100: :no_mouth: :no_mouth:");                        heil.Add(":100: :100: :100: :100: :no_mouth: :no_mouth: :100: :100: :no_mouth: :no_mouth:");                        heil.Add(":no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :100: :100: :no_mouth: :no_mouth:");                        heil.Add(":no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :100: :100: :no_mouth: :no_mouth:");
                        await e.Channel.SendMessage(string.Join("\n", heil));                    }
                    if(e.Message.Text.ToLower() == "jews?")                    {                        await e.Channel.SendMessage("Do I smell gas?");                    }                }            };
        }
        private void Help()        {            commands.CreateGroup("help dm", cgb =>            {                cgb.CreateCommand("")                .Alias(new string[] { "" })                .Do(async (e) =>                {                    CommandsUsed++;                    var name = e.User.Nickname != null ? e.User.Nickname : e.User.Name;
                    var helpList = new List<string>();
                    helpList.Add("```");                    helpList.Add("The prefix = %");                    helpList.Add("%help 1 = commands");                    helpList.Add("%help 2 = info");                    helpList.Add("%help 3 = admin");                    helpList.Add("%help 4 = files");                    helpList.Add("%help 5 = fun!!!");                    helpList.Add("```");
                    await e.Channel.SendMessage(name + " look in you inbox");                    await e.User.SendMessage(string.Join("\n", helpList));                });
                cgb.CreateCommand("1")                    .Alias(new string[] { "1" })                    .Do(async (e) =>                    {                        CommandsUsed++;                        var helpList = new List<string>();
                        helpList.Add("```");                        helpList.Add("This is the list of Commands");                        helpList.Add(" ");                        helpList.Add("%help - help");                        helpList.Add("%say - let the bot say someting");                        helpList.Add("%hello - hi!");                        helpList.Add("%bye bye - bye");                        helpList.Add("%dead - :skull_crossbones::dead::skull_crossbones:");                        helpList.Add("%nice - lol");                        helpList.Add("%roll + number up to 12 - roles a random number");                        helpList.Add("%gps - tels you about gps");                        helpList.Add("```");
                        await e.User.SendMessage(string.Join("\n", helpList));                    });
                cgb.CreateCommand("2")                    .Alias(new string[] { "2" })                    .Do(async (e) =>                    {                        CommandsUsed++;                        var helpList = new List<string>();
                        helpList.Add("```");                        helpList.Add("This is the list of info Commands");                        helpList.Add(" ");                        helpList.Add("%serverinfo - gives info about the server");                        helpList.Add("%userinfo @user - gives info about a user in the server");                        helpList.Add("%date - to say which day it is");                        helpList.Add("%CommandsUsed - says the amount of commands used");                        helpList.Add("%uptime - says the time the bot is active");                        helpList.Add("```");
                        await e.User.SendMessage(string.Join("\n", helpList));                    });
                cgb.CreateCommand("3")                .Alias(new string[] { "3" })                .Do(async (e) =>                {                    CommandsUsed++;                    var helpList = new List<string>();
                    helpList.Add("```");                    helpList.Add("This is the list of the admin commands");                    helpList.Add(" ");                    helpList.Add("%playing + game - sets the playing game of the bot");                    helpList.Add("%clear / clr - clears the chat");                    helpList.Add("```");
                    await e.User.SendMessage(string.Join("\n", helpList));                });
                cgb.CreateCommand("4")                .Alias(new string[] { "4" })                .Do(async (e) =>                {                    CommandsUsed++;                    var helpList = new List<string>();
                    helpList.Add("```");                    helpList.Add("This is the list of file commands");                    helpList.Add(" ");                    helpList.Add("%maketxt + {name} - makes a file.txt");                    helpList.Add("%write + - writes in a file and then sends it");                    helpList.Add("%cleartext - clears the written text what you have written in %write");                    helpList.Add("```");
                    await e.User.SendMessage(string.Join("\n", helpList));                });
                cgb.CreateCommand("5")                .Alias(new string[] { "5" })                .Do(async (e) =>                {                    CommandsUsed++;                    var helpList = new List<string>();
                    helpList.Add("```");                    helpList.Add("This is the list of funny commands");                    helpList.Add(" ");                    helpList.Add("%gif - sends you a gif");                    helpList.Add("%picture - sends a picture");                    helpList.Add("%funny - sends a funny picture");                    helpList.Add("```");
                    await e.User.SendMessage(string.Join("\n", helpList));                });            });
            commands.CreateGroup("help", cgb =>            {                cgb.CreateCommand("")                .Alias(new string[] { "" })                .Do(async (e) =>                {                    CommandsUsed++;                    var name = e.User.Nickname != null ? e.User.Nickname : e.User.Name;
                    var helpList = new List<string>();
                    helpList.Add("```");                    helpList.Add("The prefix = %");                    helpList.Add("%help 1 = commands");                    helpList.Add("%help 2 = info");                    helpList.Add("%help 3 = admin");                    helpList.Add("%help 4 = files");                    helpList.Add("%help 5 = fun!!!");                    helpList.Add("```");
                    await e.Channel.SendMessage(string.Join("\n", helpList));                });
                cgb.CreateCommand("1")                    .Alias(new string[] { "1" })                    .Do(async (e) =>                    {                        CommandsUsed++;                        var helpList = new List<string>();
                        helpList.Add("```");                        helpList.Add("This is the list of Commands");                        helpList.Add(" ");                        helpList.Add("%help - help");                        helpList.Add("%say - let the bot say someting");                        helpList.Add("%hello - hi!");                        helpList.Add("%bye bye - bye");                        helpList.Add("%dead - :skull_crossbones::dead::skull_crossbones:");                        helpList.Add("%nice - lol");                        helpList.Add("%roll + number up to 12 - roles a random number");                        helpList.Add("%gps - tels you about gps");                        helpList.Add("```");
                        await e.Channel.SendMessage(string.Join("\n", helpList));                    });
                cgb.CreateCommand("2")                    .Alias(new string[] { "2" })                    .Do(async (e) =>                    {                        CommandsUsed++;                        var helpList = new List<string>();
                        helpList.Add("```");                        helpList.Add("This is the list of info Commands");                        helpList.Add(" ");                        helpList.Add("%serverinfo - gives info about the server");                        helpList.Add("%userinfo @user - gives info about a user in the server");                        helpList.Add("%date - to say which day it is");                        helpList.Add("%uptime - says the time the bot is active");                        helpList.Add("%CommandCount - says the amount of commands used");                        helpList.Add("```");
                        await e.Channel.SendMessage(string.Join("\n", helpList));                    });
                cgb.CreateCommand("3")                .Alias(new string[] { "3" })                .Do(async (e) =>                {                    CommandsUsed++;                    var helpList = new List<string>();
                    helpList.Add("```");                    helpList.Add("This is the list of the admin commands");                    helpList.Add(" ");                    helpList.Add("%playing + game - sets the playing game of the bot");                    helpList.Add("%clear / clr - clears the chat");                    helpList.Add("```");
                    await e.Channel.SendMessage(string.Join("\n", helpList));                });
                cgb.CreateCommand("4")                .Alias(new string[] { "4" })                .Do(async (e) =>                {                    CommandsUsed++;                    var helpList = new List<string>();
                    helpList.Add("```");                    helpList.Add("This is the list of file commands");                    helpList.Add(" ");                    helpList.Add("%maketxt + {name} - makes a file.txt");                    helpList.Add("%write + - writes in a file and then sends it");                    helpList.Add("%cleartext - clears the written text what you have written in %write");                    helpList.Add("```");
                    await e.Channel.SendMessage(string.Join("\n", helpList));                });
                cgb.CreateCommand("5")                .Alias(new string[] { "5" })                .Do(async (e) =>                {                    CommandsUsed++;                    var helpList = new List<string>();
                    helpList.Add("```");                    helpList.Add("This is the list of funny commands");                    helpList.Add(" ");                    helpList.Add("%gif - sends you a gif");                    helpList.Add("%picture - sends a picture");                    helpList.Add("%funny - sends a funny picture");                    helpList.Add("```");
                    await e.Channel.SendMessage(string.Join("\n", helpList));                });            });
            commands.CreateGroup("read", c =>            {                c.CreateCommand("1")                .Do(async (e) =>                {                    var path = @"C:\users\deoxysjr\Documents\Visual Studio 2015\Projects\Superbot\Superbot\bin\Debug\file\text.txt";                    string line = File.ReadLines(path).Skip(1).Take(1).First();
                    await e.Channel.SendMessage($"{line}");                });
                c.CreateCommand("2")                .Do(async (e) =>                {                    var path = @"D:\Super Bot\Commands\Commands.txt";                    string line = File.ReadLines(path).Skip(1).Take(1).First();
                    await e.Channel.SendMessage($"{line}");                });
                c.CreateCommand("3")                .Do(async (e) =>                {                    await e.Channel.SendMessage("");                });
                c.CreateCommand("")                .Do(async (e) =>                {                    await e.Channel.SendMessage("");                });
                c.CreateCommand("")                .Do(async (e) =>                {                    await e.Channel.SendMessage("");                });
                c.CreateCommand("")                .Do(async (e) =>                {                    await e.Channel.SendMessage("");                });
                c.CreateCommand("")                .Do(async (e) =>                {                    await e.Channel.SendMessage("");                });
                c.CreateCommand("")                .Do(async (e) =>                {                    await e.Channel.SendMessage("");                });
                c.CreateCommand("")                .Do(async (e) =>                {                    await e.Channel.SendMessage("");                });
                c.CreateCommand("")                .Do(async (e) =>                {                    await e.Channel.SendMessage("");                });
                c.CreateCommand("")                .Do(async (e) =>                {                    await e.Channel.SendMessage("");                });
                c.CreateCommand("")                .Do(async (e) =>                {                    await e.Channel.SendMessage("");                });
                c.CreateCommand("")                .Do(async (e) =>                {                    await e.Channel.SendMessage("");                });
                c.CreateCommand("")                .Do(async (e) =>                {                    await e.Channel.SendMessage("");                });
                c.CreateCommand("")                .Do(async (e) =>                {                    await e.Channel.SendMessage("");                });
                c.CreateCommand("")                .Do(async (e) =>                {                    await e.Channel.SendMessage("");                });            });
            discord.MessageReceived += async (s, e) =>            {                if (e.Message.Text == $"{BotPrefix}help")                {                    await e.Channel.SendMessage($"Available commands: {BotPrefix}help, {BotPrefix}info, {BotPrefix}summon, {BotPrefix}disconnect, {BotPrefix}play");                }                else if (e.Message.Text == $"{BotPrefix}info")                    await e.Channel.SendMessage($"Hiya! I'm SharpTunes, a Discord Music Bot written in C# using Discord.Net. You can see my list of commands with `{BotPrefix}help` and check out my source code at <https://github.com/Noahkiq/MusicBot>.");                else if (e.Message.Text == $"{BotPrefix}summon") // Detect if message is m!summon                {                    if (e.User.VoiceChannel == null) // Checks if 'userVC' is null                    {                        await e.Channel.SendMessage($"You must be in a voice channel to use this command!"); // Give error message if 'userVC' is null                    }                    else                    {                        string userVC = e.User.VoiceChannel.Name; // Define 'userVC' variable                        var voiceChannel = discord.FindServers(e.Server.Name).FirstOrDefault().FindChannels(userVC).FirstOrDefault(); // Grabs VC object                        _vClient = await discord.GetService<AudioService>() // We use GetService to find the AudioService that we installed earlier. In previous versions, this was equivelent to discord.Audio()                                .Join(voiceChannel); // Join the Voice Channel, and return the IAudioClient.                        await e.Channel.SendMessage($"👌");                    }                }                else if (e.Message.Text == $"{BotPrefix}disconnect")                {                    if (_vClient != null)                    {                        await _vClient.Disconnect();                        _vClient = null;                        await e.Channel.SendMessage($"👋");                    }                    else                    {                        await e.Channel.SendMessage($"The bot is not currently in a voice channel.");                    }                }                    else if (e.Message.Text.StartsWith($"{BotPrefix}play"))                    {                        if (e.Message.Text == $"{BotPrefix}play")                            await e.Channel.SendMessage($"Proper usage: `{BotPrefix}play [youtube video url]`");                        else                        {                            string rawinput = e.Message.RawText.Replace($"{BotPrefix}play ", ""); // Grab raw video input                            string filtering = rawinput.Replace("<", ""); // Remove '<' from input                            string input = filtering.Replace(">", ""); // Remove '>' from input                            playMessage = e.Message; // Set 'playMessage' ID
                            var newFilename = Guid.NewGuid().ToString(); // Create file name                            var mp3OutputFolder = $"{Directory.GetCurrentDirectory()}\\videos\\"; // Grab video folder                            Directory.CreateDirectory(mp3OutputFolder); // Create video folder if not found
                            var downloader = new AudioDownloader(input, newFilename, mp3OutputFolder);                            downloader.ProgressDownload += downloader_ProgressDownload;                            downloader.FinishedDownload += downloader_FinishedDownload;                            downloader.Download();
                            videoName = downloader.OutputName; // Grab video name
                            string filePath = $"{mp3OutputFolder}{newFilename}.mp3"; // Grab music file to play
                            var channelCount = discord.GetService<AudioService>().Config.Channels; // Get the number of AudioChannels our AudioService has been configured to use.                            var OutFormat = new WaveFormat(48000, 16, channelCount); // Create a new Output Format, using the spec that Discord will accept, and with the number of channels that our client supports.                            using (var MP3Reader = new Mp3FileReader(filePath)) // Create a new Disposable MP3FileReader, to read audio from the filePath parameter                            using (var resampler = new MediaFoundationResampler(MP3Reader, OutFormat)) // Create a Disposable Resampler, which will convert the read MP3 data to PCM, using our Output Format                            {                                resampler.ResamplerQuality = 60; // Set the quality of the resampler to 60, the highest quality                                int blockSize = OutFormat.AverageBytesPerSecond / 50; // Establish the size of our AudioBuffer                                byte[] buffer = new byte[blockSize];                                int byteCount;
                                while ((byteCount = resampler.Read(buffer, 0, blockSize)) > 0) // Read audio into our buffer, and keep a loop open while data is present                                {                                    if (byteCount < blockSize)                                    {                                        // Incomplete Frame                                        for (int i = byteCount; i < blockSize; i++)                                            buffer[i] = 0;                                    }                                    _vClient.Send(buffer, 0, blockSize); // Send the buffer to Discord                                }                            }
                            _vClient.Wait(); // Waits for the currently playing sound file to end.                        }                    }            };        }
        private void CommandHelp()        {
        }
        private void downloader_FinishedDownload(object sender, DownloadEventArgs e)        {            playMessage.Channel.SendMessage($"Finished downloading! Now playing {videoName}");        }
        private void downloader_ProgressDownload(object sender, ProgressEventArgs e)        {            //nothing        }
        private static void gpsCooldown(object source, ElapsedEventArgs e)        {            if (gpsCooldownInt > 0)                gpsCooldownInt--;        }
        private void Log(object sender, LogMessageEventArgs e)        {            if (e.Severity == LogSeverity.Info)            {                Console.ForegroundColor = ConsoleColor.Green;                Console.WriteLine($"[{e.Source}] {e.Message}");            }            if (e.Severity == LogSeverity.Error)            {                Console.ForegroundColor = ConsoleColor.Red;                Console.WriteLine($"[{e.Source}] {e.Message}");            }            if (e.Severity == LogSeverity.Warning)            {                Console.ForegroundColor = ConsoleColor.Yellow;                Console.WriteLine($"[{e.Source}] {e.Message}");            }            if (e.Severity == LogSeverity.Debug)            {                Console.ForegroundColor = ConsoleColor.DarkGray;                Console.WriteLine($"[{e.Source}] {e.Message}");            }                    }    }}
