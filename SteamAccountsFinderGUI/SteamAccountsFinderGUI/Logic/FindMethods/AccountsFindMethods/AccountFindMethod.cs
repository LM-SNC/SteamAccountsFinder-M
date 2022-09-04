using System.Collections.Generic;

namespace SteamAccountsFinder
{
    public abstract class AccountFindMethod : FindMethod
    {
        public void SetSteamLocation(string steamLocation)
        {
            _steamPath = steamLocation;
        }
        
        public abstract IEnumerable<long> Find();
    }
}