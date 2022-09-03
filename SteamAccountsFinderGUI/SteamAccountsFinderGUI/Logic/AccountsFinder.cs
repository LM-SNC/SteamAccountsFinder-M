using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SteamAccountsFinder.AccountsFindMethods;
using SteamAccountsFinderGUI;

namespace SteamAccountsFinder
{ 
    public class AccountsFinder
    {
        private string _steamPath;
        
        private List<LocationFindMethod> _locationFindMethods;
        private List<AccountFindMethod> _accountsFindMethods;

        public AccountsFinder()
        {
            _accountsFindMethods = new List<AccountFindMethod>();
            _locationFindMethods = new List<LocationFindMethod>();
        }

        public void Init()
        {
            _steamPath = GetSteamLocation();
            Console.WriteLine("Steam: " + _steamPath);

            if (string.IsNullOrEmpty(_steamPath))
                throw new Exception("Steam not found");
        }
        
        public async Task<List<SteamAccount>> GetAccounts()
        {
            List<SteamAccount> steamAccounts = new List<SteamAccount>();
            List<long> existsAccounts = new List<long>();

            List<Task<SteamAccount>> steamAccountsTasks = new List<Task<SteamAccount>>();

            foreach (var findAccountsMethod in _accountsFindMethods)
            {
                foreach (var account in findAccountsMethod.GetAccounts())
                {
                    long steamId = Steam.Validate64(account);
                    if (!existsAccounts.Contains(steamId))
                    {
                        existsAccounts.Add(steamId);
                        steamAccountsTasks.Add(Steam.GetAccountInfo(steamId));
                    }
                }
            }

            await Task.WhenAll(steamAccountsTasks);
            foreach (var t in steamAccountsTasks)
                steamAccounts.Add(t.Result);


            return steamAccounts;
        }
        
        private string GetSteamLocation()
        {
            foreach (var locFindMethod in _locationFindMethods)
            {
                string location = locFindMethod.Get();
                if (!string.IsNullOrEmpty(location))
                    return location;
            }
            return null;
        }
        
        public void AddLocationMethod(LocationFindMethod locationFindMethod)
        {
            _locationFindMethods.Add(locationFindMethod);
        }
        
        public void AddAccountsMethod(AccountFindMethod accountFindMethod)
        {
            accountFindMethod.SetSteamLocation(_steamPath);
            _accountsFindMethods.Add(accountFindMethod);
        }
    }
}