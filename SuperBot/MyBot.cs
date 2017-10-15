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
using System.Diagnostics;

namespace Superbot
{
    class MyBot
    {
        public DiscordClient discord;
        public CommandService commands;

        Random rand;

        public static string commandPrefix = "%";

        public static string Month = "januari";
        public bool playcircle = true;
        public static int playnumber = 0;
        public static int spamnum = 0;
        public static bool done = false;

        //cpu en ram list
        static List<float> AvailableCPU = new List<float>();
        static List<float> AvailableRAM = new List<float>();

        protected static PerformanceCounter cpuCounter;
        protected static PerformanceCounter ramCounter;
        static List<PerformanceCounter> cpuCounters = new List<PerformanceCounter>();
        static List<PerformanceCounter> core = new List<PerformanceCounter>();

        //time usage
        public static DateTime MessageSent;
        public static string dayofweek;

        //StartUpTime
        public static DateTime StartupTime = DateTime.Now;

        //music
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
                //x.HelpMode = HelpMode.Public;
            });

            commands = discord.GetService<CommandService>();

            Commands();
            command.Commands(commands, discord);
            help.HelpCommands(commands, discord);
            commandhelp.Commandhelp(discord);
            som.Som(commands, discord);
            colors.Colors.ColorCommands(commands, discord);
            Weather.Weather.WeatherCommand(commands, discord, done);
            animesearch.AnimeCommands(commands, discord);
            Test.testCommand(commands, discord);
	    Rank.levels(commands, discord, rand);
			
            discord.ExecuteAndWait(async () =>
            {
                while (true)
                {
                    //await discord.Connect(token, TokenType.User);
                    await discord.Connect(token, TokenType.Bot);
                    var now = DateTime.Now;
                    await Task.Delay(100);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] Bot connected correctly");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] StartUp time is: {now - StartupTime}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Title = "Superbot.exe";
                    discord.SetGame($"{DateTime.Now}");

                    await Task.Delay(20);

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
                        .Alias(new string[] {"inv"})
                        .Do(async (e) =>
                        {
                            CommandUsed.CommandAdd();
                            discord.SetStatus(UserStatus.Invisible);
                            await e.Channel.SendMessage("You bot has been set to: **Invisible**");
                        });

                        cgb.CreateCommand("DoNotDisturb")
                        .Alias(new string[] {"dnd"})
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
        }

        private void Commands()
        {
            commands.CreateCommand("close")
                .Do(async (e) =>
                {
                    CommandUsed.CommandAdd();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] used the close command");
                    if (e.User.Id == 245140333330038785)
                    {
                        await e.Channel.SendMessage($"{e.User} is stoping @superbot");
                        await e.Channel.SendMessage("confirm the stop in the Console");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine("Do you want to stop me");
                        Console.WriteLine("Yes or No");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        var read = Console.ReadLine();
                        if (read == "Yes" || read == "yes" || read == "Y" || read == "y")
                        {
                            await e.Channel.SendMessage("The bot has been stoped");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The Bot has been Disconected");
                            await Task.Delay(5000);
                            await discord.Disconnect();
                        }  
                        //return;
                        if (read == "No" || read == "no" || read == "N" || read == "n")
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
                    if (e.User.Id == 245140333330038785)
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
                    /*if (e.Message.Text.ToLower().Contains("ja"))
                    {
                        await e.Channel.SendMessage("nee");
                    }

                    if (e.Message.Text.ToLower().Contains("fuck"))
                    {
                        await e.Channel.SendMessage("nee");
                    }

                    if (e.Message.Text.ToLower().Contains("nee"))
                    {
                        await e.Channel.SendMessage("ja");
                    }*/

                    double num;
                    bool Isnum = true;
                    if (Isnum == double.TryParse(e.Message.Text.ToString(), out num))
                        await e.Channel.SendMessage(e.User.Name + " you meant " + (double.Parse(e.Message.Text.ToString()) + 1).ToString());

                    if (e.Message.Text.ToLower().Contains("*triggerd*"))
                    {
                        await e.Channel.SendTTSMessage($"*Did you just assume my gender?*");
                    }

                    if (e.Message.Text.ToLower().Contains("yuki"))
                    {
                        await e.Channel.SendTTSMessage("YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKIYUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKIYUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI YUKI");
                    }

                    if (e.Message.Text.ToLower().Contains("heil"))
                    {
                        var heil = new List<string>();

                        heil.Add(":no_mouth: :no_mouth: :100: :100: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth:");
                        heil.Add(":no_mouth: :no_mouth: :100: :100: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth:");
                        heil.Add(":no_mouth: :no_mouth: :100: :100: :no_mouth: :no_mouth: :100: :100: :100: :100:");
                        heil.Add(":no_mouth: :no_mouth: :100: :100: :no_mouth: :no_mouth: :100: :100: :100: :100:");
                        heil.Add(":no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth:");
                        heil.Add(":no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth:");
                        heil.Add(":100: :100: :100: :100: :no_mouth: :no_mouth: :100: :100: :no_mouth: :no_mouth:");
                        heil.Add(":100: :100: :100: :100: :no_mouth: :no_mouth: :100: :100: :no_mouth: :no_mouth:");
                        heil.Add(":no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :100: :100: :no_mouth: :no_mouth:");
                        heil.Add(":no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :no_mouth: :100: :100: :no_mouth: :no_mouth:");

                        await e.Channel.SendMessage(string.Join("\n", heil));
                    }

                    if(e.Message.Text.ToLower().Contains("jews?"))
                    {
                        await e.Channel.SendMessage("Do I smell gas?");
                    }
                    if(e.Message.Text.ToLower() =="1")
                    {
                        await e.Channel.SendMessage("2");
                        Console.WriteLine("haha");
                    }
                    if (e.Message.Text.ToLower().Contains("kanker") || e.Message.Text.ToLower().Contains("cancer"))
                    {
                        Message[] messagesToDelete;
                        messagesToDelete = await e.Channel.DownloadMessages(1);
                        await e.Channel.DeleteMessages(messagesToDelete);
                        await e.Channel.SendMessage("You can't say that");
                    }
                    if (e.Message.Text.ToLower() == "rip")
                    {
                        await e.Channel.SendFile(@".\file\rip.jpg");
                    }
                    if (e.Message.Text.ToLower() == "status")
                    {
                        var list = new List<string>();
                        var core = new List<float>();
                        cpuCounter = new PerformanceCounter();
                        cpuCounter.CategoryName = "Processor";
                        cpuCounter.CounterName = "% Processor Time";
                        cpuCounter.InstanceName = "_Total";

                        ramCounter = new PerformanceCounter("Memory", "Available MBytes");

                        int procCount = Environment.ProcessorCount;
                        for (int i = 0; i < procCount; i++)
                        {
                            PerformanceCounter pc = new PerformanceCounter("Processor", "% Processor Time", i.ToString());
                            cpuCounters.Add(pc);
                        }

                        //float cpu = cpuCounter.NextValue();
                        float cpu = procCount;
                        float sum = 0;
                        foreach (PerformanceCounter a in cpuCounters)
                        {
                            sum = sum + a.NextValue();
                            float sum1 = sum / (procCount);
                            core.Add(sum1);
                        }
                        sum = sum / (procCount);
                        float ram = 8000 - ramCounter.NextValue();
                        list.Add("```ini");
                        //list.Add($"??? {string.Join(",", cpuCounters)}");
                        list.Add($"Cpu:         {sum}");
                        list.Add($"core 0:      {core[0]}");
                        list.Add($"core 1:      {core[1]}");
                        list.Add($"core 2:      {core[2]}");
                        list.Add($"core 3:      {core[3]}");
                        list.Add($"Cores:       {cpu}");
                        list.Add($"Ram used:    {ram}");
                        list.Add("```");

                        await e.Channel.SendMessage(string.Join("\n", list));
                        await e.Channel.SendMessage(string.Format("CPU Value 1: {0}, cpu value 2: {1}, RAM value: {2}", sum, cpu, ram));
                        sum = 0;
                        procCount = 0;
                        core.Clear();
                    }
                }
            };
	    
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
                    await e.Channel.SendMessage("666");
                });

                c.CreateCommand("3")
                .Do(async (e) =>
                {
                    var path = @"./file/text.txt";
                    string line = File.ReadLines(path).Skip(3).Take(1).First();

                    await e.Channel.SendMessage($"{line}");
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
            });

            /*discord.MessageReceived += (s, e) =>
            {
                Console.WriteLine($"{e.User} said: {e.Message.Text}");
            };*/

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
            else if (day == "Sunday")
            {
                dayofweek = "Zondag";
            }
                    
        }

        public static async Task<string> searchAnime2(string tag)
        {
            tag = tag.Replace(" ", "_");
            string website = $"https://yande.re/post.xml?limit=100&tags={tag}";
            try
            {
                var toReturn = await Task.Run(async () =>
                {
                    using (var http = new HttpClient())
                    {
                        Utils.AddFakeHeaders(http);
                        var data = await http.GetStreamAsync(website).ConfigureAwait(false);
                        var doc = new XmlDocument();
                        doc.Load(data);

                        var node = doc.LastChild.ChildNodes[Utils.getRandInt(0, 100)];

                        var url = node.Attributes["file_url"].Value;
                        if (!url.StartsWith("http"))
                            url = "https:" + url;
                        return url;
                    }
                }).ConfigureAwait(false);
                return toReturn;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<string> searchAnime(string tag)
        {

            tag = tag.Replace(" ", "_");
            string website = $"https://rule34.xxx/index.php?page=dapi&s=post&q=index&tags={tag}";
            try
            {
                var toReturn = await Task.Run(async () =>
                {
                    using (var http = new HttpClient())
                    {
                        Utils.AddFakeHeaders(http);
                        var data = await http.GetStreamAsync(website).ConfigureAwait(false);
                        var doc = new XmlDocument();
                        doc.Load(data);

                        var node = doc.LastChild.ChildNodes[Utils.getRandInt(0, 100)];

                        var url = node.Attributes["file_url"].Value;
                        if (!url.StartsWith("http"))
                            url = "https:" + url;
                        return url;
                    }
                }).ConfigureAwait(false);
                return toReturn;
            }
            catch (Exception)
            {
                return null;
            }
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
                Console.WriteLine($"[{time.Hour}:{time.Minute}:{time.Second},{time.Millisecond}] [{e.Source}] {e.Message}" 
                                + $"\n{e.Exception}");
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
