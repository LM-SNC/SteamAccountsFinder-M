namespace SteamAccountsFinder
{
    public abstract class LocationFindMethod : FindMethod
    {
        public string Get()
        {
            Find();
            return _steamPath.Replace("steam.exe", "");
        }
        
        protected abstract void Find();
    }
}