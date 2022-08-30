using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace SteamAccountsFinder.AccountsFindMethods
{
    public class ARegeditMethod : AccountFindMethod
    {
        protected override void Find()
        {
            try
            {
                foreach (var account in Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Valve\Steam\Users").GetSubKeyNames())
                    AddAccount(account);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}