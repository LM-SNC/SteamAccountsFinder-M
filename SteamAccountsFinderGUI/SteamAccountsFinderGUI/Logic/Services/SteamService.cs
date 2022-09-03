using System.Threading.Tasks;
using SteamAccountsFinderGUI;

namespace SteamAccountsFinder
{
    public abstract class SteamService
    {
        public Task<SteamAccount?> GetAccountInfo(long steamId)
        {
            return FindAccountInfo(steamId);
        }

        protected abstract Task<SteamAccount?> FindAccountInfo(long steamId);
    }
}