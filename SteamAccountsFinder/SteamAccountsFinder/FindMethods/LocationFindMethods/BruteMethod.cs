using System;
using System.IO;

namespace SteamAccountsFinder.LocationFindMethods
{
    public class BruteMethod : LocationFindMethod
    {
       
        
        private string Search(string path, int level = 0)
        {
            if (level <= 2)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                try
                {
                    foreach (var directory in directoryInfo.GetDirectories())
                    {
                        string directoryName = directory.FullName;
                        if (IsSteamPath(directoryName) || IsSteamPath(directoryName = Search(directoryName, level + 1)))
                            return directoryName;
                    }
                }
                catch (Exception e)
                {
                    
                }
            }
        
            return String.Empty;
        }
        
        private bool IsSteamPath(string path)
        {
            if (!path.Equals(String.Empty))
            {
                DirectoryInfo needDirectory = new DirectoryInfo(path);
                try
                {
                    foreach (var file in needDirectory.GetFiles())
                        // Console.WriteLine(FileVersionInfo.GetVersionInfo(file.FullName).OriginalFilename!);
                        //if (FileVersionInfo.GetVersionInfo(file.FullName).OriginalFilename!.ToLower().Contains("steam.exe"))
                        if (file.Name.Equals("streaming_client.exe"))
                            return true;
                        
                }
                catch (Exception e)
                {
                    //ignore
                }
            }
            return false;
        }

        protected override void Find()
        {
            string findPath;
            foreach (var drive in DriveInfo.GetDrives())
            {
                findPath = Search(drive.Name);
                if (findPath != String.Empty)
                    _steamPath = findPath;
            }
        }
    }
}