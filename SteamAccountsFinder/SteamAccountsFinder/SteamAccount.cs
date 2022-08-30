using System;

namespace SteamAccountsFinder
{
    public class SteamAccount
    {
        public string UserName { get; set; }
        private long _SteamId64;
        public string Avatar { get; set; }
        public bool HaveVac { get; set; }
        public DateTime LastActivity { get; set; }
        public string ProfileLink{ get; set; }

        public long SteamId64
        {
            get
            {
                return _SteamId64;
            }
            set
            {
                _SteamId64 = value;
                ProfileLink = "https://steamcommunity.com/profiles/" + _SteamId64;
            }
        }
    }
}