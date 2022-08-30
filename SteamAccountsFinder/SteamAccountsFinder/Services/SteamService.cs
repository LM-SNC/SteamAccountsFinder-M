namespace SteamAccountsFinder
{
    public abstract class SteamService
    {
        protected SteamAccount _steamAccount = null;
        
        public SteamAccount GetAccountInfo(long steamId)
        {
            FindAccountInfo(steamId);
            return _steamAccount;
        }

        protected abstract void FindAccountInfo(long steamId);
    }
}