using System;
using System.IO;
using System.Linq;
using static System.String;

namespace SteamAccountsFinder.LocationFindMethods
{
    public class BruteMethod : LocationFindMethod
    {
        private string Search(string path, int level = 0)
        {
            if (level > 2) return Empty;
            
            try
            {
                foreach (var directory in Directory.EnumerateDirectories(path))
                {
                    if (IsSteamPath(directory))
                        return directory;
                    var result = Search(directory, level + 1);
                    if (result != Empty) return result;
                }
            }
            catch
            {

            }

            return Empty;
        }

        private bool IsSteamPath(string path)
        {
            if (path.Equals(Empty)) return false;

            try
            {
                if (Directory.EnumerateFiles(path).Any(filePath => filePath.EndsWith("\\streaming_client.exe")))
                    return true;
            }
            catch (Exception e)
            {
                //ignore
            }
            return false;
        }

        protected override void Find()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                var findPath = Search(drive.Name);
                if (findPath == Empty) continue;
                
                _steamPath = findPath;
                //break;
            }
        }
    }
}