using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;

namespace Superbot
{
    class help
    {
        internal static void HelpCommands(CommandService commands, DiscordClient discord)
        {
            commands.CreateGroup("help dm", cgb =>
            {
                cgb.CreateCommand("")
                    .Do(async (e) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %help dm");
                        Console.ForegroundColor = ConsoleColor.White;
                        CommandUsed.CommandAdd();
                        var name = e.User.Nickname != null ? e.User.Nickname : e.User.Name;

                        var helpList = new List<string>();

                        helpList.Add("```");
                        helpList.Add("The prefix = %");
                        helpList.Add("%help dm 1 = commands");
                        helpList.Add("%help dm 2 = info");
                        helpList.Add("%help dm 3 = admin");
                        helpList.Add("%help dm 4 = files");
                        helpList.Add("%help dm 5 = fun!!!");
                        helpList.Add("%help dm all = all Commands");
                        helpList.Add("```");

                        await e.User.SendMessage(string.Join("\n", helpList));
                    });

                cgb.CreateCommand("1")
                    .Do(async (e) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %help dm 1");
                        Console.ForegroundColor = ConsoleColor.White;
                        CommandUsed.CommandAdd();
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
                        helpList.Add("%gps - tells you about gps");
                        helpList.Add("```");

                        await e.User.SendMessage(string.Join("\n", helpList));
                    });

                cgb.CreateCommand("2")
                    .Do(async (e) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %help dm 2");
                        Console.ForegroundColor = ConsoleColor.White;
                        CommandUsed.CommandAdd();
                        var helpList = new List<string>();

                        helpList.Add("```");
                        helpList.Add("This is the list of info Commands");
                        helpList.Add(" ");
                        helpList.Add("%serverinfo - gives info about the server");
                        helpList.Add("%userinfo @user - gives info about a user in the server");
                        helpList.Add("%date - to say which day it is");
                        helpList.Add("%CommandUsed.CommandAdd(); - says the amount of commands used");
                        helpList.Add("%uptime - says the time the bot is active");
                        helpList.Add("```");

                        await e.User.SendMessage(string.Join("\n", helpList));
                    });

                cgb.CreateCommand("3")
                    .Do(async (e) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %help dm 3");
                        Console.ForegroundColor = ConsoleColor.White;
                        CommandUsed.CommandAdd();
                        var helpList = new List<string>();

                        helpList.Add("```");
                        helpList.Add("This is the list of the admin commands");
                        helpList.Add(" ");
                        helpList.Add("%playing + game - sets the playing game of the bot");
                        helpList.Add("%clear / clr - clears the chat");
                        helpList.Add("%CommandCountClear - clears the commandcount log");
                        helpList.Add("%airlock + @user - kicks users from the server");
                        helpList.Add("```");

                        await e.User.SendMessage(string.Join("\n", helpList));
                    });

