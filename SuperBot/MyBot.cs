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
using System.Xml;
using System.Net.Http;

namespace Superbot
{
    class MyBot
    {
        public DiscordClient discord;
        public CommandService commands;

        Random rand;

        public static string commandPrefix = "%";

        public static int gpsCooldownInt = 0;
        public static string Month = "januari";
        public readonly string token = "token";
        //public readonly string token = "token";
        public bool playcircle = true;
        public static int playnumber = 0;

        //time usage
        public static DateTime MessageSent;
        public static bool spday = false;
        public static string specialday = "(day)";
        public static string specialdaydescription = "(description)";
        public static string dayofweek = "dinsdag";

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
                using (TextReader reader = File.OpenText(@"./file/prefix.txt"))
                {
                    string text = reader.ReadLine();
                    string[] bits = text.Split(' ');
                    char c = char.Parse(bits[0]);
                    x.PrefixChar = c;
                }

                x.AllowMentionPrefix = true;
                x.HelpMode = HelpMode.Disabled;
            });

            commands = discord.GetService<CommandService>();

            Commands();
            Help();
            command.Commands(commands, discord);
            help.HelpCommands(commands, discord);

            discord.ExecuteAndWait(async () =>
            {
                while (true)
                {
                    //await discord.Connect(token, TokenType.User);
                    await discord.Connect(token, TokenType.Bot);
                    var now = DateTime.Now;
                    await Task.Delay(200);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] Bot connected correctly");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] StartUp time is: {now - StartupTime}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Title = "Superbot.exe";
                    await Task.Delay(20);
                    if (Console.ReadLine() == "exit")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Disconnecting");
                        await Task.Delay(5000);
                        await discord.Disconnect();
                    }
                    else
                    {
                        Console.WriteLine("nothing hapened");
                    }

                    commands.CreateGroup("status", cgb =>
                    {
                        cgb.CreateCommand("idle")
                        .Do(async (e) =>
                        {
                            discord.SetStatus(UserStatus.Idle);
                            await e.Channel.SendMessage("You bot has been set to: **Idle**");
                            CommandUsed.CommandAdd();
                        });

                        cgb.CreateCommand("Invisible")
                        .Alias(new string[] { "inv" })
                        .Do(async (e) =>
                        {
                            CommandUsed.CommandAdd();
                            discord.SetStatus(UserStatus.Invisible);
                            await e.Channel.SendMessage("You bot has been set to: **Invisible**");
                        });

                        cgb.CreateCommand("DoNotDisturb")
                        .Alias(new string[] { "dnd" })
                        .Do(async (e) =>
                        {
                            CommandUsed.CommandAdd();
                            discord.SetStatus(UserStatus.DoNotDisturb);
                            await e.Channel.SendMessage("You bot has been set to: **DoNotDisturb**");
                        });

                        cgb.CreateCommand("Online")
                        .Do(async (e) =>
                        {
                            CommandUsed.CommandAdd();
                            discord.SetStatus(UserStatus.Online);
                            await e.Channel.SendMessage("You bot has been set to: **Online**");
                        });
                    });


                    break;
                }
            });

            PlayCircle.Playcircle(discord);
        }

        private void Commands()
        {
            commands.CreateCommand("close")
                .Do(async (e) =>
                {
                    CommandUsed.CommandAdd();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] used the close command");
                    if (e.User.Id == ID)
                    {
                        await e.Channel.SendMessage($"{e.User} Super Bot is stoping");
                        await e.Channel.SendMessage("confirm the stop in the Console");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine("Do you want to stop me");
                        Console.WriteLine("Yes or No");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        var read = Console.ReadLine();
                        if (read == "Yes")
                        {
                            await e.Channel.SendMessage("The bot has been stoped");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The Bot has been Disconected");
                            await Task.Delay(5000);
                            await discord.Disconnect();
                        }
                        if (read == "yes")
                        {
                            await e.Channel.SendMessage("The bot has been stoped");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The Bot has been Disconected");
                            await Task.Delay(5000);
                            await discord.Disconnect();
                        }

                        //return;
                        if (read == "No")
                        {
                            await e.Channel.SendMessage("The bot has not been stoped");
                            Console.WriteLine("haha I tryed");
                            return;
                        }
                        if (read == "no")
                        {
                            await e.Channel.SendMessage("The bot has not been stoped");
                            Console.WriteLine("haha I tryed");
                            return;
                        }
                        else
                        {
                            await e.Channel.SendMessage("The bot has not been stoped");
                            Console.WriteLine("The Bot has not been Disconected");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Or you typed it wrong");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"{e.User.Name} or {e.User.Nickname} tryed to stop me");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                });
            commands.CreateCommand("hard stop")
                .Do(async (e) =>
                {
                    if (e.User.Id == ID)
                    {
                        await e.Channel.SendMessage("The bot is reconnecting");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The Bot has been Disconected");
                        await Task.Delay(5000);
                        await discord.Disconnect();
                        await discord.Connect(token, TokenType.Bot);
                        //Console.ForegroundColor = ConsoleColor.Green;
                        //Console.WriteLine("The Bot has been reconnected");
                        //Console.ForegroundColor = ConsoleColor.White;
                        //await e.Channel.SendMessage("The bot has been reconnected");
                    }
                });

            discord.MessageReceived += async (s, e) =>
            {
                if (!e.User.IsBot)
                {
                    if (e.Message.Text.ToLower().Contains("lol "))
                    {
                        await e.Channel.SendMessage("lol");
                    }

                    if (e.Message.Text.ToLower() == "ja")
                    {
                        await e.Channel.SendMessage("nee");
                    }

                    if (e.Message.Text.ToLower() == "nee")
                    {
                        await e.Channel.SendMessage("ja");
                    }

                    if (e.Message.Text.ToLower().Contains("*triggerd*"))
                    {
                        await e.Channel.SendTTSMessage($"*Did you just assume my gender?*");
                    }

                    if (e.Message.Text.ToLower().Contains("yuki"))
                    {
                        await e.Channel.SendTTSMessage("YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKIYUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKIYUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI");
                    }

                    if (e.Message.Text.ToLower() == "1")
                    {
                        await e.Channel.SendMessage("2");
                        Console.WriteLine("haha");
                    }
                }
            };

        }

        private void Help()
        {
            commands.CreateGroup("read", c =>
            {
                c.CreateCommand("1")
                .Do(async (e) =>
                {
                    var path = @"./file/text.txt";
                    string line = File.ReadLines(path).Skip(1).Take(1).First();

                    await e.Channel.SendMessage($"{line}");
                });

                c.CreateCommand("2")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("");
                });

                c.CreateCommand("3")
                .Do(async (e) =>
                {
                    var path = @"./file/text.txt";
                    string line = File.ReadLines(path).Skip(3).Take(1).First();

                    await e.Channel.SendMessage($"{ +1}");
                });

                c.CreateCommand("4")
                .Do(async (e) =>
                {
                    using (TextReader reader = File.OpenText(@"./file/commandsused.txt"))
                    {
                        string text = reader.ReadLine();
                        string[] bits = text.Split(' ');
                        int x = int.Parse(bits[0]);
                        await e.Channel.SendMessage($"i have read: {x}");
                    }

                });

                c.CreateCommand("")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("");
                });

                c.CreateCommand("")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("");
                });

                c.CreateCommand("")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("");
                });

                c.CreateCommand("")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("");
                });

                c.CreateCommand("")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("");
                });

                c.CreateCommand("")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("");
                });

                c.CreateCommand("")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("");
                });

                c.CreateCommand("")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("");
                });

                c.CreateCommand("")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("");
                });

                c.CreateCommand("")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("");
                });

                c.CreateCommand("")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("");
                });

                c.CreateCommand("")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("");
                });
            });

            discord.MessageReceived += async (s, e) =>
            {
                if (e.Message.Text == $"{BotPrefix}help")
                {
                    await e.Channel.SendMessage($"Available commands: {BotPrefix}help, {BotPrefix}info, {BotPrefix}summon, {BotPrefix}disconnect, {BotPrefix}play");
                }
                else if (e.Message.Text == $"{BotPrefix}info")
                    await e.Channel.SendMessage($"Hiya! I'm SharpTunes, a Discord Music Bot written in C# using Discord.Net. You can see my list of commands with `{BotPrefix}help` and check out my source code at <https://github.com/Noahkiq/MusicBot>.");
                else if (e.Message.Text == $"{BotPrefix}summon") // Detect if message is m!summon
                {
                    if (e.User.VoiceChannel == null) // Checks if 'userVC' is null
                    {
                        await e.Channel.SendMessage($"You must be in a voice channel to use this command!"); // Give error message if 'userVC' is null
                    }
                    else
                    {
                        string userVC = e.User.VoiceChannel.Name; // Define 'userVC' variable
                        var voiceChannel = discord.FindServers(e.Server.Name).FirstOrDefault().FindChannels(userVC).FirstOrDefault(); // Grabs VC object
                        _vClient = await discord.GetService<AudioService>() // We use GetService to find the AudioService that we installed earlier. In previous versions, this was equivelent to discord.Audio()
                                .Join(voiceChannel); // Join the Voice Channel, and return the IAudioClient.
                        await e.Channel.SendMessage($"👌");
                    }
                }
                else if (e.Message.Text == $"{BotPrefix}disconnect")
                {
                    if (_vClient != null)
                    {
                        await _vClient.Disconnect();
                        _vClient = null;
                        await e.Channel.SendMessage($"👋");
                    }
                    else
                    {
                        await e.Channel.SendMessage($"The bot is not currently in a voice channel.");
                    }
                }
                else if (e.Message.Text.StartsWith($"{BotPrefix}play"))
                {
                    if (e.Message.Text == $"{BotPrefix}play")
                        await e.Channel.SendMessage($"Proper usage: `{BotPrefix}play [youtube video url]`");
                    else
                    {
                        string rawinput = e.Message.RawText.Replace($"{BotPrefix}play ", ""); // Grab raw video input
                        string filtering = rawinput.Replace("<", ""); // Remove '<' from input
                        string input = filtering.Replace(">", ""); // Remove '>' from input
                        playMessage = e.Message; // Set 'playMessage' ID

                        var newFilename = Guid.NewGuid().ToString(); // Create file name
                        var mp3OutputFolder = $"{Directory.GetCurrentDirectory()}\\videos\\"; // Grab video folder
                        Directory.CreateDirectory(mp3OutputFolder); // Create video folder if not found

                        var downloader = new AudioDownloader(input, newFilename, mp3OutputFolder);
                        downloader.ProgressDownload += downloader_ProgressDownload;
                        downloader.FinishedDownload += downloader_FinishedDownload;
                        downloader.Download();

                        videoName = downloader.OutputName; // Grab video name

                        string filePath = $"{mp3OutputFolder}{newFilename}.mp3"; // Grab music file to play

                        var channelCount = discord.GetService<AudioService>().Config.Channels; // Get the number of AudioChannels our AudioService has been configured to use.
                        var OutFormat = new WaveFormat(48000, 16, channelCount); // Create a new Output Format, using the spec that Discord will accept, and with the number of channels that our client supports.
                        using (var MP3Reader = new Mp3FileReader(filePath)) // Create a new Disposable MP3FileReader, to read audio from the filePath parameter
                        using (var resampler = new MediaFoundationResampler(MP3Reader, OutFormat)) // Create a Disposable Resampler, which will convert the read MP3 data to PCM, using our Output Format
                        {
                            resampler.ResamplerQuality = 60; // Set the quality of the resampler to 60, the highest quality
                            int blockSize = OutFormat.AverageBytesPerSecond / 50; // Establish the size of our AudioBuffer
                            byte[] buffer = new byte[blockSize];
                            int byteCount;

                            while ((byteCount = resampler.Read(buffer, 0, blockSize)) > 0) // Read audio into our buffer, and keep a loop open while data is present
                            {
                                if (byteCount < blockSize)
                                {
                                    // Incomplete Frame
                                    for (int i = byteCount; i < blockSize; i++)
                                        buffer[i] = 0;
                                }
                                _vClient.Send(buffer, 0, blockSize); // Send the buffer to Discord
                            }
                        }

                        _vClient.Wait(); // Waits for the currently playing sound file to end.
                    }
                }
            };
        }

        public static void month()
        {
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
        }

        public static void speciald()
        {
            var day = DateTime.Now.DayOfYear;
            if (day == 1)
            {
                specialday = "New year";
                specialdaydescription = "It's the beging of a new year";
                spday = true;
            }

            if (day == 365)
            {
                specialday = "New year's eve";
                specialdaydescription = "The day befor new year";
                spday = true;
            }


        }

        public static void dayOfweek(CommandEventArgs e)
        {
            DateTime messagesent = e.Message.Timestamp;
            var day = messagesent.DayOfWeek.ToString();
            if (day == "Monday")
            {
                dayofweek = "Maandag";
            }
            else if (day == "")
            {
                dayofweek = "Dinsdag";
            }
            else if (day == "Wednesday")
            {
                dayofweek = "Woensdag";
            }
            else if (day == "Thursday")
            {
                dayofweek = "Donderdag";
            }
            else if (day == "")
            {
                dayofweek = "Vrijdag";
            }
            else if (day == "")
            {
                dayofweek = "Zaterdag";
            }
            else if (day == "")
            {
                dayofweek = "Zondag";
            }

        }

        private void downloader_FinishedDownload(object sender, DownloadEventArgs e)
        {
            playMessage.Channel.SendMessage($"Finished downloading! Now playing {videoName}");
        }

        private void downloader_ProgressDownload(object sender, ProgressEventArgs e)
        {
            //nothing
        }

        public static void gpsCooldown(object source, ElapsedEventArgs e)
        {
            if (gpsCooldownInt > 0)
                gpsCooldownInt--;
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            var time = DateTime.Now;
            if (e.Severity == LogSeverity.Info)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{time.Hour}:{time.Minute}:{time.Second},{time.Millisecond}] [{e.Source}] {e.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            if (e.Severity == LogSeverity.Error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[{time.Hour}:{time.Minute}:{time.Second},{time.Millisecond}] [{e.Source}] {e.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            if (e.Severity == LogSeverity.Warning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[{time.Hour}:{time.Minute}:{time.Second},{time.Millisecond}] [{e.Source}] {e.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            if (e.Severity == LogSeverity.Debug)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"[{time.Hour}:{time.Minute}:{time.Second},{time.Millisecond}] [{e.Source}] {e.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }

        }
    }
}
