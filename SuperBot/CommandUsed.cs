using System.IO;
using System.Xml;

namespace Superbot
{
    class CommandUsed
    {

        public static void CommandAdd()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("./file/commandsused.xml");
            string number = xDoc.SelectSingleNode("root/commands").InnerText;
            xDoc.SelectSingleNode("root/commands").InnerText = (int.Parse(number) + 1).ToString();
            xDoc.Save("./file/commandsused.xml");
        }

        public static void ClearAdd(int v)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("./file/commandsused.xml");
            string number = xDoc.SelectSingleNode("root/clear").InnerText;
            xDoc.SelectSingleNode("root/clear").InnerText = (int.Parse(number) + v).ToString();
            xDoc.Save("./file/commandsused.xml");
        }
    }
}