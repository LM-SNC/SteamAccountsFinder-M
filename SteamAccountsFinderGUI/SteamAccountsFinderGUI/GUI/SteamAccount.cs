using System.Windows.Media;
using Brush = System.Windows.Media.Brush;

namespace SteamAccountsFinderGUI
{
    public class SteamAccount
    {
        public string UserName { get; set; }
        public string Avatar { get; set; }
        private string _backgroundColor;
        private string _vacTextColor;
        public string ProfileLink{ get; set; }
        
        
        private long _SteamId64;
        
        private bool _haveVac { get; set; }
        
        
        
        
        public string BackgroundColor => _backgroundColor;

        public string VacTextColor => _vacTextColor;
        

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

        public string VacText
        {
            get { return _haveVac ? "Yes" : "No"; }
        }
        public bool HaveVac
        {
            get
            {
                return _haveVac;
            }
            set
            {
                _haveVac = value;
                if (_haveVac)
                {
                    _backgroundColor = "#943247";//"#943247"
                    _vacTextColor = Colors.IndianRed.ToString(); //"#943247"
                }
                else
                {
                    _backgroundColor = "#63687b";
                    _vacTextColor = "#ccc"; //"#943247"
                }
            }
        }
    }
}