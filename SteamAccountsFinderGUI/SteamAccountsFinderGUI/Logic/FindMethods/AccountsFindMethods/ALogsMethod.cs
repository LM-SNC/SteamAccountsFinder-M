using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SteamAccountsFinder.AccountsFindMethods
{
    public class ALogsMethod : AccountFindMethod
    {
        public override IEnumerable<long> Find()
        {
            string fullPath = _steamPath + "\\logs\\webhelper.txt";
            FileInfo fileInfo = new FileInfo(fullPath);

            if (fileInfo.Exists)
            {
                FileInfo tmpLog = fileInfo.CopyTo(fullPath + ".tmp.txt", true);
                Regex rx = new Regex(@"accountid=(.*?)&");
                
                using (var streamReader = new StreamReader(tmpLog.OpenRead(), 
                           Encoding.UTF8, true, 512))
                {
                    streamReader.ReadLine();
                    String line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (line.Contains("accountid"))
                            if (long.TryParse(rx.Match(line).Groups[1].Value, out long steamid))
                                yield return steamid;
                    }
                }
                tmpLog.Delete();
            }
        }
    }
}