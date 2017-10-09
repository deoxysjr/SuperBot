using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Superbot
{
    class Utils
    {
        public static void AddFakeHeaders(HttpClient http)
        {
            http.DefaultRequestHeaders.Clear();
            http.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1");
            http.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
        }

        public static int Compute(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }

        public static int getRandInt(int min, int max)
        {
            return new Random().Next(min, max + 1); //for ints
        }

        public static string toBinary(int number)
        {
            if (number == 0)
                return "0";
            string binary = "";
            while (number > 0)
            {
                int rem = number % 2;
                binary = rem + binary;
                number = number / 2;
            }

            return binary;
        }

        public static string toHex(string text)
        {
            string textInput = text;
            string hexOutput = "";
            string hexDelimiter = " ";

            foreach (char textCharacter in textInput.ToCharArray())
            {
                int decCharacter = Convert.ToInt32(textCharacter);
                string hexCharacter = string.Format("{0:X}", decCharacter);
                hexOutput += hexCharacter + hexDelimiter;
            }
            return hexOutput;
        }

        public static string fromHex(string hex)
        {
            string hexinput = hex;
            string textOutput = "";
            string hexDelimiter = " ";

            foreach (string hexCharacter in hexinput.Replace(hexDelimiter, " ").Trim().Split(' '))
            {
                int decCharacter = Convert.ToInt32(hexCharacter, 16);
                char textCharacter = (char)decCharacter;
                textOutput += textCharacter;
            }

            return textOutput;
        }

        public static string morseCode(string text, Dictionary<char, string> charToMorse)
        {
            string input = text.ToLower();
            StringBuilder output = new StringBuilder();
            foreach (char c in input)
                if (charToMorse.ContainsKey(c))
                    output.Append(charToMorse[c] + " ");

            return output.ToString();
        }

        public static string MD5_encodeing(string textToEncode)
        {
            MD5 md5Hash = MD5.Create();

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(textToEncode));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}

