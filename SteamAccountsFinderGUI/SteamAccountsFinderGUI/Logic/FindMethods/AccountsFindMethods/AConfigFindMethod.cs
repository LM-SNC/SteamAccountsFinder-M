using System;
using System.IO;
using System.Text;

namespace SteamAccountsFinder
{
    public class AConfigFindMethod : AccountFindMethod
    {
        protected override void Find()
        {
            FileInfo fileInfo = new FileInfo(_steamPath + "\\config\\loginusers.vdf");
            if (fileInfo.Exists)
            {
                using (var streamReader = new StreamReader(fileInfo.Open(FileMode.Open), Encoding.UTF8, true, 512))
                {
                    streamReader.ReadLine();
                    String[] lines = {"", ""};
                    while ((lines[1] = streamReader.ReadLine()) != null)
                    {
                        if (lines[0].Contains("\"") && lines[1].Contains("{"))
                            AddAccount(lines[0].Replace("\"", ""));
                        lines[0] = lines[1];
                    }
                }
            }
        }
    }
}