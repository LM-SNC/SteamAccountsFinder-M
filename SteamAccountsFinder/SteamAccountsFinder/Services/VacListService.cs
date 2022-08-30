using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace SteamAccountsFinder
{
    public class VacListService : SteamService
    {
        protected override void FindAccountInfo(long steamId)
        {
            SteamAccount steamAccount;
            
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create("https://vaclist.net/api/account?q=" + steamId);

            request.Method = "GET";
            request.Accept = "application/json";
            request.Timeout = 15000;

            try
            {
                var response = (HttpWebResponse)request.GetResponse();

                steamAccount = new SteamAccount();
                
                StreamReader reader = new StreamReader(response.GetResponseStream());
                StringBuilder output = new StringBuilder();
                output.Append(reader.ReadToEnd());
                response.Close();

                dynamic array = JsonConvert.DeserializeObject(output.ToString());
                steamAccount.UserName = array["personaname"];
                steamAccount.Avatar = array["avatar"];
                steamAccount.HaveVac = array["vac_bans"] + array["game_bans"] > 0;
                steamAccount.SteamId64 = array["steam_id"];

                _steamAccount = steamAccount;
            }
            catch (Exception e)
            {
                //ignored
            }
        }
    }
}