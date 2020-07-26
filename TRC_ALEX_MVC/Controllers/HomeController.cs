using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRC_ALEX_MVC.Models;
using TRC_ALEX_MVC.Services;

namespace TRC_ALEX_MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() => View(new InputSvc().GetTestFilenames());
        
        /// <summary>
        /// Load the board in its current state
        /// </summary>
        public ActionResult Play()
        {
            var board = TRCSession.Current.Board;
            var bot = TRCSession.Current.Robot;
            var validation = TRCSession.Current.Validation;
            var boardVM = new BoardVM()
            {
                Width = board.Width,
                Height = board.Height,
                HasAPosition = bot.HasAPosition
            };
            if(bot.HasAPosition)
            {
                boardVM.RobotPos = new Tuple<ulong, ulong>(bot.Vector.Position.X, bot.Vector.Position.Y);
                boardVM.RobotDir = bot.Vector.Dir.ToString();
                if (validation != null)
                {
                    boardVM.Validating = true;
                    boardVM.ValidatePos = new Tuple<ulong, ulong>(validation.Position.X, validation.Position.Y);
                    boardVM.ValidateDir = validation.Dir.ToString();
                    TRCSession.Current.Validation = null;
                }
            }
            
            return View(boardVM);
        }

        /// <summary>
        /// Execute the requested command
        /// </summary>
        public void Step(string cmd)
        {
            Application.Execute(cmd);
        }
        
        /// <summary>
        /// Reset the board and robot
        /// </summary>
        public void Reset()
        {
            new Application().Init();
        }
    }
}