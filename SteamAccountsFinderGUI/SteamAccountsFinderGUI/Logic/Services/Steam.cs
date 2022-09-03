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
            _steamServices = new List<SteamService> { new VacListService() };
        }
        public async Task<SteamAccount?> GetAccountInfo(long steamId)
        {
            foreach (var steamService in _steamServices)
            {
                var steamAccount = await steamService.GetAccountInfo(steamId);

                if (steamAccount != null)
                    return steamAccount;
            }

            return null;
        }

        private static long GetSteamId64(long steamId)
        {
            return 76561197960265728 + steamId;
        }

        public static long Validate64(long steamId)
        {
            return steamId >= 76561197960265728?  steamId : GetSteamId64(steamId);
        }
        
        public long GetSteamId32(long steamId)
        {
            return Math.Abs(76561197960265728 - steamId);
        }
    }
}