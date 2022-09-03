using System;
using System.Collections.Generic;
using System.IO;

namespace SteamAccountsFinder.LocationFindMethods
{
    public class DefaultPathMethod : LocationFindMethod
    {
        private List<string> _deafultPathList;

        public DefaultPathMethod()
        {
            _deafultPathList = new();
        }
        
        

        public DefaultPathMethod AddDefaultPath(string path)
        {
            _deafultPathList.Add(path);
            return this;
        }

        protected override void Find()
        {
            foreach (var path in _deafultPathList)
                if (File.Exists(path))
                    _steamPath = path;
        }
    }
}