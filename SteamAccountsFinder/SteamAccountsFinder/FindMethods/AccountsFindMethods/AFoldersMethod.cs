using System;
using System.Collections.Generic;
using System.IO;

namespace SteamAccountsFinder.AccountsFindMethods
{
    public class AFoldersMethod : AccountFindMethod
    {

        protected override void Find()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_steamPath + "\\userdata");
            if (!directoryInfo.Exists)
                return;
            try
            {
                foreach (var directory in directoryInfo.GetDirectories())
                {
                    AddAccount(directory.Name);
                }
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}