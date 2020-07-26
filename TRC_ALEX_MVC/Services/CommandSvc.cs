using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TRC_ALEX_MVC.Services
{
    public class CommandSvc
    {
        private List<string> _validCommands = new List<string>
        {
            "MOVE",
            "LEFT",
            "RIGHT",
            "REPORT"
        };

        /// <summary>
        /// Produces a list of the valid commands given a line of text
        /// </summary>
        public List<string> GetCommands(string command)
        {
            var empty = new List<string>();
            var commands = empty;
            var reg = new Regex(@"\s");
            var regPlace = new Regex(@"PLACE \d,\d,\w*\b");
            var regValid = new Regex(@"VALIDATE \d,\d,\w*\b");

            var placeCommands = regPlace.Matches(command);
            var validateCommands = regValid.Matches(command);

            if (command.Length <= 0)
                return empty;

            if (command[0] == '#')
                return empty;

            if (command.Length >= 5 && command.StartsWith("ECHO"))
            {
                return empty;
            }

            foreach (Match cmd in placeCommands)
            {
                commands.Add(cmd.Value);
            }

            foreach (Match cmd in validateCommands)
            {
                commands.Add(cmd.Value);
            }

            var otherCmds = reg.Split(command).Where(c => _validCommands.Contains(c));
            commands.AddRange(otherCmds);
            
            return commands;
        }
    }
}