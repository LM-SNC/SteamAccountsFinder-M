using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using SteamAccountsFinder.AccountsFindMethods;
using SteamAccountsFinderGUI;

namespace SteamAccountsFinder
{
    public class AccountsFinder
    {
        private readonly Steam _steam;
        private string _steamPath;

        private readonly List<LocationFindMethod> _locationFindMethods = new();
        private readonly List<AccountFindMethod> _accountsFindMethods = new();

        public AccountsFinder(Steam steam)
        {
            _steam = steam;
        }

        public void FindSteamLocation()
        {
            _steamPath = GetSteamLocation();
            Console.WriteLine("Steam: " + _steamPath);

            if (string.IsNullOrEmpty(_steamPath))
                throw new Exception("Steam not found");
        }

        public async Task<List<SteamAccount>> GetAccounts(ProgressBar progressBar)
        {
            var steamAccounts = new List<SteamAccount>();
            var existsAccounts = new List<long>();

            var steamAccountsTasks = new List<Task<SteamAccount?>>();

            var completedCount = 0;
            async Task<SteamAccount?> GetAccountInfoProgress(long steamId)
            {
                var result = await _steam.GetAccountInfo(steamId);

                progressBar.Value = completedCount++;

                return result;
            }

            foreach (var findAccountsMethod in _accountsFindMethods)
            {
                foreach (var account in findAccountsMethod.GetAccounts())
                {
                    var steamId = Steam.Validate64(account);
                    if (!existsAccounts.Contains(steamId))
                    {
                        existsAccounts.Add(steamId);
                        steamAccountsTasks.Add(GetAccountInfoProgress(steamId));
                    }
                }
            }

            progressBar.Maximum = steamAccountsTasks.Count;

            await Task.WhenAll(steamAccountsTasks);

            foreach (var t in steamAccountsTasks)
            {
                var result = t.Result;
                if (result != null)
                    steamAccounts.Add(result);
            }

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