                cgb.CreateCommand("4")
                    .Do(async (e) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %help dm 4");
                        Console.ForegroundColor = ConsoleColor.White;
                        CommandUsed.CommandAdd();
                        var helpList = new List<string>();

                        helpList.Add("```");
                        helpList.Add("This is the list of file commands");
                        helpList.Add(" ");
                        helpList.Add("%maketxt + {name} - makes a file.txt");
                        helpList.Add("%write + - writes in a file and then sends it");
                        helpList.Add("%cleartext - clears the written text what you have written in %write");
                        helpList.Add("```");

                        await e.User.SendMessage(string.Join("\n", helpList));
                    });

                cgb.CreateCommand("5")
                    .Do(async (e) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %help dm 5");
                        Console.ForegroundColor = ConsoleColor.White;
                        CommandUsed.CommandAdd();
                        var helpList = new List<string>();

                        helpList.Add("```");
                        helpList.Add("This is the list of funny commands");
                        helpList.Add(" ");
                        helpList.Add("%gif - sends you a gif");
                        helpList.Add("%picture - sends a picture");
                        helpList.Add("%funny - sends a funny picture");
                        helpList.Add("```");

                        await e.User.SendMessage(string.Join("\n", helpList));
                    });

                cgb.CreateCommand("all")
                    .Do(async (e) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %help dm all");
                        Console.ForegroundColor = ConsoleColor.White;

                        CommandUsed.CommandAdd();
                        var helpList = new List<string>();

                        helpList.Add("This is the list of Commands");
                        helpList.Add("```");
                        helpList.Add("%help - help");
                        helpList.Add("%say - let the bot say someting");
                        helpList.Add("%hello - hi!");
                        helpList.Add("%bye bye - bye");
                        helpList.Add("%dead - :skull_crossbones::dead::skull_crossbones:");
                        helpList.Add("%nice - lol");
                        helpList.Add("%roll + number up to 12 - roles a random number");
                        helpList.Add("%gps - tells you about gps");
                        helpList.Add("```");
                        helpList.Add(" ");
                        helpList.Add("This is the list of info Commands");
                        helpList.Add("```");
                        helpList.Add("%serverinfo - gives info about the server");
                        helpList.Add("%userinfo @user - gives info about a user in the server");
                        helpList.Add("%date - to say which day it is");
                        helpList.Add("%uptime - says the time the bot is active");
                        helpList.Add("%CommandCount - says the amount of commands used");
                        helpList.Add("```");
                        helpList.Add(" ");
                        helpList.Add("This is the list of the admin commands");
                        helpList.Add("```");
                        helpList.Add("%playing + game - sets the playing game of the bot");
                        helpList.Add("%clear / clr - clears the chat");
                        helpList.Add("%CommandCountClear - clears the commandcount log");
                        helpList.Add("%airlock + @user - kicks users from the server");
                        helpList.Add("```");
                        helpList.Add(" ");
                        helpList.Add("This is the list of file commands");
                        helpList.Add("```");
                        helpList.Add("%maketxt + {name} - makes a file.txt");
                        helpList.Add("%write + - writes in a file and then sends it");
                        helpList.Add("%cleartext - clears the written text what you have written in %write");
                        helpList.Add("```");
                        helpList.Add(" ");
                        helpList.Add("This is the list of funny commands");
                        helpList.Add("```");
                        helpList.Add("%gif - sends you a gif");
                        helpList.Add("%picture - sends a picture");
                        helpList.Add("%funny - sends a funny picture");
                        helpList.Add("```");

                        await e.Channel.SendMessage(string.Join("\n", helpList));
                    });
            });

            commands.CreateGroup("help", cgb =>
            {
                cgb.CreateCommand("")
                    .Do(async (e) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %help");
                        Console.ForegroundColor = ConsoleColor.White;
                        CommandUsed.CommandAdd();
                        var name = e.User.Nickname != null ? e.User.Nickname : e.User.Name;

                        var helpList = new List<string>();

                        helpList.Add("```");
                        helpList.Add("The prefix = %");
                        helpList.Add("%help 1 = commands");
                        helpList.Add("%help 2 = info");
                        helpList.Add("%help 3 = admin");
                        helpList.Add("%help 4 = files");
                        helpList.Add("%help 5 = fun!!!");
                        helpList.Add("%help all = all Commands");
                        helpList.Add("```");

                        await e.Channel.SendMessage(string.Join("\n", helpList));
                    });

                cgb.CreateCommand("1")
                    .Do(async (e) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %help 1");
                        Console.ForegroundColor = ConsoleColor.White;
                        CommandUsed.CommandAdd();
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
                        helpList.Add("%gps - tells you about gps");
                        helpList.Add("```");

                        await e.Channel.SendMessage(string.Join("\n", helpList));
                    });

                cgb.CreateCommand("2")
                    .Do(async (e) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %help 2");
                        Console.ForegroundColor = ConsoleColor.White;
                        CommandUsed.CommandAdd();
                        var helpList = new List<string>();

                        helpList.Add("```");
                        helpList.Add("This is the list of info Commands");
                        helpList.Add(" ");
                        helpList.Add("%serverinfo - gives info about the server");
                        helpList.Add("%userinfo @user - gives info about a user in the server");
                        helpList.Add("%date - to say which day it is");
                        helpList.Add("%uptime - says the time the bot is active");
                        helpList.Add("%CommandCount - says the amount of commands used");
                        helpList.Add("```");

                        await e.Channel.SendMessage(string.Join("\n", helpList));
                    });

                cgb.CreateCommand("3")
                    .Do(async (e) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %help 3");
                        Console.ForegroundColor = ConsoleColor.White;
                        CommandUsed.CommandAdd();
                        var helpList = new List<string>();

                        helpList.Add("```");
                        helpList.Add("This is the list of the admin commands");
                        helpList.Add(" ");
                        helpList.Add("%playing + game - sets the playing game of the bot");
                        helpList.Add("%clear / clr - clears the chat");
                        helpList.Add("%CommandCountClear - clears the commandcount log");
                        helpList.Add("%airlock + @user - kicks users from the server");
                        helpList.Add("```");

                        await e.Channel.SendMessage(string.Join("\n", helpList));
                    });

                cgb.CreateCommand("4")
                    .Do(async (e) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %help 4");
                        Console.ForegroundColor = ConsoleColor.White;
                        CommandUsed.CommandAdd();
                        var helpList = new List<string>();

                        helpList.Add("```");
                        helpList.Add("This is the list of file commands");
                        helpList.Add(" ");
                        helpList.Add("%maketxt + {name} - makes a file.txt");
                        helpList.Add("%write + - writes in a file and then sends it");
                        helpList.Add("%cleartext - clears the written text what you have written in %write");
                        helpList.Add("```");

                        await e.Channel.SendMessage(string.Join("\n", helpList));
                    });

                cgb.CreateCommand("5")
                    .Do(async (e) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %help 5");
                        Console.ForegroundColor = ConsoleColor.White;
                        CommandUsed.CommandAdd();
                        var helpList = new List<string>();

                        helpList.Add("```");
                        helpList.Add("This is the list of funny commands");
                        helpList.Add(" ");
                        helpList.Add("%gif - sends you a gif");
                        helpList.Add("%picture - sends a picture");
                        helpList.Add("%funny - sends a funny picture");
                        helpList.Add("```");

                        await e.Channel.SendMessage(string.Join("\n", helpList));
                    });

                cgb.CreateCommand("all")
                    .Do(async (e) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second},{DateTime.Now.Millisecond}] [{e.User.Name}] Used %help all");
                        Console.ForegroundColor = ConsoleColor.White;

                        CommandUsed.CommandAdd();
                        var helpList = new List<string>();

                        helpList.Add("This is the list of Commands");
                        helpList.Add("```");
                        helpList.Add("%help - help");
                        helpList.Add("%say - let the bot say someting");
                        helpList.Add("%hello - hi!");
                        helpList.Add("%bye bye - bye");
                        helpList.Add("%dead - :skull_crossbones::dead::skull_crossbones:");
                        helpList.Add("%nice - lol");
                        helpList.Add("%roll + number up to 12 - roles a random number");
                        helpList.Add("%gps - tells you about gps");
                        helpList.Add("```");
                        helpList.Add(" ");
                        helpList.Add("This is the list of info Commands");
                        helpList.Add("```");
                        helpList.Add("%serverinfo - gives info about the server");
                        helpList.Add("%userinfo @user - gives info about a user in the server");
                        helpList.Add("%date - to say which day it is");
                        helpList.Add("%uptime - says the time the bot is active");
                        helpList.Add("%CommandCount - says the amount of commands used");
                        helpList.Add("```");
                        helpList.Add(" ");
                        helpList.Add("This is the list of the admin commands");
                        helpList.Add("```");
                        helpList.Add("%playing + game - sets the playing game of the bot");
                        helpList.Add("%clear / clr - clears the chat");
                        helpList.Add("%CommandCountClear - clears the commandcount log");
                        helpList.Add("%airlock + @user - kicks users from the server");
                        helpList.Add("```");
                        helpList.Add(" ");
                        helpList.Add("This is the list of file commands");
                        helpList.Add("```");
                        helpList.Add("%maketxt + {name} - makes a file.txt");
                        helpList.Add("%write + - writes in a file and then sends it");
                        helpList.Add("%cleartext - clears the written text what you have written in %write");
                        helpList.Add("```");
                        helpList.Add(" ");
                        helpList.Add("This is the list of funny commands");
                        helpList.Add("```");
                        helpList.Add("%gif - sends you a gif");
                        helpList.Add("%picture - sends a picture");
                        helpList.Add("%funny - sends a funny picture");
                        helpList.Add("```");

                        await e.Channel.SendMessage(string.Join("\n", helpList));
                    });
            });
        }
    }
}
