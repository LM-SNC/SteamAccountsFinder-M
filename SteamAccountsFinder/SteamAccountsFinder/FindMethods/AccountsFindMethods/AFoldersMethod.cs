using System.Collections.Generic;
using System.IO;

namespace SteamAccountsFinder.AccountsFindMethods
{
    public class AFoldersMethod : AccountFindMethod
    {

        public override IEnumerable<long> Find()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_steamPath + "\\userdata");
            if (!directoryInfo.Exists)
                yield break;
            foreach (var directory in directoryInfo.GetDirectories())
                if (long.TryParse(directory.Name, out long steamId))
                    yield return steamId;
        }
    }
}