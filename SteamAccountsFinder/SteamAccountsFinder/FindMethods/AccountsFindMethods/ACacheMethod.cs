using System.Collections.Generic;
using System.IO;

namespace SteamAccountsFinder
{
    public class ACacheMethod : AccountFindMethod
    {
        public override IEnumerable<long> Find()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_steamPath + "\\appcache\\stats");
            if (!directoryInfo.Exists)
                yield break;
            foreach (var file in directoryInfo.GetFiles())
                if (file.Name.Contains("UserGameStats_"))
                    if (long.TryParse(file.Name.Split("_")[1], out long steamId))
                        yield return steamId;
        }
    }
}