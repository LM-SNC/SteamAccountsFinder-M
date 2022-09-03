using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SteamAccountsFinderGUI;

namespace SteamAccountsFinder
{
    public class VacListService : SteamService
    {
        protected override async Task<SteamAccount?> FindAccountInfo(long steamId)
        {
            var request =
                (HttpWebRequest)WebRequest.Create("https://vaclist.net/api/account?q=" + steamId);

            request.Method = "GET";
            request.Accept = "application/json";
            request.Timeout = 15000;

            try
            {
                var response =  (HttpWebResponse)await request.GetResponseAsync();

                var steamAccount = new SteamAccount();
                
                var reader = new StreamReader(response.GetResponseStream());
                var output = new StringBuilder();
                output.Append(await reader.ReadToEndAsync());
                response.Close();

                dynamic array = await JsonConvert.DeserializeObjectAsync(output.ToString());
                steamAccount.UserName = array["personaname"];
                steamAccount.Avatar = array["avatar"];
                steamAccount.HaveVac = array["vac_bans"] + array["game_bans"] > 0;
                steamAccount.SteamId64 = array["steam_id"];

                return steamAccount;
            }
            catch (Exception e)
            {
                //ignored
            }

            return null;
        }
    }
}