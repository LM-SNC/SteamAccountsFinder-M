using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SteamAccountsFinder.AccountsFindMethods;
using SteamAccountsFinder.LocationFindMethods;

namespace SteamAccountsFinder
{
    class Program
    {
        
        static void Main(string[] args)
        {
            new Steam();
            SteamAccountsFinder steamAccountFinder = new SteamAccountsFinder();

            //Methods for finding a steam location
            steamAccountFinder.AddLocationMethod(new RegeditMethod(@"SOFTWARE\Valve\Steam1"));
            steamAccountFinder.AddLocationMethod(new DefaultPathMethod()
                .AddDefaultPath(@"С:\Program Files (x86)\Steam\steam.exe")
                .AddDefaultPath(@"E:\Steam\steam.exe")
                .AddDefaultPath(@"C:\Steam\steam.exe")
                .AddDefaultPath(@"F:\Steam\steam.exe")
                .AddDefaultPath(@"D:\Steam\steam.exe")
                .AddDefaultPath(@"G:\Steam\steam.exe")
                .AddDefaultPath(@"N:\Steam\steam.exe"));
            steamAccountFinder.AddLocationMethod(new BruteMethod());
            steamAccountFinder.Init();
            
            //Account search methods
            steamAccountFinder.AddAccountsMethod(new ALogsMethod());
            steamAccountFinder.AddAccountsMethod(new ARegeditMethod());
            steamAccountFinder.AddAccountsMethod(new AConfigFindMethod());
            steamAccountFinder.AddAccountsMethod(new AFoldersMethod());
            steamAccountFinder.AddAccountsMethod(new ACacheMethod());

            bool isDone = false;
            Task.Run(() =>
            {
                while (!isDone)
                {
                    Console.Write("Find accounts");
                    for (int i = 0; i < 4; i++)
                    {
                        if (isDone)
                            break;
                        Task.Delay(500).Wait();
                        Console.Write(".");
                    }

                    ClearCurrentConsoleLine();
                }
            });

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var account in steamAccountFinder.GetAccounts().Result)
            {
                stringBuilder.Append("Name: " + account.UserName + '\n');
                //stringBuilder.Append("Avatar: " + account.Avatar + '\n');
                stringBuilder.Append("Vac: " + account.HaveVac + '\n');
                stringBuilder.Append("SteamId: " + account.SteamId64 + '\n');
                stringBuilder.Append("Profile link: " + account.ProfileLink + '\n');
                stringBuilder.Append('\n');
            }
            
            isDone = true;
            
            FileInfo fileInfo = new FileInfo("AccountsInfo.txt");
            if (fileInfo.Exists)
                fileInfo.Delete();
            
            File.WriteAllTextAsync("AccountsInfo.txt", stringBuilder.ToString());
            
            Console.Clear();
            Console.WriteLine((stringBuilder.Length > 0? "OK" : "Fail") + "....Press any key to exit");
            Console.ReadLine();
            
        }
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write("                       ");
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}