using System.Collections.Generic;

namespace SteamAccountsFinder.AccountsFindMethods
{
    public abstract class AccountFindMethod : FindMethod
    {
        protected List<long> _accounts = new ();

        public List<long> GetAccounts()
        {
            Find();
            return _accounts;
        }

        protected void AddAccount(long account)
        {
            _accounts.Add(account);
        }
        
        protected void AddAccount(string account)
        {
            if (long.TryParse(account, out long lAccount))
                AddAccount(lAccount);
        }

        public void SetSteamLocation(string steamLocation)
        {
            _steamPath = steamLocation;
        }
    }
}