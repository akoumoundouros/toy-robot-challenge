using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToyRobotChallenge;
using ToyRobotChallenge.Geometry;
using TRC_ALEX_MVC.Services;

namespace TRC_ALEX_MVC
{
    public class Application
    {
        public void Init()
        {
            new TRCSession().Load(5, 5);
        }

        /// <summary>
        /// Sends the command to the robot in session
        /// </summary>
        public static void Execute(string command)
        {
            var splitCmd = SplitCommand(command);
            var bot = TRCSession.Current.Robot;
            try
            {
                switch (splitCmd[0])
                {
                    case "PLACE":
                        var x = ulong.Parse(splitCmd[1]);
                        var y = ulong.Parse(splitCmd[2]);
                        var dir = GetDirFromString(splitCmd[3]);
                        var vect = new Vector2d<ulong>(new Point2d<ulong>(x, y), dir);
                        bot.Place(vect);
                        break;
                    case "MOVE":
                        bot.Move();
                        break;
                    case "LEFT":
                        bot.TurnLeft();
                        break;
                    case "RIGHT":
                        bot.TurnRight();
                        break;
                    case "VALIDATE":
                        x = ulong.Parse(splitCmd[1]);
                        y = ulong.Parse(splitCmd[2]);
                        dir = GetDirFromString(splitCmd[3]);
                        TRCSession.Current.Validation = new Vector2d<ulong>(new Point2d<ulong>(x, y), dir);
                        break;
                    default:
                        break;
                }
            }
            catch { }
        }

        /// <summary>
        /// Splits the command by comma/space
        /// </summary>
        private static string[] SplitCommand(string command)
        {
            return command.Split(',',' ');
        }

        /// <summary>
        /// Required to get the direction enum from string
        /// </summary>
        private static Direction GetDirFromString(string dir)
        {
            var type = Direction.North.GetType();
            var word = dir.FirstOrDefault() + dir.Substring(1).ToLower();
            return (Direction)Enum.Parse(type, word);
        }
    }
}