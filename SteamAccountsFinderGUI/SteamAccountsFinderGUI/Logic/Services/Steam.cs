using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SteamAccountsFinderGUI;

namespace SteamAccountsFinder
{
    public class Steam
    {
        private static List<SteamService> _steamServices;

        public Steam()
        {
            _steamServices = new List<SteamService>();
            
            _steamServices.Add(new VacListService());
        }
        public static Task<SteamAccount> GetAccountInfo(long steamId)
        {
            return Task.Run(() =>
            {
                foreach (var steamService in _steamServices)
                {
                    var steamAccount = steamService.GetAccountInfo(steamId);

                    if (steamAccount != null)
                        return steamAccount;
                }

                return null;
            });
        }
        
        public static long GetSteamId64(long steamId)
        {
            return 76561197960265728 + steamId;
        }

        public static long Validate64(long steamId)
        {
            return steamId >= 76561197960265728?  steamId : GetSteamId64(steamId);
        }
        
        public static long GetSteamId32(long steamId)
        {
            return Math.Abs(76561197960265728 - steamId);
        }
    }
}