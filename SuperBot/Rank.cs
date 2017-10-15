using System;
using Discord;
using Discord.Commands;
using System.Xml;
using System.Collections.Generic;
using System.Linq;

namespace Superbot
{
    public class Rank
    {
        public static void levels(CommandService commands, DiscordClient discord, Random rand)
        {
            discord.UserJoined += (s, e) =>
            {
                try
                {
                    bool exists = false;
                    XmlDocument doc = new XmlDocument();
                    doc.Load("./levels_superbot.xml");
                    foreach (XmlNode node in doc.SelectNodes("root/users"))
                    {
                        if (node.SelectSingleNode("UserID").Attributes[0].InnerText == e.User.Id.ToString())
                        {
                            exists = true;
                            break;
                        }
                        else
                        {
                            exists = false;
                        }
                    }
                    if (exists == false)
                    {
                        XmlElement AddUser = doc.CreateElement("UserID");
                        XmlElement CurrentXp = doc.CreateElement("CurrentXp");
                        XmlElement CurrentLVL = doc.CreateElement("CurrentLVL");
                        XmlElement Xpneeded = doc.CreateElement("XpNeeded");
                        AddUser.SetAttribute("ID", e.User.Id.ToString());
                        CurrentLVL.InnerText = "1";
                        CurrentXp.InnerText = "0";
                        Xpneeded.InnerText = "15";

                        XmlNode root = doc.SelectSingleNode("root/users");
                        AddUser.AppendChild(CurrentXp);
                        AddUser.AppendChild(Xpneeded);
                        AddUser.AppendChild(CurrentLVL);
                        root.AppendChild(AddUser);
                        doc.Save(@"./levels_superbot.xml");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            };

            discord.MessageReceived += async (s, e) =>
            {
                if (!e.User.IsBot)
                {
                    try
                    {
                        bool exist = false;
                        XmlDocument doc = new XmlDocument();
                        doc.Load("./levels_superbot.xml");
                        foreach (XmlNode node in doc.SelectNodes("root/users/UserID"))
                        {
                            if (node.Attributes[0].InnerText == e.User.Id.ToString())
                            {
                                exist = true;
                                string Currentlvl = node.SelectSingleNode("CurrentLVL").InnerText;
                                string XpNeeded = node.SelectSingleNode("XpNeeded").InnerText;
                                int Xp = rand.Next(1, 4);
                                string oldCurrentxp = node.SelectSingleNode("CurrentXp").InnerText;
                                int newCurrentxp = int.Parse(oldCurrentxp) + Xp;
                                if (newCurrentxp >= int.Parse(XpNeeded))
                                {
                                    int current = newCurrentxp - int.Parse(XpNeeded);
                                    int newLevel = int.Parse(Currentlvl) + 1;
                                    double newXpNeeded = Math.Round(int.Parse(XpNeeded) * 1.2, 0);
                                    node.SelectSingleNode("CurrentLVL").InnerText = newLevel.ToString();
                                    node.SelectSingleNode("XpNeeded").InnerText = newXpNeeded.ToString();
                                    node.SelectSingleNode("CurrentXp").InnerText = current.ToString();
                                    await e.Channel.SendMessage(e.User.Mention + ", you reached Level: " + newLevel);
                                    doc.Save(@"./levels_superbot.xml");
                                }
                                else
                                {
                                    node.SelectSingleNode("CurrentXp").InnerText = newCurrentxp.ToString();
                                    doc.Save(@"./levels_superbot.xml");
                                }
                                break;
                            }
                            else
                            {
                                exist = false;
                            }
                        }
                        if (exist == false)
                        {
                            XmlElement AddUser = doc.CreateElement("UserID");
                            XmlElement CurrentXp = doc.CreateElement("CurrentXp");
                            XmlElement CurrentLVL = doc.CreateElement("CurrentLVL");
                            XmlElement Xpneeded = doc.CreateElement("XpNeeded");
                            AddUser.SetAttribute("ID", e.User.Id.ToString());
                            CurrentLVL.InnerText = "1";
                            CurrentXp.InnerText = "0";
                            Xpneeded.InnerText = "15";

                            XmlNode root = doc.SelectSingleNode("root/users");
                            AddUser.AppendChild(CurrentXp);
                            AddUser.AppendChild(Xpneeded);
                            AddUser.AppendChild(CurrentLVL);
                            root.AppendChild(AddUser);
                            doc.Save(@"./levels_superbot.xml");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                    }
                }
            };

            commands.CreateCommand("rank")
                .Parameter("name", ParameterType.Optional)
                .Do(async (e) =>
                {
                    bool done = true;
                    if (done == true)
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load("./levels_superbot.xml");
                        var list = new List<string>();
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
                        if (u.IsBot)
                        {
                            await e.Channel.SendMessage("Bots don't have ranks");
                            return;
                        }
                        bool found = false;
                        foreach (XmlNode node in doc.SelectNodes("root/users/UserID"))
                        {
                            if (node.Attributes[0].InnerText == u.Id.ToString())
                            {
                                found = true;
                                string needed = node.SelectSingleNode("XpNeeded").InnerText;
                                string Current = node.SelectSingleNode("CurrentXp").InnerText;
                                list.Add(u.Mention + "'s Level is " + node.SelectSingleNode("CurrentLVL").InnerText);
                                list.Add($"<{Current}/{needed}>");
                                double precent = Math.Round(double.Parse(Current) / double.Parse(needed) * 100, 0);
                                if (precent == 0)
                                    list.Add("0%(----------)100%");
                                if (precent >= 1 && precent <= 9)
                                    list.Add("0%(=---------)100%");
                                if (precent >= 10 && precent <= 19)
                                    list.Add("0%(==--------)100%");
                                if (precent >= 20 && precent <= 29)
                                    list.Add("0%(===-------)100%");
                                if (precent >= 30 && precent <= 39)
                                    list.Add("0%(====------)100%");
                                if (precent >= 40 && precent <= 49)
                                    list.Add("0%(=====-----)100%");
                                if (precent >= 50 && precent <= 59)
                                    list.Add("0%(======----)100%");
                                if (precent >= 60 && precent <= 69)
                                    list.Add("0%(=======---)100%");
                                if (precent >= 70 && precent <= 79)
                                    list.Add("0%(========--)100%");
                                if (precent >= 80 && precent <= 89)
                                    list.Add("0%(=========-)100%");
                                if (precent >= 90 && precent <= 99)
                                    list.Add("0%(==========)100%");
                                await e.Channel.SendMessage(string.Join("\n", list));
                                break;
                            }
                        }
                        if (found == false)
                        {
                            await e.Channel.SendMessage($"sorry but {u.Mention} does not have a rank");
                        }
                    }
                    else
                    {
                        await e.Channel.SendMessage("nothing here?");
                    }
                });
        }

        
    }
}