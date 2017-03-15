            commands.CreateCommand("MakeFile")
                .Parameter("file", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    var lol = new List<string>();

                    lol.Add($"{e.Server.Name}");
                    lol.Add($"{e.Server.Region}");
                    lol.Add($"{string.Join(", ", e.Server.Roles.Where(x => !x.Name.Contains("@everyone")))}");

                    var path = $@"C:\Users\deoxysjr\Documents\Visual Studio 2015\Projects\Superbot\Superbot\bin\Debug\file\{e.Args[0]}.txt";
                    var file = $"{Directory.GetCurrentDirectory()}\\file\\"; // Grab video folder
                    //File.CreateText(e.Args[0]); // Create video folder if not found
                    File.WriteAllText(path, $"{string.Join("\n", lol)}");

                    await e.Channel.SendMessage("The file is been made");
                });

            commands.CreateCommand("write")
                .Parameter("line", ParameterType.Required)
                .Do(async (e) =>
                {
                    var path = @"C:\users\deoxysjr\Documents\Visual Studio 2015\Projects\Superbot\Superbot\bin\Debug\file\text.txt";
                    File.AppendAllText(path, $"\r\n{e.Args[0]}");

                    await e.Channel.SendFile(@"C:\users\deoxysjr\Documents\Visual Studio 2015\Projects\Superbot\Superbot\bin\Debug\file\text.txt");
                });
