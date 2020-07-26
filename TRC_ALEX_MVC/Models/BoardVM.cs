using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using ToyRobotChallenge.Geometry;

namespace TRC_ALEX_MVC.Models
{
    public class BoardVM
    {
        public ulong Width { get; set; }
        public ulong Height { get; set; }

        public bool HasAPosition { get; set; }

        public Tuple<ulong,ulong> RobotPos { get; set; }
        public string RobotDir { get; set; }

        public bool Validating { get; set; }
        public Tuple<ulong,ulong> ValidatePos { get; set; }
        public string ValidateDir { get; set; }
    }
}