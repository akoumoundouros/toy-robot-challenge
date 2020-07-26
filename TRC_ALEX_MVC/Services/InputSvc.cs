using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TRC_ALEX_MVC.Services
{
    public class InputSvc
    {
        private string _testDataDir = HttpContext.Current.Server.MapPath("~") + "\\..\\Test Data\\";
        private CommandSvc _cmdSvc = new CommandSvc();

        public IEnumerable<string> GetTestFilenames()
        {
            var inputFiles = Directory.EnumerateFiles(_testDataDir).Select(s => s.Substring(s.LastIndexOf("\\") + 1));
            return inputFiles;
        }

        public string GetTestFileCommands(string name)
        {
            var input = "";
            using (FileStream fs = new FileStream(_testDataDir + name, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        var cmd = _cmdSvc.GetCommands(sr.ReadLine().ToUpper());
                        foreach (var line in cmd)
                        {
                            input += line + "\n";
                        }
                    }

                }
            }
            return input;
        }
        
    }
}