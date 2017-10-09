using System;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace Superbot
{
    class animesearch
    {
        public static void AnimeCommands(CommandService commands, DiscordClient discord)
        {
            commands.CreateCommand("anime")
                .Parameter("name", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    string name = (e.Args[0]);
                    //string json = @"{'Name': 'Bad Boys', 'ReleaseDate': '1995-4-7T00:00:00', 'Genres': [ 'Action', 'Comedy' ]}";
                    string url = $@"https://aniapi.nadekobot.me/anime/{name}";
                    string res;
                    string urls = "user";
                    var list = new List<string>();
                    try
                    {
                        using (var http = new HttpClient())
                        {
                            res = await http.GetStringAsync(url).ConfigureAwait(false);
                            var json = JsonConvert.DeserializeObject<dynamic>(res);
                            list.Add("English Title: " + json.title_english.ToString());
                            list.Add("Japanese title: " + json.title_japanese.ToString());
                            list.Add("Romaji title: " + json.title_romaji.ToString());
                            list.Add("Synonyms: " + string.Join(", ", json.synonyms));
                            list.Add("Type: " + json.type.ToString());
                            list.Add("Average score: " + json.average_score.ToString());
                            list.Add("status: " + json.airing_status.ToString());
                            list.Add("Episodes: " + json.total_episodes.ToString());
                            list.Add("Duration: " + json.duration.ToString());
                            list.Add("Adult: " + json.adult);
                            list.Add($"Description: \n```{(json.description.ToString().Replace("<br>", ""))}```");
                            urls = json.image_url_lge.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        await e.Channel.SendMessage(ex.ToString());
                    }
                    using (var client = new WebClient())
                    {
                        try
                        {
                            client.DownloadFile(urls, $"{name}.jpg");
                        }
                        catch (Exception s)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    await e.Channel.SendMessage(string.Join("\n", list));
                    await e.Channel.SendFile($"{name}.jpg");
                    if (File.Exists($@"{name}.jpg"))
                        File.Delete($@"{name}.jpg");
                });
        }
    }
}
