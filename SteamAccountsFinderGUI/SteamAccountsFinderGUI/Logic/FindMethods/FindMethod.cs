using System;
using System.Collections.Generic;

namespace SteamAccountsFinder
{
    public abstract class FindMethod
    {
        protected string _steamPath = String.Empty;
        protected abstract void Find();
    }
}