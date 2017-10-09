using System;
using Discord;
using Discord.Commands;

namespace Superbot
{
    class som
    {
        internal static void Som(CommandService commands, DiscordClient discord)
        {
            commands.CreateCommand("som")
                .Parameter("val1", ParameterType.Required)
                .Parameter("op1", ParameterType.Required)
                .Parameter("val2", ParameterType.Required)
                .Parameter("op2", ParameterType.Optional)
                .Parameter("val3", ParameterType.Optional)
                .Do(async (e) =>
                {
                    try
                    {
                        var Operator1 = e.Args[1];
                        var Operator2 = e.Args[3];

                        if (Operator1 == "+")
                        {
                            var val1 = e.Args[0];
                            var val2 = e.Args[2];
                            if (int.Parse(val1) == 9 && int.Parse(val2) == 10 || int.Parse(val1) == 10 && int.Parse(val2) == 9)
                            {
                                await e.Channel.SendTTSMessage("21");
                                return;
                            }
                            if (Operator2 == "")
                            {
                                double awnser1 = double.Parse(val1) + double.Parse(val2);
                                double som1 = Math.Round(awnser1, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} + {val2} = {som1}");
                            }
                            if (Operator2 == "+")
                            {
                                double val3 = double.Parse(e.Args[4]);
                                double awnser = double.Parse(val1) + double.Parse(val2) + val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} + {val2} + {val3} = {som}");
                            }
                            if (Operator2 == "-")
                            {
                                double val3 = double.Parse(e.Args[4]);
                                double awnser = double.Parse(val1) + double.Parse(val2) - val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} + {val2} - {val3} = {som}");
                            }
                            if (Operator2 == "*")
                            {
                                var val3 = e.Args[4];
                                if (val1.Contains("(") && val2.Contains(")"))
                                {
                                    var val1proces = val1.Replace("(", "");
                                    double val1done = double.Parse(val1proces);
                                    var val2proces = val2.Replace(")", "");
                                    double val2done = double.Parse(val2proces);
                                    double awnser1 = val1done + val2done;
                                    double awnser = awnser1 * double.Parse(val3);
                                    double som = Math.Round(awnser, 3);
                                    await e.Channel.SendMessage($"the awnser from {val1} + {val2} * {val3} = {som}");
                                }
                                else
                                {
                                    double awnser = double.Parse(val1) + double.Parse(val2) * double.Parse(val3);
                                    double som = Math.Round(awnser, 3);
                                    await e.Channel.SendMessage($"the awnser from {val1} + {val2} * {val3} = {som}");
                                }
                            }
                            if (Operator2 == "/")
                            {
                                var val3 = e.Args[4];
                                if (val1.Contains("(") || val2.Contains(")"))
                                {
                                    string val1proces = val1.Replace("(", "");
                                    string val2proces = val2.Replace(")", "");
                                    double awnser = (double.Parse(val1proces) + double.Parse(val2proces)) / double.Parse(val3);
                                    await e.Channel.SendMessage($"the awnser from {val1} + {val2} / {val3} = {awnser}");
                                }
                                else
                                {
                                    double awnser = (double.Parse(val1) + double.Parse(val2)) / double.Parse(val3);
                                    double som = Math.Round(awnser, 3);
                                    await e.Channel.SendMessage($"the awnser from {val1} + {val2} / {val3} = {som}");
                                }
                            }
                        }
                        if (Operator1 == "-")
                        {
                            double val1 = double.Parse(e.Args[0]);
                            double val2 = double.Parse(e.Args[2]);
                            if (Operator2 == "")
                            {
                                double awnser1 = val1 - val2;
                                double som1 = Math.Round(awnser1, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} - {val2} = {som1}");
                            }
                            if (Operator2 == "+")
                            {
                                double val3 = double.Parse(e.Args[4]);
                                double awnser = val1 - val2 + val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} - {val2} + {val3} = {som}");
                            }
                            if (Operator2 == "-")
                            {
                                double val3 = double.Parse(e.Args[4]);
                                double awnser = val1 - val2 - val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} - {val2} - {val3} = {som}");
                            }
                            if (Operator2 == "*")
                            {
                                double val3 = double.Parse(e.Args[4]);
                                double awnser = val1 - val2 * val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} - {val2} * {val3} = {som}");
                            }
                            if (Operator2 == "/")
                            {
                                double val3 = double.Parse(e.Args[4]);
                                double awnser = val1 + val2 / val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} - {val2} / {val3} = {som}");
                            }
                        }
                        if (Operator1 == "*")
                        {
                            double val1 = double.Parse(e.Args[0]);
                            double val2 = double.Parse(e.Args[2]);
                            if (Operator2 == "")
                            {
                                double awnser1 = val1 * val2;
                                double som1 = Math.Round(awnser1, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} * {val2} = {som1}");
                            }
                            if (Operator2 == "+")
                            {
                                double val3 = double.Parse(e.Args[4]);
                                double awnser = val1 * val2 + val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} * {val2} + {val3} = {som}");
                            }
                            if (Operator2 == "-")
                            {
                                double val3 = double.Parse(e.Args[4]);
                                double awnser = val1 * val2 - val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} * {val2} - {val3} = {som}");
                            }
                            if (Operator2 == "*")
                            {
                                double val3 = double.Parse(e.Args[4]);
                                double awnser = val1 * val2 * val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} * {val2} * {val3} = {som}");
                            }
                            if (Operator2 == "/")
                            {
                                double val3 = double.Parse(e.Args[4]);
                                double awnser = val1 * val2 / val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} * {val2} / {val3} = {som}");
                            }
                        }
                        if (Operator1 == "/")
                        {
                            double val1 = double.Parse(e.Args[0]);
                            double val2 = double.Parse(e.Args[2]);
                            if (Operator2 == "")
                            {
                                double awnser1 = val1 / val2;
                                double som1 = Math.Round(awnser1, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} / {val2} = {som1}");
                            }
                            if (Operator2 == "+")
                            {
                                double val3 = double.Parse(e.Args[4]);
                                double awnser = val1 / val2 + val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} / {val2} + {val3} = {som}");
                            }
                            if (Operator2 == "-")
                            {
                                double val3 = double.Parse(e.Args[4]);
                                double awnser = val1 / val2 - val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} / {val2} - {val3} = {som}");
                            }
                            if (Operator2 == "*")
                            {
                                double val3 = double.Parse(e.Args[4]);
                                double awnser = val1 / val2 * val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} / {val2} * {val3} = {som}");
                            }
                            if (Operator2 == "/")
                            {
                                double val3 = double.Parse(e.Args[4]);
                                double awnser = val1 / val2 / val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} / {val2} / {val3} = {som}");
                            }
                        }
                        if (Operator1 == "^")
                        {
                            long val1l = long.Parse(e.Args[0]);
                            long val2l = long.Parse(e.Args[2]);
                            if (Operator2 == "")
                            {
                                double awnser1 = val1l ^ val2l;
                                double som1 = Math.Round(awnser1, 3);
                                await e.Channel.SendMessage($"the awnser from {val1l} ^ {val2l} = {som1}");
                            }
                            if (Operator2 == "+")
                            {
                                long val3 = long.Parse(e.Args[4]);
                                double awnser = val1l ^ val2l + val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1l} ^ {val2l} + {val3} = {som}");
                            }
                            if (Operator2 == "-")
                            {
                                long val3 = long.Parse(e.Args[4]);
                                double awnser = val1l ^ val2l - val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1l} ^ {val2l} - {val3} = {som}");
                            }
                            if (Operator2 == "*")
                            {
                                long val3 = long.Parse(e.Args[4]);
                                double awnser = val1l ^ val2l * val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1l} ^ {val2l} * {val3} = {som}");
                            }
                            if (Operator2 == "/")
                            {
                                long val3 = long.Parse(e.Args[4]);
                                double awnser = val1l ^ val2l / val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1l} ^ {val2l} / {val3} = {som}");
                            }
                        }
                        if (Operator1 == "%")
                        {
                            double val1 = double.Parse(e.Args[0]);
                            double val2 = double.Parse(e.Args[2]);
                            if (Operator2 == "")
                            {
                                double awnser1 = val1 % val2;
                                double som1 = Math.Round(awnser1, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} % {val2} = {som1}");
                            }
                            if (Operator2 == "+")
                            {
                                double val3 = double.Parse(e.Args[4]);
                                double awnser = val1 % val2 + val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} % {val2} + {val3} = {som}");
                            }
                            if (Operator2 == "-")
                            {
                                double val3 = double.Parse(e.Args[4]);
                                double awnser = val1 % val2 - val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} % {val2} - {val3} = {som}");
                            }
                            if (Operator2 == "*")
                            {
                                double val3 = double.Parse(e.Args[4]);
                                double awnser = val1 % val2 * val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} % {val2} * {val3} = {som}");
                            }
                            if (Operator2 == "/")
                            {
                                double val3 = double.Parse(e.Args[4]);
                                double awnser = val1 % val2 / val3;
                                double som = Math.Round(awnser, 3);
                                await e.Channel.SendMessage($"the awnser from {val1} % {val2} / {val3} = {som}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await e.Channel.SendMessage(ex.Message.ToString());
                    }
                });
        }
    }
}
