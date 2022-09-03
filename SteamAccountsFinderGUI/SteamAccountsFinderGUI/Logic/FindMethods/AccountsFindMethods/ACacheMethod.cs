using System.IO;

namespace SteamAccountsFinder
{
    public class ACacheMethod : AccountFindMethod
    {
        protected override void Find()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_steamPath + "\\appcache\\stats");
            if (!directoryInfo.Exists)
                return;
            foreach (var file in directoryInfo.GetFiles())
                if (file.Name.Contains("UserGameStats_"))
                    AddAccount(file.Name.Split("_")[1]);
        }
    }
}