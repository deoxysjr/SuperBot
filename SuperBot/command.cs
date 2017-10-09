using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Discord;
using Discord.Commands;
using System.Timers;
using System.Net;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml;
using Newtonsoft.Json;
using System.Net.Http;

namespace Superbot
{
    class command
    {
        internal static void Commands(CommandService commands, DiscordClient discord)
        {
            Random rand;

            string[] Hentai; string[] gifs; string[] funny; string[] music;
            string[] lol; string[] oppai; string[] picture; string[] tags;
            string[] tags2; string[] letter;
            

            Hentai = new string[] {"Hentai/Hentai 1.gif", "Hentai/Hentai 2.jpg", "Hentai/Hentai 3.gif", "Hentai/Hentai 4.jpg", "Hentai/Hentai 5.gif", "Hentai/Hentai 6.jpg" };
            gifs = new string[] {"./Gif's/gif1.gif", "./Gif's/gif2.gif", "./Gif's/gif3.gif", "./Gif's/gif4.gif", "./Gif's/gif5.gif", "./Gif's/gif6.gif", "./Gif's/gif7.gif",
                                 "./Gif's/gif8.gif", "./Gif's/gif9.gif", "./Gif's/gif10.gif", "./Gif's/gif11.gif"};
            funny = new string[] {"./funny picture/funny1.jpg", "./funny picture/funny2.jpg", "./funny picture/funny3.jpg", "./funny picture/funny4.jpg", "./funny picture/funny5.jpg", "./funny picture/funny6.jpg",
                                  "./funny picture/funny7.jpg", "./funny picture/funny8.jpg", "./funny picture/funny9.jpg", "./funny picture/funny10.jpg", "./funny picture/funny11.jpg", "./funny picture/funny12.jpg",
                                  "./funny picture/funny13.jpg", "./funny picture/funny14.jpg", "./funny picture/funny15.jpg", "./funny picture/funny16.jpg", "./funny picture/funny17.jpg", "./funny picture/funny18.jpg",
                                  "./funny picture/funny19.jpg", "./funny picture/funny20.jpg", "./funny picture/funny21.jpg", "./funny picture/funny22.jpg", "./funny picture/funny23.jpg", "./funny picture/funny24.jpg",
                                  "./funny picture/funny25.jpg", "./funny picture/funny26.jpg", "./funny picture/funny27.jpg", "./funny picture/funny28.jpg"};
            music = new string[] {"https://www.youtube.com/watch?v=vFMA5oAXZy8", "https://www.youtube.com/watch?v=WPXOGR8Z5S4&t=1541s", "https://www.youtube.com/watch?v=B5Puhn0uRb4",
                                  "https://www.youtube.com/watch?v=L9r-aOQb3XM", "https://www.youtube.com/watch?v=bDtam1dO_xI"};
            lol = new string[] {"https://www.youtube.com/watch?v=bhBGFy7SKCc", "https://www.youtube.com/watch?v=eCkho9tAhQo", "https://www.youtube.com/watch?v=2552wmwL3i0",
                                "https://www.youtube.com/watch?v=YwKfUoJ_ERY", "http://imgur.com/V06OE0B"};
            oppai = new string[] {"Boobs/boobs 1.jpg", "Boobs/boobs 2.jpg", "Boobs/boobs 3.jpg", "Boobs/boobs 4.jpg", "Boobs/boobs 5.jpg", "Boobs/boobs 6.jpg", "Boobs/boobs 7.jpg", "Boobs/boobs 8.jpg",
                                  "Boobs/boobs 9.jpg", "Boobs/boobs 10.jpg", "Boobs/boobs 11.jpg", "Boobs/boobs 12.jpg", "Boobs/boobs 13.jpg", "Boobs/boobs 14.jpg", "Boobs/boobs 15.jpg",
                                  "Boobs/boobs 16.jpg", "Boobs/boobs 17.jpg", "Boobs/boobs 18.jpg", "Boobs/boobs 19.jpg", "Boobs/boobs 20.jpg", "Boobs/boobs 21.jpg", "Boobs/boobs 22.jpg","Boobs/boobs 23.jpg"};
            picture = new string[] {"afbeeldingen/afbeelding1.jpg", "afbeeldingen/afbeelding2.jpg", "afbeeldingen/afbeelding3.jpg", "afbeeldingen/afbeelding4.jpg", "afbeeldingen/afbeelding5.jpg",
                                    "afbeeldingen/afbeelding6.jpg", "afbeeldingen/afbeelding7.jpg", "afbeeldingen/afbeelding8.jpg"};
            tags = new string[] {"black eyes", "black hair", "blue eyes", "blush", "brown eyes", "brown hair","circlet", "fate/grand order", "harukawa maki", "ikari mio",
                                 "jeanne alter", "long hair", "white hair", "twintails", "vore", "Phineas_and_Ferb", "furry", "futa"};
            tags2 = new string[] {"anal", "animal ears", "nipples", "pussy", "uncensored", "torn clothes", "penis", "breasts", "bondage", "nopan", "naked", "loli", "cum", "topless", "bottomless",
                                  "no bra", "swimsuits", "cameltoe", "open shirt", "see through", "wet", "cleavage", "erect nipples", "shirt lift", "thighhighs", "undressing", "breast hold",
                                  "feet", "bikini", "sword", "sex", "pussy juice", "monster", "yuri", "fingering", "bodysuit", "overwatch"};
            letter = new string[] {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
            Dictionary<char, string> charToMorse = new Dictionary<char, string>() { {'a', ".-"}, {'b', "-..."}, {'c', "–.-."}, {'d', "–.."}, {'e', "."}, {'f', "..-."}, {'g', "--."}, {'h', "...."}, {'i', ".."},  {'j', ".---"}, {'k', "-.-"}, {'l', ".-.."}, {'m', "--"}, {'n', "–."}, {'o', "---"}, {'p', ".--."}, {'q', "--.-"}, {'r', ".-."}, {'s', "..."}, {'t', "-"}, {'u', "..-"}, {'v', "...-"}, {'w', ".--"}, {'x', "–..-"}, {'y', "-.--"}, {'z', "--.."}, {'0', "-----"}, {'1', ".----"}, {'2', "..---"}, {'3', "...--"}, {'4', "....-"}, {'5', "....."}, {'6', "–...."}, {'7', "--..."}, {'8', "---.."}, {'9', "----."}, {' ', "/"}, {'.', ".-.-.-"}, {',', "--..--"}, {':', "---..."}, {'?', "..--.."}, {'!', "..--."}, {'\\', ".----."}, {'-', "-....-"}, {'/', "-..-."}, {'”', ".-..-."}, {'@', ".--.-."}, {'=', "–...-"} };


            rand = new Random();

            //
            //                         Commands
            //

            commands.CreateCommand("commands")
                .Do(async (e) =>
                {
                    var serverlist = new List<string>();
                    int count = commands.AllCommands.Count();
                    await e.Channel.SendMessage($"I have {count} commands");
                    serverlist.Add("```ini");
                    foreach (var server in commands.AllCommands)
                    {
                        
                        serverlist.Add($"{server.Text}");
                    }
                    serverlist.Add("```");
                    await e.Channel.SendMessage($"{string.Join(", ", serverlist)}");
                });

            commands.CreateCommand("Playing")
                .Alias(new string[] { "play" })
                .Parameter("text", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    CommandUsed.CommandAdd();

                    if (e.User.Id == 245140333330038785)
                    {
                        string text = e.Args[0];
                        await e.Channel.SendMessage("Your bot is now playing: " + $"(**{text}**)");
                        discord.SetGame(text);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"{e.User} used %playing");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"Your bot is now playing: ({text})");
                        Console.ResetColor();
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
                    Message[] messagesToDelete2;
                    messagesToDelete = await e.Channel.DownloadMessages(100);
                    await e.Channel.DeleteMessages(messagesToDelete);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.Channel.Name}] [{e.User.Name}] Used %clear and deleted ({messagesToDelete.Count()}) messages");
                    Console.ResetColor();
                    CommandUsed.ClearAdd(messagesToDelete.Count());
                    await Task.Delay(20);
                    await e.Channel.SendMessage($":white_check_mark: I cleared the chat {e.User.Name} I deleted ({messagesToDelete.Count()}) messages");
                    await Task.Delay(2000);
                    messagesToDelete2 = await e.Channel.DownloadMessages(3);
                    await e.Channel.DeleteMessages(messagesToDelete2);
                });

            commands.CreateCommand("clear 10")
                .Alias(new string[] { "clr 10" })
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %clear 10");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    Message[] messagesToDelete;
                    Message[] messagesToDelete2;
                    messagesToDelete = await e.Channel.DownloadMessages(10);
                    await e.Channel.DeleteMessages(messagesToDelete);
                    CommandUsed.ClearAdd(messagesToDelete.Count());
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
                    Console.ResetColor();
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
                    Console.ResetColor();
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
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    int randomMusicIndex = rand.Next(music.Length);
                    string musicToPost = music[randomMusicIndex];
                    await e.Channel.SendMessage(musicToPost);
                });

            commands.CreateCommand("oppai")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %oppai");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    int randomOppaiIndex = rand.Next(oppai.Length);
                    string oppaiToPost = oppai[randomOppaiIndex];
                    await e.Channel.SendFile(oppaiToPost);
                });
            commands.CreateCommand("hentai")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %hentai");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    int randomHentai = rand.Next(Hentai.Length);
                    string HentaiToPost = Hentai[randomHentai];
                    await e.Channel.SendFile(HentaiToPost);
                });

            commands.CreateCommand("gif")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %gif");
                    Console.ResetColor();
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
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    int randomFunny = rand.Next(funny.Length);
                    string funnyToPost = funny[randomFunny];
                    await e.Channel.SendFile(funnyToPost);
                });

            commands.CreateCommand("CommandsCount")
                .Alias(new string[] { "cc" })
                .Do(async (e) =>
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load("./file/commandsused.xml");
                    string c = xDoc.SelectSingleNode("root/commands").InnerText;
                    await e.Channel.SendMessage($"the amount of commands = **{c}**");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %CommandsCount = ({c})");
                    Console.ResetColor();
                });

            commands.CreateCommand("CommandsCountClear")
                .Alias(new string[] { "ccclear" })
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %CommandsCountClear");
                    Console.ResetColor();
                    if (e.User.Id == 245140333330038785)
                    {
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.Load("./file/commandsused.xml");
                        xDoc.SelectSingleNode("root/commands").InnerText = "0";
                        xDoc.Save("./file/commandsused.xml");
                        await e.Channel.SendMessage("CommandsUsed is set to 0");
                    }
                    else
                        await e.Channel.SendMessage("You don't have that permision");

                });
				
            commands.CreateCommand("MessageClearCount")
                .Alias(new string[] { "mcc" })
                .Do(async (e) =>
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load("./file/commandsused.xml");
                    string c = xDoc.SelectSingleNode("root/clear").InnerText;
                    await e.Channel.SendMessage($"the amount of messages deleted = **{c}**");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %MessageClearCount = ({c})");
                    Console.ResetColor();
                    
                });

            commands.CreateCommand("MessageClearCountClear")
                .Alias(new string[] { "mccc" })
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %MessageClearCountClear");
                    Console.ResetColor();
                    if (e.User.Id == 245140333330038785)
                    {
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.Load("./file/commandsused.xml");
                        xDoc.SelectSingleNode("root/commands").InnerText = "0";
                        xDoc.Save("./file/commandsused.xml");
                        await e.Channel.SendMessage("message clear count is set to 0");
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
                    Console.ResetColor();
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
                    Console.ResetColor();
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
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    await e.Channel.SendFile($@"./file/{e.Args[0]}.txt");
                });

            commands.CreateCommand("cleartext")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %cleartext and cleared the file text.txt");
                    Console.ResetColor();
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
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    await e.Channel.SendMessage($":wave::skin-tone-1: Bye Bye! {e.User.Name} :wave::skin-tone-1:");
                });

            commands.CreateCommand("hello")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %hello");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    await e.Channel.SendMessage($"hello! {e.User.Name}");
                });

            commands.CreateCommand("dead")
                .Alias(new string[] { ":skull_crossbones:" })
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %dead");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    await e.Channel.SendMessage(":skull_crossbones: :knife: kill your self :knife: :skull_crossbones:");
                });

            commands.CreateCommand("NL")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %nl");
                    Console.ResetColor();
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
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    await e.Channel.SendMessage(":grinning: :joy: :grinning: :joy: :grinning: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :grinning: :joy: :joy: ");
                });

            commands.CreateCommand("lol")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %lol");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    await e.Channel.SendMessage("https://www.youtube.com/watch?v=bhBGFy7SKCc");
                });

            commands.CreateCommand("hi")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %hi");
                    Console.ResetColor();
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
                    Console.ResetColor();
                });

            commands.CreateCommand("nice")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %nice haha noob");
                    Console.ResetColor();
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
                    Console.ResetColor();
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
                    Console.ResetColor();
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
                        await e.Channel.SendMessage("You don't have the admin permission for this command!"+
                            "\nYou need the role Admin for this command");
                    }
                });

            commands.CreateCommand("time")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %time");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    var MessageSent = e.Message.Timestamp;


                    MyBot.month();
                    days.specialdays.speciald();
                    MyBot.dayOfweek(e);

                    var time = new List<string>();

                    time.Add("```ini");
                    time.Add($"Year   = ({MessageSent.Year})");
                    time.Add($"Month  = ({MessageSent.Month}, {MyBot.Month})");
                    time.Add($"Day    = ({MyBot.dayofweek + " Or " + MessageSent.DayOfWeek}  - {MessageSent.Day})");
                    time.Add($"Hour   = ({MessageSent.Hour + 2})");
                    time.Add($"Minute = ({MessageSent.Minute})");
                    time.Add($"Is it a specialday today: {days.specialdays.spday}");
                    if (days.specialdays.spday == true)
                    {
                        time.Add("today is a specialday it is");
                        time.Add($"         ({days.specialdays.specialday})");
                        time.Add($"{days.specialdays.specialdaydescription}");
                    }
                    time.Add("```");

                    await e.Channel.SendMessage(string.Join("\n", time));
                });

            commands.CreateCommand("date")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %date");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    MyBot.MessageSent = e.Message.Timestamp;
                    await e.Channel.SendMessage($"``{MyBot.MessageSent.DayOfWeek} {MyBot.MessageSent.Day}-{MyBot.MessageSent.Month}-{MyBot.MessageSent.Year} ``");
                });

            commands.CreateCommand("uptime")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %uptime");
                    Console.ResetColor();
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
                    Console.ResetColor();
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
                    Console.ResetColor();
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

            commands.CreateCommand("itsnotrape")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %itsnotrape");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    await e.Channel.SendMessage("There is no such thing as rape. Any female who leaves her rightful place in the house and the kitchen is fucking begging for cock in her holes. If she gets the cock she so badly is asking for, it's not fucking rape, it's a damn slut getting what she fucking deserves. Males still rule this fucking world. In most of the world, a fucking bitch can get killed for looking at a man straight in the eye. In America and Europe, every day dumb sluts get their holes penetrated without their so - called consent, which isn't rape, just them getting the fucking dick they deserve up their asses. Sexual abuse is on the rise, spousal abuse is on the rise and more bitches die every year. Fucking cunts. I am so glad I was born a man. I am so glad there is a bunch of retarded sluts jumping trough hoops just to get my cock. Haha, females are so fucking sad. We treat you bitches like shit, and you still spend time, money and effort on trying to look good for us. Way to be a good slave, whores. Now keep acting like sluts and sucking our cocks. And if you change your mind after you leave the house, too fucking bad, you're getting your holes fucked and there isn't shit you can do about it because that's your only fucking purpose in life.");
                });

            commands.CreateCommand("picture")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %picture");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    int randomPictueIndex = rand.Next(picture.Length);
                    string pictureToPost = picture[randomPictueIndex];
                    await e.Channel.SendFile(pictureToPost);
                });

            commands.CreateCommand("yandere")
                .Parameter("tag", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    CommandUsed.CommandAdd();
                    if (!e.Channel.Name.Contains("nsfw"))
                    {
                        await e.Channel.SendMessage("That is a NSFW only command!");
                        return;
                    }
                    string args = e.Args[0];
                    string tag;
                    if (args == "")
                    {
                        tag = tags2[Utils.getRandInt(0, tags2.Length - 1)];
                    }
                    else
                    {
                        tag = string.Join("_", args);
                    }
                    var url = await MyBot.searchAnime2(tag).ConfigureAwait(false);
                    await e.Channel.SendMessage($"Finding yandere image... tag=(**{tag}**)");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %yander {tag} result = ");
                    Console.ResetColor();

                    if (url == null)
                    {
                        await e.Channel.SendMessage("No results");
                        Console.Write($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] ");
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"No results");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine(url);
                        Console.ResetColor();
                        using (var client = new WebClient())
                        {
                            try
                            {
                                if (url.Contains(".gif"))
                                {
                                    client.DownloadFile(url, $@"./nsfw.gif");
                                    await e.Channel.SendFile($@"./nsfw.gif");
                                    File.AppendAllText(@"./file/nsfw_yandere_links.txt", $"\r\n[{DateTime.Now.TimeOfDay}] {e.User.Name} used %yandere with the tag ({tag}) the result was");
                                    File.AppendAllText(@"./file/nsfw_yandere_links.txt", $"\r\n{url}");
                                    File.AppendAllText(@"./file/nsfw_yandere_links.txt", "\r\n ");
                                    File.AppendAllText($@"./file/yandere/users/{e.User.Id} - {e.User.Name}.txt", $"\r\n[{DateTime.Now.TimeOfDay}] {e.User.Name} used %yander with the tag ({tag}) the result was");
                                    File.AppendAllText($@"./file/yandere/users/{e.User.Id} - {e.User.Name}.txt", $"\r\n{url}");
                                    File.AppendAllText($@"./file/yandere/users/{e.User.Id} - {e.User.Name}.txt", "\r\n ");
                                }
                                if (url.Contains(".png"))
                                {
                                    client.DownloadFile(url, $@"./nsfw.jpg");
                                    await e.Channel.SendFile($@"./nsfw.jpg");
                                    File.AppendAllText(@"./file/nsfw_yandere_links.txt", $"\r\n[{DateTime.Now.TimeOfDay}] {e.User.Name} used %yandere with the tag ({tag}) the result was");
                                    File.AppendAllText(@"./file/nsfw_yandere_links.txt", $"\r\n{url}");
                                    File.AppendAllText(@"./file/nsfw_yandere_links.txt", "\r\n ");
                                    File.AppendAllText($@"./file/yandere/users/{e.User.Id} - {e.User.Name}.txt", $"\r\n[{DateTime.Now.TimeOfDay}] {e.User.Name} used %yander with the tag ({tag}) the result was");
                                    File.AppendAllText($@"./file/yandere/users/{e.User.Id} - {e.User.Name}.txt", $"\r\n{url}");
                                    File.AppendAllText($@"./file/yandere/users/{e.User.Id} - {e.User.Name}.txt", "\r\n ");
                                }
                                if (url.Contains(".jpg"))
                                {
                                    client.DownloadFile(url, $@"./nsfw.jpg");
                                    await e.Channel.SendFile($@"./nsfw.jpg");
                                    File.AppendAllText(@"./file/nsfw_yandere_links.txt", $"\r\n[{DateTime.Now.TimeOfDay}] {e.User.Name} used %yandere with the tag ({tag}) the result was");
                                    File.AppendAllText(@"./file/nsfw_yandere_links.txt", $"\r\n{url}");
                                    File.AppendAllText(@"./file/nsfw_yandere_links.txt", $"\r\n ");
                                    File.AppendAllText($@"./file/yandere/users/{e.User.Id} - {e.User.Name}.txt", $"\r\n[{DateTime.Now.TimeOfDay}] {e.User.Name} used %yander with the tag ({tag}) the result was");
                                    File.AppendAllText($@"./file/yandere/users/{e.User.Id} - {e.User.Name}.txt", $"\r\n{url}");
                                    File.AppendAllText($@"./file/yandere/users/{e.User.Id} - {e.User.Name}.txt", "\r\n ");
                                }
                                if (url.Contains(".webm"))
                                {
                                    await e.Channel.SendMessage(url);
                                    File.AppendAllText(@"./file/nsfw_yandere_links.txt", $"\r\n[{DateTime.Now.TimeOfDay}] {e.User.Name} used %yandere with the tag {tag} the result was");
                                    File.AppendAllText(@"./file/nsfw_yandere_links.txt", $"\r\n{url}");
                                    File.AppendAllText(@"./file/nsfw_yandere_links.txt", "\r\n ");
                                    File.AppendAllText($@"./file/yandere/users/{e.User.Id} - {e.User.Name}.txt", $"\r\n[{DateTime.Now.TimeOfDay}] {e.User.Name} used %yander with the tag ({tag}) the result was");
                                    File.AppendAllText($@"./file/yandere/users/{e.User.Id} - {e.User.Name}.txt", $"\r\n{url}");
                                    File.AppendAllText($@"./file/yandere/users/{e.User.Id} - {e.User.Name}.txt", "\r\n ");
                                }
                                //client.DownloadFile(url, @"./nsfw.png");
                                //client.DownloadFile(url, @"./nsfw.gif");
                                //await e.Channel.SendFile(@"./nsfw.gif");
                                //await e.Channel.SendFile(@"./nsfw.png");
                                if (File.Exists(@"./nsfw.gif"))
                                {
                                    File.Delete(@"./nsfw.gif");
                                }
                                if (File.Exists(@"./nsfw.jpg"))
                                {
                                    File.Delete(@"./nsfw.jpg");
                                }
                            }
                            catch (Exception s)
                            {
                                if (s.Message.ToString().Contains("413"))
                                {
                                    long length = new FileInfo("./nsfw").Length;
                                    await e.Channel.SendMessage($"An error occured :(\n{s.Message.ToString()} {length}");
                                }
                                else
                                {
                                    await e.Channel.SendMessage($"An error occured :(\n{s.Message.ToString()}");
                                }
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] An error occured :(\n{s.Message.ToString()}");
                                Console.ResetColor();
                            }
                        }
                    }

                });

            commands.CreateCommand("rule34")
                .Parameter("tag", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    CommandUsed.CommandAdd();
                    if (!e.Channel.Name.Contains("nsfw"))
                    {
                        await e.Channel.SendMessage("That is a NSFW only command!");
                        return;
                    }
                    string args = e.Args[0];
                    string tag;
                    if (args == "")
                    {
                        tag = tags[Utils.getRandInt(0, tags.Length - 1)];
                    }
                    else
                    {
                        tag = string.Join("_", args);
                    }

                    var url = await MyBot.searchAnime(tag).ConfigureAwait(false);
                    await e.Channel.SendMessage($"Finding rule34 image... tag=(**{tag}**)");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %rule34 {tag} result = ");
                    Console.ResetColor();

                    if (url == null)
                    {
                        await e.Channel.SendMessage("No results");
                        Console.Write($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] ");
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"No results");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine(url);
                        Console.ResetColor();
                        using (var client = new WebClient())
                        {
                            try
                            {
                                if (url.Contains(".gif"))
                                {
                                    client.DownloadFile(url, @"./nsfw.gif");
                                    await e.Channel.SendIsTyping();
                                    await e.Channel.SendFile(@"./nsfw.gif");
                                    File.AppendAllText(@"./file/nsfw_rule34_links.txt", $"\r\n{e.User.Name} used %rule34 with the tag ({tag}) the result was");
                                    File.AppendAllText(@"./file/nsfw_rule34_links.txt", $"\r\n{url}");
                                    File.AppendAllText(@"./file/nsfw_rule34_links.txt", "\r\n ");
                                    File.AppendAllText($@"./file/rule34/{e.User.Id}.txt", $"\r\n{e.User.Name} used %rule34 with the tag ({tag}) the result was");
                                    File.AppendAllText($@"./file/rule34/{e.User.Id}.txt", $"\r\n{url}");
                                    File.AppendAllText($@"./file/rule34/{e.User.Id}.txt", "\r\n ");
                                }
                                if (url.Contains(".png"))
                                {
                                    client.DownloadFile(url, @"./nsfw.png");
                                    await e.Channel.SendIsTyping();
                                    await e.Channel.SendFile(@"./nsfw.png");
                                    File.AppendAllText(@"./file/nsfw_rule34_links.txt", $"\r\n{e.User.Name} used %rule34 with the tag ({tag}) the result was");
                                    File.AppendAllText(@"./file/nsfw_rule34_links.txt", $"\r\n{url}");
                                    File.AppendAllText(@"./file/nsfw_rule34_links.txt", "\r\n ");
                                    File.AppendAllText($@"./file/rule34/{e.User.Id}.txt", $"\r\n{e.User.Name} used %rule34 with the tag ({tag}) the result was");
                                    File.AppendAllText($@"./file/rule34/{e.User.Id}.txt", $"\r\n{url}");
                                    File.AppendAllText($@"./file/rule34/{e.User.Id}.txt", "\r\n ");
                                }
                                if (url.Contains(".jpg"))
                                {
                                    client.DownloadFile(url, @"./nsfw.jpg");
                                    await e.Channel.SendIsTyping();
                                    await e.Channel.SendFile(@"./nsfw.jpg");
                                    File.AppendAllText(@"./file/nsfw_rule34_links.txt", $"\r\n{e.User.Name} used %rule34 with the tag ({tag}) the result was");
                                    File.AppendAllText(@"./file/nsfw_rule34_links.txt", $"\r\n{url}");
                                    File.AppendAllText(@"./file/nsfw_rule34_links.txt", $"\r\n ");
                                    File.AppendAllText($@"./file/rule34/{e.User.Id}.txt", $"\r\n{e.User.Name} used %rule34 with the tag ({tag}) the result was");
                                    File.AppendAllText($@"./file/rule34/{e.User.Id}.txt", $"\r\n{url}");
                                    File.AppendAllText($@"./file/rule34/{e.User.Id}.txt", "\r\n ");
                                }
                                if (url.Contains(".webm"))
                                {
                                    await e.Channel.SendIsTyping();
                                    await e.Channel.SendMessage(url);
                                    File.AppendAllText(@"./file/nsfw_rule34_links.txt", $"\r\n{e.User.Name} used %rule34 with the tag ({tag}) the result was");
                                    File.AppendAllText(@"./file/nsfw_rule34_links.txt", $"\r\n{url}");
                                    File.AppendAllText(@"./file/nsfw_rule34_links.txt", "\r\n ");
                                    File.AppendAllText($@"./file/rule34/{e.User.Id}.txt", $"\r\n{e.User.Name} used %rule34 with the tag ({tag}) the result was");
                                    File.AppendAllText($@"./file/rule34/{e.User.Id}.txt", $"\r\n{url}");
                                    File.AppendAllText($@"./file/rule34/{e.User.Id}.txt", "\r\n ");
                                }
                                if (url.Contains(".jpeg"))
                                {
                                    client.DownloadFile(url, @"./nsfw.jpeg");
                                    await e.Channel.SendFile(@"./nsfw.jpeg");
                                    File.AppendAllText(@"./file/nsfw_rule34_links.txt", $"\r\n{e.User.Name} used %rule34 with the tag ({tag}) the result was");
                                    File.AppendAllText(@"./file/nsfw_rule34_links.txt", $"\r\n{url}");
                                    File.AppendAllText(@"./file/nsfw_rule34_links.txt", "\r\n ");
                                    File.AppendAllText($@"./file/rule34/{e.User.Id}.txt", $"\r\n{e.User.Name} used %rule34 with the tag ({tag}) the result was");
                                    File.AppendAllText($@"./file/rule34/{e.User.Id}.txt", $"\r\n{url}");
                                    File.AppendAllText($@"./file/rule34/{e.User.Id}.txt", "\r\n ");
                                }
                                //client.DownloadFile(url, @"./nsfw.png");
                                //client.DownloadFile(url, @"./nsfw.gif");
                                //await e.Channel.SendFile(@"./nsfw.gif");
                                //await e.Channel.SendFile(@"./nsfw.png");
                                if (File.Exists(@"./nsfw.png"))
                                {
                                    File.Delete(@"./nsfw.png");
                                }
                                if (File.Exists(@"./nsfw.gif"))
                                {
                                    File.Delete(@"./nsfw.gif");
                                }
                                if (File.Exists(@"./nsfw.jpg"))
                                {
                                    File.Delete(@"./nsfw.jpg");
                                }
                                if (File.Exists(@"./nsfw.jpeg"))
                                {
                                    File.Delete(@"./nsfw.jpeg");
                                }
                            }
                            catch (Exception s)
                            {
                                await e.Channel.SendMessage($"An error occured :(\n{s.Message.ToString()}");
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] An error occured :(\n{s.Message.ToString()}");
                                Console.ResetColor();
                            }
                        }
                    }

                });

            commands.CreateCommand("airlock")
                .Parameter("name", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %airlock {e.Args[0]}");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
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
                        if (u.Id == 245140333330038785)
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

            commands.CreateCommand("spam")
                .Parameter("stop", ParameterType.Optional)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %spam");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    var now = DateTime.Now;

                    if (e.Args[0] == "stop")
                    {
                        return;
                    }

                    //var spam = new List<string>();
                    if (MyBot.spamnum < 4)
                    {
                        MyBot.spamnum++;
                        for (long i = 0 + 1; i >= 0; i++)
                        {
                            if (i < 51)
                            {
                                //spam.Add($"[{rand.Next(1, 201)}]");
                                await Task.Delay(1020);
                                await e.Channel.SendMessage($"{i.ToString()}");
                            }
                            else
                            {
                                //await e.Channel.SendMessage(string.Join(", ", spam));
                                await e.Channel.SendMessage($"{DateTime.Now - now}");
                                MyBot.spamnum--;
                                break;
                            }
                        }
                    }
                    else
                    {
                        await e.Channel.SendMessage("there are already 3 spam commands used wait until the are done");
                    }
                });

            commands.CreateCommand("random")
                .Parameter("minval", ParameterType.Optional)
                .Parameter("maxval", ParameterType.Optional)
                .Parameter("amount", ParameterType.Optional)
                .Do(async (e) =>
                {
                    CommandUsed.CommandAdd();
                    var spam = new List<string>();

                    int minval = int.Parse(e.Args[0]);
                    int maxval = int.Parse(e.Args[1]);
                    int amount = int.Parse(e.Args[2]);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %random min= {minval}, max= {maxval}, amount= {amount}");
                    Console.ResetColor();

                    if (e.Args[0] == null)
                        minval = 1;
                    if (e.Args[1] == null)
                        maxval = 10;
                    if (e.Args[2] == null)
                        amount = 1;

                    if (minval > maxval)
                    {
                        await e.Channel.SendMessage("You are so stupid you can't even count, because your minimun number is higher then your maximun number");
                        return;
                    }

                    if (amount > 399 || maxval > 9 && amount > 398)
                    {
                        await e.Channel.SendMessage("I can't sent that many");
                        return;
                    }

                    spam.Add("```");

                    for (long i = 0; i >= 0; i++)
                    {

                        if (i < amount)
                        {
                            spam.Add($"[{rand.Next(minval, maxval + 1)}], ");
                        }
                        else
                        {
                            spam.Add("```");
                            await e.Channel.SendMessage(string.Join("", spam));
                            break;
                        }
                    }
                });

            commands.CreateCommand("max val")
                .Parameter("int type", ParameterType.Required)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %max val");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    var op = e.Args[0];
                    if (op == "int16" || op == "short")
                    {
                        await e.Channel.SendMessage($"{short.MaxValue}");
                    }
                    if (op == "int32" || op == "int")
                    {
                        await e.Channel.SendMessage($"{int.MaxValue}");
                    }
                    if (op == "int64" || op == "long")
                    {
                        await e.Channel.SendMessage($"{long.MaxValue}");
                    }
                    if (op == "double")
                    {
                        await e.Channel.SendMessage($"{double.MaxValue}");
                    }
                });

            commands.CreateCommand("min val")
                .Parameter("int type", ParameterType.Required)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %min val");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    var op = e.Args[0];
                    if (op == "int16" || op == "short")
                    {
                        await e.Channel.SendMessage($"{short.MinValue}");
                    }
                    if (op == "int32" || op == "int")
                    {
                        await e.Channel.SendMessage($"{int.MinValue}");
                    }
                    if (op == "int64" || op == "long")
                    {
                        await e.Channel.SendMessage($"{long.MinValue}");
                    }
                    if (op == "double")
                    {
                        await e.Channel.SendMessage($"{double.MinValue}");
                    }
                });

            commands.CreateCommand("password")
                .Parameter("nummer", ParameterType.Required)
                .Do(async (e) =>
                {
                    CommandUsed.CommandAdd();
                    var password = new List<string>();
                    
                    if (int.Parse(e.Args[0]) < 21 && int.Parse(e.Args[0]) > 3)
                    {
                        for (int i = 0; i >= 0; i++)
                        {

                            if (i < int.Parse(e.Args[0]))
                            {
                                int randomLetter = rand.Next(letter.Length);
                                string LetterToPost = letter[randomLetter];
                                password.Add(LetterToPost);
                            }
                            else
                            {
                                await e.Channel.SendMessage($"Your random password = {string.Join("", password)}");
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %password and the result was: {string.Join("", password)}");
                                Console.ResetColor();
                                break;
                            }
                        }
                        
                    }
                    else
                    {
                        await e.Channel.SendMessage("sorry I can only do 4 up to 20");
                    }
                    
                });

            commands.CreateCommand("servers")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %servers");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    await e.Channel.SendMessage($"{string.Join(", ", discord.Servers.Count())}");
                });

            commands.CreateCommand("mach")
                .Parameter("mach", ParameterType.Required)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %mach {e.Args[0]}");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    double machm_s = double.Parse(e.Args[0]) * 340.29;
                    double machkm_h = double.Parse(e.Args[0]) * 1225.044;
                    await e.Channel.SendMessage($"mach {e.Args[0]}: {machm_s}m/s or {machkm_h} km/h");
                });

            commands.CreateCommand("user url")
                .Parameter("user", ParameterType.Optional)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %user url");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
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

                    var url = u.AvatarUrl;

                    using (var client = new WebClient())
                    {
                        try
                        {
                            client.DownloadFile(url, "user.jpg");
                        }
                        catch(Exception s)
                        {
                            Console.WriteLine(s.Message.ToString());
                        }
                    }
                    await e.Channel.SendFile(@"user.jpg");
                    if (File.Exists(@"user.jpg"))
                        File.Delete(@"user.jpg");
                });

            commands.CreateCommand("myspeed")
                .Parameter("type", ParameterType.Required)
                .Parameter("time", ParameterType.Required)
                .Parameter("distance", ParameterType.Required)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %myspeed");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    string type = e.Args[0];
                    string dist = e.Args[1];
                    string time = e.Args[2];

                    if (type == "m/s")
                    {
                        Console.WriteLine($"{dist} + {time}");
                        double dist_m = double.Parse(dist);
                        double time_s = double.Parse(time);
                        Console.WriteLine($"{dist} + {time}");

                        var sum1 = dist_m / time_s;
                        var sum2 = dist_m / time_s * 3.6;

                        await e.Channel.SendMessage($"m/s: {sum1}, km/h: {sum2}");
                        return;
                    }
                    if (type == "km/h")
                    {
                        Console.WriteLine($"{dist} + {time}");
                        double dist_km = double.Parse(dist);
                        double time_h = double.Parse(time);

                        var sum1 = (dist_km / time_h) / 3.6;
                        var sum2 = dist_km / time_h;
                    

                        await e.Channel.SendMessage($"m/s: {sum1}, km/h: {sum2}");
                        return;
                    }
                    if (type == "m/h")
                    {
                        Console.WriteLine($"{dist} + {time}");
                        double dist_m = double.Parse(dist);
                        double time_h = double.Parse(time);
                        Console.WriteLine($"{dist} + {time}");

                        var sum = dist_m / time_h;
                        var sum1 = (dist_m / time_h) / 3600;
                        var sum2 = (dist_m / time_h) / 1000;

                        await e.Channel.SendMessage($"m/h: {sum} m/s: {sum1}, km/h: {sum2}");
                        return;
                    }
                    if (type == "km/s")
                    {
                        Console.WriteLine($"{dist} + {time}");
                        double dist_km = double.Parse(dist);
                        double time_s = double.Parse(time);
                        Console.WriteLine($"{dist} + {time}");

                        var sum1 = (dist_km / time_s) * 1000;
                        var sum2 = (dist_km / time_s) * 3600;

                        await e.Channel.SendMessage($"m/s: {sum1}, km/h: {sum2}");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"sorry but {type} {dist} {time} does not work");
                        await e.Channel.SendMessage($"sorry but {type} {dist} {time} does not work");
                    }
                });

            commands.CreateCommand("temp")
                .Parameter("temp type", ParameterType.Multiple)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %temp");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    var list = new List<string>();
                    var temp_type = e.Args[0];
                    var temp = double.Parse(e.Args[1]);
                    
                    if (temp.ToString().Contains("."))
                        temp = double.Parse(temp.ToString().Replace(".", ","));

                    if (temp_type == "C" || temp_type == "c")
                    {
                        var temp_C = temp;
                        var temp_F = temp * 1.8 + 32;
                        var temp_K = temp * 1 + 273.15;
                        var temp_R = temp * 1.8 + 491.67;

                        list.Add($"Celsius:         {temp_C}°C");
                        list.Add($"Fahrenheit:   {temp_F}°F");
                        list.Add($"Kelvin:           {temp_K}°K");
                        list.Add($"Rankine:        {temp_R}°R");

                        await e.Channel.SendMessage(string.Join("\n", list));
                    }

                    if (temp_type == "F" || temp_type == "f")
                    {
                        var temp_C = (temp - 32) / 1.8;
                        var temp_F = temp;
                        var temp_K = (temp - 32) / 1.8 + 273.15;
                        var temp_R = (temp - 32) + 491.67;

                        list.Add($"Celsius:         {temp_C}°C");
                        list.Add($"Fahrenheit:   {temp_F}°F");
                        list.Add($"Kelvin:           {temp_K}°K");
                        list.Add($"Rankine:        {temp_R}°R");

                        await e.Channel.SendMessage(string.Join("\n", list));
                    }

                    if (temp_type == "K" || temp_type == "k")
                    {
                        var temp_C = temp - 273.15;
                        var temp_F = (temp - 273.15) * 1.8 + 32;
                        var temp_K = temp;
                        var temp_R = (temp - 273.15) * 1.8 + 491.67;

                        list.Add($"Celsius:         {temp_C}°C");
                        list.Add($"Fahrenheit:   {temp_F}°F");
                        list.Add($"Kelvin:           {temp_K}°K");
                        list.Add($"Rankine:        {temp_R}°R");

                        await e.Channel.SendMessage(string.Join("\n", list));
                    }

                    if (temp_type == "R" || temp_type == "r")
                    {
                        var temp_C = (temp - 491.67) / 1.8;
                        var temp_F = (temp - 491.67) + 32;
                        var temp_K = (temp - 491.67) / 1.8 + 273.15;
                        var temp_R = temp;

                        list.Add($"Celsius:         {temp_C}°C");
                        list.Add($"Fahrenheit:   {temp_F}°F");
                        list.Add($"Kelvin:           {temp_K}°K");
                        list.Add($"Rankine:        {temp_R}°R");

                        await e.Channel.SendMessage(string.Join("\n", list));
                    }
                    if (temp_type != "C" && temp_type != "c" && temp_type != "F" && temp_type != "f" && temp_type != "K" && temp_type != "k" && temp_type != "R" && temp_type != "r")
                    {
                        await e.Channel.SendMessage($"Sorry you can't do (**{temp_type} {temp}**) but you can do (C/c, F/f, K/k, R/r)");
                    }
                });

            commands.CreateCommand("length")
                .Parameter("", ParameterType.Multiple)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %length");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    var list = new List<double>();

                    var length = double.Parse(e.Args[0]);
                    var length_type = e.Args[1];

                    if (length_type == "KM" || length_type == "km")
                    {
                        list.Add(length);              //KM
                        list.Add(length * 10);         //HM
                        list.Add(length * 100);        //DAM
                        list.Add(length * 1000);       //M
                        list.Add(length * 10000);      //DM

                        var CM = length * 100000;
                        double som_CM = Math.Round(CM, 5);
                        var MM = length * 1000000;
                        double sum_MM = Math.Round(MM, 6);
                        var µM = length * 100000000;
                        double sum_µM = Math.Round(µM, 9);
                        list.Add(som_CM);    //CM
                        list.Add(sum_MM);    //MM
                        list.Add(sum_µM);    //µM
                        await e.Channel.SendMessage($"KM:    {list[0]}" + $"\nHM:  {list[1]}" + $"\nDAM: {list[2]}" + $"\nM:   {list[3]}" + $"\nDM:  {list[4]}" + $"\nCM:  {list[5]}" + $"\nMM:  {list[6]}" + $"\nµM:  {list[7]}");
                    }
                    if (length_type == "HM" || length_type == "hm")
                    {
                        list.Add(length * 0.1);       //KM
                        list.Add(length);             //HM
                        list.Add(length * 10);        //DAM
                        list.Add(length * 100);       //M
                        list.Add(length * 1000);      //DM
                        list.Add(length * 10000);     //CM
                        list.Add(length * 100000);    //MM
                        list.Add(length * 10000000); //µM
                        await e.Channel.SendMessage($"KM:    {list[0]}" + $"\nHM:  {list[1]}" + $"\nDAM: {list[2]}" + $"\nM:   {list[3]}" + $"\nDM:  {list[4]}" + $"\nCM:  {list[5]}" + $"\nMM:  {list[6]}" + $"\nµM:  {list[7]}");
                    }
                    if (length_type == "DAM" || length_type == "dam")
                    {
                        list.Add(length * 0.01);     //KM
                        list.Add(length * 0.1);      //HM
                        list.Add(length);            //DAM
                        list.Add(length * 10);       //M
                        list.Add(length * 100);      //DM
                        list.Add(length * 1000);     //CM
                        list.Add(length * 10000);    //MM
                        list.Add(length * 1000000); //µM
                        await e.Channel.SendMessage($"KM:    {list[0]}" + $"\nHM:  {list[1]}" + $"\nDAM: {list[2]}" + $"\nM:   {list[3]}" + $"\nDM:  {list[4]}" + $"\nCM:  {list[5]}" + $"\nMM:  {list[6]}" + $"\nµM:  {list[7]}");
                    }
                    if (length_type == "M" || length_type == "m")
                    {
                        list.Add(length * 0.001);   //KM
                        list.Add(length * 0.01);    //HM
                        list.Add(length * 0.1);     //DAM
                        list.Add(length);           //M
                        list.Add(length * 10);      //DM
                        list.Add(length * 100);     //CM
                        list.Add(length * 1000);    //MM
                        list.Add(length * 100000); //µM
                        await e.Channel.SendMessage($"KM:    {list[0]}" + $"\nHM:  {list[1]}" + $"\nDAM: {list[2]}" + $"\nM:   {list[3]}" + $"\nDM:  {list[4]}" + $"\nCM:  {list[5]}" + $"\nMM:  {list[6]}" + $"\nµM:  {list[7]}");
                    }
                    if (length_type == "DM" || length_type == "dm")
                    {
                        list.Add(length * 0.0001);    //KM
                        list.Add(length * 0.001);     //HM
                        list.Add(length * 0.01);      //DAM
                        list.Add(length * 0.1);       //M
                        list.Add(length);             //DM
                        list.Add(length * 10);        //CM
                        list.Add(length * 100);       //MM
                        list.Add(length * 10000);    //µM
                        await e.Channel.SendMessage($"KM:    {list[0]}" + $"\nHM:  {list[1]}" + $"\nDAM: {list[2]}" + $"\nM:   {list[3]}" + $"\nDM:  {list[4]}" + $"\nCM:  {list[5]}" + $"\nMM:  {list[6]}" + $"\nµM:  {list[7]}");
                    }
                    if (length_type == "CM" || length_type == "cm")
                    {
                        list.Add(length * 0.00001);    //KM
                        list.Add(length * 0.0001);     //HM
                        list.Add(length * 0.001);      //DAM
                        list.Add(length * 0.01);       //M
                        list.Add(length * 0.1);        //DM
                        list.Add(length);              //CM
                        list.Add(length * 10);         //MM
                        list.Add(length * 10000);      //µM
                        await e.Channel.SendMessage($"KM:    {list[0]}" + $"\nHM:  {list[1]}" + $"\nDAM: {list[2]}" + $"\nM:   {list[3]}" + $"\nDM:  {list[4]}" + $"\nCM:  {list[5]}" + $"\nMM:  {list[6]}" + $"\nµM:  {list[7]}");
                    }
                    if (length_type == "MM" || length_type == "mm")
                    {
                        list.Add(length * 0.000001);    //KM
                        list.Add(length * 0.00001);     //HM
                        list.Add(length * 0.0001);      //DAM
                        list.Add(length * 0.001);       //M
                        list.Add(length * 0.01);        //DM
                        list.Add(length * 0.1);         //CM
                        list.Add(length);               //MM
                        list.Add(length * 1000);      //µM
                        await e.Channel.SendMessage($"KM:    {list[0]}" + $"\nHM:  {list[1]}" + $"\nDAM: {list[2]}" + $"\nM:   {list[3]}" + $"\nDM:  {list[4]}" + $"\nCM:  {list[5]}" + $"\nMM:  {list[6]}" + $"\nµM:  {list[7]}");
                    }
                    if (length_type == "µM" || length_type == "µm")
                    {
                        list.Add(length * 0.0000000001);    //KM
                        list.Add(length * 0.000000001);     //HM
                        list.Add(length * 0.00000001);      //DAM
                        list.Add(length * 0.0000001);       //M
                        list.Add(length * 0.000001);        //DM
                        list.Add(length * 0.00001);         //CM
                        list.Add(length * 0.001);          //MM
                        list.Add(length);                   //µM
                        await e.Channel.SendMessage($"KM:    {list[0]}" + $"\nHM:  {list[1]}" + $"\nDAM: {list[2]}" + $"\nM:   {list[3]}" + $"\nDM:  {list[4]}" + $"\nCM:  {list[5]}" + $"\nMM:  {list[6]}" + $"\nµM:  {list[7]}");
                    }
                    if (length_type.ToLower() == "mile")
                    {
                        await e.Channel.SendMessage($"KM:{length * 1.609344}\nM:{length * 1609.344}");
                    }
                    if (length_type.ToLower() == "feet")
                    {
                        await e.Channel.SendMessage($"M:{length * 0.3048}\nCM:{length * 30.48}");
                    }

                    await e.Channel.SendMessage($"KM:    {list[0]}" + $"\nHM:  {list[1]}" + $"\nDAM: {list[2]}" + $"\nM:   {list[3]}" + $"\nDM:  {list[4]}" + $"\nCM:  {list[5]}" + $"\nMM:  {list[6]}" + $"\nµM:  {list[7]}");
                });

            commands.CreateCommand("coinflip")
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %coinflip");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    int limit = 100;
                    int headsCount = 0;
                    int tailsCount = 0;
                    int head_tail;

                    for(int i = 0; i < limit; i++)
                    {
                        head_tail = rand.Next(2);
                        if (head_tail == 0)
                            headsCount++;
                        if (head_tail == 1)
                            tailsCount++;
                    }

                    await e.Channel.SendMessage($"heads: {headsCount}" 
                                               +$"\ntails: {tailsCount}");
                });

            commands.CreateCommand("tobinary")
                .Parameter("number", ParameterType.Optional)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %tobinary");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    int number = int.Parse(e.Args[0]);
                    var binary = Utils.toBinary(number);
                    await e.Channel.SendMessage(binary);
                });

            commands.CreateCommand("lettercount")
                .Parameter("text", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %lettercount");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    var list = new List<string>();
                    string text = e.Args[0];
                    text = text.ToLower();
                    list.Add("```");

                    int[] c = new int[char.MaxValue];
                    foreach (char t in text)
                    {
                        c[t]++;
                    }

                    for (int i = 0; i < char.MaxValue; i++)
                    {
                        if (c[i] > 0 && char.IsLetterOrDigit((char)i))
                        {
                            list.Add($"{(char)i}: {c[i]}");
                        }
                    }
                    list.Add("```");
                    await e.Channel.SendMessage(string.Join("\n", list));
                });

            commands.CreateCommand("toHex")
                .Parameter("text", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %toHex");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    string text = e.Args[0];
                    var Hex = Utils.toHex(text);
                    await e.Channel.SendMessage($"text input: {text}" + $"\nHex result: {Hex}");
                });

            commands.CreateCommand("fromHex")
                .Parameter("hex", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %fromHex");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    string Hex = e.Args[0];
                    var Text = Utils.fromHex(Hex);
                    await e.Channel.SendMessage($"hex input: {Hex}" + $"\ntext result: {Text}");
                });

            commands.CreateCommand("morse")
                .Parameter("text", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %morse");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    string text = e.Args[0];
                    var morse = Utils.morseCode(text, charToMorse);
                    await e.Channel.SendMessage($"morse: {morse}");
                });

            commands.CreateCommand("piramide")
                .Parameter("lines", ParameterType.Required)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %piramide");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    var pira = new List<string>();
                    var line = new List<string>();
                    int num = int.Parse(e.Args[0]);
                    int count = 1;
                    if (num >= 36)
                    {
                        await e.Channel.SendMessage($"Sorry but {num} is to high");
                    }
                    else
                    {
                        line.Add("enjoy your piramide\n");
                        for (int lines = num; lines >= 1; lines--)
                        {
                            for (int spaces = lines; spaces >= 0; spaces--)
                            {
                                line.Add(" ");
                            }
                            for (int star = 1; star <= count; star++)
                            {
                                line.Add("* ");
                            }
                            count++;
                            pira.Add(string.Join("", line));
                            line.Clear();
                        }

                        await e.Channel.SendMessage(string.Join("\n", pira));
                    }
                });

            commands.CreateCommand("toMD5")
                .Parameter("text", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %toMD5");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    var TextToEncode = e.Args[0];
                    var MD5_Encoded = Utils.MD5_encodeing(TextToEncode);
                    await e.Channel.SendMessage($"Output: {MD5_Encoded}");
                });

            commands.CreateCommand("Citys")
                .Parameter("letter", ParameterType.Required)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %Citys");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    string leter = e.Args[0].ToLower();
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load("Weather/Citys.xml");
                    var list = new List<string>();
                    list.Add("```");
                    if (leter == "a")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/A/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "b")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/B/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "c")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/C/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "d")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/D/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "e")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/E/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "f")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/F/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "g")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/G/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "h")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/H/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "i")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/I/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "j")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/J/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "k")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/K/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "l")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/L/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "m")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/M/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "n")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/N/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "o")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/O/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "p")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/P/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "q")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/Q/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "r")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/R/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "s")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/S/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "t")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/T/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "u")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/U/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "v")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/V/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "w")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/W/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "x")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/X/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "y")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/Y/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    if (leter == "z")
                    {
                        foreach (XmlNode node in xDoc.SelectNodes("Citys/Z/city"))
                        {
                            list.Add("City: " + node.SelectSingleNode("name").InnerText + ", CityId: " + node.SelectSingleNode("id").InnerText + ", Country: " + node.SelectSingleNode("country").InnerText);
                        }
                    }
                    list.Add("```");
                    await e.Channel.SendMessage(string.Join("\n", list));
                });

            commands.CreateCommand("addcity")
                .Parameter("text", ParameterType.Multiple)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %addcity");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    var letters = new List<char>();
                    string city = e.Args[0];
                    string id = e.Args[1];
                    string country = e.Args[2];
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load("weather/Citys.xml");
                    foreach(char leter in city)
                    {
                        letters.Add(leter);
                    }
                    XmlNode node = xDoc.SelectSingleNode($"Citys/{letters[0].ToString().ToUpper()}");
                    XmlNode newnode = xDoc.CreateNode(XmlNodeType.Element, "niger", null);
                    newnode.InnerText = "hello";
                    node.AppendChild(newnode);
                    xDoc.Save("weather/Citys.xml");

                    await e.Channel.SendMessage("Thank you for you city");
                });

            commands.CreateCommand("fact")
                .Parameter("number", ParameterType.Optional)
                .Parameter("number", ParameterType.Optional)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %fact");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    int count = 0;
                    var list = new List<string>();
                    var numberlist = new List<string>();
                    XmlDocument doc = new XmlDocument();
                    doc.Load("facts.xml");
                    foreach (XmlNode node in doc.SelectNodes("root/number"))
                    {
                        count++;
                        numberlist.Add(node.InnerText);
                    }
                    foreach (XmlNode node in doc.SelectNodes("root/fact"))
                    {
                        list.Add(node.InnerText);
                    }
                    if (e.Args[0] == "")
                    {
                        int randomfact = rand.Next(count);
                        string fact = list[randomfact - 1];
                        await e.Channel.SendMessage($"``#{numberlist[randomfact - 1]} {count}``\n" + fact);
                    }
                    if (e.Args[0] != "" && e.Args[1] == "")
                    {
                        try
                        {
                            await e.Channel.SendMessage($"``#{numberlist[int.Parse(e.Args[0]) - 1]} {count}``\n" + list[int.Parse(e.Args[0]) - 1]);
                        }
                        catch (Exception ex)
                        {
                            await e.Channel.SendMessage(ex.ToString());
                        }
                    }
                    if (e.Args[1] != "")
                    {
                        try
                        {
                            int num = int.Parse(e.Args[1]) - int.Parse(e.Args[0]);
                            if (num > 10 || num < 0)
                            {
                                await e.Channel.SendMessage("Sorry but you can't do that!");
                            }
                            else
                            {
                                for (int i = int.Parse(e.Args[0]); i <= int.Parse(e.Args[1]); i++)
                                {
                                    await e.Channel.SendMessage($"``#{numberlist[i - 1]} {count}``\n" + list[i - 1]);
                                    await Task.Delay(1200);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            await e.Channel.SendMessage(ex.ToString());
                        }
                    }
                    
                });

            commands.CreateCommand("wiki")
                .Parameter("", ParameterType.Required)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %wiki");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    try
                    {
                        await e.Channel.SendMessage($"https://en.wikipedia.org/wiki/{e.Args[0]}");
                    }
                    catch (Exception ex)
                    {
                        await e.Channel.SendMessage(ex.ToString());
                    }
                });

            commands.CreateCommand("remember")
                .Parameter("", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %remember");
                    Console.ResetColor();
                    CommandUsed.CommandAdd();
                    int count = 0;
                    try
                    {
                        await e.Channel.SendIsTyping();
                        XmlDocument doc = new XmlDocument();
                        doc.Load("./remember.xml");
                        
                        // Create a new element node.
                        XmlNode newElem = doc.CreateNode("element", "rem", "");
                        newElem.InnerText = e.Args[0];

                        Console.WriteLine("Add the new element to the document...");
                        XmlNode root = doc.DocumentElement;
                        root.AppendChild(newElem);
                        doc.Save(@"./remember.xml");
                        foreach (XmlNode nod in doc.SelectSingleNode("root"))
                            count++;
                        await e.Channel.SendMessage($"done {count}");
                    }
                    catch (Exception ex)
                    {
                        await e.Channel.SendMessage(ex.Message.ToString());
                    }
                });
            }
        }
    }
