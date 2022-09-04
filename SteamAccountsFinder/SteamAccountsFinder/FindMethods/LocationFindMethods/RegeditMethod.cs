using System;
using Microsoft.Win32;

namespace SteamAccountsFinder.LocationFindMethods
{
    public class RegeditMethod : LocationFindMethod
    {
        private string _subKey;
        public RegeditMethod(string subKey)
        {
            _subKey = subKey;
        }


        protected override void Find()
        {
            try
            {
                _steamPath = Registry.CurrentUser.OpenSubKey(_subKey).GetValue("SteamExe").ToString();
            }
            catch
            {
                // ignored
            }
        }
    }
}