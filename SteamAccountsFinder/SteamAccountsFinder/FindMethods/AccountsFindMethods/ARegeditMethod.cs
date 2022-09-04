using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace SteamAccountsFinder.AccountsFindMethods
{
    public class ARegeditMethod : AccountFindMethod
    {
        public override IEnumerable<long> Find()
        {
            var regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Valve\Steam\Users");
            if (regKey == null)
                yield break;
            foreach (var account in regKey.GetSubKeyNames())
                if (long.TryParse(account, out long steamId))
                    yield return steamId;
        }
    }
}