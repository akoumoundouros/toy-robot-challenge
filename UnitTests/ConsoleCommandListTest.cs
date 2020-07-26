using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TRC_ALEX_MVC;
using TRC_ALEX_MVC.Services;

namespace UnitTests
{
    [TestClass]
    public class ConsoleCommandListTest
    {
        [TestMethod]
        public void WhenParsingCoordinatesThatAreToBigIgnoreTheCommand()
        {
            var cmdString = "PLACE 18446744073709551616,18446744073709551616,EAST";
            var cmdSvc = new CommandSvc();
            var commandList = cmdSvc.GetCommands(cmdString);

            Assert.IsNotNull(commandList);
            Assert.AreEqual(0, commandList.Count);
        }
        
        [TestMethod]
        public void WhenParsingCoordinatesThatAreToNegativeIgnoreTheCommand()
        {
            var cmdString = "PLACE -4,-1,WEST";
            var cmdSvc = new CommandSvc();
            var commandList = cmdSvc.GetCommands(cmdString);

            Assert.IsNotNull(commandList);
            Assert.AreEqual(0, commandList.Count);
        }
        
        [TestMethod]
        public void WhenParsingMultipleCommandsOnASingleLineCreateMultipleCommands()
        {
            var cmdString = "RIGHT MOVE MOVE REPORT LEFT";
            var commandList = new CommandSvc().GetCommands(cmdString);

            Assert.IsNotNull(commandList);
            Assert.AreEqual(5, commandList.Count);
        }
        
        [TestMethod]
        public void WhenParsingMultipleCommandsOnAMultipleLineCreateMultipleCommands()
        {
            var cmdString = "PLACE 4,1,WEST\n" +
                            "MOVE \n" + 
                            "MOVE \n" +
                            "REPORT \n" +
                            "PLACE 4,1,WEST\n";

            var commandList = new CommandSvc().GetCommands(cmdString);

            Assert.IsNotNull(commandList);
            Assert.AreEqual(5, commandList.Count);
        }
        
        [TestMethod]
        public void WhenParsingMultipleCommandsOnASingleLineIgnoreUnknownCommandsInbetweenAndCreateMultipleCommands()
        {
            var cmdString = "PLACE PLACE 4,1,WEST MOVE MOVE REPORT PLACE 4,1,WEST";
            var commandList = new CommandSvc().GetCommands(cmdString);

            Assert.IsNotNull(commandList);
            Assert.AreEqual(5, commandList.Count);
        }
        
        [TestMethod]
        public void WhenParsingMultipleCommandsOnMultipleLinesIgnoreUnknownCommandsInbetweenAndCreateMultipleCommands()
        {
            var cmdString = "PLACE 4,1,WEST asdfsdaffg\n" +
                            "MOVE ddfadf\n" +
                            " dadfafddeef MOVE \n" +
                            "fdadfas REPORT RepORT\n" +
                            "PLACE PLACE PLACE 4,1,WEST asdfasdf\n";

            var commandList = new CommandSvc().GetCommands(cmdString);

            Assert.IsNotNull(commandList);
            Assert.AreEqual(5, commandList.Count);
        }
    }
}
