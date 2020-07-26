using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToyRobotChallenge;
using ToyRobotChallenge.Environment;
using ToyRobotChallenge.Geometry;

namespace TRC_ALEX_MVC.Services
{
    public class TRCSession
    {
        public IBoard Board { get; set; }
        public ToyRobot Robot { get; set; }
        public Vector2d<ulong> Validation { get; set; }

        public void Load(ulong width, ulong height)
        {
            Current.Board = new Board(width, height);
            Current.Robot = new ToyRobot(Current.Board);
        }
        
        public static TRCSession Current
        {
            get
            {
                // Create session object if it doesn't exist already.
                if (HttpContext.Current.Session["sessionVar"] == null)
                {
                    HttpContext.Current.Session["sessionVar"] = new TRCSession();
                }

                // Return it.
                return (TRCSession)HttpContext.Current.Session["sessionVar"];
            }
            set { HttpContext.Current.Session["sessionVar"] = value; }
        }
    }
}