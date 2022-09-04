using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SteamAccountsFinder
{
    public class AConfigFindMethod : AccountFindMethod
    {
        public override IEnumerable<long> Find()
        {
            FileInfo fileInfo = new FileInfo(_steamPath + "\\config\\loginusers.vdf");
            if (fileInfo.Exists)
            {
                using var streamReader = new StreamReader(fileInfo.Open(FileMode.Open), Encoding.UTF8, true, 512);
                streamReader.ReadLine();
                String[] lines = {"", ""};
                while ((lines[1] = streamReader.ReadLine()) != null)
                {
                    if (lines[0].Contains("\"") && lines[1].Contains("{"))
                        if(long.TryParse(lines[0].Replace("\"", ""), out long lAccount))
                            yield return lAccount;
                    lines[0] = lines[1];
                }
            }
        }
    }
}