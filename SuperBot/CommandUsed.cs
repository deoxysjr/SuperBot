using System.IO;

namespace Superbot
{
    class CommandUsed
    {
        public static void CommandAdd()
        {
            int x;

            using (TextReader reader = File.OpenText(@".\file\commandsused.txt"))
            {
                string text = reader.ReadLine();
                string[] bits = text.Split(' ');
                x = int.Parse(bits[0]);
                x++;
            }
            File.WriteAllText(@".\file\commandsused.txt", x.ToString());
        }
    }
}