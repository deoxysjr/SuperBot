using Discord;
using Discord.Commands;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuperBot
{
    class MyBot
    {
      DiscordClient discord;
      CommandService commands;
      
      public MyBot()
      {
          discord = new DiscordClient(x =>            
          {                
          x.LogLevel = LogSeverity.Info;                
          x.LogHandler = Log;            
          });
          
          discord.UsingCommands(x =>            
          {                
          x.PrefixChar = '%';                
          x.AllowMentionPrefix = true;                
          //x.HelpMode = HelpMode.Public;            
          });
          
          commands = discord.GetService<CommandService>();
          
	  Commands();	
      
          discord.ExecuteAndWait(async () =>            
          {                
              while (true)                
              {                    
              await discord.Connect("token", TokenType.Bot);                    
              Console.WriteLine("Bot connected correctly");                    
              discord.SetGame("%help for commands");                    
	      break;
	      }
	   });
	      
	private void commands()
	{
		
	}	
	      
	private void Log(object sender, LogMessageEventArgs e)        
	{            
	Console.WriteLine($"[{e.Source}] {e.Message}");
	}
    }
}
