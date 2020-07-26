using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRC_ALEX_MVC.Services;

namespace TRC_ALEX_MVC.Controllers
{
    public class InputController : Controller
    {
        /// <summary>
        /// Returns a list of command strings
        /// </summary>
        public JsonResult Index(string id)
        {
            var inputSvc = new InputSvc();
            var commands = inputSvc.GetTestFileCommands(id);
            return Json(commands, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Returns a list of available test files from the 'Test Data' directory
        /// </summary>
        public JsonResult GetTestFilenames()
        {
            var inputFiles = new InputSvc().GetTestFilenames();
            return Json(inputFiles, JsonRequestBehavior.AllowGet);
        }
    }
}